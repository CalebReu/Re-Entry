using UnityEngine;

public class button_scene_change : MonoBehaviour
{
    //this code is for UI buttons that switch scenes when clicked.
    public int scene; // set this in the editor to load the scene you want.
    public void changeScene() {
        SceneHandler.Instance.loadScene(scene);
    }
}
