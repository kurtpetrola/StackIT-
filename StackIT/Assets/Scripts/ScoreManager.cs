using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Add this using directive

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text unlockText; // Reference to the unlock message text element
    private int playerScore = 0;
    private int stackedItems = 0; // Track the number of stacked items

    private bool isUnlockMessageShowing = false;

    public void IncreaseScore()
    {
        playerScore++;

        // Check if an item is stacked
        if (playerScore == 4)
        {
            stackedItems++;
        }

        playerScore += 1 * stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();

        // Check if the player has successfully stacked 5 items
        if (playerScore == 3 && !isUnlockMessageShowing)
        {
            StartCoroutine(ShowUnlockMessage());
        }
    }

    private IEnumerator ShowUnlockMessage()
    {
        isUnlockMessageShowing = true;
        unlockText.text = "You Unlock The 2x item";
        unlockText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f); // Display the message for 5 seconds
        unlockText.gameObject.SetActive(false);
        isUnlockMessageShowing = false;
    }
}
