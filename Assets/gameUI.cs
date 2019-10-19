using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class gameUI : MonoBehaviour
{
    [SerializeField] int creditAmount = 0;          // Contains the player's bank roll. Must start the game with 0 
    [SerializeField] TextMeshProUGUI creditText;    // The textbox for credits  
    [SerializeField] GameObject pickTileText;       // "Pick a Tile!" 
    [SerializeField] TextMeshProUGUI winText;       // Win text 

    int tile1Value, tile2Value, Tile3Value; 
    
    public GameObject playButton;                   // Play 100 Button
    public GameObject Tile1, Tile2, Tile3;          // All 3 Tiles (Chests)
    public bool moneyAddFlag = true;                // Flag to disable Play 100 from doing anything



    // Start is called before the first frame update
    void Start()
    {
        // Restart the amount of credits to zero at the beginning of the game
        creditAmount = 0;
        //Restart the playButton back to show up
        playButton.SetActive(true);

        // Don't show pick a tile text yet
        pickTileText.SetActive(false); 

        // Let all the tiles be unactive until we hit play 
        Tile1.SetActive(false);
        Tile2.SetActive(false);
        Tile3.SetActive(false);
    }


    // Once the game needs to restart to its normal values in order to play again 
    void resetGame()
    {

        Tile1.SetActive(false);
        Tile2.SetActive(false);
        Tile3.SetActive(false);
        pickTileText.SetActive(false);
        moneyAddFlag = true; 

    }

    /*
     * Button -> Play 100
     * Function -> Once play 100 is pressed this will randomly assign a set 
     * then the numbers in that set to a tile value
     */
    void assignValues()
    {

        int[] valueArray = new int[3];      // Array that will hold the sets 
        int setNumber = Random.Range(1, 5); // choose number between 1 - 4
        
        // This switch statement randomly chooses one of the sets to put into the game 
        switch(setNumber)
        {
            case 1:
                valueArray = new int[] { 25, 50, 200 };
                break;
            case 2:
                valueArray = new int[] { 10, 15, 500 };
                break;
            case 3:
                valueArray = new int[] { 5, 5, 1000 };
                break;
            case 4:
                valueArray = new int[] { 0, 10, 1000 };
                break;
            default:
                Debug.Log("Didn't select a case");
                break; 
        }

        // For Debugging to make sure values are printing
        //foreach (int num in valueArray)
        //    Debug.Log(num + " ");

        int assignTileNum = Random.Range(0, 3); //choose number between 0 - 2 to index through array
        // Assign a random value from the array into tile 1
        tile1Value = valueArray[assignTileNum];
        int check1 = assignTileNum;
        // Assign a random value from the array into tile 2
        tile2Value = valueArray[assignTileNum];
        int check2 = assignTileNum;
        // Assign a random value from the array into tile 3
        Tile3Value = valueArray[assignTileNum];
        int check3 = assignTileNum; 

        
        
        // If tile 1 and 2 have the same value, assign a different value 
        while (check1 == check2)
        {
            assignTileNum = Random.Range(0, 3);
            check2 = assignTileNum;
            tile2Value = valueArray[assignTileNum];
        }

        
        // If tile 3 matches any other values 
       while(check3 == check1 ^ check3 == check2)
       {
           assignTileNum = Random.Range(0, 3);
            check3 = assignTileNum;
           Tile3Value = valueArray[assignTileNum];
       }
       
        // After all numbers are set let player pick a tile
        pickTileText.SetActive(true); 

       // For Debugging to make sure values are printing different numbers
        Debug.Log(tile1Value);
        Debug.Log(tile2Value);
        Debug.Log(Tile3Value);

        // Disable the play button so the user may not click it again while already in the round 
        //playButton.SetActive(false);
        moneyAddFlag = false; // disables button without making it disappear 


    }

    // Update amount if tile 1 is is pressed then restart game
    public void clickTile1()
    {
        creditAmount = creditAmount + tile1Value;
        creditText.text = "Credits: " + creditAmount.ToString();
        winText.text = "Win: " + tile1Value.ToString();
        resetGame();
        

    }

    // Update amount if tile 2 is is pressed then restart game
    public void clickTile2()
    {
        creditAmount = creditAmount + tile2Value;
        creditText.text = "Credits: " + creditAmount.ToString();
        winText.text = "Win: " + tile2Value;
        resetGame();
        

    }

    // Update amount if tile 3 is is pressed then restart game
    public void clickTile3()
    {
        creditAmount = creditAmount + Tile3Value;
        creditText.text = "Credits: " + creditAmount.ToString();
        winText.text = "Win: " + Tile3Value;
        resetGame();

    }



    /*
    * Button -> AddMoney
    * GameObj -> addCreditsObj
    * Function -> When clicked it will add 2000 credits to the credits box
    */
    public void onClickAddMoney()
    {

        // When clicked AddMoney Button adds 2000 credits
        creditAmount += 2000;
        //Display current amount of credits 
        creditText.text = "Credits: " + creditAmount.ToString();

    }

    /*
    * Button -> Play100
    * GameObj -> addCreditsObj
    * Function -> This can only be played when Credits has 100 or more. When pressed, Credits will be reduced 
    * by 100 and play beings. 
    */
    public void onClickPlay()
    {
        
        // This can only be played when credits have 100 or more 
        if(creditAmount >= 100 && moneyAddFlag)
        {
            // When pressed, credits will be reduced by 100 
            creditAmount -= 100;
            // Display the current amount of credits
            creditText.text = "Credits: " + creditAmount.ToString();
            // TODO: Start playing the game 
            Tile1.SetActive(true);
            Tile2.SetActive(true);
            Tile3.SetActive(true);

            tile1Value = 0;
            tile2Value = 0;
            Tile3Value = 0;

            assignValues();

        }

    }

    /*
    * Button -> Collect
    * GameObj -> addCreditsObj
    * Function -> This will zero out the Credits box and disable the Play 100 button.
    */
    public void onClickCollect()
    {
        // This will zero out the Credits box
        creditAmount -= creditAmount;
        // Display the amount of credits 
        creditText.text = "Credits: " + creditAmount.ToString();
        // Disable the "Play 100" button 
        // playButton.SetActive(false);
        moneyAddFlag = false;   // This disables the button without making it disappear 

    }




}
