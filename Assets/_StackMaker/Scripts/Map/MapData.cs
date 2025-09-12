using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    #region --- Properties ---

    public int Level { get => level; }

    #endregion

    #region --- Fields ---

    [SerializeField] private int level;

    #endregion
}
