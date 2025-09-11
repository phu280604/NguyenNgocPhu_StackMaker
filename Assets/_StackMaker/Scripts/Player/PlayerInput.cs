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
    }

    public void GetEndInput(ref Vector2 vect)
    {
        if (Input.GetMouseButtonUp(0))
            vect = Input.mousePosition;
    }

    #endregion
}
