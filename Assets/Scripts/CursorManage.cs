using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    List<Texture2D> cursors;

    public void SetCursorManager(string cursorName)
    {
        Texture2D currentCursor = null;
        for(int i = 0; i < cursors.Count; i++) 
        {
            if (cursors[i].name == cursorName) 
                currentCursor = cursors[i];
        }
        
        if(currentCursor != null)
            Cursor.SetCursor(currentCursor, new Vector2(currentCursor.width / 2, currentCursor.height / 2), CursorMode.Auto);
    }
}
