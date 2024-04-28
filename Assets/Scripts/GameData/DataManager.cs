using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    [Header("Time Data")]

    [SerializeField] private float gameTimeInMinutes;
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

    [Header("Singleton")]

    public static DataManager dataManager;

    private void Awake()
    {
        if (!dataManager)
        {
            dataManager = this;
        }
    }
    void Start()
    {
        
    }

    private void Init()
    {
        GameTime = gameTimeInMinutes * 60;
        InitialGameTime = GameTime;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTimer();
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

    public void ResetTime()
    {
        GameTime = InitialGameTime;
        Time.timeScale = 1;
    }
}
