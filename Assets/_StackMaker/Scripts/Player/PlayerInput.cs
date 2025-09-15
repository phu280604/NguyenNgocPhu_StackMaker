using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region --- Methods ---

    public void GetStartInput(ref Vector2 vect)
    {
        if (Input.GetMouseButtonDown(0))
            vect = Input.mousePosition;

        if(Input.touchCount > 0)
        { 
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
                vect = touch.position;
        }
    }

    public void GetEndInput(ref Vector2 vect)
    {
        if (Input.GetMouseButtonUp(0))
            vect = Input.mousePosition;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
                vect = touch.position;
        }
    }

    #endregion
}
