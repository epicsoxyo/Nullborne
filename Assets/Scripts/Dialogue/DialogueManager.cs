using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

using Nullborne.UI;



namespace Nullborne.Dialogue
{

    public class DialogueManager : MonoBehaviour
    {

        public static DialogueManager instance;

        public UnityEvent dialogueEnd = new UnityEvent();

        private TextMeshProUGUI dialogueTextBox;
        private Image downArrow;

        private string speaker;
        private string[] currentDialogue;
        private int currentDialogueIndex = 0;

        [SerializeField] private float charactersPerSecond;
        private bool isReading = false;



        private void Awake()
        {
            
            if(instance != null)
            {
                Debug.LogWarning("Multiple DialogueManager instances detected!");
                return;
            }

            instance = this;

        }



        private void Start()
        {

            dialogueTextBox = GetComponentInChildren<TextMeshProUGUI>();

            downArrow = GetComponentsInChildren<Image>()[1];
            downArrow.enabled = false;

        }



        private void Update()
        {
            
            if(currentDialogue == null || !Input.GetButtonDown("Submit")) return;

            if(currentDialogueIndex >= currentDialogue.Length)
            {
                CloseDialogue();
                return;
            }

            if(isReading) SkipDialogue();
            else StartCoroutine("ReadDialogue");

        }



        public void OpenDialogue(DialogueAsset dialogueAsset)
        {

            if(UIScreenManager.instance)
            {
                UIScreenManager.instance.ToggleDialogue(true);
            }

            speaker = dialogueAsset.speaker;
            currentDialogue = dialogueAsset.dialogue;
            currentDialogueIndex = 0;

            StopAllCoroutines();
            StartCoroutine("ReadDialogue");

        }



        private IEnumerator ReadDialogue()
        {

            isReading = true;

            downArrow.enabled = false;

            for(int i = 1; i <= currentDialogue[currentDialogueIndex].Length; i++)
            {
                string speakerString = (speaker == "") ? ("") : (speaker + ": ");
                string substring = currentDialogue[currentDialogueIndex].Substring(0, i);

                dialogueTextBox.SetText(speakerString + substring);

                yield return new WaitForSeconds(1 / charactersPerSecond);
            }

            downArrow.enabled = true;

            isReading = false;

            currentDialogueIndex++;

        }



        private IEnumerator ReadMenuText(string menuText)
        {

            isReading = true;

            for(int i = 1; i <= menuText.Length; i++)
            {
                string substring = menuText.Substring(0, i);

                dialogueTextBox.SetText(substring);

                yield return new WaitForSeconds(1 / charactersPerSecond);
            }

            isReading = false;

        }



        private void SkipDialogue()
        {

            StopAllCoroutines();

            string speakerString = (speaker == "") ? ("") : (speaker + ": ");
            dialogueTextBox.SetText(speakerString + currentDialogue[currentDialogueIndex]);

            downArrow.enabled = true;

            isReading = false;

            currentDialogueIndex++;

        }



        public void CloseDialogue()
        {

            Debug.Log("Closing dialogue");

            StopAllCoroutines();

            downArrow.enabled = false;

            currentDialogue = null;

            if(UIScreenManager.instance)
            {
                Debug.Log("There is an instance of screen manager");
                UIScreenManager.instance.ToggleDialogue(false);
            }

            dialogueEnd.Invoke();

        }

    }

}