using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MouseClick : MonoBehaviour
{
 
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.one;
    public bool ShortClicked = false;
    public GameObject NextParent;
 
    Vector3 target;

    public void Start()
    {
        target = transform.localPosition;
    }



    void Update()
    {

        transform.localPosition= Vector3.SmoothDamp(transform.localPosition, target, ref velocity, smoothTime);

        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
            }
            else
            {
                Debug.Log("not ui");
            }
        }

    }


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results); return results.Count > 1;
    }








}
       