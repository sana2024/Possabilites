using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Piece : MonoBehaviour
{
    [SerializeField] public string PieceType;
  //  bool PieceClicked = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            PieceClicked = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            PieceClicked = false;
        }

        if(PieceClicked == true)
        {
           // RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero);
          //  Debug.Log(hit.collider.name);
           // this.transform.position = GetMousePos();
        }

        */
    }

    public void move(Vector3 newPos)
    {
        transform.position = newPos;
    }

    /*
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        return worldPos;
    }
    */
}
