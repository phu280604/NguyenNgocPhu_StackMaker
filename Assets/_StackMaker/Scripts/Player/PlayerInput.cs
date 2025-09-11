using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region --- Methods ---

    public Vector2 GetStartInput() => !Input.GetMouseButtonDown(0) ? Vector2.zero : Input.mousePosition;

    public Vector2 GetEndInput() => !Input.GetMouseButtonUp(0) ? Vector2.zero : Input.mousePosition;

    #endregion
}
