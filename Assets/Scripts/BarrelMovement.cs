using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    private float speed = 3f;
    public bool movingRight;
    public bool isDestroyed = false;
    void Start()
    {
        StartCoroutine("ChangeBarrelDirection");
    }

    void Update()
    {
        if(movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        
    }

    public IEnumerator ChangeBarrelDirection()
    {
        while(isDestroyed == false)
        {
            yield return new WaitForSeconds(3.5f);
            Debug.Log("Me giro");
            movingRight = !movingRight;
        }
    }
}
