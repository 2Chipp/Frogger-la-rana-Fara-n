using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    public enum PlayerStatus { Undefined, Winner, Loser }

    [SerializeField] private GameObject camCurtain;
    [SerializeField] private ParticleSystem myParticleSystem;
    [SerializeField] private float particleSystemOffset;

    private DataManager dataManager;
    private EventManager eventManager;

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
        eventManager.onRestartGame += RestartGame;
    }
    // Update is called once per frame
    public void EndGame()
    {
        camCurtain.SetActive(true);
        if(dataManager.MyPlayerStatus == PlayerStatus.Winner) PlayParticleSystem(dataManager.PlayerPosition);
    }

    public void PlayParticleSystem(Vector3 playerPosition)
    {
        myParticleSystem.transform.position = playerPosition + Vector3.up * particleSystemOffset;
        myParticleSystem.Play();
    }

    public void StopParticleSystem()
    {
        myParticleSystem.Stop();
    }

    public void RestartGame()
    {
        StopParticleSystem();
        camCurtain.SetActive(false);
    }


    private void OnDestroy()
    {
        eventManager.onEndGame -= EndGame;
        eventManager.onRestartGame -= RestartGame;
    }
}
