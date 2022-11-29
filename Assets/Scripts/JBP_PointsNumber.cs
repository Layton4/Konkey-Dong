using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JBP_PointsNumber : MonoBehaviour
{
    public float verticalSpeed = 1f;

    public float scaleFactor = 10f;

    public float scorePoints;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = $"+{scorePoints.ToString()}";
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + verticalSpeed * Time.deltaTime, 0);
        transform.localScale *= 1 - Time.deltaTime / scaleFactor;
    }

}
