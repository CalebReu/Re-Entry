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

    // Increasable stats (1 means no change in stat):
    private float fireRateMod = 1f;
    private float bulletSpeedMod = 1f;
    private float bulletSizeMod = 1f;
    private float damageMod = 1f;

    private shotType equipped;
    public enum shotType { SIMPLE, TRIPLE, SHOTGUN };
    
    // start gets called when new level loaded
    void Start()
    {
        hudPanel.ResetHUD(); // note this resets score/lives @ start of every level, do we want this to happen???
        currNumEnemies = countNumEnemiesInScene();
    }

    public void UpdateScore()
    {
        score++;
        hudPanel.UpdateScore(score);
    }

    // SCENE TRANSITION METHODS
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

    private int countNumEnemiesInScene()
    {
        List<GameObject> enemies = new List<GameObject>();
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        return enemies.Count;
    }

    // UPGRADE METHODS
    public void SetWeapon(shotType newWeapon)
    {
        equipped = newWeapon;
    }

    public void SetFireRateMod(float newMod)
    {
        fireRateMod += newMod;
    }

    public void SetBulletSizeMod(float newMod)
    {
        bulletSizeMod += newMod;
    }

    public void SetBulletSpeedMod(float newMod)
    {
        bulletSpeedMod += newMod;
    }

    public void SetDamageMod(float newMod)
    {
        damageMod += newMod;
    }

    // OPTIONAL TODO: Upgrade receive animation for juice :)
}
