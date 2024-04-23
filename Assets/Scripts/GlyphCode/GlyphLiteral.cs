using UnityEngine;
using TMPro;



namespace Nullborne.GlyphCode
{

    public enum GlyphLiteralType : byte
    {
        LITERAL_SAVE = 0x00,
        LITERAL_WITCH = 0x01,
        LITERAL_STATUE = 0x02,

        // add literals here...
    }



    [RequireComponent(typeof(TMP_InputField))]
    public class GlyphLiteral : Glyph
    {

        [SerializeField] private GlyphLiteralType operand_;
        public GlyphLiteralType operand{get{return operand_;}}



        private void Awake()
        {
            glyphCode_ = GlyphCode.GLYPH_LITERAL;
        }


    }

}