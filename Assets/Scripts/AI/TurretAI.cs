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

        [SerializeField] private DialogueAsset dialogue_;

        private CinemachineVirtualCamera cinemachineVirtualCamera_;




        private void Awake()
        {
            playerTransform_ = GameObject.FindWithTag("Player").transform;
            cinemachineVirtualCamera_ = FindFirstObjectByType<CinemachineVirtualCamera>();
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

            cinemachineVirtualCamera_.LookAt = transform.GetChild(0);
            // cinemachineVirtualCamera_.m_LookAt = transform.GetChild(0);

            cinemachineVirtualCamera_.m_Lens = new LensSettings
            (
                25f,
                cinemachineVirtualCamera_.m_Lens.OrthographicSize,
                cinemachineVirtualCamera_.m_Lens.NearClipPlane,
                cinemachineVirtualCamera_.m_Lens.FarClipPlane,
                cinemachineVirtualCamera_.m_Lens.Dutch
            );

            DialogueManager.instance.OpenDialogue(dialogue_);
            DialogueManager.instance.dialogueEnd.AddListener(() => StartCoroutine("ExplodePlayer"));

        }



        private IEnumerator ExplodePlayer()
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

            yield return new WaitForSeconds(1f);

            PlayerKiller playerKiller = playerTransform_.GetComponent<PlayerKiller>();

            Debug.Log(playerKiller);

            playerKiller.TriggerFirstPlayerDeath();

        }



        public void Explode()
        {

            // explode, then call player explosion that loads scene where nullborne realises that killing witches is punishable by death

        }

    }

}