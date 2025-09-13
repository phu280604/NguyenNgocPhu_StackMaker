using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingBrick : MonoBehaviour
{
    #region --- Methods ---

    public GameObject CollectBrick(float posY, Transform parent)
    {
        _collider.enabled = false;
        transform.SetParent(parent);
        transform.position = new Vector3(parent.position.x, posY, parent.position.z);
        
        return gameObject;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private MeshRenderer _spriteRenderer;
    [SerializeField] private Collider _collider;

    #endregion
}
