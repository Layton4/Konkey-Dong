using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_ladderBarrelDetector : MonoBehaviour
{
    private Collider2D ladderCollider;
    [SerializeField] private LayerMask JBP_barrelLayer;

    private void Awake()
    {
        ladderCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        //BarrelCheck();
        //BarrelsCheck2();
    }
    public void BarrelCheck()
    {
        float extraHeight = 0.2f;
        RaycastHit2D raycastHit = Physics2D.Raycast(ladderCollider.bounds.center, Vector2.up, ladderCollider.bounds.extents.y + extraHeight, JBP_barrelLayer);

        Color rayColor;

        rayColor = Color.red;

        if(raycastHit.collider != null)
        {
            JBP_Barrel barrelScript = raycastHit.collider.gameObject.GetComponent<JBP_Barrel>();
            
            if (barrelScript.randomRoute == 1)
            {
                barrelScript.randomRoute = 0; //we make sure we just do this line once

                rayColor = Color.green; //it turns green when it collides with the top part of a Ladder

                Debug.Log("Pito");

                barrelScript.goDown = true; //make the boolean true to make it fall to the next floor through the ladder

                raycastHit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                raycastHit.collider.gameObject.GetComponent<Animator>().SetBool("isGoingDown", true); //we change the sprite of barrel to the one going down

            }

            else
            {
                barrelScript.randomRoute = 0;
                rayColor = Color.yellow;
                //barrelScript.goDown = false;
            }
        }

        Debug.DrawRay(ladderCollider.bounds.center, Vector2.up * (ladderCollider.bounds.extents.y + extraHeight), rayColor);

    }

    public void BarrelsCheck2()
    {
        float extraHeight = 0.2f;
        RaycastHit2D raycastHit = Physics2D.Raycast(ladderCollider.bounds.center, Vector2.up, ladderCollider.bounds.extents.y + extraHeight, JBP_barrelLayer);

        Color rayColor;

        rayColor = Color.red;

        if (raycastHit.collider != null)
        {
            JBP_Barrel barrelScript = raycastHit.collider.gameObject.GetComponent<JBP_Barrel>();
            barrelScript.randomRoute = 1;
            rayColor = Color.green; //it turns green when it collides with the top part of a Ladder
        }

        Debug.DrawRay(ladderCollider.bounds.center, Vector2.up * (ladderCollider.bounds.extents.y + extraHeight), rayColor);
    }
}
