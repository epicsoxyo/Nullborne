using UnityEngine;



namespace Nullborne.UI
{

    public enum UIScreen
    {
        SCREEN_EMPTY, // no UI elements on screen
        SCREEN_MAIN, // main gameplay HUD
        SCREEN_TRANSMUTER, // transmuter menu
        SCREEN_DIALOGUE, // immersive dialogue screens

        // add additional screens here...
    }



    public class UIScreenManager : MonoBehaviour
    {

        public static UIScreenManager instance;

        UIScreenElement[] screenElements_;

        [SerializeField] private UIScreen startingScreen_;
        private UIScreen currentScreen_;
        public UIScreen currentScreen{get{return currentScreen_;}}



        private void Start()
        {

            if(instance != null)
            {
                Debug.LogWarning("Multiple Screen Managers detected!");
                return;
            }
            instance = this;

            screenElements_ = FindObjectsOfType<UIScreenElement>();

            QuickSwitchToScreen(startingScreen_);
            currentScreen_ = startingScreen_;

        }



        public void QuickSwitchToScreen(UIScreen screen)
        {
            foreach(UIScreenElement element in screenElements_)
            {
                element.isOnScreen = (element.screen == screen);
            }
            currentScreen_ = screen;
        }



        public void SwitchToScreen(UIScreen screen)
        {
            foreach(UIScreenElement element in screenElements_)
            {
                if(element.screen == screen)
                    element.EnterTransition();
                else
                    element.ExitTransition();
            }
            currentScreen_ = screen;
        }

    }

}