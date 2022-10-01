using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager: MonoBehaviour
{
    //public GameObject[] selectionBarrels;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SelectButton(GameObject selectionImages)
    {
        selectionImages.SetActive(true);
    }
    public void DeselectButton(GameObject selectionImages)
    {
        selectionImages.SetActive(false);
    }
}
