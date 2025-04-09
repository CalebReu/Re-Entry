using UnityEngine;

public class transitionHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public void transitionOut() {
        anim.SetTrigger("Finished");
    }
    public void NextScene() {
        SceneHandler.Instance.checkVictoryScreen();
    }
   
}
