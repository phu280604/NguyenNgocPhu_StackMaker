using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Start()
    {
        OnInit();
    }

    #endregion

    #region --- Methods ---

    public virtual void OnInit()
    {
        _rectTransform = GetComponent<RectTransform>();

        float ratio = (float)Screen.height / (float)Screen.width;
        if (_isHandlingRabbitEars)
        {
            if(ratio > 2.1f)
            {
                Vector2 leftBottom = _rectTransform.offsetMin;
                Vector2 rightTop = _rectTransform.offsetMax;
                rightTop.y = -100f;
                _rectTransform.offsetMax = rightTop;
                leftBottom.y = 0f;
                _rectTransform.offsetMin = leftBottom;
                //_offsetY = 100f;
            }
        }

        if (_isWidescreenProcessing)
        {
            ratio = (float)Screen.width / (float)Screen.height;
            if(ratio < 2.1f)
            {
                float ratioDefault = 850 / 1920f;
                float ratioThis = ratio;

                float value = 1 - (ratioThis - ratioDefault);

                float with = _rectTransform.rect.width * value;
                _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, with);
            }
        }
    }

    public virtual void SetUp()
    {
        UIManager.Instance.AddBackUI(this);
        UIManager.Instance.PushBackAction(this, BackKey);
    }

    public virtual void BackKey()
    {
        
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void CloseDirectly()
    {
        gameObject.SetActive(false);
        if (_isDestroyOnClose)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Close(float delayTime)
    {
        Invoke(nameof(CloseDirectly), delayTime);
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private bool _isDestroyOnClose = false;
    [SerializeField] private bool _isHandlingRabbitEars = false;
    [SerializeField] private bool _isWidescreenProcessing = false;

    protected RectTransform _rectTransform;

    //private Animator _animator;

    //private float _offsetY = 0f;

    #endregion
}

