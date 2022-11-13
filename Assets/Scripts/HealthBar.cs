using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Sprite[] healthBarImages;

    Image healthBarImage;
    int energyCanAmount;

    void Start()
    {
        healthBarImage = GetComponent<Image>();
    }

    public void GrabbedEnergyCan()
    {
        energyCanAmount += 1;

        switch (energyCanAmount)
        {
            case 1:
                healthBarImage.sprite = healthBarImages[0];
                break;

            case 2:
                healthBarImage.sprite = healthBarImages[1];
                break;
            
            case 3:
                healthBarImage.sprite = healthBarImages[2];
                FindObjectOfType<Player>().Jetpack();
                energyCanAmount = 0;
                healthBarImage.sprite = healthBarImages[3];
                break;
        }
    }
}
