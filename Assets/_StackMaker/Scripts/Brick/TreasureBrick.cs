using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBrick : BrickHandler
{
    #region --- Overrides ---

    public override void Execute()
    {
        base.Execute();

        _closingChest.SetActive(false);
        _openingChes.SetActive(true);

        IsTriggered = true;
    }

    #endregion

    #region --- Fields ---

    [SerializeField] private GameObject _closingChest;
    [SerializeField] private GameObject _openingChes;

    #endregion
}
