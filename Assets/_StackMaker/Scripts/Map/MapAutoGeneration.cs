using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MapAutoGeneration : MonoBehaviour
{
    #region --- Unity Methods ---

    private void Awake()
    {
        GenerateMap();
    }

    #endregion

    #region --- Methods ---

    private void GenerateMap()
    {
        int rows = _mapMatrix.GetLength(0);
        int cols = _mapMatrix.GetLength(1);

        int cStarted = 0, cBrick = 0, cBlocked = 0, cGround = 0;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                Vector3 pos = new Vector3(x, 0, y);
                cGround = SpawnHandle(_goGround, _goParGround, new Vector3(x, -1, y), "Ground", cGround);
                switch (_mapMatrix[x, y])
                {
                    case 0:
                        cBlocked = SpawnHandle(_goBlockedBrick, _goParBlockedBrick, pos, "Blocked", cBlocked);
                        break;
                    case 1:
                        cBrick = SpawnHandle(_goNormalBrick, _goParNormalBrick, pos, "Brick", cBrick);
                        break;
                    case 2:
                        cStarted = SpawnHandle(_goStartedBrick, _goParStartedBrick, pos, "Started", cStarted);
                        break;
                }
            }
        }
    }

    private int SpawnHandle(GameObject prefab, Transform parent, Vector3 position, string name, int count = 0)
    {
        GameObject goTile = Instantiate(prefab, position, Quaternion.identity, parent);
        goTile.name = $"{name} #{++count}";

        return count;
    }

    #endregion

    #region --- Fields ---

    [Header("--- Parent ---")]
    [SerializeField] private Transform _goParStartedBrick;
    [SerializeField] private Transform _goParNormalBrick;
    [SerializeField] private Transform _goParBlockedBrick;
    [SerializeField] private Transform _goParGround;
    
    [Header("--- Brick ---")]
    [SerializeField] private GameObject _goStartedBrick;
    [SerializeField] private GameObject _goNormalBrick;
    [SerializeField] private GameObject _goBlockedBrick;
    [SerializeField] private GameObject _goGround;
    
    private int[,] _mapMatrix = {
        {0, 0, 0, 0, 0},
        {0, 2, 1, 1, 0},
        {0, 0, 0, 1, 0},
        {0, 1, 1, 1, 0},
        {0, 1, 0, 0, 0},
        {0, 1, 1, 1, 0},
        {0, 1, 0, 1, 0},
        {0, 1, 1, 1, 0},
        {0, 0, 0, 1, 0},
        {0, 0, 1, 1, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 1, 1, 0},
        {0, 0, 0, 1, 0},
        {0, 1, 1, 1, 0},
        {0, 1, 0, 0, 0},
        {0, 1, 1, 1, 0},
        {0, 0, 0, 1, 0},
        {0, 0, 0, 1, 0},
        {0, 0, 1, 1, 0},
        {0, 0, 1, 0, 0},
        {0, 0, 1, 1, 0},
        {0, 0, 0, 1, 0},
        {0, 0, 0, 0, 0},
    };

    #endregion
}
