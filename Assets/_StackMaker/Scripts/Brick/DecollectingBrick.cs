using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecollectingBrick : MonoBehaviour
{
    #region --- Methods ---

    public void DecollectBrick(List<GameObject> bricks)
    {
        if(bricks.Count <= 0) return;

        _collider.enabled = false;
        _spriteRenderer.enabled = true;

        GameObject lastBrick = bricks[bricks.Count - 1];

        bricks.RemoveAt(bricks.Count - 1);
        lastBrick.SetActive(false);
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private MeshRenderer _spriteRenderer;
    [SerializeField] private Collider _collider;

    #endregion
}
