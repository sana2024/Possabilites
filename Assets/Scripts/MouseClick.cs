using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    float temps;
    bool click = false;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.one;
   public bool ShortClicked = false;
    [SerializeField] AnimationCurve animationCurve;

    Rigidbody2D rb;
 
    void Update()
    {

 

        if (Input.GetMouseButtonDown(0))
        {
            temps = Time.time;
            click = true;
        }

        if (click == true)
        {

            if ((Time.time - temps) > 0.2)
            {
                this.transform.position = GetObject();
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            click = false;
 
 
            if ((Time.time - temps) < 0.2)
            {

                ShortClicked = true;

            }
        }


        if(ShortClicked == true)
        {

        var newPos = new Vector3(4.5f, 0, 0);
            // transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
            transform.position = Vector3.Lerp(transform.position, newPos, animationCurve.Evaluate(smoothTime));

            if(transform.position == newPos)
            {
                ShortClicked = false;
            }
         
        }


    }

   public Vector2 GetObject()
    {
 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

 
    
        return mousePos2D;
    
  

    }
        
}
