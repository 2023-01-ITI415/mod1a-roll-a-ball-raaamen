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

    void OnMove(InputValue value){
        Debug.Log(value);
        Debug.Log(value.Get<Vector2>());
        Vector2 movement = value.Get<Vector2>();
        movementForceX = movement.x;
        movementForceY = movement.y;
    }


    private void FixedUpdate() {
        rb.AddForce(new Vector3(movementForceX * movementSpeed, 0, movementForceY * movementSpeed));
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
