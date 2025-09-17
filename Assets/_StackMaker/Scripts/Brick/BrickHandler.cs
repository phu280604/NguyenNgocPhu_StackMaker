using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BrickHandler : MonoBehaviour
{
    #region --- Methods ---

    public virtual void Execute(float distance, Transform parent, ref List<GameObject> bricks)
    {
        
    }

    public virtual void Execute(ref List<GameObject> bricks)
    {

    }

    public virtual void Execute()
    {

    }

    #endregion

    #region --- Properties ---

    public bool IsTriggered {  get; protected set; } = false;

    #endregion
}
