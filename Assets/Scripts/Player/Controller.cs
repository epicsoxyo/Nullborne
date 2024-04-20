using UnityEngine;
using UnityEngine.AI;
using Nullborne.UI;



namespace Nullborne.Player
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class Controller : MonoBehaviour
    {

        private NavMeshAgent navMeshAgent_;
        
        private UIScreen currentUIScreen_;



        private void Awake()
        {
            navMeshAgent_ = GetComponent<NavMeshAgent>();
        }



        private void Start()
        {
            currentUIScreen_ = UIScreenManager.instance.currentScreen;            
        }



        private void Update()
        {
            
            if(!Input.GetKeyDown("tab")) return;

            if(currentUIScreen_ == UIScreen.SCREEN_MAIN)
                currentUIScreen_ = UIScreen.SCREEN_TRANSMUTER;
            else if(currentUIScreen_ == UIScreen.SCREEN_TRANSMUTER)
                currentUIScreen_ = UIScreen.SCREEN_MAIN;
            
            UIScreenManager.instance.SwitchToScreen(currentUIScreen_);

        }



        private void FixedUpdate()
        {

            Vector2 input = MovePlayer();

            if (input.magnitude >= 0.01f)
                MoveRelativeToCamera(input);

        }



        // moves player based on user input
        private Vector2 MovePlayer()
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

        }

    }

}