using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region --- Unity Methods ---

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        Vector2 dir = GetDirection();
        if(dir != Vector2.zero) Debug.Log(dir);

    }

    #endregion

    #region --- Methods ---

    private void OnInit()
    {
        _eDirect = EDirection.NONE;
    }

    private Vector2 GetDirection()
    {
        _sPoint = _input.GetStartInput();
        _ePoint = _input.GetEndInput();

        return (_ePoint - _sPoint).normalized;
    }

    private void DetermineDirection()
    {
        switch (_eDirect)
        {
            case EDirection.NONE:
                _moveDir = Vector3.zero;
                break;
            case EDirection.FORWARD:
                _moveDir = Vector3.forward;
                break;
            case EDirection.BACKWARD:
                _moveDir = Vector3.back;
                break;
            case EDirection.LEFT:
                _moveDir = Vector3.left;
                break;
            case EDirection.RIGHT:
                _moveDir = Vector3.right;
                break;
        }
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private PlayerInput _input;

    private EDirection _eDirect;

    [SerializeField] private Vector2 _sPoint;
    [SerializeField] private Vector2 _ePoint;

    [SerializeField] private Vector3 _moveDir;

    #endregion
}
