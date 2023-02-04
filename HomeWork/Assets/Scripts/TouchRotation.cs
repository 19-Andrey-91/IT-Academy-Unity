using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class TouchRotation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Transform obj;


    private Vector2 startPos = Vector2.zero;
    private Vector2 direction = Vector2.zero;

    bool isHold = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        isHold = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHold = false;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (isHold)
                    {
                        direction = touch.position - startPos;
                    }
                    break;

                case TouchPhase.Ended:
                    startPos = Vector2.zero;
                    direction = Vector2.zero;
                    break;
            }
            obj.rotation = Quaternion.Euler(0f, direction.x, 0f);
        }
    }
}
