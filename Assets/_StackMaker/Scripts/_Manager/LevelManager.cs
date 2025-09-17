using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    #region --- Methods ---

    public void OnInit()
    {
        if(_lvl == 0)
            _lvl = PlayerPrefs.GetInt(PlayerPrefsName.CURRENT_LEVEL, 1);

        StartCoroutine(SetLevel());
    }

    private IEnumerator SetLevel()
    {
        SpawnMap();

        yield return new WaitForEndOfFrame();
        SpawnPlayer();

        yield return new WaitForEndOfFrame();
        _cameraFollowing.SetTarget(LevelManager.Instance.GoPlayer.transform);
        UIManager.Instance.GetUI<UIGamePlay>().ChangeText(_lvl);

        yield return new WaitForEndOfFrame();
    }

    public void ResetLevel()
    {
        _lvl = 1;
    }

    public void NextLevel()
    {
        _lvl++;
        PlayerPrefs.SetInt(PlayerPrefsName.CURRENT_LEVEL, _lvl);
    }

    public void SpawnPlayer()
    {
        DespawnPlayer();
        Vector3 startedPos = new Vector3(_mapAutoGeneration.GoStarted.transform.position.x, 0.7f, _mapAutoGeneration.GoStarted.transform.position.z);
        _goPlayer = _objectSpawning.SpawnObject(startedPos, Quaternion.identity);

        PlayerController plCtrl = _goPlayer.GetComponent<PlayerController>();

        plCtrl.RotationComp.Rotation(_direction);
        plCtrl.CollectBrick(_mapAutoGeneration.GoStarted.GetComponent<CollectingBrick>(), false);
    }

    public void DespawnPlayer()
    {
        if (_goPlayer != null)
            Destroy(_goPlayer);
    }

    public void SpawnMap()
    {
        _mapAutoGeneration.OnInit();
        DespawnMap();
        _mapAutoGeneration.GenerateMap();
        GetRotation(_mapAutoGeneration.Rotation);
    }

    public void DespawnMap()
    {
        _mapAutoGeneration.OnDespawn();
    }

    private void GetRotation(EDirection dir, int angle = 60)
    {
        switch (dir)
        {
            case EDirection.NONE:
                _direction = Vector3.zero;
                break;
            case EDirection.FORWARD:
                _direction = Vector3.down * angle * 2;
                break;
            case EDirection.BACKWARD:
                _direction = Vector3.up * angle;
                break;
            case EDirection.LEFT:
                _direction = Vector3.up * angle * 2;
                break;
            case EDirection.RIGHT:
                _direction = Vector3.down * angle;
                break;
        }
    }

    #endregion

    #region --- Properties ---

    public GameObject GoPlayer => _goPlayer;
    public int CurrentLevel => _lvl;

    #endregion

    #region --- Fields ---

    [Header("--- Components ---")]
    [SerializeField] private ObjectSpawning _objectSpawning;
    [SerializeField] private MapAutoGeneration _mapAutoGeneration;
    [SerializeField] private ObjectFollowing _cameraFollowing;

    [Header("--- Int ---")]
    [SerializeField] private int _lvl;

    private GameObject _goPlayer;

    private Vector3 _direction;

    #endregion
}
