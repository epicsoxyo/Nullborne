using System.Collections;

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



        private void Awake()
        {
            particleSystem_ = GetComponent<ParticleSystem>();
            meshRenderers_ = GetComponentsInChildren<SkinnedMeshRenderer>();
            fadeScreen_ = FindFirstObjectByType<FadeScreen>();
        }



        public void TriggerFirstPlayerDeath()
        {

            StartCoroutine(ExplodeThenLoad("1.1_FirstDeath"));

        }



        public void KillPlayer()
        {

            // ExplodeThenLoad("")

        }



        public void KillPlayerForMurder()
        {

            // ExplodeThenLoad("")

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