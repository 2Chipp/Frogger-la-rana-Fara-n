using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    [Header("Time Data")]

    [SerializeField] private float gameTimeInMinutes = 2;
    private float InitialGameTime { get; set; }
    public float GameTime { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }

    [Header("Player Data")]

    [SerializeField] private int playerLives;

    [SerializeField] public bool PlayingState { get; set; }
    public int Lives { get; set; }
    public int Coins { get; set; }
    public int Points { get; set; }
    public int MaxPoints { get; set; }
    public GameFinisher.PlayerStatus MyPlayerStatus { get; set; }

    [Header("Singleton")]

    public static DataManager dataManager;

    private void Awake()
    {
        if (dataManager == null)
        {
            dataManager = this;
        }
        else Destroy(this);
    }
    void Start()
    {
        Init();
    }

    private void Init()
    {
        GameTime = gameTimeInMinutes * 60;
        InitialGameTime = GameTime;
        Time.timeScale = 0;
        MyPlayerStatus = GameFinisher.PlayerStatus.Undefined;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTimer();
        SetMaxPoints();
    }

    void CountDownTimer()
    {
        if (PlayingState)
        {
            GameTime -= Time.deltaTime;
            Minutes = (int) GameTime / 60;
            Seconds = (int) GameTime - (Minutes * 60);

            if (GameTime <= 0)
            {
                //player.Lose();
            }
        }

    }

    void SetMaxPoints()
    {
        MaxPoints = Points > MaxPoints ? Points : MaxPoints;
    }

    public void ResetTime()
    {
        GameTime = InitialGameTime;
        Time.timeScale = 1;
    }
}
