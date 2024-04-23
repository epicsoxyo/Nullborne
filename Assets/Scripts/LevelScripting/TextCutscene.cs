using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Nullborne.Dialogue
{

    public class TextCutscene : MonoBehaviour
    {

        [SerializeField] private DialogueAsset dialogueAsset_;

        private Button nextSceneButton_;
        [SerializeField] private string nextScene_;


    
        private void Start()
        {

            StartCoroutine("WaitForDialogueManager");

            nextSceneButton_ = FindFirstObjectByType<Button>();

            if(nextSceneButton_ == null) return;

            nextSceneButton_.onClick.AddListener(() => SceneManager.LoadScene(nextScene_));
            nextSceneButton_.gameObject.SetActive(false);

        }



        private IEnumerator WaitForDialogueManager()
        {

            while(DialogueManager.instance == null) yield return null;

            DialogueManager.instance.OpenDialogue(dialogueAsset_);
            DialogueManager.instance.dialogueEnd.AddListener(LoadNextScene);

        }



        private void Update()
        {
            
            if(Input.GetButtonDown("Cancel"))
            {
                DialogueManager.instance.CloseDialogue();
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