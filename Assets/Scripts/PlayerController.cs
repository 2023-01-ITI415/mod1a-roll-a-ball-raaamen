using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{

    //UI Stuff
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;

    private int score;

    private Rigidbody rb;
    private float movementForceX;
    private float movementForceY;
    public float movementSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score=0;
        UpdateScoreText();
    }

    public void OnMove(InputValue value){
        Debug.Log(value);
        Debug.Log(value.Get<Vector2>());
        Vector2 movement = value.Get<Vector2>();
        movementForceX = movement.x;
        movementForceY = movement.y;
    }



    private void FixedUpdate() {
        rb.AddForce(new Vector3(movementForceX * movementSpeed, 0, movementForceY * movementSpeed));
        
        
        //The new input system refuses to work on my project so i did it the old way
        //i troubleshooted this for like 4 days and could not get it to work
        //i have 0 idea what i'm doing wrong with it, made sure everything looked good in the inspector
        //and in my code
        //if you find what i did wrong PLEASE let me know, it drove me a bit crazy
        if(Input.GetKey(KeyCode.LeftArrow)){
            rb.AddForce(new Vector3(-1*movementSpeed, 0, 0));
        }
        else if(Input.GetKey(KeyCode.RightArrow)){
            rb.AddForce(new Vector3(1*movementSpeed, 0, 0));
        }
        else if(Input.GetKey(KeyCode.UpArrow)){
            rb.AddForce(new Vector3(0, 0, 1*movementSpeed));
        }
        else if(Input.GetKey(KeyCode.DownArrow)){
            rb.AddForce(new Vector3(0, 0, -1*movementSpeed));
        }
    }

    void UpdateScoreText(){
        scoreText.text = "Current Score: "+score;
        if (score == 11){
            winText.gameObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "PickUp")
        {
            score++;
            UpdateScoreText();
            Destroy(other.gameObject);
        }
    }
}
