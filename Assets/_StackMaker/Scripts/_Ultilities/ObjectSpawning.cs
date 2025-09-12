using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawning : MonoBehaviour
{
    #region --- Methods ---

    public GameObject SpawnObject()
    {
        return Instantiate(_objectPrefab, Vector3.zero, Quaternion.identity, _parentObject);
    }

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        return Instantiate(_objectPrefab, position, rotation, _parentObject);
    }

    #endregion

    #region --- Fields ---

    [Header("--- Transform ---")]
    [SerializeField] private Transform _parentObject;

    [Header("--- GameObject ---")]
    [SerializeField] private GameObject _objectPrefab;

    #endregion
}
