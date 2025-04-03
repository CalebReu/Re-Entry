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
    [SerializeField] private float InvincibilityDuration; // the amount of time the player will be invincible for after taking damage;
    private int score = 0;
    private int lives = 3;
    private float invincibilityTimer; // stores the time (in seconds) that the player is invincible, to avoid taking damage really fast from multiple things at the same time.
    public bool isPlayerInvincible; //stores if the player is currently invincible or not.
      // Increasable stats (1 means no change in stat):

    [SerializeField] private int currNumEnemies;

    // start gets called when new level loaded
    void Start()
    {
        score = SceneHandler.Instance.getStats()[0]; // retreives the score saved in the scenehandler
        lives = SceneHandler.Instance.getStats()[1] ==0? MAX_LIVES: SceneHandler.Instance.getStats()[1];// retreives the lives saved in the scenehandler 
        hudPanel.ResetHUD(); 
        currNumEnemies = countNumEnemiesInScene();
    }
    private void Update()
    {
       isPlayerInvincible = updateInvincibility();
        SceneHandler.Instance.saveStats(score, lives); // saves the score and lives every frame.
        PlayerController.Instance.UpdateRPM();
    }

    private bool updateInvincibility(){ // updates the timer on invincibility and returns if it is still active or not.
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime; // lower the invincibility timer;
            return true;
        }
        else
        {
            invincibilityTimer = 0;
            return false;
        }
    }

    public void UpdateScore()
    {
        score++;
        hudPanel.UpdateScore(score);
    }
    public int getScore() {
        return score;
    }
    public int getlives()
    {
        return lives;
    }
    // SCENE TRANSITION METHODS
    public void UpdateEnemyCount()
    {
        currNumEnemies--;
       // Debug.Log("Enemies remaining: "+currNumEnemies);
        if (isLevelCompleted())
        {
            SceneHandler.Instance.checkVictoryScreen();
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


    public void loseLive(float dmg) { // calculates the new life total then calls the private function for updating the lives
        int newTotal = (int)Mathf.Max(lives - dmg, 0); // no negative values here!
        if (!isPlayerInvincible) //only updates lives if the player is not invincible
        {   UpdateLives(newTotal);  // updates the lives.
            isPlayerInvincible = !isPlayerInvincible; // makes the player invincible (to block any more damage this frame);
            invincibilityTimer = InvincibilityDuration;// makes the player invincible for the next (InvincibilityDuration) seconds
            CameraShake.Instance.TriggerShake();
            AudioManager.instance.PlaySound(AudioManager.instance.explosionClip);
            Flash_Sprite.Instance.flashForDuration(InvincibilityDuration);
        }
    }
    private void UpdateLives(int lives) {
        this.lives = lives;
        hudPanel.UpdateLives(lives);
        if (lives == 0) //checks for GameOver whenever Lives are updated, might need to put this in the Update Function to make it more reliable.
        {
            GameOver();
            Debug.Log("GAME OVER");
        }
    }
    private void GameOver() {
        SceneHandler.Instance.GameOver();
    }
}
