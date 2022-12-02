using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_PlayerController : MonoBehaviour
{
    #region Horizontal movement variables

    //Mario Horizontal movement
    public float movingSpeed = 5; //speed to move left or right

    private Rigidbody2D marioRigidbody;

    private Vector2 moveDirection;
    private Vector3 marioYRotation = new Vector3(0, 180, 0);
    private float HorizontalInput;


    #endregion

    //Jumping variables
    public float jumpForce = 4f; //the force we will use to jump

    #region IsOnGround
    private bool grounded;
    private Collider2D[] results; //Collider to keep the colliders that are in contact with the player
    private Collider2D marioCollider;
    #endregion

    #region Mario Climbing

    private bool isClimbing;
    private float VerticalInput;

    #endregion

    private Animator marioAnimator;

    private JBP_GameManager gameManagerScript;

    public ParticleSystem timeParticles;

    [SerializeField] private LayerMask JBP_BarrelLayer;

    public GameObject JBP_PointsCanvas;

    public Vector3 deadPosition;


    private AudioSource JBP_marioAudioSource;
    public AudioClip marioJump;
    public AudioClip JBP_barrelScore;

    private Vector2 gravityModified = new Vector2(0, -1.5f);

    private void Awake()
    {
        gameManagerScript = FindObjectOfType<JBP_GameManager>();

        marioRigidbody = GetComponent<Rigidbody2D>();
        marioCollider = GetComponent<Collider2D>();

        marioAnimator = GetComponent<Animator>();

        JBP_marioAudioSource = GetComponent<AudioSource>();


        results = new Collider2D[4]; //The array of collider will have 4 empty space, we will not be in touch of more than 4 colliders at the same time
    }

    void Update()
    {
        CheckCollision();

        BarrelJumped();

        marioAnimator.SetBool("isOnGround", grounded);
        marioAnimator.SetBool("isJumping", !grounded && !isClimbing);
        marioAnimator.SetBool("isClimbing", isClimbing);
        if (gameManagerScript.isGameover == false) //if we are still playing
        {
            if (isClimbing) //and we enter in a climb collider
            {
                VerticalInput = Input.GetAxisRaw("Vertical");
                moveDirection.y = VerticalInput * movingSpeed / 0.9f;
            }

            #region Mario Jump
            else if (grounded && Input.GetButtonDown("Jump")) { moveDirection = Vector2.up * jumpForce; JBP_marioAudioSource.PlayOneShot(marioJump, 1f); } //if we press the button Jump we aply force up

            else { moveDirection += (Physics2D.gravity + gravityModified) * Time.deltaTime; } //when we are not pressing the button the gravity affect the player to return to the ground

            if (grounded) { moveDirection.y = Mathf.Max(moveDirection.y, -1f); } //to avoid a high negative force down to the character we put a limit of -1 

            #endregion

            HorizontalInput = Input.GetAxisRaw("Horizontal");
            moveDirection.x = HorizontalInput * movingSpeed; //The player is moved by HorizontalInput

            MarioIsWalking();

            #region Mario Rotation
            //Mario Rotation When we change direction
            if (HorizontalInput > 0) { transform.rotation = Quaternion.Euler(0, 0, 0); }
            else if (HorizontalInput < 0) { transform.rotation = Quaternion.Euler(marioYRotation); }
            #endregion

        }
        else
        {
            marioAnimator.SetBool("isGameover", true);
            transform.position = deadPosition; //When we die we make sure to stay on the place and not be affected by any momentum conserved from our dead
        }
    }

    private void FixedUpdate()
    {
        marioRigidbody.MovePosition(marioRigidbody.position + moveDirection * Time.fixedDeltaTime); //each frame we let the player move to the position we told from input
    }

    private void MarioIsWalking() //This method controlls the player walking animation, the change between idle and walking animation
    {
        if (grounded == true && HorizontalInput != 0)
        {
            marioAnimator.SetBool("isMoving", true);
        }
        else
        {
            marioAnimator.SetBool("isMoving", false);
        }
    }

    private void CheckCollision()
    {
        grounded = false;
        isClimbing = false;
        marioCollider.enabled = true;

        Vector2 size = marioCollider.bounds.size; //this is the zone we will check if we are in contact with other colliders

        size.y += 0.1f; //we add more high to the zone pass over the ground collider and detect we are on the ground
        size.x /= 2f; //we reduce in half the zone in x to avoid bugs when we climb, to not start climbing when our nose touch the ladder

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results); //OverlapBox check what is inside the zone size and save it inside the array results.
        //OverlapBoxNonAllox save it inside results and return the number of element that saved

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if(hit.layer == LayerMask.NameToLayer("Ground")) //if we are in contact with ground layer
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.3); //We make sure we are in contact with the low part of the player

                Physics2D.IgnoreCollision(marioCollider, results[i], !grounded); //JumpMan ignores a collider if when it enter in contact with his head
            }

            else if(hit.layer == LayerMask.NameToLayer("Ladder")) //If we are in a ladder and we hit the climb button we can climb
            {
                if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    isClimbing = true;
                }
            }

            if(hit.layer == LayerMask.NameToLayer("LadderDown"))
            {
                if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    isClimbing = true;
                    marioCollider.enabled = false;
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D otherColider)
    {
        if(otherColider.gameObject.CompareTag("Barrel") && gameManagerScript.isGameover == false) //if the player collide with a barrel
        {
            deadPosition = transform.position;
            //gameManagerScript.isGameover = true; //we activate the gameover
            marioAnimator.SetBool("isGameover", true); //change our sprite to the gameover sprite
            StopAllCoroutines(); //the corroutine of attack is stopped
            gameManagerScript.showGameOverPanel(true);
            StartCoroutine(gameManagerScript.JBP_deadPlayer());
            Destroy(otherColider.gameObject); //we destroy the barrel
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Clock")) //if we take a clock we add time to the timer and make disapear the clock instantiating particles
        {
            Instantiate(timeParticles, otherCollider.gameObject.transform.position, timeParticles.transform.rotation);
            gameManagerScript.WinTime();
            Destroy(otherCollider.gameObject);

        }

    }


    public bool BarrelJumped()
    {
        float extraHeight = 0.6f;
        //we have a ray to the floor, that only detect if it collides with a barrel
        RaycastHit2D raycastHit = Physics2D.Raycast(marioCollider.bounds.center, Vector2.down, marioCollider.bounds.extents.y + extraHeight, JBP_BarrelLayer);

        Color rayColor;

        if (raycastHit.collider != null && raycastHit.collider.gameObject.GetComponent<JBP_Barrel>().isJumped == false && !isClimbing)
        {
            rayColor = Color.green; //it turns green when it collides with a barrel

            Instantiate(JBP_PointsCanvas, raycastHit.collider.gameObject.transform.position, JBP_PointsCanvas.transform.rotation);
            JBP_marioAudioSource.PlayOneShot(JBP_barrelScore, 1f);

            raycastHit.collider.gameObject.GetComponent<JBP_Barrel>().isJumped = true; //we activate the boolean isJumped to avoid jump two times the same barrel, and just score once
            gameManagerScript.score += 10;
        }

        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(marioCollider.bounds.center, Vector2.down * (marioCollider.bounds.extents.y + extraHeight), rayColor);
        return raycastHit.collider != null;
    }

}
