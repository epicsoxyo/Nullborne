using Nullborne.Dialogue;
using Nullborne.Quests;
using UnityEngine;

public class LevelOneScript : MonoBehaviour
{

    [SerializeField] private DialogueAsset openingDialogue;
    [SerializeField] private QuestAsset tutorialQuest;

    private void Start()
    {

        FadeScreen.instance.FadeIn();
        DialogueManager.instance.OpenDialogue(openingDialogue);
        QuestManager.instance.SetCurrentQuest(tutorialQuest);

    }

}