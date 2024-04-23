using UnityEngine;



namespace Nullborne.Quests
{

    [RequireComponent(typeof(Collider))]
    public class GoToLocationQuest : MonoBehaviour
    {

        private Collider coll;
        [SerializeField] private string questEventName_;



        private void Awake()
        {
            coll = GetComponent<Collider>();
        }



        private void OnTriggerEnter(Collider other)
        {

            if(!other.CompareTag("Player")) return;

            QuestManager.instance.MarkQuestAsComplete(questEventName_);
            Destroy(gameObject);

        }

    }

}