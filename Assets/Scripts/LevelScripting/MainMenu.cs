using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



namespace Nullborne.UI
{

    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private Button startButton_;
        [SerializeField] private Button quitButton_;

        [SerializeField] private string startingScene_;



        private void Start()
        {
            startButton_.onClick.AddListener(() => SceneManager.LoadScene(startingScene_));
            quitButton_.onClick.AddListener(() => Application.Quit());
        }


    }

}