using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGamePlay : UICanvas
{
    #region --- unity Methods ---

    public override void BackKey()
    {
        base.BackKey();

        LevelManager.Instance.DespawnPlayer();
        LevelManager.Instance.DespawnMap();
    }

    #endregion

    #region --- Methods ---

    public void ChangeText(int levelCount)
    {
        _txtLevel.text = $"Level {levelCount}";
    }

    public void BackUI()
    {
        GameManager.Instance.ChangeState(EManagerState.MainMenu);
        GameManager.Instance.ChangeScreen();
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private TextMeshProUGUI _txtLevel;

    #endregion
}
