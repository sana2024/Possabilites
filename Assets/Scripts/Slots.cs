using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Slots : MonoBehaviour
{
    //slot properties
    public int SlotNum;
    public string SlotColor = "";

    // the distance between pieces in a slot when insantiated
    float distance = 0.6f;

    //checks whatever the piece is on tiop side of board or botton side
    public int up;

    [SerializeField] public List<Piece> pieces = new List<Piece>();

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        // gives the slot a color based on pices color in it
        foreach (var piece in pieces)
        {
            if (piece.PieceType == "white")
            {
                SlotColor = "white";
            }
            else
            {
                SlotColor = "black";
            }
        }

        
    }

    // removes pieces from slot
    public void RemovePiece()
    {
      var last = pieces.Last();
        pieces.Remove(last);
        
        
    }

    // adds pieces to the slot
    public void addPiece(Piece piece)
    {
        double add = -(0.1 * this.howManyPieces() + 1);
        piece.move(new Vector3(transform.position.x, transform.position.y + ((pieces.Count * distance) * up), (float)add));
        pieces.Add(piece);
    }

    public void FirstPostion(Piece piece)
    {
        double add = -(0.1 * this.howManyPieces() + 1);
        piece.startPostion(new Vector3(transform.position.x, transform.position.y + ((pieces.Count * distance) * up), (float)add));
        pieces.Add(piece);
    }

    public int howManyPieces()
    {
        return pieces.Count;
    }
}
