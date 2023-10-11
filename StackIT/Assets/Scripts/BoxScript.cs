 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // ... (other code remains the same)

    private float min_X = -2.2f, max_X = 2.2f;
    private int playerScore = 0;
    private int landedBoxCount = 0; // Added to track successfully landed boxes

    private bool canMove;
    private float move_Speed = 2.5f;
    public GameObject[] objectsToDrop;
    private Rigidbody2D myBody;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    private ScoreManager scoreManager;
    private float currentBoxMoveSpeed = 5f; // Added to store the current box's movement speed

    private GameObject lastDroppedItem; // Added to track the last dropped item
    private bool droppedTwoItemsInSuccession; // Added to track if two items were dropped in succession

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

    if (Random.Range(0, 2) > 0)
    {
        move_Speed = 4f;
    }
    

    
    currentBoxMoveSpeed = move_Speed;

    GameplayController.instance.currentBox = this;
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
            GameObject spawnedObject = Instantiate(objectToDrop, transform.position, Quaternion.identity);

            // Set the spawned object's initial movement speed based on the current box's movement speed
            if (spawnedObject.GetComponent<Rigidbody2D>() != null)
            {
                Rigidbody2D objRigidbody = spawnedObject.GetComponent<Rigidbody2D>();
                objRigidbody.velocity = new Vector2(currentBoxMoveSpeed, objRigidbody.velocity.y);

                // Check if two items were dropped in succession
                if (lastDroppedItem != null)
                {
                    droppedTwoItemsInSuccession = true;
                }
                lastDroppedItem = spawnedObject;
            }
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

        // Increment the count of landed boxes.
        landedBoxCount++;

        // Check if the player has successfully landed 10 stacks of boxes
        if (landedBoxCount % 2 == 0)
        {
            // Update the movement here
            UpdateBoxMovement();
        }

        // Reset the droppedTwoItemsInSuccession flag
        droppedTwoItemsInSuccession = false;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();
    }

    void UpdateBoxMovement()
    {
        // Increase the move_Speed by 1f.
        move_Speed += 1f;

        // Reset the count of landed boxes.
        landedBoxCount = 0;
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