using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private float min_X = -2.2f, max_X = 2.2f;

    private bool canMove;
    private float move_Speed = 2f;
    public GameObject[] objectsToDrop;
    private Rigidbody2D myBody;
   
    private bool gameOver;
    private bool ignoreColliosn;
    private bool ignoreTrigger;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f;

    }

    void Start()
    {
        canMove = true;

        if(UnityEngine.Random.Range(0, 2) > 0)
        {
            move_Speed *= -1f;
        }

        GameplayController.instance.currentBox = this;
        
    }

   
    void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if(canMove)
        {
            Vector3 temp = transform.position;

            temp.x += move_Speed * Time.deltaTime;

            if(temp.x > max_X)
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
        myBody.gravityScale = UnityEngine.Random.Range(2, 4);

        // Check if the objectsToDrop array is not empty
        if (objectsToDrop.Length > 0)
        {
            // Choose a random object from the objectsToDrop array.
            int randomIndex = UnityEngine.Random.Range(0, objectsToDrop.Length);
            GameObject objectToDrop = objectsToDrop[randomIndex];

            // Instantiate the chosen object at the current position of the box.
            Instantiate(objectToDrop, transform.position, Quaternion.identity);
        }
        
    }



    void Landed()
    {
        if (gameOver)
            return;

            ignoreColliosn = true;
        ignoreTrigger = true;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();
    }
    void RestartGame()
    {
        GameplayController.instance.RestartGame();
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreColliosn)
            return;

        if (target.gameObject.tag == "Platform")

        {
            Invoke("Landed", 1f);
            ignoreColliosn = true;
        }

        if (target.gameObject.tag == "Box")

        {
            Invoke("Landed", 1f);
            ignoreColliosn = true;
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;

        if (target.tag == "GameOver")
        {
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;

            Invoke("RestartGame", 2f);
        }
    }
}
