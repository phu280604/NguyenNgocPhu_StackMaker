using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    #region --- Methods ---

    public void Rotation(Vector3 rotation)
    {
        _trAvatar.rotation = Quaternion.Euler(rotation);
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private Transform _trAvatar;

    #endregion
}
