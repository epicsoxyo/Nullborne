using UnityEngine;
using UnityEngine.UI;

using TMPro;



namespace Nullborne.GlyphCode
{

    public enum GlyphCallback : byte
    {
        CB_ONDEATH = 0x00,
        CB_ONCAST = 0x01,

        // add callbacks here...
    }



    public class TransmuterCallback : MonoBehaviour
    {

        [SerializeField] private Button leftButton_;
        [SerializeField] private Button rightButton_;
        [SerializeField] private TextMeshProUGUI callbackText_;

        [SerializeField] private GlyphCallback startingCallback_;
        private GlyphCallback currentCallback_;



        private void Start()
        {

            leftButton_.onClick.AddListener(() => SwitchCallbacks(false));
            rightButton_.onClick.AddListener(() => SwitchCallbacks(true));

            currentCallback_ = startingCallback_;

            UpdateCallbackText();

        }



        private void SwitchCallbacks(bool forward)
        {

            currentCallback_ = forward ? GetNextCallback() : GetPreviousCallback();

            UpdateCallbackText();

        }



        // yes i know these are jank but i dont have the time quite frankly -----
        private GlyphCallback GetNextCallback()
        {

            switch(currentCallback_)
            {
                case GlyphCallback.CB_ONDEATH:
                    return GlyphCallback.CB_ONCAST;
            }

            return GlyphCallback.CB_ONDEATH;

        }

        private GlyphCallback GetPreviousCallback()
        {

            switch(currentCallback_)
            {
                case GlyphCallback.CB_ONCAST:
                    return GlyphCallback.CB_ONDEATH;
            }

            return GlyphCallback.CB_ONCAST;

        }
        // ----------------------------------------------------------------------



        public string GetCallbackName(GlyphCallback glyphCallback)
        {

            switch(glyphCallback)
            {
                case GlyphCallback.CB_ONDEATH:
                    return "On Death";
                case GlyphCallback.CB_ONCAST:
                    return "On Cast";
            }

            Debug.LogError(glyphCallback + " does not have a string name!");
            return "";

        }

        private void UpdateCallbackText()
        {
            callbackText_.SetText(GetCallbackName(currentCallback_));
        }

    }
}