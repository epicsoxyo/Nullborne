using UnityEngine;

using Nullborne.Dialogue;
using Nullborne.Quests;



namespace Nullborne.Levels
{

    public class FirstDeathScript : LevelScript
    {

        [Header("Dialogues")]
        [SerializeField] private DialogueAsset openingDialogue_;
        // [SerializeField] private DialogueAsset tutorialDialogue1_;

        // [Header("Quests")]
        // [SerializeField] private QuestAsset tutorialQuest_;
        // [SerializeField] private QuestAsset tutorialQuest2_;

        // private DialogueWall dialogueWall_;


        private void Start()
        {

            // dialogueWall_ = FindFirstObjectByType<DialogueWall>();

            Invoke("Opening", 0.5f);

        }



        private void Opening()
        {
            FadeScreen.instance.FadeIn();
            DialogueManager.instance.OpenDialogue(openingDialogue_);
            // QuestManager.instance.SetCurrentQuest(tutorialQuest_);
        }



        public override void QuestCompleted()
        {

            // switch(questsCompleted_)
            // {
            //     case 0:
            //         Destroy(dialogueWall_.gameObject);
            //         // DialogueManager.instance.OpenDialogue(tutorialDialogue1_);
            //         // QuestManager.instance.SetCurrentQuest(tutorialQuest2_);
            //         break;
            // }

            // questsCompleted_++;

        }

    }

}