using System.Collections;
using System.Collections.Generic;
using Nullborne.Dialogue;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace Nullborne.Levels
{

    public class LevelEnd : MonoBehaviour
    {

        [SerializeField] private DialogueAsset levelEndDialogue_;


        private void OnTriggerEnter(Collider other)
        {
            
            if(!other.CompareTag("Player")) return;

            DialogueManager.instance.OpenDialogue(levelEndDialogue_);
            DialogueManager.instance.dialogueEnd.AddListener(() => SceneManager.LoadScene("DemoEnd"));

        }

    }

}