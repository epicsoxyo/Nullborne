using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Nullborne.GlyphCode
{

    public class Glyph : MonoBehaviour
    {

        [SerializeField] private GlyphCode glyphCode;

        public GlyphCode GetGlyph()
        {
            return glyphCode;
        }

    }

}