using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI countEnd; 
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody rb;
    private int count;
    private int lost;
    private float movementX;
    private float movementY;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lost = 0;  

        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false); 

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnCollisionEnter()
    {
        if (GameObject.Find("Walls"))
        {
            lost = 1;
            SetCountText();
        }
    }

    void PauseGame() //pause game to end game
    {
        Time.timeScale = 0;
    }

    void OnJump()
    {
        Vector3 jump = new Vector3(0.0f, 20.0f, 0.0f);
        Vector3 jumpAmount = jump * Time.fixedDeltaTime;
        rb.AddForce(jumpAmount * speed, ForceMode.Impulse);
    }

    void SetCountText() 
    {
        if (lost == 1)
        {
            countEnd.text = "Final Score: " + count.ToString();
            countText.text = "";
            loseTextObject.SetActive(true);
            PauseGame();

        }
        else
        {


            countText.text = "Count: " + count.ToString();
            if (count >= 10)
            {
                winTextObject.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, 0.4f);
        rb.AddForce(movement * speed);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
}
