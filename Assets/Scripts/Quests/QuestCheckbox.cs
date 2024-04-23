using TMPro;
using UnityEngine;



namespace Nullborne.Quests
{

    public class QuestCheckbox : MonoBehaviour
    {

        private TextMeshProUGUI questTextBox_;

        private bool isComplete_ = false;
        public bool isComplete{get{return isComplete_;}}



        private void Awake()
        {
            questTextBox_ = GetComponentInChildren<TextMeshProUGUI>();
        }



        public void UpdateQuestText(string questText)
        {
            if(questText == null) return;
            questTextBox_.SetText(questText);
        }



        public void MarkAsComplete()
        {
            isComplete_ = true;
            questTextBox_.fontStyle = FontStyles.Strikethrough;
        }

    }

}