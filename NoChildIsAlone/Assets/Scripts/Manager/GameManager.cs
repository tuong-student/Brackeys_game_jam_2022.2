using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UDL.Core;

public class GameManager : AbstractView
{
    #region Components
    #endregion

    #region Stats
    float score;
    float timer;
    #endregion

    public static GameManager InsGameManager;

    public static GameManager Create(Transform parent = null)
    {
        return Instantiate<GameManager>(Resources.Load<GameManager>("Prefabs/Manager/----GameManager----"), parent);
    }

    private void Awake()
    {
        if (InsGameManager == null) InsGameManager = this;
    }

    private void FixedUpdate()
    {
        this.timer -= Time.deltaTime;
    }

    public void AddScore(float score)
    {
        this.score += score;
    }

    public void AddTimer(float time)
    {
        this.timer += time;
    }
}
