using UnityEngine;
using TMPro;
using System.Collections;
public class display_Final_Score : MonoBehaviour
{
    private int score = SceneHandler.Instance.getStats()[0];
    private int displayedScore = 0;
    [SerializeField] private TextMeshProUGUI txtScore;
    private int finalscore;
    bool bonusAdded = false;
    private void Start()
    {
        calculateScore();
    }
    public void calculateScore() {
        finalscore =100 * score ;
    }
    public void addLivesBonus() {
        StopAllCoroutines();
        if (bonusAdded) {
            
            return;
        }
        int remainingLives = SceneHandler.Instance.getStats()[1];
        finalscore *= remainingLives;
        string bonusString = "x" + remainingLives + " lives remaining";
        StartCoroutine(tallyScore(finalscore, bonusString));
        bonusAdded = true;
    }
    public void startScoreCount() {
        StartCoroutine(tallyScore(finalscore,""));
    }
    private IEnumerator tallyScore(int targetScore, string bonus)
    {
        float speed = 1/((targetScore - displayedScore)+1);
        while (displayedScore <= targetScore)
        {
            if (displayedScore <= targetScore)
            {
                displayedScore++;
                txtScore.SetText("Score: " + displayedScore+" "+bonus);
            }
            yield return new WaitForSeconds(speed*5);
        }
        txtScore.SetText("Score: " + targetScore);
        StopAllCoroutines();
        if (!bonusAdded)
        {
            addLivesBonus();
        }
        else {
            txtScore.SetText("Final Score: " + targetScore);
        }
        
        
    }

}
