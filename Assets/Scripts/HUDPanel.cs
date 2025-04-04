using TMPro;
using UnityEngine;

public class HUDPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private TextMeshProUGUI txtLives;

    public void ResetHUD()
    {
        txtScore.SetText("Score: " + GameManager.Instance.getScore());
        txtLives.SetText("Lives: " + GameManager.Instance.getlives());
    }

    // TODO: decide if it's necessary to specify the increment (int param)
    // because honestly we're usually just incrementing/decrememnting by 1
    // Maybe powerups will require a different number tho, so leaving like this for now
    public void UpdateScore(int score)
    {
        txtScore.SetText("Score: " + score);
    }

    public void UpdateLives(int lives)
    {
        txtLives.SetText("Lives: " + lives);
    }
}
