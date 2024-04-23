using UnityEngine;
using TMPro;
using Nullborne.Levels;



namespace Nullborne.Quests
{

    public class QuestManager : MonoBehaviour
    {

        public static QuestManager instance;

        private TextMeshProUGUI questGoalField;

        [SerializeField] private GameObject questCheckboxPrefab;
        private QuestCheckbox[] questCheckboxes_;

        string[] questEventListeners_;
        private bool[] completed;



        private void Awake()
        {

            if(instance != null)
            {
                Debug.LogWarning("Multiple QuestManager instances detected!");
                return;
            }
            instance = this;

            questGoalField = GetComponentInChildren<TextMeshProUGUI>();

        }



        public void SetCurrentQuest(QuestAsset questAsset)
        {

            questGoalField.SetText(questAsset.questGoal);

            ClearQuestList();

            string[] questChecklist = questAsset.questChecklist;

            questEventListeners_ = questAsset.questEventListeners;

            questCheckboxes_ = new QuestCheckbox[questChecklist.Length];

            completed = new bool[questChecklist.Length];
            for(int i = 0; i < completed.Length; i++) completed[i] = false;

            for(int i = 0; i < questChecklist.Length; i++)
            {
                questCheckboxes_[i] = Instantiate(questCheckboxPrefab).GetComponent<QuestCheckbox>();
                questCheckboxes_[i].transform.SetParent(transform);

                questCheckboxes_[i].UpdateQuestText(questChecklist[i]);
            }

        }



        private void ClearQuestList()
        {

            for (int i = transform.childCount - 1; i > 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

        }



        // not the best way of doing this but eh i dont have time
        public void MarkQuestAsComplete(string questListener)
        {

            for(int i = 0; i < questEventListeners_.Length; i++)
            {
                if(questEventListeners_[i] == questListener)
                {
                    questCheckboxes_[i].MarkAsComplete();
                    if(CheckIfComplete()) return;
                }
            }

        }


        // nor this
        private bool CheckIfComplete()
        {

            foreach(QuestCheckbox questCheckbox in questCheckboxes_)
            {
                if(!questCheckbox.isComplete) return false;
            }

            LevelScript.instance.QuestCompleted();
            return true;

        }

    }

}