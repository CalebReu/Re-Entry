using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
# endif

/*
Responsible for:
- Keeping track of current scene (index and string)
- Updating current scene when switching scenes
- Loading the correct scene

Scenes we need (TODO):
- TitleScreen -> Play/Exit DONE
- Level1 DONE
- Level2 DONE
- Level3
- UpgradeScreen
- GameOver -> Try Again/Exit
- WinScreen? -> Play Again/Exit
- Pause? 
*/
public class SceneHandler : SingletonMonoBehavior<SceneHandler>
{
    // Player stat varibles -------------------------------------------
    // Increasable stats (1 means no change in stat):
    public float fireRateMod = 1f;
    public float bulletSpeedMod = 1f;
    public float bulletSizeMod = 1f;
    public float damageMod = 1f;
    public enum shotType { SIMPLE, TRIPLE, SHOTGUN };
    public shotType equipped;


    // Stuff to store/organize scene info -----------------------------------
    private const int
        TITLE_SCREEN = 0,
        LEVEL1 = 1,
        LEVEL2 = 2,
        LEVEL3 = 3,
        UPGRADE_SCREEN = 4,
        GAME_OVER = 5,
        WIN_SCREEN = 6;

    private Dictionary<int, string> scenes = new Dictionary<int, string>
    {
        {TITLE_SCREEN, "TitleScreen"},
        {LEVEL1, "Level_1"},
        {LEVEL2, "Level_2"},
        {LEVEL3, "Level_3"},
        {UPGRADE_SCREEN, "UpgradeScreen"},
        {GAME_OVER, "GameOver"},
        {WIN_SCREEN, "WinScreen"}
    };

    // Stuff to deal w scene transitions -----------------------------------

    [SerializeField] private int currScene;

    protected override void Awake()
    {
        base.Awake(); // Singleton behavior
        currScene = TITLE_SCREEN; // initialization -------
        fireRateMod = 1f;
        damageMod = 1;
        bulletSizeMod = 0.25f;
        bulletSpeedMod = 1f;
        equipped = shotType.SIMPLE;
    }
    public void StartGame()
    {
        currScene = LEVEL1;
        loadScene(currScene);
    }
    public void MainMenu()
    {
        currScene = TITLE_SCREEN;
        loadScene(currScene);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void NextLevel()
    {
        if (currScene < LEVEL3)
        {
            // advance to next level
            currScene++;
            loadScene(currScene);
            // but first, get upgrade
            //loadScene(UPGRADE_SCREEN);
            // I'm doing it this way so that when we leave upgrade screen, currScene still represents the current level to be loaded
            // so all that has to be done when we come back is loadScene(currScene)
        }
        else
        {
            // end of the game
            currScene = WIN_SCREEN;
            loadScene(currScene);
        }
    }
    public int getScene()
    {
        return currScene; // returns the current scene 
    }

    public void GameOver()
    {
        currScene = GAME_OVER;
        loadScene(currScene);
    }

    public void UpgradeScreen()
    {
        loadScene(UPGRADE_SCREEN);
        // Does not update currScene so that when NextLevel() is called it won't break.
    }

    public void checkVictoryScreen()
    {
        if (currScene < LEVEL3)
        {
            UpgradeScreen();
        }
        else
        {
            NextLevel();
        }
    }

    // UPGRADE METHODS --------------------------------------------------
    public void SetWeapon(shotType newWeapon)
    {
        equipped = newWeapon;
    }
    public void SetFireRateMod(float newMod)
    {
        fireRateMod += newMod;
    }

    // helper methods -------------------------------------------------
    public void loadScene(int index)
    {
        SceneManager.LoadScene(scenes[index]);
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
}


