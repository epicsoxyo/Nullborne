using UnityEngine;

using Cinemachine;



namespace Nullborne.Dialogue
{

    [RequireComponent(typeof(Collider))]
    public class FocusedDialogue : MonoBehaviour
    {

        [SerializeField] private Transform focus_;

        [SerializeField] private DialogueAsset dialogue_;

        private CinemachineVirtualCamera cinemachineVirtualCamera_;



        private void Start()
        {
            cinemachineVirtualCamera_ = FindFirstObjectByType<CinemachineVirtualCamera>();
        }



        private void OnTriggerEnter(Collider other)
        {

            if(!other.CompareTag("Player")) return;

            cinemachineVirtualCamera_.m_Follow = focus_;
            cinemachineVirtualCamera_.m_LookAt = focus_;

            DialogueManager.instance.dialogueEnd.AddListener(ReturnFocusToPlayer);
            DialogueManager.instance.OpenDialogue(dialogue_);

        }



        public void ReturnFocusToPlayer()
        {

            Transform playerTransform = GameObject.FindWithTag("Player").transform;

            cinemachineVirtualCamera_.m_Follow = playerTransform;
            cinemachineVirtualCamera_.m_LookAt = playerTransform;

            Destroy(gameObject);

        }

    }

}