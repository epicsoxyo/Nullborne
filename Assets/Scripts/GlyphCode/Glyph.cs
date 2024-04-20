using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Nullborne.GlyphCode
{

    public class Glyph : MonoBehaviour
    {

        [SerializeField] private GlyphCode glyphCode_;
        public GlyphCode glyphCode{get{return glyphCode_;}}

        [SerializeField] private int glyphCost_;
        public int glyphCost{get{return glyphCost_;}}

    }

}