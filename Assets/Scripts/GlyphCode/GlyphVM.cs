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

        private Stack<byte> stack_ = new Stack<byte>();
        [SerializeField] private Button compileButton_;
        public ParticleSystem particleSystem_;


        private void Start()
        {
            compileButton_.onClick.AddListener(() =>
            {
                StartCoroutine(Interpret(Compile()));
            });
        }



        public Queue<byte> Compile()
        {

            List<Glyph> glyphs = DepthFirstGlyphsList();
            Queue<byte> bytecode = new Queue<byte>();

            foreach(Glyph glyph in glyphs)
            {
                bytecode.Enqueue((byte)glyph.GetGlyph());

                GlyphLiteral glyphLiteral = glyph.GetComponent<GlyphLiteral>();

                if(glyphLiteral != null) bytecode.Enqueue((byte)glyphLiteral.GetOperand());
            }

            return bytecode;

        }



        private IEnumerator Interpret(Queue<byte> bytecode)
        {

            while(bytecode.Count > 0)
            {

                switch((GlyphCode)bytecode.Dequeue())
                {
                    case GlyphCode.GLYPH_LITERAL:
                        stack_.Push(bytecode.Dequeue());
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
                        particleSystem_.Play();
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