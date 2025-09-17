using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : Singleton<UIManager>
{
    #region --- Methods ---

    #region -- Canvas --
    /// <summary>
    /// Open UI Canvas
    /// </summary>
    /// <typeparam name="T">  </typeparam>
    /// <returns></returns>
    public T OpenUI<T>() where T : UICanvas
    {
        UICanvas uiCanvas = GetUI<T>();

        uiCanvas.SetUp();
        uiCanvas.Open();

        return uiCanvas as T;
    }

    public void CloseUI<T>() where T : UICanvas
    {
        if (IsOpened<T>())
        {
            GetUI<T>().CloseDirectly();
        }
    }

    public void CloseAll()
    {
        foreach(var item in _uiCanvases)
        {
            if(item.Value != null && item.Value.gameObject.activeInHierarchy)
            {
                item.Value.CloseDirectly();
            }
        }
    }

    public bool IsOpened<T>() where T : UICanvas
    {
        return IsLoaded<T>() && _uiCanvases[typeof(T)].gameObject.activeInHierarchy;
    }

    public bool IsLoaded<T>() where T : UICanvas
    {
        System.Type type = typeof(T);
        return _uiCanvases.ContainsKey(type) && _uiCanvases[type] != null;
    }

    public T GetUI<T>() where T : UICanvas
    {
        if (!IsLoaded<T>())
        {
            UICanvas canvas = Instantiate(GetUIPrefab<T>(), _uiParent);
            _uiCanvases[typeof(T)] = canvas;
        }

        return _uiCanvases[typeof(T)] as T;
    }

    public T GetUIPrefab<T>() where T : UICanvas
    {
        if (!_uiPrefabs.ContainsKey(typeof(T)))
        {
            for(int i = 0; i < _uiResource.Length; i++)
            {
                if (_uiResource[i] is T)
                {
                    _uiPrefabs[typeof(T)] = _uiResource[i];
                    break;
                }
            }
        }

        return _uiPrefabs[typeof(T)] as T;
    }
    #endregion

    #region -- Back Button --

    public void PopBackAction()
    {
        if(BackTopUI != null && backCanvas.Count > 1)
        {
            BackActionEvents[BackTopUI]?.Invoke();
            UICanvas preTopUI =  BackTopUI;

            BackActionEvents.Remove(BackTopUI);
            RemoveBackUI(BackTopUI);

            preTopUI.CloseDirectly();
            BackTopUI.Open();
        }
    }

    public void PushBackAction(UICanvas canvas, UnityAction action)
    {
        if (!BackActionEvents.ContainsKey(canvas))
        {
            BackActionEvents.Add(canvas, action);
        }
    }

    public void AddBackUI(UICanvas canvas)
    {
        if (!backCanvas.Contains(canvas))
        {
            backCanvas.Add(canvas);
        }
    }

    public void RemoveBackUI(UICanvas canvas)
    {
        backCanvas.Remove(canvas);
    }

    #endregion

    #endregion

    #region --- Properties ---

    public UICanvas BackTopUI
    {
        get
        {
            UICanvas canvas = null;
            if(backCanvas.Count > 0)
            {
                canvas = backCanvas[backCanvas.Count - 1];
            }

            return canvas;
        }
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private UICanvas[] _uiResource;

    private Dictionary<System.Type, UICanvas> _uiPrefabs = new Dictionary<System.Type, UICanvas>();
    private Dictionary<System.Type, UICanvas> _uiCanvases = new Dictionary<System.Type, UICanvas>();

    private Dictionary<UICanvas, UnityAction> BackActionEvents = new Dictionary<UICanvas, UnityAction>();
    private List<UICanvas> backCanvas = new List<UICanvas>();

    [SerializeField] private Transform _uiParent;

    #endregion
}
