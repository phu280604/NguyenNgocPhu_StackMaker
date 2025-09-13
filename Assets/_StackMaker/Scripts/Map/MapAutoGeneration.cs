using System;
using System.Collections.Generic;
using UnityEngine;

public class MapAutoGeneration : MonoBehaviour
{
    #region --- Methods ---

    public void OnInit()
    {
        if (_goBricks != null) return;
        _goBricks = new Dictionary<int, List<GameObject>>();
    }

    public void OnDeSpawn()
    {
        foreach (var item in _goBricks)
        {
            int highestCount = item.Value.Count;
            for(int i = highestCount - 1; i >= 0; i--)
            {
                GameObject go = item.Value[i];
                if (go != null)
                {
                    item.Value.RemoveAt(i);
                    Destroy(go);
                }
            }

            _goBricks[item.Key].Clear();
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
    private void SpawnHandle(GameObject prefab, Transform parent, Vector3 position, string name)
    {
        GameObject goTile = Instantiate(prefab, position, Quaternion.identity, parent);

        int key = HashName(goTile.name);
        if (_goBricks.ContainsKey(key))
            _goBricks[key].Add(goTile);
        else
            _goBricks[key] = new List<GameObject>() { goTile };

        goTile.name = $"{name} #{_goBricks[key].Count}";
    }

    public void GenerateMap()
    {
        if(GameManager.Instance.CurrentLevel > _data.Count)
            GameManager.Instance.ResetLevel();

        MapMatrix map = CheckLevel(GameManager.Instance.CurrentLevel);

        Rotation = map.direction;

        int rows = map.rows;
        int cols = map.cols;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                Vector3 pos = new Vector3(x, 0, y);
                switch (map.GetValue(x, y))
                {
                    case 0:
                        SpawnHandle(_goBlockedBrick, _goParBlockedBrick, pos, "Blocked");
                        break;
                    case 1:
                        SpawnHandle(_goNormalBrick, _goParNormalBrick, pos, "Brick");
                        SpawnHandle(_goGround, _goParGround, new Vector3(x, -1, y), "Ground");
                        break;
                    case 2:
                        StartedPos = pos;
                        SpawnHandle(_goStartedBrick, _goParPointBrick, pos, "Started");
                        break;
                    case 3:
                        SpawnHandle(_goEndBrick, _goParPointBrick, pos, "End");
                        break;
                }
            }
        }
    }

    #endregion

    #region --- Properties ---

    public Vector3 StartedPos { get; private set; }
    public EDirection Rotation { get; private set; }

    #endregion

    #region --- Fields ---

    [Header("--- Parent ---")]
    [SerializeField] private Transform _goParPointBrick;
    [SerializeField] private Transform _goParNormalBrick;
    [SerializeField] private Transform _goParBlockedBrick;
    [SerializeField] private Transform _goParGround;
    
    [Header("--- Brick ---")]
    [SerializeField] private GameObject _goStartedBrick;
    [SerializeField] private GameObject _goEndBrick;
    [SerializeField] private GameObject _goNormalBrick;
    [SerializeField] private GameObject _goBlockedBrick;
    [SerializeField] private GameObject _goGround;

    private Dictionary<int, List<GameObject>> _goBricks;

    [Header("--- Matrix Data ---")]
    [SerializeField] private List<MapMatrix> _data;

    #endregion
}
