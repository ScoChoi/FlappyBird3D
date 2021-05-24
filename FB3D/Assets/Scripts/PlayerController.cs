using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI countEnd;
    public GameObject BeginText; // working on
    public GameObject ControlsText;

    public GameObject winTextObject;
    public GameObject loseTextObject;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool canJump = true;
    public float _timer = 0f;
    public float _duration = 0.05f;
    public float maxSpeed = 10000.0f;

    private Rigidbody rb;
    private int start;
    private int count;
    private int lost;
    private float movementX;
    private float movementY;
    private float lastz;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        lost = 0;
        start = 0;

        //SetCountText();
        BeginText.SetActive(true);
        ControlsText.SetActive(true);
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

        if(start == 0)
        {
            StartGame();
        }

        if (canJump == true) {
            Vector3 jump = new Vector3(0.0f, 55.0f, -2f);
            Vector3 jumpAmount = jump * Time.fixedDeltaTime;
            rb.AddForce(jumpAmount * speed, ForceMode.Impulse);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            canJump = false;
            SFXScript.sfxInstance.flap.PlayOneShot(SFXScript.sfxInstance.flapJump);
        }
    }

    void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    void StartGame()
    {
        start = 2;
        Time.timeScale = 1f;

        ControlsText.SetActive(false);
        BeginText.SetActive(false);

     


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
            if (count >= 66 )
            {
                winTextObject.SetActive(true);
                PauseGame();
            }
        }
    }

    void FixedUpdate()
    {
        
        


        if(start == 0)
        {
            PauseGame();
        }


           

        if (canJump == false) {
            _timer += Time.deltaTime;
            if(_timer >= _duration) {
                _timer = 0f;
                canJump = true;
            }
        }
        Vector3 movement = new Vector3(movementX, 0.0f, 0.5f);
        rb.AddForce(movement * speed);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);


        Debug.Log((transform.position.z - lastz) / Time.deltaTime);

        lastz = transform.position.z;

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
