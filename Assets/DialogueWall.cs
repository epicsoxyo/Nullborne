using UnityEngine;



namespace Nullborne.Dialogue
{

    [RequireComponent(typeof(Collider))]
    public class DialogueWall : MonoBehaviour
    {

        private Collider coll;
        [SerializeField] private DialogueAsset dialogue;



        private void Awake()
        {
            coll = GetComponent<Collider>();
        }



        private void OnTriggerEnter(Collider other)
        {

            Debug.Log("Something entered my trigger");

            if(!other.CompareTag("Player")) return;
            
            Debug.Log("That thing was a player");
            
            DialogueManager.instance.OpenDialogue(dialogue);

        }


    }

}