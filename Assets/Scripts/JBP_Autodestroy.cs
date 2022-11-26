using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_Autodestroy : MonoBehaviour
{
    public float lifeTime;
    void Start()
    {
        Destroy(gameObject, lifeTime);        
    }

}
