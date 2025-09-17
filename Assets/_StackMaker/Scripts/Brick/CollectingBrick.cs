using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingBrick : BrickHandler
{
    #region --- Overrides ---

    public override void Execute(float distance, Transform parent, ref List<GameObject> bricks)
    {
        base.Execute(distance, parent, ref bricks);

        // Close Collider.
        _collider.enabled = false;

        // Spawn brick with parent is Player.
        Vector3 pos = new Vector3(parent.position.x, distance, parent.position.z);
        GameObject newBrick = Instantiate(this.gameObject, pos, Quaternion.identity, parent);
        bricks.Add(newBrick);

        // Open Collider - Close Sprite.
        _collider.enabled = true;
        _spriteRenderer.enabled = false;

        // Triggered.
        IsTriggered = true;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private MeshRenderer _spriteRenderer;
    [SerializeField] private Collider _collider;

    #endregion
}
