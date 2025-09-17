using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    #region --- Methods ---

    public void OnPlay()
    {
        GameManager.Instance.ChangeState(EManagerState.Gameplay);
        GameManager.Instance.ChangeScreen();
    }

    #endregion
}
