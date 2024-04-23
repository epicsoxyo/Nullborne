using UnityEngine;



namespace Nullborne.Levels
{

    public abstract class LevelScript : MonoBehaviour
    {

        public static LevelScript instance;
        protected int questsCompleted_ = 0;




        private void Awake()
        {

            if(instance != null)
            {
                Debug.LogWarning("Multiple level script instances detected!");
                return;
            }

            instance = this;

        }



        public abstract void QuestCompleted();

    }

}