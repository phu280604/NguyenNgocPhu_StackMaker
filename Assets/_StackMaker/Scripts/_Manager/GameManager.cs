using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Awake()
    {
        OnInit();
    }

    #endregion

    #region --- Methods ---

    private void OnInit()
    {
        Instance = this;

        _lvl = PlayerPrefs.GetInt(PlayerPrefsName.CURRENT_LEVEL, 1);
        StartCoroutine(SetLevel());
    }

    private IEnumerator SetLevel()
    {
        LevelManager.Instance.SpawnMap();

        yield return new WaitForEndOfFrame();
        LevelManager.Instance.SpawnPlayer();

        yield return new WaitForEndOfFrame();
        _cameraFollowing.SetTarget(LevelManager.Instance.GoPlayer.transform);

        yield return new WaitForEndOfFrame();
    }

    public void ResetLevel()
    {
        _lvl = 1;
    }

    public void NextLevel()
    {
        _lvl++;
        StartCoroutine(SetLevel());
        PlayerPrefs.SetInt(PlayerPrefsName.CURRENT_LEVEL, _lvl);
    }

    #endregion

    #region --- Properties ---

    public int CurrentLevel => _lvl;

    #endregion 

    #region --- Fields ---

    public static GameManager Instance { get; private set; }

    [Header("--- Components ---")]
    [SerializeField] private ObjectFollowing _cameraFollowing;

    [Header("--- Int ---")]
    [SerializeField] private int _lvl;

    #endregion
}
