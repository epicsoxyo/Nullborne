using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Nullborne.Player;
using Nullborne.Quests;
using Nullborne.Dialogue;
using Nullborne.UI;
using Nullborne.AI;



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
        GLYPH_SOUND = 0x06,

        // add new glyphs here...
    }



    public class GlyphVM : MonoBehaviour
    {

        private List<byte> bytecode_ = new List<byte>();

        [SerializeField] private Button compileButton_;
        [SerializeField] private Button runButton_;

        [SerializeField] private DialogueAsset missingOperandDialogue_;

        [SerializeField] private GameObject onDeathFacadePrefab_;

        // mana:
        private ManaManager manaManager_;
        private int manaCost_ = 0;

        // quest completion:
        private bool hasCompiledOnDeath_ = false;
        private bool hasCompiledOnCast_ = false;



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

            bytecode_.Clear();

            GlyphCallback glyphCallback = GetComponentInChildren<GlyphCallback>(false);
            List<Glyph> glyphs = GetDepthFirstGlyphsList();

            if(glyphCallback.callbackType == GlyphCallbackType.CB_ONDEATH)
            {
                if(!hasCompiledOnDeath_ && glyphs.Count == 1 && glyphs[0].GetComponentInChildren<GlyphLiteral>())
                {
                    CompleteOnDeathQuest(glyphCallback);
                }
                return;
            }

            if(glyphCallback.callbackType != GlyphCallbackType.CB_ONCAST) return;

            manaCost_ = 0;

            foreach(Glyph glyph in glyphs)
            {
                if(glyph.GetComponentInChildren<DropArea>())
                {
                    GlyphLiteral glyphLiteral = glyph.GetComponentInChildren<GlyphLiteral>();

                    if(glyphLiteral == null)
                    {
                        DialogueManager.instance.OpenDialogue(missingOperandDialogue_);
                        bytecode_.Clear();
                        return;
                    }

                    bytecode_.Add((byte)GlyphCode.GLYPH_LITERAL);
                    bytecode_.Add((byte)glyphLiteral.operand);
                }

                bytecode_.Add((byte)glyph.glyphCode);
                manaCost_ += glyph.glyphCost;
            }

            if(!hasCompiledOnCast_ && bytecode_.Count > 0 && glyphCallback.callbackType == GlyphCallbackType.CB_ONCAST)
            {
                hasCompiledOnCast_ = true;
                QuestManager.instance.MarkQuestAsComplete("CompileOnCast");
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



        private void CompleteOnDeathQuest(GlyphCallback glyphCallback)
        {

            hasCompiledOnDeath_ = true;
            QuestManager.instance.MarkQuestAsComplete("CompileOnDeath");

            Transform callbackTransform = glyphCallback.transform;

            for(int i = callbackTransform.childCount - 1; i >= 0; i--)
            {
                Destroy(callbackTransform.GetChild(i).gameObject);
            }

            Destroy(callbackTransform.GetComponent<VerticalLayoutGroup>());

            Instantiate(onDeathFacadePrefab_, callbackTransform);

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

            Debug.Log(bytecode_.Count);

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
                    case GlyphCode.GLYPH_WAIT:
                        yield return new WaitForSeconds(3f);
                        break;
                    case GlyphCode.GLYPH_EXPLODE:
                        Explode((GlyphLiteralType)codeStack.Pop());
                        break;
                    case GlyphCode.GLYPH_SOUND:
                        Sound((GlyphLiteralType)codeStack.Pop());
                        break;
                    default:
                        Debug.LogWarning("GLYPH: Implementation error");
                        yield break;

                }
            }

        }



        private void Explode(GlyphLiteralType glyphLiteral)
        {

            switch(glyphLiteral)
            {
                case GlyphLiteralType.LITERAL_NULLBORNE:
                    FindFirstObjectByType<PlayerKiller>().Suicide();
                    return;
                case GlyphLiteralType.LITERAL_GUARDIAN:
                    FindFirstObjectByType<TurretAI>().Explode();
                    return;
                case GlyphLiteralType.LITERAL_STATUE:
                    FindFirstObjectByType<Statue>().Explode();
                    return;
            }

            Debug.LogError("EXPLODE: Implementation error");

        }



        private void Sound(GlyphLiteralType glyphLiteral)
        {

            switch(glyphLiteral)
            {
                case GlyphLiteralType.LITERAL_NULLBORNE:
                    FindFirstObjectByType<PlayerKiller>().KillPlayerForSound();
                    return;
                case GlyphLiteralType.LITERAL_GUARDIAN:
                    FindFirstObjectByType<TurretAI>().Sound();
                    return;
                case GlyphLiteralType.LITERAL_STATUE:
                    FindFirstObjectByType<Statue>().Sound();
                    return;
            }


        }

    }

}