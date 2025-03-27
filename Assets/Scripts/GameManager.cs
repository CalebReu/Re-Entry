using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    // constants
    public const int MAX_LIVES = 3;

    // vars
    [SerializeField] private HUDPanel hudPanel;
    private int score = 0;
    private int lives = 3;

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

    public void UpdateScore()
    {
        score++;
        hudPanel.UpdateScore(score);
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
