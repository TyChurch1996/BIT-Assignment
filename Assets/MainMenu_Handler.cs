using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_Handler : MonoBehaviour
{

    private object mainMenu;
    private object nameSelect;
    private object charSelect;
    private string playerName;
    private string chosenChar;

    // Start is called before the first frame update
    void Start()
    {
        //get children to canvas object
        mainMenu = gameObject.transform.GetChild(0);
        nameSelect = gameObject.transform.GetChild(1);
        charSelect = gameObject.transform.GetChild(2);

        //get setinactive from parent object to prevent issues with trying to run inactive code
        //leave main menu active
        

        

    }

/* 
 *on button start clicked do this
 * -------------------------------
 * make name select menu active
 * make mainmenu inactive
 * ===============================
 * 
 * on button clicked for next 
 * ---------------------------------
 * make whatever name is in textbox players name and load charselect set this inactive
 * else
 * default to pablo[insert pretzle full name here] and load char select set this inactive
 * ===============================
 * 
 * on character select load
 * ----------------------------------
 * click left or right arrow to move through sprite array list and chose a character sprite
 * 
 *================================
 * 
 * on begin clicked
 * ---------------------------------------
 * store selected name and character
 * load level 1 
 * hide menu
 * load name to new scene
 * spawn player prefab based on selected sprite name string
 *
 * 
 */
}
