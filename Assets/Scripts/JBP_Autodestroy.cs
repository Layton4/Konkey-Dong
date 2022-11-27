using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JBP_Autodestroy : MonoBehaviour
{
    public float lifeTime;
    void Start()
    {
        Destroy(gameObject, lifeTime); //From the spawn of the object in scene it has a lifetime before it gets destroyed.        
    }

}
