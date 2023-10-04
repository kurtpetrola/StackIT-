using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    private int playerScore = 0;

    
    public void IncreaseScore()
    {
        playerScore++;
        scoreText.text = "Score: " + playerScore.ToString();
    }
}
