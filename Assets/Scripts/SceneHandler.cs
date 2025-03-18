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
- TitleScreen -> Play/Exit
- Level1
- Level2
- Level3
- UpgradeScreen
- GameOver -> Try Again/Exit
- WinScreen? -> Play Again/Exit
- Pause? 
*/
public class SceneHandler : SingletonMonoBehavior<SceneHandler>
{
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
        {LEVEL1, "Level1"},
        {LEVEL2, "Level2"},
        {LEVEL3, "Level3"},
        {UPGRADE_SCREEN, "UpgradeScreen"},
        {GAME_OVER, "GameOver"},
        {WIN_SCREEN, "WinScreen"}
    };

    // Stuff to deal w scene transitions -----------------------------------

    [SerializeField] private int currScene;

    protected override void Awake()
    {
        base.Awake(); // Singleton behavior
        currScene = TITLE_SCREEN; // initialization
    }
    public void StartGame()
    {
        currScene = LEVEL1;
        //loadScene(currScene);
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
            // but first, get upgrade
            //loadScene(UPGRADE_SCREEN);
            // I'm doing it this way so that when we leave upgrade screen, currScene still represents the current level to be loaded
            // so all that has to be done when we come back is loadScene(currScene)
        }
        else
        {
            // end of the game
            currScene = WIN_SCREEN;
            //loadScene(currScene);
        }
    }

    public void GameOver()
    {
        currScene = GAME_OVER;
        //loadScene(currScene);
    }
    // helper methods
    private void loadScene(int index)
    {
        SceneManager.LoadScene(scenes[index]);
    }


}


