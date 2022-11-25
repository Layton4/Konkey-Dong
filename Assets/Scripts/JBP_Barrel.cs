using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_Barrel : MonoBehaviour
{
    private Rigidbody2D barrelRigidbody;
    private float speed = 3f;

    private void Awake()
    {
        barrelRigidbody = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Ground"))
        {
            barrelRigidbody.AddForce(otherCollider.transform.right * speed, ForceMode2D.Impulse);
        }

        if(otherCollider.gameObject.CompareTag("DestroyZone"))
        {
            Destroy(gameObject);
        }
    }
}
