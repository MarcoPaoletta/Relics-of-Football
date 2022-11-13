using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovementButtonsListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonHeldDown;

    public void OnPointerDown(PointerEventData eventData){
        isButtonHeldDown = true;
    }
    
    public void OnPointerUp(PointerEventData eventData){
        isButtonHeldDown = false;
    }
}
