using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    EventManager eventManager;

    [Header("Time Data")]

    [SerializeField] private float gameTimeInMinutes = 2;
    private float InitialGameTime { get; set; }
    public float GameTime { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }

    [Header("Player Data")]

    [SerializeField] private int playerLives;

    public bool PlayingState { get; set; }
    public int Lives { get; set; }
    public int Coins { get; set; }
    public int Points { get; set; }
    public int MaxPoints { get; set; }
    public GameFinisher.PlayerStatus MyPlayerStatus { get; set; }
    public Vector3 PlayerPosition { get; set; }

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
        eventManager = EventManager.eventManager;
        GameTime = gameTimeInMinutes * 60;
        InitialGameTime = GameTime;
        Lives = playerLives;
        Time.timeScale = 0;
        MyPlayerStatus = GameFinisher.PlayerStatus.Undefined;

        eventManager.onEndGame += EndGame;
        eventManager.onRestartGame += RestartGame;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTimer();
        SetMaxPoints();
        DataCheck();
    }

    void CountDownTimer()
    {
        if (PlayingState)
        {
            GameTime -= Time.deltaTime;
            Minutes = (int) GameTime / 60;
            Seconds = (int) GameTime - (Minutes * 60);
        }

    }

    void DataCheck()
    {
        if(GameTime <= 0 || Lives <= 0)
        {
            eventManager.EndGame();
        }
    }

    void SetMaxPoints()
    {
        MaxPoints = Points > MaxPoints ? Points : MaxPoints;
    }

    public void RestartGame()
    {
        GameTime = InitialGameTime;
        Time.timeScale = 1;
        Coins = 0;
        Points = 0;
    }

    public void EndGame()
    {
        PlayingState = false;
    }

    private void OnDestroy()
    {
        eventManager.onEndGame -= EndGame;
        eventManager.onRestartGame -= RestartGame;
    }
}
