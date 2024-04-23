using System.Collections;

using UnityEngine;

using Cinemachine;

using Nullborne.Dialogue;
using Nullborne.Player;



namespace Nullborne.AI
{

    [RequireComponent(typeof(Collider))]
    public class TurretAI : MonoBehaviour
    {

        private Transform playerTransform_;

        private bool playerIsNearby_ = false;
        private bool hasAlerted_ = false;

        [SerializeField] private DialogueAsset spottedDialogue_;
        [SerializeField] private DialogueAsset distractedDialogue_;
        [SerializeField] private DialogueAsset soundDialogue_;

        private CinemachineVirtualCamera cinemachineVirtualCamera_;

        private ParticleSystem particleSystem_;
        private SkinnedMeshRenderer[] skinnedMeshRenderers_;




        private void Awake()
        {
            playerTransform_ = GameObject.FindWithTag("Player").transform;
            cinemachineVirtualCamera_ = FindFirstObjectByType<CinemachineVirtualCamera>();
            particleSystem_ = GetComponentInChildren<ParticleSystem>();
            skinnedMeshRenderers_ = GetComponentsInChildren<SkinnedMeshRenderer>();
        }



        private void Update()
        {
            
            if(!playerIsNearby_ || hasAlerted_) return;

            Alert();

            hasAlerted_ = true;

        }



        private void OnTriggerEnter(Collider other)
        {
            
            if(!other.CompareTag("Player")) return;

            playerIsNearby_ = true;

        }



        private void OnTriggerExit(Collider other)
        {
            
            if(!other.CompareTag("Player")) return;

            playerIsNearby_ = false;

        }



        private bool PlayerIsVisible()
        {

            RaycastHit hit;

            if(Physics.Raycast(transform.position, playerTransform_.position - transform.position, out hit, LayerMask.GetMask("Player"))
            && hit.collider.CompareTag("Player"))
            {

                return true;
            }

            return false;

        }




        public void Alert()
        {

            FocusOnTurret();

            DialogueManager.instance.OpenDialogue(spottedDialogue_);
            DialogueManager.instance.dialogueEnd.AddListener(() => StartCoroutine("ExplodePlayer"));

        }



        private IEnumerator ExplodePlayer()
        {

            FocusOnPlayer();

            yield return new WaitForSeconds(1f);

            playerTransform_.GetComponent<PlayerKiller>().KillPlayer();

        }



        private void FocusOnTurret()
        {

            cinemachineVirtualCamera_.LookAt = transform.GetChild(0);

            cinemachineVirtualCamera_.m_Lens = new LensSettings
            (
                25f,
                cinemachineVirtualCamera_.m_Lens.OrthographicSize,
                cinemachineVirtualCamera_.m_Lens.NearClipPlane,
                cinemachineVirtualCamera_.m_Lens.FarClipPlane,
                cinemachineVirtualCamera_.m_Lens.Dutch
            );

        }



        private void FocusOnPlayer()
        {

            cinemachineVirtualCamera_.LookAt = playerTransform_;

            cinemachineVirtualCamera_.m_Lens = new LensSettings
            (
                60f,
                cinemachineVirtualCamera_.m_Lens.OrthographicSize,
                cinemachineVirtualCamera_.m_Lens.NearClipPlane,
                cinemachineVirtualCamera_.m_Lens.FarClipPlane,
                cinemachineVirtualCamera_.m_Lens.Dutch
            );

        }




        public void Explode()
        {

            particleSystem_.Play();

            foreach(SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers_)
            {
                skinnedMeshRenderer.enabled = false;
            }

            Invoke("KillPlayerForMurder", 1f);

        }

        private void KillPlayerForMurder()
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerKiller>().KillPlayerForMurder();
        }



        public void Distract(Transform lookAtTransform)
        {

            transform.LookAt(lookAtTransform);

            GetComponent<Collider>().enabled = false;

            FocusOnTurret();

            DialogueManager.instance.OpenDialogue(distractedDialogue_);
            DialogueManager.instance.dialogueEnd.AddListener(() => FocusOnPlayer());

        }



        public void Sound()
        {

            FocusOnTurret();

            DialogueManager.instance.OpenDialogue(soundDialogue_);
            DialogueManager.instance.dialogueEnd.AddListener(() => 
            {
                FocusOnPlayer();
                DialogueManager.instance.dialogueEnd.RemoveListener(FocusOnPlayer);
            });

        }

    }

}