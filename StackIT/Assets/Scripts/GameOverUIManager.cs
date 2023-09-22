using UnityEngine;
using UnityEngine.UI;

public class GameOverUIManager : MonoBehaviour
{
    public static GameOverUIManager Instance;
public Text pointsText;

    public GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowGameOverUI()
    {
        // Display the game over screen elements.
        gameOverPanel.SetActive(true);

        // Set the points text and handle button clicks.
        // You can do this by accessing the relevant Text and Button components on your UI elements.
    }
    public void RestartGame()
{
    // Restart the game by reloading the current scene.
    UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
}
public void GoToMainMap()
{
    // Load the main map scene.
    UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
}

    public void ShowGameOverUI(int score)
{
    // Display the game over screen elements.
    gameOverPanel.SetActive(true);

    // Set the points text to display the player's score.
    pointsText.text = "Points: " + score.ToString();

    // Handle button clicks as described in previous responses.
}


}
