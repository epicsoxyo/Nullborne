using Nullborne.Player;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;
    private Controller[] players;



    private void Awake()
    {

        if(instance != null)
        {
            Debug.LogWarning("Multiple PlayerManager instances detected!");
            return;
        }

        instance = this;

        players = FindObjectsOfType<Controller>();

    }



    public void EnableAllPlayers(bool isEnabled)
    {
        foreach(Controller player in players)
        {
            player.enabled = isEnabled;
        }
    }

}   