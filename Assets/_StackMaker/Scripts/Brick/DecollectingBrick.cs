using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DecollectingBrick : MonoBehaviour
{
    #region --- Methods ---

    public void DecollectBrick(List<GameObject> bricks)
    {
        if(bricks.Count <= 0 || IsShow) return;

        IsShow = true;
        _spriteRenderer.enabled = true;

        GameObject lastBrick = bricks[bricks.Count - 1];

        
        Destroy(lastBrick);
        bricks.RemoveAt(bricks.Count - 1);
    }

    #endregion

    #region --- Properties ---

    public bool IsShow { get; private set; }

    #endregion

    #region --- Fields ---

    [SerializeField] private MeshRenderer _spriteRenderer;
    [SerializeField] private Collider _collider;

    #endregion
}
