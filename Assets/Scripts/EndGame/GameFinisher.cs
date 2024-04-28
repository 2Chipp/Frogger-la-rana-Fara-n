using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    public enum PlayerStatus { Winner, Loser }

    public static PlayerStatus MyPlayerStatus { get; set; }

    [SerializeField] private GameObject camCurtain;
    [SerializeField] private GameObject myParticleSystem;

    DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        dataManager = DataManager.dataManager;
    }
    // Update is called once per frame
    public void EndGame()
    {
        switch (MyPlayerStatus)
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
}
