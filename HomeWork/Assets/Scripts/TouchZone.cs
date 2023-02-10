using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isTouchButton = false;

    public UnityAction<PointerEventData> TouchDown;
    public bool IsTouchButton { get => isTouchButton; private set => isTouchButton = value; }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsTouchButton = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsTouchButton = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(TouchDown!= null)
        {
            TouchDown(eventData);
        }
    }
}
