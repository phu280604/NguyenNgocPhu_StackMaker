using System;
using System.Collections.Generic;
using UnityEngine;

public class MapAutoGeneration : MonoBehaviour
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
        _goBrick = new Dictionary<int, List<GameObject>>();
        GenerateMap();
    }

    private void OnDeSpawn()
    {
        foreach (var item in _goBrick)
        {
            var arr = item.Value.ToArray();
            foreach (var go in arr)
                Destroy(go);
            _goBrick[item.Key].Clear();
        }

    }

    private MapMatrix CheckLevel(int curLvl)
    {
        foreach (var item in _data)
        {
            if (item.lvl == curLvl)
                return item;
        }

        return _data[0];
    }

    private int HashName(string name)
    {
        int hash = 0;

        for (int i = 0; i < name.Length; i++)
            hash += Convert.ToInt32(name[i]);

        return hash % 100;
    }
    private int SpawnHandle(GameObject prefab, Transform parent, Vector3 position, string name, int count = 0)
    {
        GameObject goTile = Instantiate(prefab, position, Quaternion.identity, parent);

        int key = HashName(goTile.name);
        if (_goBrick.ContainsKey(key))
            _goBrick[key].Add(goTile);
        else
            _goBrick[key] = new List<GameObject>() { goTile };

        goTile.name = $"{name} #{_goBrick[key].Count}";

        return count;
    }

    private void GenerateMap(int curLvl = 1)
    {
        MapMatrix map = CheckLevel(curLvl);
        int rows = map.rows;
        int cols = map.cols;

        int cStarted = 0, cBrick = 0, cBlocked = 0, cGround = 0;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                Vector3 pos = new Vector3(x, 0, y);
                cGround = SpawnHandle(_goGround, _goParGround, new Vector3(x, -1, y), "Ground", cGround);
                switch (map.GetValue(x, y))
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

    [SerializeField] private Dictionary<int, List<GameObject>> _goBrick;

    [SerializeField] private List<MapMatrix> _data;

    #endregion
}
