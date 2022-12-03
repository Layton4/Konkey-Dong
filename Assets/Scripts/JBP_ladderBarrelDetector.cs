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
        //BarrelsCheck2();
    }
    

    public void BarrelsCheck2()
    {
        float extraHeight = 0.2f;

        Vector3 offset = new Vector3(-0.05f, 0, 0);

        if (transform.rotation.y == 0)
        {
            offset = new Vector3(0.05f, 0, 0);
        }
        

        RaycastHit2D raycastHit = Physics2D.Raycast(ladderCollider.bounds.center + offset, Vector2.up, ladderCollider.bounds.extents.y + extraHeight, JBP_barrelLayer);

        Color rayColor;

        rayColor = Color.red;

        if (raycastHit.collider != null && raycastHit.collider.gameObject.GetComponent<JBP_Barrel>())
        {
            JBP_Barrel barrelScript = raycastHit.collider.gameObject.GetComponent<JBP_Barrel>();
            barrelScript.randomRoute = 1;
            barrelScript.StopBarrel();
            rayColor = Color.green; //it turns green when it collides with the top part of a Ladder
        }

        Debug.DrawRay(ladderCollider.bounds.center + offset, Vector2.up * (ladderCollider.bounds.extents.y + extraHeight), rayColor);
    }
}
