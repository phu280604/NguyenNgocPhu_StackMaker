using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    #region --- Methods ---

    public void SpawnPlayer()
    {
        if (_goPlayer != null)
            Destroy(_goPlayer);

        Vector3 startedPos = new Vector3(_mapAutoGeneration.StartedPos.x, 0.7f, _mapAutoGeneration.StartedPos.z);
        _goPlayer = _objectSpawning.SpawnObject(startedPos, Quaternion.identity);
        _goPlayer.GetComponent<PlayerRotation>().Rotation(_direction);
    }

    public void SpawnMap()
    {
        _mapAutoGeneration.OnInit();
        _mapAutoGeneration.OnDeSpawn();
        _mapAutoGeneration.GenerateMap();
        GetRotation(_mapAutoGeneration.Rotation);
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

    #endregion

    #region --- Fields ---

    public static LevelManager Instance { get; private set; }

    [Header("--- Components ---")]
    [SerializeField] private ObjectSpawning _objectSpawning;
    [SerializeField] private MapAutoGeneration _mapAutoGeneration;

    private GameObject _goPlayer;

    private Vector3 _direction;

    #endregion
}
