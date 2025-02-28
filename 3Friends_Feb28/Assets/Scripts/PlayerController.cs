using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header ("Serializefields")]
    [SerializeField] float speedFactor;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject wheel;
    [SerializeField] ParticleSystem smoke;

    [Header("Public variables")]

 //   public Transform escalator;
    public bool isOnElevator = false;
    public bool isAtExit = false;
    public bool isActive = false;
    public bool isDead = false;
    public bool isEntered = false;

    [Header("Priviate readonly variables")]
    private readonly float wheelSpeed = 180.0f;
    private readonly float moveSpeed = 5.0f;
    private readonly float fallDepth = -20.0f;
    private readonly float delayCollision = 2.0f;
    private readonly float upGravityMultiplier = 3.0f;
    private readonly float downGravityMultiplier = 6.0f;
    private readonly float followHeight = -5.0f;
    private readonly string claire = "Claire";
    private readonly string john = "John";
    private readonly string thomas = "Thomas";
    public float horizontalInput;
    public float yPos;

    [Header("Priviate variables")]
    private Rigidbody playerRb;
    private bool isOnGround = true;
    private Vector3 startPostion;
    private new CameraController camera;
    private bool isCollide;
    private UIController uiController;
    private CountDown countDown;
    private MainManager mainManager;
    private bool isFalling = false;
    private float playerView;
    private string currentStageName;

    // Start is called before the first frame update
    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        countDown = GameObject.Find("GameUI").GetComponent<CountDown>();
        uiController = GameObject.Find("GameUI").GetComponent<UIController>();
        startPostion = this.transform.position;
        camera = GameObject.Find("Main Camera").GetComponent<CameraController>();
        playerRb = this.gameObject.GetComponent<Rigidbody>();
        yPos = startPostion.y;
        currentStageName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camera.isGameOver)
        {
            // if pause is enable switch is disabled
            if (!uiController.isPauseOn && countDown.counter < 0)
                SwitchPlayer();
            if (this.isActive)
            {
                // player moves
                horizontalInput = Input.GetAxis("Horizontal");
                PlayerMovement(horizontalInput);
                PlayerOrientation();
                PlaySmokeEffect(horizontalInput);

                if (this.transform.position.y <= followHeight && !isFalling)
                {
                    isFalling = true;
                    mainManager.PlaySFXSound(mainManager.fallClip);
                }

                // player jump
                if (Input.GetKeyDown(KeyCode.Space) && this.isOnGround)
                {
                    Jump();
                    this.isOnGround = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (this.transform.position.y < fallDepth)
            camera.LoadGameOverPage();
    }

    // player movement forwrd and backward

    private void PlayerMovement(float horInput)
    {
        if (horInput != 0 && currentStageName != "Stage01")
        {
            if (horInput > 0)
                this.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed * speedFactor * horInput);
            else if (horInput < 0)
                this.transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * speedFactor * horInput);
            wheel.transform.Rotate(new Vector3(0, 0, ( wheelSpeed * Time.deltaTime)));
        }
        else
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * moveSpeed * speedFactor * horInput);
            wheel.transform.Rotate(new Vector3(0, 0, (horInput * wheelSpeed * Time.deltaTime)));
        }

    }

    private void PlaySmokeEffect(float horInput)
    {
        if (!camera.isGameOver)
        {
            if (horInput != 0 && !this.smoke.isPlaying)
            {
                this.smoke.Play();
            }
            else if (horInput == 0 && this.smoke.isPlaying)
            {
                this.smoke.Stop();
            }
        }
    }
    // change the orientation of the playe to the direction it is moving starting stage 02
    private void PlayerOrientation()
    {
        if (currentStageName == "Stage02" && horizontalInput != 0)
        {
            Vector3 playerView = new Vector3(horizontalInput , 0 , 0 );
            this.transform.rotation = Quaternion.LookRotation(playerView);

        }
    }
    public void FixedUpdate()
    {
        if (playerRb.velocity.y  < 0)
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * downGravityMultiplier * Time.deltaTime;
        }
        else if (playerRb.velocity.y > 0)
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * upGravityMultiplier * Time.deltaTime;
        }
    }
    private void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce , ForceMode.VelocityChange);
        mainManager.PlaySFXSound(mainManager.jumpClip);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        if (collision.gameObject.CompareTag("Ground"))
            {
                this.isOnGround = true;
        }

        if (collision.gameObject.CompareTag("Wall") && !isCollide)
        {
            mainManager.PlaySFXSound(mainManager.wallCollideClip);
            isCollide = true;
            StartCoroutine(DelayNextCollisionSound());
        }
    }

    public void SwitchPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (this.gameObject.name == claire)
            {
                this.isActive = true;
            }
            else
            {
                this.isActive = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (this.gameObject.name == john)
            {
                this.isActive = true;
            }
            else
            {
                this.isActive = false;

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (this.gameObject.name == thomas)
            {
                this.isActive = true;
            }
            else
            {
                this.isActive = false;
            }
        }
    }

    IEnumerator DelayNextCollisionSound()
    {
        yield return new WaitForSeconds(delayCollision);
        isCollide = false;
    }

}
