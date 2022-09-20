using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameManager : MonoBehaviour
{
    //--------- dices ---------
    public int diceValue1 = 5;
    public int diceValue2 = 6;

    public int BigDice = 0;
    public int SmallDice = 0;


    [SerializeField] private GameObject[] pieces = new GameObject[2];
    [SerializeField] private Slots[] slots = new Slots[26];

    [SerializeField] MouseClick mouseClick;
    [SerializeField] bool BigMoved = false;
    [SerializeField] bool smallMoved = false;

    // ----- players-----
    [SerializeField] string Player = "black";
    string enemy = "";

    //-------- Slots a nd Postions -------------
    private int[] startPositions = { 2, 0, 0, 0, 0, 5, 0, 3, 0, 0, 0, 5, 5, 0, 0, 0, 3, 0, 1, 0, 0, 0, 0, 2 };
    private int[] startColors = { 0, -1, -1, -1, -1, 1, -1, 1, -1, -1, -1, 0, 1, -1, -1, -1, 0, -1, 0, -1, -1, -1, -1, 1 };

    public string[] SlotColors = new string[24];
    public int[] MovableSlots = new int[4];

    public int[] moves = new int[4];

    int MoveCounter = 0;
    float ClickTimer = 0;
    bool MouseDown = false;


    Slots BiggerSlot;
    Slots SmallerSlot;

    Piece piece;

    [SerializeField]  GameObject[] PieceObjects = new GameObject[24];

    float temps;
    bool click = false;

    bool canPlayBig = false;
    bool canPlaySmall = false;

    bool BigPlayed = false;
    bool SmallPlayed = false;

 


 
      

    // Start is called before the first frame update
    void Start()
    {
         

        if (Player == "white")
        {
            enemy = "black";
            

        }
        else
        {
            enemy = "white";
        }



        if (diceValue1 > diceValue2)
        {
            BigDice = diceValue1;
            SmallDice = diceValue2;


        }
        else
        {
            BigDice = diceValue2;
            SmallDice = diceValue1;
        }




        for (int i = 0; i < 24; i++)
        {
            

            for (int j = 0; j < startPositions[i]; j++)
            {
                piece = Instantiate(pieces[startColors[i]], new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Piece>();
                slots[i].addPiece(piece);

            
            }
 
        }


        
        PieceObjects = GameObject.FindGameObjectsWithTag("Piece");

        for(int i = 0; i< PieceObjects.Length; i++)
        {
            int pieceId = i + 1;
            PieceObjects[i].GetComponent<Piece>().name = "piece (" +  pieceId + ")";
        }
        



    }

    public Slots GetSlot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        LayerMask mask = LayerMask.GetMask("slot");
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero , 20.0f, mask);

        return hit.collider.gameObject.GetComponent<Slots>();
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
                if (GetSlot().SlotColor == Player)
                {
 

                }
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            click = false;

            if ((Time.time - temps) < 0.2)
            {
                // MoveClick();
                Possab(GetSlot());
                MovePiecesWithClick();
            }
        }



 

    }
    


    public int PlayerMovement(int SlotNum, int Dice)
    {
        if (Player == "white")
        {
            return SlotNum - Dice;
        }
        if (Player == "black")
        {
            return SlotNum + Dice;
        }

        return 0;

    }

    public void Possab(Slots ClickedSlot )
    {
        var BigDicePosb = ClickedSlot.SlotNum - BigDice ;
        var smallDicePosb = ClickedSlot.SlotNum - SmallDice;

         if(BigDicePosb - 1 >= 0)
        {

         Slots BigSlot = slots[BigDicePosb -1 ];
         Slots SmallSlot = slots[smallDicePosb -1 ];


            if(BigSlot.SlotColor != enemy || (BigSlot.SlotColor == enemy && BigSlot.pieces.Count ==1))
            {
               SpriteRenderer BigSprite = BigSlot.GetComponentInChildren<SpriteRenderer>();
               Debug.Log(BigSlot);
               BigSprite.color = new Color(BigSprite.color.r, BigSprite.color.g, BigSprite.color.b, 0.5f);
                canPlayBig = true;
              
            }


            if(SmallSlot.SlotColor != enemy || (SmallSlot.SlotColor == enemy && SmallSlot.pieces.Count == 1))
            {
               SpriteRenderer smallSprite = SmallSlot.GetComponentInChildren<SpriteRenderer>();
               Debug.Log(SmallSlot);
               smallSprite.color = new Color(smallSprite.color.r, smallSprite.color.g, smallSprite.color.b, 0.5f);
                canPlaySmall = true;
                
            }


        }


 

    }

    public void MoveClick()
    {
        if (GetSlot().SlotColor == Player)
        {



            MoveCounter++;

            if (BigMoved == false)
            {

                int bigMove = PlayerMovement(GetSlot().SlotNum, BigDice);

                if (bigMove - 1 >= 0)
                {



                    if (slots[bigMove - 1].SlotColor == enemy && slots[bigMove - 1].pieces.Count > 1)
                    {
                        Debug.Log("not valid move");
                    }
                    else
                    {

                        if (slots[bigMove - 1].pieces.Count == 1 && slots[bigMove - 1].pieces.First().PieceType != Player)
                        {
                            var hittedPiece = slots[bigMove - 1].pieces.First();

                            slots[bigMove - 1].pieces.Remove(hittedPiece);

                            Debug.Log(hittedPiece.PieceType);

                            if (hittedPiece.PieceType == "black")
                            {
                                slots[24].addPiece(hittedPiece);
                            }

                            if (hittedPiece.PieceType == "white")
                            {
                                slots[25].addPiece(hittedPiece);
                            }


                        }


                            var last = GetSlot().pieces.LastOrDefault();

                            slots[bigMove - 1].addPiece(last);
                            GetSlot().RemovePiece();

                            BiggerSlot = slots[bigMove - 1];


                            if (diceValue1 != diceValue2)
                            {

                                BigMoved = true;
                            }
                            else
                            {
                                smallMoved = true;
                                if (MoveCounter == 4)
                                {
                                    BigMoved = true;
                                }
                            }

                        }
                    }

                }
                if (MoveCounter >= 2 && smallMoved == false && BigMoved == true)
                {


                    int SmallMove = PlayerMovement(GetSlot().SlotNum, SmallDice);
                    if (slots[SmallMove - 1].SlotColor == enemy && slots[SmallMove - 1].pieces.Count > 1)
                    {
                        Debug.Log("not valid move");
                    }
                    else
                    {

                        if (slots[SmallMove - 1].pieces.Count == 1 && slots[SmallMove - 1].pieces.First().PieceType != Player)
                        {
                            var hittedPiece = slots[SmallMove - 1].pieces.First();

                            slots[SmallMove - 1].pieces.Remove(hittedPiece);

                            Debug.Log(hittedPiece.PieceType);

                            if (hittedPiece.PieceType == "black")
                            {
                                slots[24].addPiece(hittedPiece);
                            }

                            if (hittedPiece.PieceType == "white")
                            {
                                slots[25].addPiece(hittedPiece);
                            }


                        }



                        var last = GetSlot().pieces.LastOrDefault();
                        slots[SmallMove - 1].addPiece(last);
                        GetSlot().RemovePiece();
                        SmallerSlot = slots[SmallMove - 1];
                        smallMoved = true;



                    }
                }
            }




        }

    public void MovePiecesWithClick()
    {
 
        if (canPlayBig == true && canPlaySmall == true )
        {
             MoveCounter++;
            if (BigPlayed == false)
            {
                int bigMove = PlayerMovement(GetSlot().SlotNum, BigDice);
                var last = GetSlot().pieces.LastOrDefault();

                HitMovement(slots[bigMove - 1]);
                slots[bigMove - 1].addPiece(last);
                GetSlot().RemovePiece();

                if (BigDice == SmallDice)
                {
                    if (MoveCounter >= 4)
                    {
                        BigPlayed = true;
                        canPlayBig = false;
                        canPlaySmall = false;
                    }
                }
                else
                {
                  BigPlayed = true;
                  canPlayBig = false;
                  canPlaySmall = false;
                }


            }

            if (SmallPlayed == false && BigPlayed == true && MoveCounter == 2)
            {
                int smallMove = PlayerMovement(GetSlot().SlotNum, SmallDice);
                var last = GetSlot().pieces.LastOrDefault();
                HitMovement(slots[smallMove - 1]);
                slots[smallMove - 1].addPiece(last);
                GetSlot().RemovePiece();

                SmallPlayed = true;
                canPlayBig = false;
                canPlaySmall = false;
            }

 

        }

        if (canPlayBig == true && canPlaySmall == false)
        {
            if(BigPlayed == false)
            {
            int bigMove = PlayerMovement(GetSlot().SlotNum, BigDice);
            var last = GetSlot().pieces.LastOrDefault();

            HitMovement(slots[bigMove - 1]);
            slots[bigMove - 1].addPiece(last);
            GetSlot().RemovePiece();

            BigPlayed = true;
            canPlayBig = false;
            canPlaySmall = false;
            }

        }

        if (canPlayBig == false && canPlaySmall == true)
        {
            if(SmallPlayed == false)
            {
            int smallMove = PlayerMovement(GetSlot().SlotNum,SmallDice);
            var last = GetSlot().pieces.LastOrDefault();
            HitMovement(slots[smallMove - 1]);
            slots[smallMove - 1].addPiece(last);
            GetSlot().RemovePiece();

            SmallPlayed = true;
            canPlayBig = false;
            canPlaySmall = false;
            }

        }
    }

    public void HitMovement(Slots destination)
    {
        if (destination.pieces.Count == 1 && destination.pieces.First().PieceType != Player)
        {
            var hittedPiece = destination.pieces.First();

            destination.pieces.Remove(hittedPiece);

            Debug.Log(hittedPiece.PieceType);

            if (hittedPiece.PieceType == "black")
            {
                slots[24].addPiece(hittedPiece);
            }

            if (hittedPiece.PieceType == "white")
            {
                slots[25].addPiece(hittedPiece);
            }


        }
    }

    }













