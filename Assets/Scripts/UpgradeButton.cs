using Unity.VisualScripting;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    enum Modifier { DAMAGE, BULLET_SPEED, BULLET_SIZE, FIRE_RATE, SIMPLE, TRIPLE, SHOTGUN, GATTLING };

    [SerializeField] private Modifier modType;
    [SerializeField] private float modAmount;
    public void DoUpgrade()
    {
        SceneHandler sceneManager = SceneHandler.Instance;
        Debug.Log(modType);
        Debug.Log(modAmount);
        switch (modType)
        {
            case Modifier.DAMAGE:
                sceneManager.SetDamageMod(modAmount);
                break;
            case Modifier.BULLET_SPEED:
                sceneManager.SetBulletSpeedMod(modAmount);
                break;
            case Modifier.BULLET_SIZE:
                sceneManager.SetBulletSizeMod(modAmount);
                break;
            case Modifier.FIRE_RATE:
                sceneManager.SetFireRateMod(modAmount);
                break;
            case Modifier.SIMPLE:
                sceneManager.SetWeapon(SceneHandler.shotType.SIMPLE);
                break;
            case Modifier.TRIPLE:
                sceneManager.SetWeapon(SceneHandler.shotType.TRIPLE);
                break;
            case Modifier.SHOTGUN:
                sceneManager.SetWeapon(SceneHandler.shotType.SHOTGUN);
                break;
            case Modifier.GATTLING:
                sceneManager.SetWeapon(SceneHandler.shotType.GATTLING);
                break;
        }

        sceneManager.NextLevel();
    }
}
