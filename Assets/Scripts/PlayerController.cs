using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float speed = 10f;
    //public float airspeed = 5f;
    //public float jumpForce = 4f;

    //Mario Horizontal Movement
    private Rigidbody2D marioRigidBody;
    public float horizontalInput;
    private Vector3 marioYRotation = new Vector3(0,180,0);

    //Movement Test
    private Vector2 moveDirection;
    public float movingSpeed = 5;

    //Jump Test
    public float jumpForce = 4f;
    private Collider2D marioCollider2D;

    //Booleans
    public bool isOnGround;
    public bool jump;
    public bool isClimbing;

    //Animator
    private Animator marioAnimator;

    //Testing Variables



    void Start()
    {
        marioCollider2D = GetComponent<Collider2D>();
        marioRigidBody = GetComponent<Rigidbody2D>(); //To have access to Mario Rigidbody
        marioAnimator = GetComponent<Animator>(); //To access to Mario Animator and animations

        isOnGround = true;
        jump = false;
        isClimbing = false;
    }

    void Update()
    {
        marioAnimator.SetBool("isOnGround", isOnGround);
        marioAnimator.SetBool("isJumping", !isOnGround);

        if(isClimbing)
        {
            if(Input.GetButtonDown("Vertical"))
            {
                isOnGround = false;
            }
            moveDirection.y = Input.GetAxisRaw("Vertical") * movingSpeed;
        }

        else if (Input.GetButtonDown("Jump") && isOnGround == true)
        {
            isOnGround = false;
            jump = true;
            moveDirection = Vector2.up * jumpForce; //This movement then we see in Fixed Update that it will move from the ground to the max high gradually
        }
        
        else if (!jump && !isOnGround) //When we have jumped and we are not touching the ground we fall down with gravity
        {
            moveDirection += Physics2D.gravity * Time.deltaTime;
        }

        #region Movement Tutorial Test

        horizontalInput = Input.GetAxisRaw("Horizontal");
        MarioIsWalking();
        moveDirection.x = horizontalInput * movingSpeed; //This is the velocity we want for mario when he moves during the game, saved on the first element of a vector2
        //moveDirection.y = 
        #endregion

        #region Mario Rotation
        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(marioYRotation);
        }
        #endregion

        #region Tests Zone
        if (isOnGround == true && horizontalInput == 0) //Si estoy en el suelo quieto frena y quedate quieto
        {
            marioRigidBody.velocity = Vector2.zero;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = new Vector3(-6.49f,-3.74f,0);
            Debug.Log("Teleport");
        }
        #endregion
    }

    private void FixedUpdate()
    {
        #region Javi Movement Idea
        /*if (isOnGround)
        {
            marioRigidBody.AddForce(Vector2.right * speed * horizontalInput * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        else
        {
            marioRigidBody.velocity = new Vector2(Mathf.Lerp(marioRigidBody.velocity.x, airspeed * horizontalInput,1f), marioRigidBody.velocity.y);
        }
        */
        #endregion

        if (jump) //When the jumpKey is pressed mario do this action once
        {
            jump = false;
            //marioRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //Tutorial Movement Test
        marioRigidBody.MovePosition(marioRigidBody.position + moveDirection * Time.fixedDeltaTime);
    }

    private void MarioIsWalking()
    {
        if(isOnGround == true && horizontalInput != 0)
        {
            marioAnimator.SetBool("isMoving", true);
        }
        else
        {
            marioAnimator.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D otherCollider2D)
    {
        if(otherCollider2D.gameObject.CompareTag("Ground"))
        {
            if(otherCollider2D.gameObject.transform.position.y < (transform.position.y - 0.4f))
            {
                isOnGround = true;
            }

            else
            {
                Physics2D.IgnoreCollision(marioCollider2D, otherCollider2D.gameObject.GetComponent<Collider2D>(), !isOnGround); //Ignore the collider if it hit you on the head to avoid hit a platform from under
            }
            
            //marioRigidBody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherTrigger)
    {
        if(otherTrigger.gameObject.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherTrigger)
    {
        if(otherTrigger.gameObject.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }

}
