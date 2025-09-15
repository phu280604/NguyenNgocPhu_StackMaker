using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingBrick : MonoBehaviour
{
    #region --- Methods ---

    public GameObject CollectBrick(float posY, Transform parent)
    {
        _collider.enabled = false;
        Vector3 pos = new Vector3(parent.position.x, posY, parent.position.z);
        GameObject newBrick = Instantiate(this.gameObject, pos, Quaternion.identity, parent);
        _collider.enabled = true;
        IsHide = true;
        _spriteRenderer.enabled = false;

        return newBrick;
    }

    #endregion

    #region --- Properties ---

    public bool IsHide { get; private set; } = false;

    #endregion

    #region --- Fields ---

    [SerializeField] private MeshRenderer _spriteRenderer;
    [SerializeField] private Collider _collider;

    #endregion
}
