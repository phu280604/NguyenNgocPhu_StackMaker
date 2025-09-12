using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowing : MonoBehaviour
{
    #region --- Unity Methods ---

    private void LateUpdate()
    {
        FollowTarget();
    }

    #endregion

    #region --- Methods ---

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    // Make the object follow the target with offset and speed
    private void FollowTarget()
    {
        if (_target == null) return;

        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _speed * Time.deltaTime);
    }

    #endregion

    #region --- Fields ---

    [Header("--- Transform ---")]
    [SerializeField] private Transform _target;

    [Header("--- Vector ---")]
    [SerializeField] private Vector3 _offset;

    [Header("--- Float ---")] 
    [SerializeField] private float _speed;

    #endregion
}
