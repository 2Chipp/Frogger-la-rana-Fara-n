using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    public static GamePlayUI gamePlayUI;
    DataManager dataManager;
    EventManager eventManager;

    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject gamePlayPanel;
    [SerializeField] GameObject winLosePanel;
    
    [SerializeField] TextMeshProUGUI minutesText;
    [SerializeField] TextMeshProUGUI secondsText;
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI livesText;
    
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI maxPointsText;
    [SerializeField] TextMeshProUGUI comentText;

    GroupData[] groupData;

    private void Awake()
    {
        if (gamePlayUI == null)
        {
            gamePlayUI = this;
        }
        else Destroy(this);
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
        groupData = FindObjectsOfType<GroupData>();
        Time.timeScale = 0;

        eventManager.onEndGame += EndGame;
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

        if (dataManager.MyPlayerStatus == GameFinisher.PlayerStatus.Winner)
        {
            comentText.text = "Felicitaciones, Ayudaste al emperador a recuperar su trono con exito";
        }
        else comentText.text = "Sigue intentando, la practica hace al maestro";
    }


    public void RestartGame()
    {
        mainMenuPanel.SetActive(false);
        winLosePanel.SetActive(false);
        gamePlayPanel.SetActive(true);
        dataManager.PlayingState = true;
        for (int i = 0; i < groupData.Length; i++)
        {
            groupData[i].ResetInitialValues();
        }
    }
    public void EndGame()
    {
        winLosePanel.SetActive(true);        
    }

    public void Continue()
    {
        Time.timeScale = 1;
        dataManager.PlayingState = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        eventManager.onEndGame -= EndGame;
    }

}
