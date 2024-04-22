using UnityEngine;



[CreateAssetMenu(fileName = "QuestAsset", menuName = "QuestAsset", order = 0)]
public class QuestAsset : ScriptableObject
{

    public string questGoal;

    public string[] questChecklist;

    public string[] questEventListeners;

}