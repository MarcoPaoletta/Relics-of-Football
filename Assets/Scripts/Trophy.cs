using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{   
    public Canvas relicGrabbedCanvas;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            relicGrabbedCanvas.gameObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Destroy(gameObject);
        }
    }
}
