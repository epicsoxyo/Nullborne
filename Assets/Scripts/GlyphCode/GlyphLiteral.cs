using UnityEngine;
using TMPro;



namespace Nullborne.GlyphCode
{

    [RequireComponent(typeof(TMP_InputField))]
    public class GlyphLiteral : Glyph
    {

        private TMP_InputField textField;
        public int operand{get{return int.Parse(textField.text);}}


        private void Awake()
        {
            textField = GetComponent<TMP_InputField>();
        }

    }

}