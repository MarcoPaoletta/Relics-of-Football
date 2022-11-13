using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnergyCan : MonoBehaviour
{
    public GameObject energyCanParticles;

    Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.5f;
                hitPosition.y = hit.point.y - 0.5f ;
                Vector3 cell = new Vector3(hitPosition.x, hitPosition.y, 0);
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }

            Instantiate(energyCanParticles, hitPosition, Quaternion.identity);
            GameObject.FindObjectOfType<HealthBar>().GrabbedEnergyCan();
        }
    }
}
