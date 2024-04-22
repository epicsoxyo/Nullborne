using UnityEngine;
using UnityEngine.SceneManagement;


namespace Nullborne.Dialogue
{

    public class TextCutscene : MonoBehaviour
    {

        [SerializeField] private DialogueAsset dialogue;
        private DialogueManager dialogueManager;

        [SerializeField] private string nextScene;


    
        private void Start()
        {

            dialogueManager = FindFirstObjectByType<DialogueManager>();
            dialogueManager.dialogueEnd.AddListener(() => SceneManager.LoadScene(nextScene));
            dialogueManager.OpenDialogue(dialogue);

        }



        private void Update()
        {
            
            if(Input.GetButtonDown("Cancel"))
            {
                dialogueManager.CloseDialogue();
            }

        }

    }

}