using UnityEngine;

public class button_scene_change : MonoBehaviour
{
    //this code is for UI buttons that switch scenes when clicked.
    public void mainMenu() // best to call main menu directly
    {
        SceneHandler.Instance.MainMenu();
    }
}
