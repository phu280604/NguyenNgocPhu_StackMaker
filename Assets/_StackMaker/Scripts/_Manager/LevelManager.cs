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

        _goPlayer = _objectSpawning.SpawnObject(_mapAutoGeneration.StartedPos, Quaternion.identity);
    }

    public void SpawnMap()
    {
        _mapAutoGeneration.OnInit();
        _mapAutoGeneration.OnDeSpawn();
        _mapAutoGeneration.GenerateMap();
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

    private Vector3 _startedPos;

    #endregion
}
