using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    float temps;
    bool click = false;
    // Update is called once per frame
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
                this.transform.position = new Vector3(4.5f, 0, 0);
            }
        }

    }

   public Vector2 GetObject()
    {
 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);


        //RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
    
        return mousePos2D;
    
  

    }
        
}
