using UnityEngine;
using TMPro;



namespace Nullborne.GlyphCode
{

    public enum GlyphLiteralType : byte
    {
        LITERAL_SAVE = 0x00,
        LITERAL_NULLBORNE = 0x01,
        LITERAL_GUARDIAN = 0x02,
        LITERAL_STATUE = 0x03,

        // add literals here...
    }



    [RequireComponent(typeof(TMP_InputField))]
    public class GlyphLiteral : MonoBehaviour
    {

        [SerializeField] private GlyphLiteralType operand_;
        public GlyphLiteralType operand{get{return operand_;}}

    }

}