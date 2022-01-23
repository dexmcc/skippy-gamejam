using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Copy pastable code - Make a New > C# Script and call it setCursor.cs
//Then, add this code in.

public class setCursor : MonoBehaviour
{
    // You must set the cursor in the inspector.
    public Texture2D crosshair;

    void Start()
    {

        //set the cursor origin to its centre. (default is upper left corner)
        Vector2 cursorOffset = new Vector2(crosshair.width / 2, crosshair.height / 2);

        //Sets the cursor to the Crosshair sprite with given offset 
        //and automatic switching to hardware default if necessary
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
    }
}
