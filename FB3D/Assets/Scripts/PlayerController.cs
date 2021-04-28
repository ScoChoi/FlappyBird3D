using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI countEnd; 
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool canJump = true;
    public float _timer = 0f;
    public float _duration = 0.4f;

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

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Wall")
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
        
        if (canJump == true) {
            Vector3 jump = new Vector3(0.0f, 45.0f, -2f);
            Vector3 jumpAmount = jump * Time.fixedDeltaTime;
            rb.AddForce(jumpAmount * speed, ForceMode.Impulse);
            canJump = false;
        }
    }

    void SetCountText() 
    {
        if (lost == 1)
        {
            countEnd.text = "Final Score: " +   count.ToString();
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
        if (canJump == false) {
            _timer += Time.deltaTime;
            if(_timer >= _duration) {
                _timer = 0f;
                canJump = true;
            }
        }
        Vector3 movement = new Vector3(movementX, 0.0f, 0.1f);
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
