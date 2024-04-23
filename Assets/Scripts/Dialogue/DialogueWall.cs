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

            if(!other.CompareTag("Player")) return;
            
            DialogueManager.instance.OpenDialogue(dialogue);

        }


    }

}