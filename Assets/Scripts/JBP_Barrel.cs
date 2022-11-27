using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_Barrel : MonoBehaviour
{
    private Rigidbody2D barrelRigidbody;
    private float speed = 3f;

    private JBP_SpawnManager JBP_SpawnManagerScript;

    private void Awake()
    {
        barrelRigidbody = GetComponent<Rigidbody2D>();
        JBP_SpawnManagerScript = GameObject.Find("JBP_SpawnManager").GetComponent<JBP_SpawnManager>();

    }

    private void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if(otherCollider.gameObject.CompareTag("Ground")) //When the barrel touch the ground is first impulsed with a bit of force to make it move and roll down with physics
        {
            barrelRigidbody.AddForce(otherCollider.transform.right * speed, ForceMode2D.Impulse);
        }

        if(otherCollider.gameObject.CompareTag("DestroyZone")) //When the barrel falls of the scenario and is out of screen we make disapear the barrel
        {
            Destroy(gameObject);
            JBP_SpawnManagerScript.JBP_barrelsOnScene.Remove(gameObject);
        }
    }
}
