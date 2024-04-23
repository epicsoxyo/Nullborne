using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Nullborne.Dialogue
{

    [RequireComponent(typeof(DialogueManager))]
    public class TextCutscene : MonoBehaviour
    {

        [SerializeField] private DialogueAsset dialogueAsset_;
        private DialogueManager dialogueManager_;

        private Button nextSceneButton_;
        [SerializeField] private string nextScene_;


    
        private void Start()
        {

            Invoke("RunCutscene", 0.5f);

            nextSceneButton_ = FindFirstObjectByType<Button>();

            if(nextSceneButton_ == null) return;

            nextSceneButton_.onClick.AddListener(() => SceneManager.LoadScene(nextScene_));
            nextSceneButton_.gameObject.SetActive(false);

        }



        private void RunCutscene()
        {

            dialogueManager_ = FindFirstObjectByType<DialogueManager>();
            dialogueManager_.dialogueEnd.AddListener(LoadNextScene);
            dialogueManager_.OpenDialogue(dialogueAsset_);

        }



        private void Update()
        {
            
            if(Input.GetButtonDown("Cancel"))
            {
                dialogueManager_.CloseDialogue();
            }

        }



        private void LoadNextScene()
        {

            if(nextSceneButton_ == null)
            {
                SceneManager.LoadScene(nextScene_);
                return;
            }

            nextSceneButton_.gameObject.SetActive(true);

        }

    }

}