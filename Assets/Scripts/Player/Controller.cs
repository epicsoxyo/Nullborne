using UnityEngine;
using UnityEngine.AI;

using Nullborne.UI;
using Nullborne.Quests;



namespace Nullborne.Player
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class Controller : MonoBehaviour
    {

        private NavMeshAgent navMeshAgent_;

        private Animator anim_;

        private bool hasOpenedTransmuter_ = false;
        


        private void Awake()
        {
            navMeshAgent_ = GetComponent<NavMeshAgent>();
            anim_ = GetComponentInChildren<Animator>();
        }



        private void Update()
        {
            
            if(Input.GetButtonDown("Menu")) SwitchUIScreens();

            Vector2 input = GetMovementInput();

            if (input.magnitude >= 0.1f)
            {
                MoveRelativeToCamera(input);
                return;
            }

            anim_.SetFloat("Speed", 0f);

        }



        private void SwitchUIScreens()
        {

            UIScreen currentUIScreen = UIScreenManager.instance.currentScreen;

            if(currentUIScreen == UIScreen.SCREEN_TRANSMUTER)
            {
                UIScreenManager.instance.SwitchToScreen(UIScreen.SCREEN_MAIN);
                return;
            }
            
            if(currentUIScreen == UIScreen.SCREEN_MAIN)
            {
                if(!hasOpenedTransmuter_)
                {
                    hasOpenedTransmuter_ = true;
                    QuestManager.instance.MarkQuestAsComplete("OpenTransmuter");
                }

                UIScreenManager.instance.SwitchToScreen(UIScreen.SCREEN_TRANSMUTER);
            }

        }



        private Vector2 GetMovementInput()
        {
            return new Vector2
            (
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical")
            );
        }



        // converts player input to coords relative to current active camera
        private void MoveRelativeToCamera(Vector2 input)
        {

            // get relative axes
            Vector3 relativeX = Camera.main.transform.right;
            Vector3 relativeY = Camera.main.transform.forward;

            // remove vertical component + renormalize position vectors
            relativeX.y = 0;
            relativeY.y = 0;
            relativeX = relativeX.normalized;
            relativeY = relativeY.normalized;

            // get relative input
            Vector3 relativeInputX = relativeX * input.x * navMeshAgent_.speed * Time.fixedDeltaTime;
            Vector3 relativeInputY = relativeY * input.y * navMeshAgent_.speed * Time.fixedDeltaTime;
            Vector3 relativeMovement = relativeInputX + relativeInputY;

            // set player position in navmesh
            Vector3 destination = transform.position + relativeMovement;
            navMeshAgent_.SetDestination(destination);

            anim_.SetFloat("Speed", relativeMovement.magnitude);

        }

    }

}