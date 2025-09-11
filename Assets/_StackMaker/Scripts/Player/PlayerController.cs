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
        DetermineDirection();
        MoveHandle();

        CheckBrick();
    }

    #endregion

    #region --- Methods ---

    private void OnInit()
    {
        _eDirect = EDirection.NONE;
        _isMove = false;
    }

    private Vector2 GetDirection()
    {

        _input.GetStartInput(ref _sPoint);
        _input.GetEndInput(ref _ePoint);

        if (_ePoint == Vector2.zero || _isMove) return Vector2.zero;

        //Debug.Log("Start Point: " + _sPoint + " End Point: " + _ePoint);

        return (_ePoint - _sPoint).normalized;
    }

    private void ResetInput()
    {
        _sPoint = Vector2.zero;
        _ePoint = Vector2.zero;
    }

    private void CalculateDirection()
    {
        Vector2 dirNor = GetDirection();

        if (dirNor == Vector2.zero || _isMove) return;

        Debug.Log("Direction: " + dirNor);
        ResetInput();

        float intX = Mathf.Abs(dirNor.x);
        float intY = Mathf.Abs(dirNor.y);

        if(intX <= intY)
        {
            if (Mathf.Sign(dirNor.y) > 0)
                _eDirect = EDirection.FORWARD;
            else
                _eDirect = EDirection.BACKWARD;
        }
        else if (intX > intY)
        {
            if (Mathf.Sign(dirNor.x) > 0)
                _eDirect = EDirection.RIGHT;
            else
                _eDirect = EDirection.LEFT;
        }
    }

    private void DetermineDirection()
    {
        if (_isMove) return;

        CalculateDirection();
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
        if(_eDirect != EDirection.NONE)
            _isMove = true;
    }

    private void CheckBrick()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + _moveDir * 0.8f, Color.red);
        if (!Physics.Raycast(transform.position, _moveDir, out hit, 0.8f)) return;

        if(hit.collider.CompareTag("BlockedBrick"))
        {
            _eDirect = EDirection.NONE;
            _isMove = false;
        }
    }

    private void MoveHandle()
    {
        if(!_isMove) return;

        transform.Translate(_moveDir * Time.deltaTime * 5f);
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private GameObject _sprite;

    [SerializeField] private PlayerInput _input;

    [SerializeField] private Vector2 _sPoint;
    [SerializeField] private Vector2 _ePoint;

    [SerializeField] private Vector3 _moveDir;

    private EDirection _eDirect;

    [SerializeField] private bool _isMove;

    #endregion
}
