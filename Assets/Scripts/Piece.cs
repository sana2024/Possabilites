using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Piece : MonoBehaviour
{
    [SerializeField] public string PieceType;
 
    float temps;
    bool click = false;
    public float smoothTime = 0.2F;
    private Vector3 velocity = Vector3.one;
    Vector3 target;


    // Start is called before the first frame update
    void Start()
    {
        target = transform.localPosition;
    }
    

    // Update is called once per frame
    void Update()
    {

        //checks whatever player is performing long prees on the mouse to drag the piece
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

           
        }

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, target, ref velocity, smoothTime);



    }

    // move piece to the destination
    public void move(Vector2 newPos , Transform parentTrans , Slots slot)
    {
       transform.parent = parentTrans;
       var posY = slot.pieces.Count * 0.5f;

        target = new Vector3(0, posY, 0);
      //  this.GetComponent<SpriteRenderer>().sortingOrder = slot.pieces.Count;

    }

    public void startPostion(Vector3 newPos)
    {
        transform.position = newPos;
    }

    // gets postion of the mouse on the screen when mouse dragged 
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        return worldPos;
    }

    // dragds the piece to the mouse postion on hold
    public void PieceDrag()
    {
        LayerMask mask = LayerMask.GetMask("piece");
        RaycastHit2D hit = Physics2D.Raycast(GetMousePos(), Vector2.zero, mask);
       // Debug.Log(hit.collider.name);
       if(hit.collider != null)
        {
           if (this.name == hit.collider.name)
           {
            this.transform.position = GetMousePos();
           }
        }

    }
    
}
