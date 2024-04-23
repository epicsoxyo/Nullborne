using UnityEngine;

using Nullborne.Player;
using Nullborne.Quests;



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
        private UIScreen previousScreen_;



        private void Awake()
        {

            if(instance != null)
            {
                Debug.LogWarning("Multiple ScreenManager instances detected!");
                return;
            }
            instance = this;

        }



        private void Start()
        {

            screenElements_ = FindObjectsOfType<UIScreenElement>();

            QuickSwitchToScreen(startingScreen_);
            currentScreen_ = startingScreen_;
            previousScreen_ = currentScreen_;

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



        public void ToggleDialogue(bool dialogueOn)
        {

            PlayerManager.instance.EnableAllPlayers(!dialogueOn);

            if(dialogueOn)
            {
                if(currentScreen_ == UIScreen.SCREEN_DIALOGUE) return;
                previousScreen_ = currentScreen_;
                SwitchToScreen(UIScreen.SCREEN_DIALOGUE);
                return;
            }

            SwitchToScreen(previousScreen_);

        }

    }

}