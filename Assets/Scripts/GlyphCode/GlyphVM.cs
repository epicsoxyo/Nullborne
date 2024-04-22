using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Nullborne.Player;
using Unity.VisualScripting;



namespace Nullborne.GlyphCode
{

    public enum GlyphCode : byte
    {
        GLYPH_LITERAL = 0x00,
        GLYPH_HAULT = 0x01,
        GLYPH_LOG = 0x02,
        GLYPH_WAIT = 0x03,
        GLYPH_EXPLODE = 0x04,
        GLYPH_REWIND = 0x05,

        // add new glyphs here...
    }



    public class GlyphVM : MonoBehaviour
    {

        private List<byte> bytecode_ = new List<byte>();

        [SerializeField] private Button compileButton_;
        [SerializeField] private Button runButton_;

        private ManaManager manaManager_;
        private int manaCost_ = 0;

        public ParticleSystem testParticles_;



        // assign buttons
        private void Start()
        {
            manaManager_ = FindFirstObjectByType<ManaManager>();

            compileButton_.onClick.AddListener(Compile);
            runButton_.onClick.AddListener(TransmuteGlyphs);
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
                Debug.Log(manaCost_);
                GlyphLiteral glyphLiteral = glyph.GetComponent<GlyphLiteral>();

                if(glyphLiteral != null) bytecode_.Add((byte)glyphLiteral.operand);
            }

        }



        public void TransmuteGlyphs()
        {
            if(manaManager_.currentMana < manaCost_) return;
            manaManager_.UseMana(manaCost_);
            StartCoroutine("Interpret");
        }



        // uses a stack to process the bytecode
        private IEnumerator Interpret()
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
                        testParticles_.Play();
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