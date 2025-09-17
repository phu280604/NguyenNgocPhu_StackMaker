using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWining : UICanvas
{
    #region --- Overrides ---

    public override void OnInit()
    {
        if(!isTrigger)
            base.OnInit();

        LevelManager.Instance.NextLevel();

        LevelManager.Instance.DespawnPlayer();
        LevelManager.Instance.DespawnMap();

        isTrigger = true;
    }

    #endregion

    #region --- Methods ---

    public void NextLevel()
    {
        GameManager.Instance.ChangeState(EManagerState.Gameplay);
        UIManager.Instance.PopBackAction();
        GameManager.Instance.ChangeScreen();
    }

    public void BackToMenu()
    {
        GameManager.Instance.ChangeState(EManagerState.MainMenu);
        GameManager.Instance.ChangeScreen();
    }

    #endregion

    #region --- Fields ---

    private bool isTrigger = false;

    #endregion
}
