using UnityEngine;



namespace Nullborne.GlyphCode
{

    public class Glyph : MonoBehaviour
    {

        [SerializeField] protected GlyphCode glyphCode_;
        public GlyphCode glyphCode{get{return glyphCode_;}}

        [SerializeField] protected int glyphCost_;
        public int glyphCost{get{return glyphCost_;}}

    }

}