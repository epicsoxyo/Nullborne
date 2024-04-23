using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Nullborne.Quests;



namespace Nullborne.GlyphCode
{

    public class CallbackSelector : MonoBehaviour
    {

        [SerializeField] private Button leftButton_;
        [SerializeField] private Button rightButton_;

        private TextMeshProUGUI callbackTextBox_;

        private GlyphCallback[] callbackList_;
        private int currentCallbackIndex_ = 0;

        private bool hasSwitchedToOnDeath_ = false;



        private void Start()
        {

            leftButton_.onClick.AddListener(() => SwitchCallbacks(false));
            rightButton_.onClick.AddListener(() => SwitchCallbacks(true));

            callbackTextBox_ = GetComponentInChildren<TextMeshProUGUI>();

            callbackList_ = FindObjectsOfType<GlyphCallback>();

            UpdateCallbackUI();

        }



        private void SwitchCallbacks(bool forward)
        {

            if(forward) IncrementCurrentCallback();
            else DecrementCurrentCallback();

            if(!hasSwitchedToOnDeath_
            && callbackList_[currentCallbackIndex_].callbackType == GlyphCallbackType.CB_ONDEATH)
            {
                hasSwitchedToOnDeath_ = true;
                QuestManager.instance.MarkQuestAsComplete("SwitchToOnDeath");
            }

            UpdateCallbackUI();

        }



        private void IncrementCurrentCallback()
        {
            currentCallbackIndex_++;
            if(currentCallbackIndex_ >= callbackList_.Length) currentCallbackIndex_ = 0;
        }



        private void DecrementCurrentCallback()
        {
            currentCallbackIndex_--;
            if(currentCallbackIndex_ < 0) currentCallbackIndex_ = callbackList_.Length - 1;
        }



        private void UpdateCallbackUI()
        {
            UpdateActiveCallback();
            UpdateCallbackText();
        }



        private void UpdateActiveCallback()
        {
            for(int i = 0; i < callbackList_.Length; i++)
            {
                callbackList_[i].gameObject.SetActive(i == currentCallbackIndex_);
            }
        }



        private void UpdateCallbackText()
        {

            string callbackText = callbackList_[currentCallbackIndex_].callbackName;

            callbackTextBox_.SetText(callbackText);

        }

    }
}