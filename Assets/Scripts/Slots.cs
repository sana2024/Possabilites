using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Slots : MonoBehaviour
{
    public int SlotNum;
    float distance = 0.68f;
    public int up;

    public string SlotColor = "";


    [SerializeField] public List<Piece> pieces = new List<Piece>();

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
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

    public void RemovePiece()
    {
      var last = pieces.Last();
        pieces.Remove(last);
        
        
    }

    public void addPiece(Piece piece)
    {
        double add = -(0.1 * this.howManyPieces() + 1);
        piece.move(new Vector3(transform.position.x, transform.position.y + ((pieces.Count * distance) * up), (float)add));
        pieces.Add(piece);
    }

    public int howManyPieces()
    {
        return pieces.Count;
    }
}
