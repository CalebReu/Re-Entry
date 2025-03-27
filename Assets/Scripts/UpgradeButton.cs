using Unity.VisualScripting;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    enum Modifier { DAMAGE, BULLET_SPEED, BULLET_SIZE, FIRE_RATE, SIMPLE, TRIPLE, SHOTGUN };

    [SerializeField] private Modifier modType;
    [SerializeField] private float modAmount;
    public void DoUpgrade()
    {
        GameManager gameManager = GameManager.Instance;
        Debug.Log(modType);
        Debug.Log(modAmount);
        switch (modType)
        {
            case Modifier.DAMAGE:
                gameManager.SetDamageMod(modAmount);
                break;
            case Modifier.BULLET_SPEED:
                gameManager.SetBulletSpeedMod(modAmount);
                break;
            case Modifier.BULLET_SIZE:
                gameManager.SetBulletSizeMod(modAmount);
                break;
            case Modifier.FIRE_RATE:
                gameManager.SetFireRateMod(modAmount);
                break;
            case Modifier.SIMPLE:
                gameManager.SetWeapon(GameManager.shotType.SIMPLE);
                break;
            case Modifier.TRIPLE:
                gameManager.SetWeapon(GameManager.shotType.TRIPLE);
                break;
            case Modifier.SHOTGUN:
                gameManager.SetWeapon(GameManager.shotType.SHOTGUN);
                break;
        }

        // After upgrade is performed, moves to next level.
        // gameManager.NextLevel()
    }
}
