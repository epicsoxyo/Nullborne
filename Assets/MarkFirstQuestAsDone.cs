using System.Collections;
using Nullborne.Quests;
using UnityEngine;

public class MarkFirstQuestAsDone : MonoBehaviour
{

    private void Start()
    {
        Invoke("WaitForQuestManager", 1f);
    }

    private void WaitForQuestManager()
    {

        QuestManager.instance.MarkQuestAsComplete("PickUpGlyphs");

    }

}