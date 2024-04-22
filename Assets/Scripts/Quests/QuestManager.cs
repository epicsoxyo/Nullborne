using UnityEngine;
using TMPro;



namespace Nullborne.Quests
{

    public class QuestManager : MonoBehaviour
    {

        public static QuestManager instance;

        private TextMeshProUGUI questGoalField;

        [SerializeField] private GameObject questCheckboxPrefab;
        private TextMeshProUGUI[] questCheckboxes;

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

            string[] questItems = questAsset.questChecklist;
            string[] questEventListeners = questAsset.questEventListeners;

            foreach(string questItem in questItems)
            {
                GameObject questCheckbox = Instantiate(questCheckboxPrefab);
                questCheckbox.transform.SetParent(transform);

                questCheckbox.GetComponentInChildren<TextMeshProUGUI>().SetText(questItem);

            }

        }



        private void ClearQuestList()
        {

            for (int i = transform.childCount - 1; i > 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

        }

    }

}