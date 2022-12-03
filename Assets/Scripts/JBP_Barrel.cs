using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_Barrel : MonoBehaviour
{
    private Collider2D barrelCollider;
    private Rigidbody2D barrelRigidbody;
    private float minspeed = 4f;
    private float maxspeed = 5.5f;

    private JBP_SpawnManager JBP_SpawnManagerScript;
    private JBP_GameManager JBP_gameManagerScript;

    public bool isJumped;

    public ParticleSystem destroyParticles;

    private Animator JBP_barrelAnimator;
    public bool goDown;
    [SerializeField] private LayerMask JBP_laderDownLayer;
    public int randomRoute;
    public bool shortRoute;


    private void Awake()
    {
        barrelCollider = GetComponent<Collider2D>();
        JBP_barrelAnimator = GetComponent<Animator>();

        JBP_gameManagerScript = GameObject.Find("JBP_GameManager").GetComponent<JBP_GameManager>();
        barrelRigidbody = GetComponent<Rigidbody2D>();
        JBP_SpawnManagerScript = GameObject.Find("JBP_SpawnManager").GetComponent<JBP_SpawnManager>();
        isJumped = false;
        goDown = false;
    }

    private void Update()
    {

        if (JBP_gameManagerScript.isGameover)
        {
            Instantiate(destroyParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        gameObject.GetComponent<Collider2D>().isTrigger = goDown;
        if(randomRoute == 1) { barrelRigidbody.velocity = Vector2.zero; }
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Ground")) //When the barrel touch the ground is first impulsed with a bit of force to make it move and roll down with physics
        {
            JBP_barrelAnimator.SetBool("isGoingDown", false); //we make sure to return to the default sprite of the barrel

            float speed = Random.Range(minspeed, maxspeed);
            barrelRigidbody.AddForce(otherCollider.transform.right * speed, ForceMode2D.Impulse);
        }

        if(otherCollider.gameObject.CompareTag("DestroyZone")) //When the barrel falls of the scenario and is out of screen we make disapear the barrel
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("downZone"))
        {
            //randomRoute = Random.Range(0, 2);
           
            
            /*
            if (randomRoute==1)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; //we stop the barrrel
            }*/

            //goDown = true;
            
        }
    }

    /*private void OnCollisionStay2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Ground"))
        {
            if (goDown)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }*/

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("downZone"))
        {
            goDown = false;
        }
    }

    private void CheckLadder()
    {
        float extraHeight = 0.4f;
        RaycastHit2D raycastHit = Physics2D.Raycast(barrelCollider.bounds.center, Vector2.down, barrelCollider.bounds.extents.y + extraHeight, JBP_laderDownLayer);

        Color rayColor;

        if (raycastHit.collider != null)
        {
            randomRoute = 0; //we make sure we just do this line once
            rayColor = Color.green; //it turns green when it collides with the top part of a Ladder
            
            goDown = true; //make the boolean true to make it fall to the next floor through the ladder
            JBP_barrelAnimator.SetBool("isGoingDown", true);

        }
        else
        {
            randomRoute = 0;
            rayColor = Color.red;
            goDown = false;
        }

        Debug.DrawRay(barrelCollider.bounds.center, Vector2.down * (barrelCollider.bounds.extents.y + extraHeight), rayColor);

    }
}
