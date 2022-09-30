using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float airspeed = 5f;
    public float jumpForce = 4f;

    //Mario Horizontal Movement
    private Rigidbody2D marioRigidBody;
    public float horizontalInput;
    private Vector3 marioYRotation = new Vector3(0,180,0);

    //Booleans
    private bool isOnGround;
    private bool jump;

    //Animator
    private Animator marioAnimator;

    //Testing Variables



    void Start()
    {
        marioRigidBody = GetComponent<Rigidbody2D>();
        marioAnimator = GetComponent<Animator>();

        isOnGround = true;
        jump = false;
    }

    void Update()
    {
        marioAnimator.SetBool("isOnGround", isOnGround);
        marioAnimator.SetBool("isJumping", !isOnGround);

        horizontalInput = Input.GetAxisRaw("Horizontal");
        MarioIsWalking();

        if(horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(marioYRotation);
        }

        if(Input.GetButtonDown("Jump") && isOnGround == true)
        {
            isOnGround = false;
            jump = true;
        }

        if(isOnGround == true && horizontalInput == 0) //Si estoy en el suelo quieto frena y quedate quieto
        {
            marioRigidBody.velocity = Vector2.zero;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = new Vector3(-6.49f,-3.74f,0);
            Debug.Log("Teleport");
        }
    }

    private void FixedUpdate()
    {
        if(isOnGround)
        {
            marioRigidBody.AddForce(Vector2.right * speed * horizontalInput * Time.fixedDeltaTime, ForceMode2D.Impulse);
            //marioRigidBody.velocity = Vector2.right * speed * horizontalInput * Time.deltaTime;
        }

        else
        {
            marioRigidBody.velocity = new Vector2(Mathf.Lerp(marioRigidBody.velocity.x, airspeed * horizontalInput,1f), marioRigidBody.velocity.y);
        }
        
        if(jump)
        {
            jump = false;
            marioRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
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
            isOnGround = true;
            marioRigidBody.velocity = Vector2.zero;
        }
    }

}
