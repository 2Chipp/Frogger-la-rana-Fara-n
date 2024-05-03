using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    public enum PlayerStatus { Undefined, Winner, Loser }

    [SerializeField] private GameObject camCurtain;
    [SerializeField] private GameObject myParticleSystem;

    DataManager dataManager;
    EventManager eventManager;

    public static GameFinisher gameFinisher;

    private void Awake()
    {
        if (gameFinisher == null)
        {
            gameFinisher = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        eventManager = EventManager.eventManager;
        dataManager = DataManager.dataManager;

        eventManager.onEndGame += EndGame;
    }
    // Update is called once per frame
    public void EndGame()
    {
        switch (dataManager.MyPlayerStatus)
        {
            default:
                break;
            case PlayerStatus.Winner:
                Win();
                break;
            case PlayerStatus.Loser:
                Lose();
                break;
        }
    }


    void Win()
    {

    }

    void Lose()
    {

    }

    private void OnDestroy()
    {
        eventManager.onEndGame -= EndGame;
    }
}
