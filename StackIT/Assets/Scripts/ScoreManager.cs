using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
<<<<<<< Updated upstream
    public Text highestScoreText;
    public Text unlockText;

    private int playerScore = 0;
    private int highestScore = 1;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;

    void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreUI();
    }
=======
    public Text unlockText; // Reference to the unlock message text element
    private int playerScore = 0;
    private int stackedItems = 0; // Track the number of stacked items
    private bool isUnlockMessageShowing = false;

    public ShopManager shopManager; // Reference to the ShopManager in the same scene
>>>>>>> Stashed changes

    public void IncreaseScore()
    {
        playerScore++;

        if (playerScore > highestScore)
        {
            highestScore = playerScore;
            SaveHighestScore();
            UpdateHighestScoreUI();
        }

        // Check if an item is stacked
        if (playerScore == 3 && !isUnlockMessageShowing)
        {
            StartCoroutine(ShowUnlockMessage());
        }

        // Check if an item is stacked
        if (playerScore == 4)
        {
            stackedItems++;
        }

        playerScore += stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();
    }

<<<<<<< Updated upstream
    private void LoadHighestScore()
    {
        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
    }

    private void SaveHighestScore()
    {
        PlayerPrefs.SetInt("HighestScore", highestScore);
        PlayerPrefs.Save();
    }

    private void UpdateHighestScoreUI()
    {
        highestScoreText.text = "Highest Score: " + highestScore.ToString();
=======
        // Check if the player has successfully stacked 5 items
        if (playerScore == 3 && !isUnlockMessageShowing)
        {
            StartCoroutine(ShowUnlockMessage());

            // Unlock the 2x item in the shop
            if (shopManager != null)
            {
                shopManager.Unlock2xItem();
            }
        }
>>>>>>> Stashed changes
    }

    private IEnumerator ShowUnlockMessage()
    {
        isUnlockMessageShowing = true;
        unlockText.text = "2X Activated";
        unlockText.gameObject.SetActive(true);
<<<<<<< Updated upstream

        yield return new WaitForSeconds(1f);
=======
        yield return new WaitForSeconds(5f); // Display the message for 5 seconds
>>>>>>> Stashed changes
        unlockText.gameObject.SetActive(false);
        isUnlockMessageShowing = false;
    }
}

