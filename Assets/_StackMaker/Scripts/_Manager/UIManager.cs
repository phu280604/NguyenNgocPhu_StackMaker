using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region --- Methods ---

    public void ShowCurrentLevel(int lvl)
    {
        _curLvlText.text = $"Level {lvl}";
        //Debug.Log($"Level {lvl}");
    }

    #endregion

    #region --- Fields ---

    public static UIManager Instance { get; private set; }

    [SerializeField] private TMP_Text _curLvlText;

    #endregion
}
