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

    private Quaternion manteinRotation;

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
        if(randomRoute == 1)
        {
            barrelRigidbody.velocity = Vector2.zero;
            transform.rotation = manteinRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Ground")) //When the barrel touch the ground is first impulsed with a bit of force to make it move and roll down with physics
        {
            
            float speed = Random.Range(minspeed, maxspeed);
            barrelRigidbody.AddForce(otherCollider.transform.right * speed, ForceMode2D.Impulse);
            goDown = false;
            JBP_barrelAnimator.SetBool("isGoingDown", false); //we make sure to return to the default sprite of the barrel
        }

        if(otherCollider.gameObject.CompareTag("DestroyZone")) //When the barrel falls of the scenario and is out of screen we make disapear the barrel
        {
            Destroy(gameObject);
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
            randomRoute = 0;
            goDown = false;
        }
    }
    public void StopBarrel()
    {
        //barrelRigidbody.velocity = Vector2.zero;
        goDown = true;
        JBP_barrelAnimator.SetBool("isGoingDown", true);

    }
}
