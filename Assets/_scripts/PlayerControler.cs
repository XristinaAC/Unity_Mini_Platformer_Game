using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour {

    public float speed;
    public float Jump;
    public RawImage life1;
    public RawImage life2;
    public RawImage life3;
    public RawImage life4;
    public bool gameOverL;
    public bool gameOverW;
    public AudioSource jumpAudio;
    public AudioSource winAudio;
    public Text restartText;
    public Text gameResultText;
    public Text exitGameText;

    private RawImage[] lifeArray;
    private int lifeCounter;
    private float jumpCounter = 0;
    bool collisionAfterJump = false;
    Vector3 startPos;
    private Rigidbody rb;
    private ConfettiInstantiate con;
    private bool restartOrExit;

    void Start () {
        jumpAudio.enabled = !jumpAudio.enabled;
        winAudio.enabled = !winAudio.enabled;
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        lifeCounter = 0;
        jumpCounter = 0;

        lifeArray = new RawImage[4];
        lifeArray[0] = life1;
        lifeArray[1] = life2;
        lifeArray[2] = life3;
        lifeArray[3] = life4;

        gameOverL = false;
        gameOverW = false;
        restartOrExit = false;
        restartText.text = "";
        gameResultText.text = "";
        exitGameText.text = "";

        con = GetComponent<ConfettiInstantiate>();
    }

   
	
	// Update is called once per frame
	void FixedUpdate () {
        if (gameOverL == true)
        {
            gameResultText.text = "!!YOU LOST!!";
            if (restartOrExit)
            {
                RestartOrEndGame();
            }
        }
        else if (gameOverW == true)
        {
            gameResultText.text = "!!YOU WIN!!";
            if (restartOrExit)
            {
                RestartOrEndGame();
            }
        }
        else
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCounter < 1)
            {
                Vector3 jump = new Vector3(0.0f, Jump, 0.0f);
                rb.AddForce(jump * speed, ForceMode.Impulse);
                jumpAudio.enabled = true;
                jumpAudio.Play();
                Debug.Log(jumpCounter);
                jumpCounter = jumpCounter + 1;
            }
            else
            {
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
                rb.AddForce(movement * speed);
            }

            if (collisionAfterJump)
            {
                jumpCounter = 0;
            }
        }

       
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Ground"))
        {
            if (gameOverW == true)
            {
                return;
            }
            if (lifeCounter!= 3)
            {
                transform.position = startPos;
                DisablingImage();
                lifeCounter = lifeCounter + 1;
            }
            else
            {
                DisablingImage();
                gameOverL = true;
                exitGameText.text = "Press \'Esc\' to Exit'";
                restartText.text = "Press \'R\' for Restart";
                restartOrExit = true;
            }
           
        }
        else
        {
            collisionAfterJump = true;
        }
    }

    void OnCollisionExit()
    {
        collisionAfterJump = false;
    }

    void DisablingImage()
    {
        lifeArray[lifeCounter].enabled = !lifeArray[lifeCounter].enabled;
    }

    void RestartOrEndGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            #pragma warning disable CS0618 // Type or member is obsolete
            Application.LoadLevel(Application.loadedLevel);
            #pragma warning restore CS0618 // Type or member is obsolete
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CC"))
        {
            gameOverW = true;
            con.ConfettiExplosion();
            winAudio.enabled = true;
            winAudio.Play();
            exitGameText.text = "Press \'Esc\' to Exit";
            restartText.text = "Press \'R\' to Restart";
            restartOrExit = true;

        }
    }
}
