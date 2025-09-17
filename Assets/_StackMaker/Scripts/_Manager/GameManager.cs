using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region --- Unity Methods ---

    private void Awake()
    {
        OnInit();
    }

    #endregion

    #region --- Methods ---

    private void OnInit()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        // Nếu muốn cố định, không cho xoay
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        ChangeState(EManagerState.MainMenu);
        ChangeScreen();
    }

    public void ChangeState(EManagerState newState)
    {
        _state = newState;
    }

    public bool IsState(EManagerState state) => _state == state;

    public void ChangeScreen()
    {
        switch (_state)
        {
            case EManagerState.MainMenu:
                UIManager.Instance.OpenUI<UIMainMenu>();
                if (UIManager.Instance.BackTopUI is UIMainMenu) break;

                UIManager.Instance.PopBackAction();
                break;
            case EManagerState.Gameplay:
                UIManager.Instance.BackTopUI?.CloseDirectly();
                UIManager.Instance.OpenUI<UIGamePlay>();
                LevelManager.Instance.OnInit();
                break;
        }
    }

    #endregion 

    #region --- Fields ---

    private EManagerState _state;

    #endregion
}
