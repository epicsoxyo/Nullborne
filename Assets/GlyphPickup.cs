using Nullborne.Dialogue;
using UnityEngine;



namespace Nullborne.GlyphCode
{

    [RequireComponent(typeof(Collider))]
    public class GlyphPickup : MonoBehaviour
    {

        [SerializeField] private DialogueAsset glyphPickupNotification_;



        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player")) return;
            
            if(GetComponent<FocusedDialogue>()) DialogueManager.instance.dialogueEnd.AddListener(PingPickupNotification);
            else PingPickupNotification();
        }



        private void PingPickupNotification()
        {
            DialogueManager.instance.OpenDialogue(glyphPickupNotification_);
            DialogueManager.instance.dialogueEnd.AddListener(() => Destroy(gameObject));
        }


    }

}