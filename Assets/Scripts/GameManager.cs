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
    private float fireRateMod = 1f;
    private float bulletSpeedMod = 1f;
    private float bulletSizeMod = 1f;
    private float damageMod = 1f;

    private shotType equipped;
    public enum shotType { SIMPLE, TRIPLE, SHOTGUN };

    void Start()
    {
        hudPanel.ResetHUD();
    }
    private void Update()
    {
       isPlayerInvincible = updateInvincibility();
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

    public void loseLive(int dmg) { // calculates the new life total then calls the private function for updating the lives
        int newTotal = Mathf.Max(lives - dmg, 0); // no negative values here!
        if (!isPlayerInvincible) //only updates lives if the player is not invincible
        {   UpdateLives(newTotal);  // updates the lives.
            isPlayerInvincible = !isPlayerInvincible; // makes the player invincible (to block any more damage this frame);
            invincibilityTimer = InvincibilityDuration;// makes the player invincible for the next (InvincibilityDuration) seconds
            CameraShake.Instance.TriggerShake();
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
