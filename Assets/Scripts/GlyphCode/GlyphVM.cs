using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;



namespace Nullborne.GlyphCode
{

    public enum GlyphCode : byte
    {
        GLYPH_LITERAL = 0x00,
        GLYPH_HAULT = 0x01,
        GLYPH_LOG = 0x02,
        GLYPH_WAIT = 0x03,
        GLYPH_EXPLODE = 0x04,

        // add new glyphs here...
    }



    public class GlyphVM : MonoBehaviour
    {

        private List<byte> bytecode_ = new List<byte>();
        private Stack<byte> stack_ = new Stack<byte>();

        [SerializeField] private Button compileButton_;
        [SerializeField] private Button runButton_;

        public ParticleSystem testParticles_;



        private void Start()
        {
            compileButton_.onClick.AddListener(Compile);
            runButton_.onClick.AddListener(() => StartCoroutine("Interpret"));
        }



        public void Compile()
        {

            List<Glyph> glyphs = DepthFirstGlyphsList();

            bytecode_.Clear();

            foreach(Glyph glyph in glyphs)
            {
                bytecode_.Add((byte)glyph.GetGlyph());

                GlyphLiteral glyphLiteral = glyph.GetComponent<GlyphLiteral>();

                if(glyphLiteral != null) bytecode_.Add((byte)glyphLiteral.GetOperand());
            }

        }



        private IEnumerator Interpret()
        {

            for(int i = 0; i < bytecode_.Count; i++)
            {

                switch((GlyphCode)bytecode_[i])
                {
                    case GlyphCode.GLYPH_LITERAL:
                        stack_.Push(bytecode_[++i]);
                        break;
                    case GlyphCode.GLYPH_HAULT:
                        stack_.Clear();
                        yield break;
                    case GlyphCode.GLYPH_LOG:
                        Debug.Log(stack_.Pop());
                        break;
                    case GlyphCode.GLYPH_WAIT:
                        yield return new WaitForSeconds(stack_.Pop());
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



        private List<Glyph> DepthFirstGlyphsList()
        {
            List<Glyph> glyphList = new List<Glyph>();

            foreach(Transform child in transform)
            {
                GetDepthFirstListOfGlyphs(ref glyphList, child);
            }

            Glyph currentGlyph = GetComponent<Glyph>();
            if(currentGlyph != null) glyphList.Add(currentGlyph);

            return glyphList;
        }

        private void GetDepthFirstListOfGlyphs(ref List<Glyph> glyphList, Transform child)
        {

            foreach(Transform grandchild in child)
            {
                GetDepthFirstListOfGlyphs(ref glyphList, grandchild);
            }

            Glyph currentGlyph = child.GetComponent<Glyph>();
            if(currentGlyph != null) glyphList.Add(currentGlyph);

        }

    }

}