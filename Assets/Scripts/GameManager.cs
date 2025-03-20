using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    // constants
    public const int MAX_LIVES = 3;

    // vars
    [SerializeField] private HUDPanel hudPanel;
    private int score = 0;
    private int lives = 3;

    void Start()
    {
        hudPanel.ResetHUD();
    }

    public void UpdateScore()
    {
        score++;
        hudPanel.UpdateScore(score);
    }
}
