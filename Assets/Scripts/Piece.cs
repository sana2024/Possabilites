using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Piece : MonoBehaviour
{
    [SerializeField] public string PieceType;
    [SerializeField] int id;
     bool PieceClicked = false;


    float temps;
    bool click = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    

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
                PieceDrag(); 
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            click = false;

            if ((Time.time - temps) < 0.2)
            {
                 
            }
        }


    }

    public void move(Vector3 newPos)
    {
        transform.position = newPos;
    }

    
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        return worldPos;
    }

    public void PieceDrag()
    {
        LayerMask mask = LayerMask.GetMask("piece");
        RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero, mask);
        Debug.Log(hit.collider.name);

        if (this.name == hit.collider.name)
        {
            this.transform.position = GetMousePos();
        }
    }
    
}
