using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
 
    }

   public void GetObject()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);


        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero , 0 , 7);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.GetComponent<Slots>());
        }
    }
        
}
