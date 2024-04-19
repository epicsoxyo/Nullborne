using UnityEngine;
using TMPro;



namespace Nullborne.GlyphCode
{

    [RequireComponent(typeof(TMP_InputField))]
    public class GlyphLiteral : Glyph
    {

        private TMP_InputField textField;


        private void Awake()
        {
            
            textField = GetComponent<TMP_InputField>();

        }


        public int GetOperand()
        {
            return int.Parse(textField.text);
        }

    }

}