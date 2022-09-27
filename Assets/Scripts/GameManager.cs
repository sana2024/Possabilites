using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GameManager : MonoBehaviour
{
    //------------------------
    //        Dices
    //------------------------
    public int diceValue1 = 5;
    public int diceValue2 = 6;

    public int BigDice = 0;
    public int SmallDice = 0;

    //------------------------
    //   Arrays And Slots
    //------------------------
    [SerializeField] private GameObject[] pieces = new GameObject[2];
    [SerializeField] private Slots[] slots = new Slots[26];
    [SerializeField] GameObject[] PieceObjects = new GameObject[24];


    [SerializeField] int WhitePiecesInHome;
    [SerializeField] int BlackPiecesInHome;

    //------------------------
    //    Start Postions
    //------------------------
    private int[] startPositions = { 2, 0, 0, 0, 0, 5, 0, 3, 0, 0, 0, 5, 5, 0, 0, 0, 3, 0, 1, 0, 0, 0, 1, 2 };
    private int[] startColors = { 0, -1, -1, -1, -1, 1, -1, 1, -1, -1, -1, 0, 1, -1, -1, -1, 0, -1, 0, -1, -1, -1, 1, 1 };


    //------------------------
    //      Players
    //------------------------
    [SerializeField] string Player = "black";
    [SerializeField] string enemy = "";

 
    //------------------------
    //     Conditions
    //------------------------
    float temps;
   public  bool click = false;

   public bool canPlayBig = false;
   public bool canPlaySmall = false;

   public bool BigPlayed = false;
   public bool SmallPlayed = false;

 
    


    //------------------------
    //     Others
    //------------------------

  public int MoveCounter = 0;
    Piece piece;
    Slots ClickedSlot;



    // Start is called before the first frame update
    void Start()
    {
         
        // check which color is the current player
        if (Player == "white")
        {
            enemy = "black";
        }
        else
        {
            enemy = "white";
        }


        // check which dice is bigger and playing first
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



        // insantiate 15 white pieces and 15 black pieces
        for (int i = 0; i < 24; i++)
        {
            
            for (int j = 0; j < startPositions[i]; j++)
            {
                piece = Instantiate(pieces[startColors[i]], new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Piece>();
                slots[i].FirstPostion(piece);

            
            }
 
        }


        // give each insantiated piece a name
        PieceObjects = GameObject.FindGameObjectsWithTag("Piece");

        for(int i = 0; i< PieceObjects.Length; i++)
        {
            int pieceId = i + 1;
            PieceObjects[i].GetComponent<Piece>().name = "piece (" +  pieceId + ")";
        }



    }

    // get the slot based on mouse postion click
    public Slots GetSlot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        LayerMask mask = LayerMask.GetMask("slot");
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero , 20.0f, mask);

        if(hit.collider != null)
        {
        return hit.collider.gameObject.GetComponent<Slots>();
        }
        else
        {
            return null;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //check if player performed a single short click or a long press

        if (Input.GetMouseButtonDown(0))
        {
            ClickedSlot = GetSlot();
            temps = Time.time;
            click = true;
        }

        if (click == true)
        {
            // get possable moves if slot pressed
            if ((Time.time - temps) > 0.2)
            {
                if(ClickedSlot != null)
                {
                    if(ClickedSlot.pieces.Count != 0 && ClickedSlot.SlotColor == Player)
                   {
                      Possab(ClickedSlot);
                   }
                }
 
            }

        }
          // place piece on disired slot when slot clicked
        if (Input.GetMouseButtonUp(0))
        {
            resetPossab();
            canPlayBig = false;
            canPlaySmall = false;
            click = false;

            if ((Time.time - temps) < 0.2)
            {
                Movements(GetSlot());
                MovePiecesWithClick();
            }
        }

 
    }

     

    // reverse movement direction based on player color
    public int MovementDirection(int SlotNum, int Dice)
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

    // Positions that player can move pieces to if clicked
    public void Movements(Slots ClickedSlot)
    {
        if (ClickedSlot != null)
        {


            //check if big dice or white dice is playable on clicked slot
            var BigDicePosb = MovementDirection(ClickedSlot.SlotNum, BigDice);
            var smallDicePosb = MovementDirection(ClickedSlot.SlotNum, SmallDice);

            if (BigDicePosb - 1 >= 0)
            {

                Slots BigSlot = slots[BigDicePosb - 1];
                Slots SmallSlot = slots[smallDicePosb - 1];


                //check if player can move Big dice on this slot
                if (BigSlot.SlotColor != enemy || (BigSlot.SlotColor == enemy && BigSlot.pieces.Count == 1))
                {

                    canPlayBig = true;

                }

                // check if player can move small dice on this slot
                if (SmallSlot.SlotColor != enemy || (SmallSlot.SlotColor == enemy && SmallSlot.pieces.Count == 1))
                {

                    canPlaySmall = true;

                }


            }
        }

    }


    // show player possablities to drag pieces to
    public void Possab(Slots ClickedSlot)
    {
        var BigDicePosb = MovementDirection(ClickedSlot.SlotNum , BigDice);
        var smallDicePosb =MovementDirection(ClickedSlot.SlotNum, SmallDice);

        if (BigDicePosb - 1 >= 0)
        {

            Slots BigSlot = slots[BigDicePosb - 1];
            Slots SmallSlot = slots[smallDicePosb - 1];


            if (BigSlot.SlotColor != enemy && BigPlayed == false || (BigSlot.SlotColor == enemy && BigSlot.pieces.Count == 1))
            {
                SpriteRenderer BigSprite = BigSlot.GetComponentInChildren<SpriteRenderer>();
                Debug.Log(BigSlot);
                BigSprite.color = new Color(BigSprite.color.r, BigSprite.color.g, BigSprite.color.b, 0.5f);
                canPlayBig = true;

            }


            if (SmallSlot.SlotColor != enemy && SmallPlayed == false || (SmallSlot.SlotColor == enemy && SmallSlot.pieces.Count == 1))
            {
                SpriteRenderer smallSprite = SmallSlot.GetComponentInChildren<SpriteRenderer>();
                Debug.Log(SmallSlot);
                smallSprite.color = new Color(smallSprite.color.r, smallSprite.color.g, smallSprite.color.b, 0.5f);
                canPlaySmall = true;

            }


        }

 

    }


    // reset possabiliy color if player released mouse click
    public void resetPossab()
    {
        for(int i= 0; i< 24; i++)
        {
            SpriteRenderer SlotSprite = slots[i].GetComponentInChildren<SpriteRenderer>();
            SlotSprite.color = new Color(SlotSprite.color.r, SlotSprite.color.g, SlotSprite.color.b, 0);
        }
    }

    // perform move action on the pice
    public void MovePiecesWithClick()
    {

         // this coondition will be true if slot can be played on both small and big dice
        if (canPlayBig == true && canPlaySmall == true )
        {

 
                MoveCounter++;

            
            //first move big dice
            if (BigPlayed == false)
            {
                int bigMove = MovementDirection(GetSlot().SlotNum, BigDice);
                var last = GetSlot().pieces.LastOrDefault();


                if (slots[bigMove - 1].SlotColor != enemy || slots[bigMove - 1].SlotColor == enemy && slots[bigMove - 1].pieces.Count == 1)
                {

 
                    HitMovement(slots[bigMove - 1]);
                    slots[bigMove - 1].addPiece(last);


                    GetSlot().RemovePiece();



                    // check if its double dice
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


            }
            //then move small dice after big dice
            if (SmallPlayed == false && BigPlayed == true && MoveCounter == 2)
            {
                int smallMove = MovementDirection(GetSlot().SlotNum, SmallDice);
                var last = GetSlot().pieces.LastOrDefault();
                HitMovement(slots[smallMove - 1]);
                slots[smallMove - 1].addPiece(last);
                GetSlot().RemovePiece();

                SmallPlayed = true;
                canPlayBig = false;
                canPlaySmall = false;
            }

 

        }

        //this codtion will be true if the slot can only be played on big dice
        if (canPlayBig == true && canPlaySmall == false)
        {
            if(BigPlayed == false)
            {
            int bigMove = MovementDirection(GetSlot().SlotNum, BigDice);
            var last = GetSlot().pieces.LastOrDefault();

            HitMovement(slots[bigMove - 1]);
            slots[bigMove - 1].addPiece(last);
            GetSlot().RemovePiece();

            BigPlayed = true;
            canPlayBig = false;
            canPlaySmall = false;
            }

        }

        // this condtion will be true if slot can only be played on small dice
        if (canPlayBig == false && canPlaySmall == true)
        {
            if(SmallPlayed == false)
            {
            int smallMove = MovementDirection(GetSlot().SlotNum,SmallDice);
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


    //checks if the slot that contains enemy pieces has only one piece in it
    //moves enemy piece to bar
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

    public void RestTurn()
    {
        if (Player == "white")
        {
            Player = "black";
            enemy = "white";
        }
        else {

            Player = "white";
            enemy = "black";
        }


        BigPlayed = false;
        SmallPlayed = false;
        MoveCounter = 0;
    }

    }













