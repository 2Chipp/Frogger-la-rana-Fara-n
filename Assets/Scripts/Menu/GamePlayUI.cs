using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    

    public GameObject MainMenuPanel;
    public GameObject GamePlayPanel;
    public GameObject WinLosePanel;
    
    public float gameTimeMinutes=1;

    public TextMeshProUGUI minutesText;
    public TextMeshProUGUI secondsText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;

    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI maxPointsText;
    public TextMeshProUGUI comentText;

    GroupData[] groupData;

    PlayerController player;

    float gameTime;
    int minutes;
    int seconds;

    float initialGameTime;


    public static bool playing;

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        player = FindObjectOfType<PlayerController>();
        gameTime = gameTimeMinutes * 60;
        initialGameTime = gameTime;
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTimer();
        SetCoinsText();
        pointsText.text = "puntuación Actual :"+player.currentPoints.ToString();
        maxPointsText.text = "puntuación Maxima :"+player.maxPoints.ToString();
        if (gameTime > 59f)
        {
            groupData = FindObjectsOfType<GroupData>();
        }
        if (player.wF)
        {
            comentText.text = "Felicitaciones, Ayudaste al emperador a recuperar su trono con exito";
        }
        else comentText.text = "Sigue intentando, la practica hace al maestro";
    }

    void CountDownTimer()
    {
        if (playing)
        {
            gameTime -= Time.deltaTime;
            minutes = (int)gameTime / 60;
            seconds = (int)gameTime - (minutes * 60);
            minutesText.text = minutes.ToString("00");
            secondsText.text = seconds.ToString("00");
            livesText.text = "Lives :" + player.lives.ToString();
            if (gameTime < 0)
            {
                player.Lose();
            }
        }

    }

    void SetCoinsText()
    {
        coinsText.text = player.currentCoins.ToString() + "/" + GroupData.totalCoins.ToString();
    }

    public void ResetTime()
    {
        gameTime = initialGameTime;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        MainMenuPanel.SetActive(false);
        WinLosePanel.SetActive(false);
        GamePlayPanel.SetActive(true);
        playing = true;
        for (int i = 0; i < groupData.Length; i++)
        {
            groupData[i].ResetInitialValues();
        }
    }


    public void Continue()
    {
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
