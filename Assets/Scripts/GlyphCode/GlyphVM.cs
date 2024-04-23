using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Nullborne.Player;
using Nullborne.Quests;
using Nullborne.Dialogue;



namespace Nullborne.GlyphCode
{

    public enum GlyphCode : byte
    {
        GLYPH_LITERAL = 0x00,
        GLYPH_HAULT = 0x01,
        GLYPH_LOG = 0x02,
        GLYPH_WAIT = 0x03,
        GLYPH_REWIND = 0x04,
        GLYPH_EXPLODE = 0x05,

        // add new glyphs here...
    }



    public class GlyphVM : MonoBehaviour
    {

        private List<byte> bytecode_ = new List<byte>();

        [SerializeField] private Button compileButton_;
        [SerializeField] private Button runButton_;

        [SerializeField] private GameObject onDeathFacadePrefab_;
        [SerializeField] private DialogueAsset facadeDialogue_;

        private ManaManager manaManager_;
        private int manaCost_ = 0;

        private bool hasCompiled_ = false;



        // assign buttons
        private void Start()
        {
            manaManager_ = FindFirstObjectByType<ManaManager>();

            compileButton_.onClick.AddListener(Compile);
            runButton_.onClick.AddListener(() => TransmuteGlyphs(GlyphCallbackType.CB_ONCAST));
        }



        // compiles the GlyphCode into bytecode
        public void Compile()
        {

            List<Glyph> glyphs = GetDepthFirstGlyphsList();

            bytecode_.Clear();
            manaCost_ = 0;

            foreach(Glyph glyph in glyphs)
            {
                bytecode_.Add((byte)glyph.glyphCode);
                manaCost_ += glyph.glyphCost;

                GlyphLiteral glyphLiteral = glyph.GetComponent<GlyphLiteral>();
                if(glyphLiteral != null) bytecode_.Add((byte)glyphLiteral.operand);
            }

            GlyphCallback glyphCallback = GetComponentInChildren<GlyphCallback>();

            if(!hasCompiled_ && bytecode_.Count > 0 && glyphCallback.callbackType == GlyphCallbackType.CB_ONDEATH)
            {
                hasCompiled_ = true;
                QuestManager.instance.MarkQuestAsComplete("ClickCompile");
                SwitchOutOnDeathFacade(glyphCallback);
            }

        }



        private void SwitchOutOnDeathFacade(GlyphCallback glyphCallback)
        {

            Transform callbackTransform = glyphCallback.transform;

            for(int i = callbackTransform.childCount - 1; i >= 0; i--)
            {
                Destroy(callbackTransform.GetChild(i).gameObject);
            }

            Destroy(callbackTransform.GetComponent<VerticalLayoutGroup>());

            GameObject onDeathFacade = Instantiate(onDeathFacadePrefab_, callbackTransform);
            onDeathFacade.GetComponent<Button>().onClick.AddListener(() => DialogueManager.instance.OpenDialogue(facadeDialogue_));

        }



        public void TransmuteGlyphs(GlyphCallbackType callback)
        {
            if(manaManager_.currentMana < manaCost_) return;
            manaManager_.UseMana(manaCost_);
            StartCoroutine(Interpret(callback));
        }



        // uses a stack to process the bytecode
        private IEnumerator Interpret(GlyphCallbackType callback)
        {

            Stack<byte> codeStack = new Stack<byte>();

            for(int i = 0; i < bytecode_.Count; i++)
            {

                switch((GlyphCode)bytecode_[i])
                {
                    case GlyphCode.GLYPH_LITERAL:
                        codeStack.Push(bytecode_[++i]);
                        break;
                    case GlyphCode.GLYPH_HAULT:
                        codeStack.Clear();
                        yield break;
                    case GlyphCode.GLYPH_LOG:
                        Debug.Log(codeStack.Pop());
                        break;
                    case GlyphCode.GLYPH_WAIT:
                        yield return new WaitForSeconds(codeStack.Pop());
                        break;
                    case GlyphCode.GLYPH_EXPLODE:
                        // testParticles_.Play();
                        break;
                    default:
                        Debug.LogWarning("GLYPH: Implementation error");
                        break;
                }
            }

        }



        // recursive function that returns the glyphcode as a depth-first list
        private List<Glyph> GetDepthFirstGlyphsList()
        {
            void GetDepthFirstListOfGlyphs(ref List<Glyph> glyphList, Transform child)
            {

                foreach(Transform grandchild in child)
                {
                    GetDepthFirstListOfGlyphs(ref glyphList, grandchild);
                }

                Glyph currentGlyph = child.GetComponent<Glyph>();
                if(currentGlyph != null) glyphList.Add(currentGlyph);

            }

            List<Glyph> glyphList = new List<Glyph>();

            foreach(Transform child in transform)
            {
                GetDepthFirstListOfGlyphs(ref glyphList, child);
            }

            Glyph currentGlyph = GetComponent<Glyph>();
            if(currentGlyph != null) glyphList.Add(currentGlyph);

            return glyphList;
        }

    }

}