using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    // constants
    public const int MAX_LIVES = 3;

    // vars
    [SerializeField] private HUDPanel hudPanel;
    private int score = 0;
    private int lives = 3;

    [SerializeField] private int currNumEnemies;

    // start gets called when new level loaded
    void Start()
    {
        hudPanel.ResetHUD(); // note this resets score/lives @ start of every level, do we want this to happen???
        currNumEnemies = CountNumEnemiesInScene();
    }

    public void UpdateScore()
    {
        score++;
        hudPanel.UpdateScore(score);
    }

    public void UpdateEnemyCount()
    {
        currNumEnemies--;

        if (isLevelCompleted())
        {
            SceneHandler.Instance.NextLevel();
        }
    }

    private bool isLevelCompleted()
    {
        if (currNumEnemies == 0)
        {
            return true;
        }
        return false;
    }

    private int CountNumEnemiesInScene()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        return enemies.Count;
    }
}
