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

    private void Awake()
    {
        gameManagerScript = FindObjectOfType<JBP_GameManager>();

        marioRigidbody = GetComponent<Rigidbody2D>();
        marioCollider = GetComponent<Collider2D>();

        marioAnimator = GetComponent<Animator>();

        results = new Collider2D[4]; //The array of collider will have 4 empty space, we will not be in touch of more than 4 colliders at the same time
    }

    void Update()
    {
        CheckCollision();

        marioAnimator.SetBool("isOnGround", grounded);
        marioAnimator.SetBool("isJumping", !grounded && !isClimbing);
        marioAnimator.SetBool("isClimbing", isClimbing);

        if(isClimbing)
        {
            VerticalInput = Input.GetAxisRaw("Vertical");
            moveDirection.y = VerticalInput * movingSpeed;
        }

        #region Mario Jump
        else if (grounded && Input.GetButtonDown("Jump")) { moveDirection = Vector2.up * jumpForce; } //if we press the button Jump we aply force up

        else { moveDirection += Physics2D.gravity * Time.deltaTime;} //when we are not pressing the button the gravity affect the player to return to the ground

        if(grounded) {moveDirection.y = Mathf.Max(moveDirection.y, -1f); } //to avoid a high negative force down to the character we put a limit of -1 

        #endregion

        HorizontalInput = Input.GetAxisRaw("Horizontal");
        moveDirection.x = HorizontalInput * movingSpeed;

        MarioIsWalking();

        #region Mario Rotation
        //Mario Rotation When we change direction
        if (HorizontalInput > 0) { transform.rotation = Quaternion.Euler(0, 0, 0); }
        else if (HorizontalInput < 0) { transform.rotation = Quaternion.Euler(marioYRotation);}
        #endregion
    }

    private void FixedUpdate()
    {
        marioRigidbody.MovePosition(marioRigidbody.position + moveDirection * Time.fixedDeltaTime);
    }

    private void MarioIsWalking()
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

    /*private void MarioIsClimbing()
    {
        if (grounded == false && isClimbing == true & VerticalInput != 0)
        {
            marioAnimator.SetBool("isMovingUp", true);
        }
        if(isClimbing == true && VerticalInput != 0)
        {
            marioAnimator.SetBool("isClimbing",true)
        }
    }*/

    private void CheckCollision()
    {
        grounded = false;
        isClimbing = false;

        Vector2 size = marioCollider.bounds.size; //this is the zone we will check if we are in contact with other colliders

        size.y += 0.1f; //we add more high to the zone pass over the ground collider and detect we are on the ground
        size.x /= 2f; //we reduce in half the zone in x to avoid bugs when we climb, to not start climbing when our nose touch the ladder

        int amount = Physics2D.OverlapBoxNonAlloc(transform.position, size, 0f, results); //OverlapBox check what is inside the zone size and save it inside the array results.
        //OverlapBoxNonAllox save it inside results and return the number of element that saved

        for (int i = 0; i < amount; i++)
        {
            GameObject hit = results[i].gameObject;

            if(hit.layer == LayerMask.NameToLayer("Ground"))
            {
                grounded = hit.transform.position.y < (transform.position.y - 0.3);

                Physics2D.IgnoreCollision(marioCollider, results[i], !grounded); //mario ignores a collider if when it enter in contact with them their position in y is higher than mario center
            }

            else if(hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                isClimbing = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D otherColider)
    {
        if(otherColider.gameObject.CompareTag("Barrel"))
        {
            gameManagerScript.isGameover = true;
            marioAnimator.SetBool("isGameover", true);
            StopAllCoroutines();
            Destroy(otherColider.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Clock"))
        {
            Instantiate(timeParticles, otherCollider.gameObject.transform.position, timeParticles.transform.rotation);
            gameManagerScript.WinTime();
            Destroy(otherCollider.gameObject);

        }
    }


}
