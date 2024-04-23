using UnityEngine;



namespace Nullborne.GlyphCode
{

    public enum GlyphCallbackType : byte
    {
        CB_ONDEATH = 0x00,
        CB_ONCAST = 0x01,

        // add callbacks here...
    }



    public class GlyphCallback : MonoBehaviour
    {

        [SerializeField] private GlyphCallbackType callbackType_;
        public GlyphCallbackType callbackType{ get{ return callbackType_; }}

        [SerializeField] private string callbackName_;
        public string callbackName{get{return callbackName_;}}

    }

}