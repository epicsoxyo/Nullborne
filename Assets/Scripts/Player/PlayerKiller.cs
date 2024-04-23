using System.Collections;
using System.Collections.Generic;
using Nullborne.Dialogue;
using Nullborne.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;



namespace Nullborne.Player
{

    [RequireComponent(typeof(ParticleSystem))]
    public class PlayerKiller : MonoBehaviour
    {

        private ParticleSystem particleSystem_;
        private SkinnedMeshRenderer[] meshRenderers_;
        private FadeScreen fadeScreen_;

        [SerializeField] private List<DialogueAsset> soundDialogue_ = new List<DialogueAsset>();
        private int soundDialogueIndex_ = 0;



        private void Awake()
        {

            particleSystem_ = GetComponent<ParticleSystem>();
            meshRenderers_ = GetComponentsInChildren<SkinnedMeshRenderer>();
            fadeScreen_ = FindFirstObjectByType<FadeScreen>();

        }


        public void KillPlayer()
        {

            if(FindFirstObjectByType<LevelOneScript>())
            {
                TriggerFirstPlayerDeath();
                return;
            }

            StartCoroutine(ExplodeThenLoad("1.2_Spotted"));

        }



        public void TriggerFirstPlayerDeath()
        {
            StartCoroutine(ExplodeThenLoad("1.1_FirstDeath"));
        }



        public void Suicide()
        {
            StartCoroutine(ExplodeThenLoad("1.3_Suicide"));
        }



        public void KillPlayerForMurder()
        {
            StartCoroutine(ExplodeThenLoad("1.4_Murder"));
        }



        public void KillPlayerForSound()
        {
            DialogueManager.instance.OpenDialogue(soundDialogue_[0]);
            DialogueManager.instance.dialogueEnd.AddListener(SoundDialogue);
        }



        public void SoundDialogue()
        {

            soundDialogueIndex_++;

            if(soundDialogueIndex_ == soundDialogue_.Count)
            {
                StartCoroutine(ExplodeThenLoad("1.2_Spotted"));
                return;
            }

            DialogueManager.instance.OpenDialogue(soundDialogue_[soundDialogueIndex_]);

        }



        private IEnumerator ExplodeThenLoad(string sceneName)
        {

            particleSystem_.Play();

            foreach(SkinnedMeshRenderer meshRenderer in meshRenderers_)
            {
                meshRenderer.enabled = false;
            }

            GetComponent<Controller>().enabled = false;

            fadeScreen_.FadeOut();

            yield return new WaitForSeconds(3f);

            SceneManager.LoadScene(sceneName);

        }

    }

}