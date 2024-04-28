using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    DataManager dataManager;

    public GameObject MainMenuPanel;
    public GameObject GamePlayPanel;
    public GameObject WinLosePanel;

    public TextMeshProUGUI minutesText;
    public TextMeshProUGUI secondsText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;

    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI maxPointsText;
    public TextMeshProUGUI comentText;

    GroupData[] groupData;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        dataManager = DataManager.dataManager;
        groupData = FindObjectsOfType<GroupData>();
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update()
    {
        SetText();
    }

    void SetText()
    {
        minutesText.text = dataManager.Minutes.ToString("00");
        secondsText.text = dataManager.Seconds.ToString("00");
        livesText.text = "Lives :" + dataManager.Lives.ToString();

        coinsText.text = dataManager.Coins.ToString() + "/" + GroupData.totalCoins.ToString();
        pointsText.text = "puntuación Actual :" + dataManager.Points.ToString();
        maxPointsText.text = "puntuación Maxima :" + dataManager.MaxPoints.ToString();

        //if (player.wF)
        //{
        //    comentText.text = "Felicitaciones, Ayudaste al emperador a recuperar su trono con exito";
        //}
        //else comentText.text = "Sigue intentando, la practica hace al maestro";
    }


    public void Restart()
    {
        MainMenuPanel.SetActive(false);
        WinLosePanel.SetActive(false);
        GamePlayPanel.SetActive(true);
        dataManager.PlayingState = true;
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
