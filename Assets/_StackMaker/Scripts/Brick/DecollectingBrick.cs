using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DecollectingBrick : BrickHandler
{
    #region --- Overrides ---

    public override void Execute(ref List<GameObject> bricks)
    {
        base.Execute(ref bricks);

        // Stop when Under 0 Brick.
        if (bricks.Count <= 0 || IsTriggered) return;


        // Copy Lastest Brick.
        GameObject lastBrick = bricks[bricks.Count - 1];

        // Destroy & Remove LastestBrick.
        Destroy(lastBrick);
        bricks.RemoveAt(bricks.Count - 1);

        // Triggered
        IsTriggered = true;

        // Open Sprite.
        _spriteRenderer.enabled = true;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private MeshRenderer _spriteRenderer;
    [SerializeField] private Collider _collider;

    #endregion
}
