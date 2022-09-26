using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            transform.parent = NextParent.transform;
            target = NextParent.transform.localPosition;
        }
    }
 

         
        

 
 
        
}
       