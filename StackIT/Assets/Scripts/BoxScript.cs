using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private float min_X = -2.2f, max_X = 2.2f;
    private int playerScore = 0;

    private bool canMove;
    private float move_Speed = 2f;
    public GameObject[] objectsToDrop;
    private Rigidbody2D myBody;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    private ScoreManager scoreManager;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f;

        // Find the ScoreManager script in the scene.
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager script not found in the scene.");
        }
    }

    void Start()
    {
        canMove = true;

        if (playerScore % 2 == 0)
        {
            move_Speed *= 1.5f;
        }

        GameplayController.instance.currentBox = this;
    }

    void UpdateBoxMovement()
    {
        // Calculate the speed increment based on playerScore
        float speedIncrement = Mathf.Pow(4f, playerScore / 2);

        // Adjust move_Speed by the calculated increment
        move_Speed *= speedIncrement;
    }

    void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;

            temp.x += move_Speed * Time.deltaTime;

            if (temp.x > max_X)
            {
                move_Speed *= -1f;
            }
            else if (temp.x < min_X)
            {
                move_Speed *= -1f;
            }
            transform.position = temp;
        }
    }

    public void DropRandomObject()
    {
        canMove = false;
        myBody.gravityScale = Random.Range(2, 4);

        // Check if the objectsToDrop array is not empty
        if (objectsToDrop.Length > 0)
        {
            // Choose a random object from the objectsToDrop array.
            int randomIndex = Random.Range(0, objectsToDrop.Length);
            GameObject objectToDrop = objectsToDrop[randomIndex];

            // Instantiate the chosen object at the current position of the box.
            Instantiate(objectToDrop, transform.position, Quaternion.identity);
        }
    }

    void Landed()
    {
        if (gameOver)
            return;

        ignoreCollision = true;
        ignoreTrigger = true;
        playerScore++;

        // Update the score using the ScoreManager.
        scoreManager.IncreaseScore();
        GameOverUIManager.Instance.IncreaseScore();

        // Check if the player has successfully landed 10 stacks of boxes
        if (playerScore % 2 == 0)
        {
            // Update the movement here
            UpdateBoxMovement();
        }

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();
    }


    void RestartGame()
    {
        GameplayController.instance.RestartGame();
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.tag == "Platform")
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }

        if (target.gameObject.tag == "Box")
        {
            Invoke("Landed", 1f);
            ignoreCollision = true;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;

        if (target.tag == "GameOver")
        {
            // Set game over flag and disable box movement.
            gameOver = true;
            canMove = false;
            ignoreTrigger = true;

            // Notify the GameOverUIManager that the game is over and pass the score.
            GameOverUIManager.Instance.ShowGameOverUI(playerScore);
        }
    }
}
