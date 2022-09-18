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
    private int[] startPositions = { 2, 0, 0, 0, 0, 5, 0, 3, 0, 0, 0, 5, 5, 0, 0, 0, 3, 0, 5, 0, 0, 0, 0, 2 };
    private int[] startColors = { 0, -1, -1, -1, -1, 1, -1, 1, -1, -1, -1, 0, 1, -1, -1, -1, 0, -1, 0, -1, -1, -1, -1, 1 };

    public string[] SlotColors = new string[24];
    public int[] MovableSlots = new int[4];

    public int[] moves = new int[4];

    int MoveCounter = 0;
    float ClickTimer = 0;
    bool MouseDown = false;


    Slots BiggerSlot;
    Slots SmallerSlot;



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
                slots[i].addPiece(Instantiate(pieces[startColors[i]], new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Piece>());
            }
        }





    }

    public Slots GetSlot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        return hit.collider.gameObject.GetComponent<Slots>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            MouseDown = true;
            Debug.Log(GetSlot());

            
            if (GetSlot().SlotColor == Player)
            {
 


                MoveCounter++;
 
                if (BigMoved == false)
                {

                    int bigMove = PlayerMovement(GetSlot().SlotNum, BigDice);

                      if(bigMove-1 >= 0)
                    {



                        if (slots[bigMove-1].SlotColor == enemy)
                        {
                            Debug.Log("not valid move");
                        }
                        else
                        {


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
                    if (slots[SmallMove - 1].SlotColor == enemy)
                    {
                        Debug.Log("not valid move");
                    }
                    else
                    {

                        //GetSlot().RemovePiece();
                        var last = GetSlot().pieces.LastOrDefault();
                        slots[SmallMove - 1].addPiece(last);
                        GetSlot().RemovePiece();
                        SmallerSlot = slots[SmallMove - 1];
                        smallMoved = true;
                    }
                }


            }
        }

        if (Input.GetMouseButtonUp(0))
        {
 
            MouseDown = false;
 
        }


        if (MouseDown == true)
        {
             ClickTimer += Time.deltaTime;

              if(ClickTimer > 0.5)
            {
               if (GetSlot().SlotColor == Player)
              {
                Possab(GetSlot());


               }
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

    public void Possab(Slots ClickedSlot)
    {
        var BigDicePosb = ClickedSlot.SlotNum - BigDice ;
        var smallDicePosb = ClickedSlot.SlotNum - SmallDice;

         if(BigDicePosb - 1 >= 0)
        {

         Slots BigSlot = slots[BigDicePosb];
         Slots SmallSlot = slots[smallDicePosb];


            if(BigSlot.SlotColor != enemy)
            {
               SpriteRenderer BigSprite = BigSlot.GetComponentInChildren<SpriteRenderer>();
               Debug.Log(BigSlot);
               BigSprite.color = new Color(BigSprite.color.r, BigSprite.color.g, BigSprite.color.b, 1f);

            }


            if(SmallSlot.SlotColor != enemy)
            {
               SpriteRenderer smallSprite = SmallSlot.GetComponentInChildren<SpriteRenderer>();
               Debug.Log(SmallSlot);
               smallSprite.color = new Color(smallSprite.color.r, smallSprite.color.g, smallSprite.color.b, 1f);
            }
 


        }


 

    }







}







