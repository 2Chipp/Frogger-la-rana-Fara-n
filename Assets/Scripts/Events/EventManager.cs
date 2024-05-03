using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    // Singleton
    public static EventManager eventManager;
    private void Awake()
    {
        if (eventManager != null) Destroy(this);
        else eventManager = this;
    }


    // Subscribed classes: GroupData, GamePlayUI, PlayerController, GameFinisher
    public event Action onEndGame;
    public void EndGame()
    {
        onEndGame?.Invoke();
    }

    // Subscribed classes: 
    public event Action onRestart;
    public void Restart()
    {
        onRestart?.Invoke();
    }
}
