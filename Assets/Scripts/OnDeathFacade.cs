using UnityEngine;
using UnityEngine.UI;

using Nullborne.Dialogue;



namespace Nullborne.UI
{

    [RequireComponent(typeof(Button))]
    public class OnDeathFacade : MonoBehaviour
    {

        private Button facadeButton_;
        [SerializeField] private DialogueAsset facadeDialogue_;


        private void Awake()
        {
            facadeButton_ = GetComponent<Button>();
            facadeButton_.onClick.AddListener(() => DialogueManager.instance.OpenDialogue(facadeDialogue_));
        }

    }

}