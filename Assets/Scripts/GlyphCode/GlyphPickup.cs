using System.Collections.Generic;
using Nullborne.Dialogue;
using Nullborne.Quests;
using UnityEngine;



namespace Nullborne.GlyphCode
{

    [RequireComponent(typeof(Collider))]
    public class GlyphPickup : MonoBehaviour
    {

        [SerializeField] private DialogueAsset glyphPickupNotification_;
        [SerializeField] private List<GameObject> glyphsToAdd_ = new List<GameObject>();
        [SerializeField] private List<GameObject> literalsToAdd_ = new List<GameObject>();



        private void OnTriggerEnter(Collider other)
        {
            if(!other.CompareTag("Player")) return;

            AddGlyphs();

            CompleteGlyphPickupQuest();
            
            if(GetComponent<FocusedDialogue>()) DialogueManager.instance.dialogueEnd.AddListener(PingPickupNotification);
            else PingPickupNotification();
        }



        private void AddGlyphs()
        {

            foreach(GameObject glyph in glyphsToAdd_)
            {
                GlyphsTray.instance.AddGlyph(glyph);
            }

            foreach(GameObject literal in literalsToAdd_)
            {
                GlyphsTray.instance.AddLiteral(literal);
            }

        }



        private void CompleteGlyphPickupQuest()
        {
            QuestManager.instance.MarkQuestAsComplete("PickUpGlyphs");
        }



        private void PingPickupNotification()
        {

            DialogueManager.instance.OpenDialogue(glyphPickupNotification_);
            DialogueManager.instance.dialogueEnd.AddListener(() => Destroy(gameObject));

        }

    }

}