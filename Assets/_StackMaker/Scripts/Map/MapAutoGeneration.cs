using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapAutoGeneration : MonoBehaviour
{
    #region --- Methods ---

    public void OnInit()
    {
        if (_goBricks != null) return;
        _goBricks = new Dictionary<int, List<GameObject>>();
    }

    public void OnDespawn()
    {
        if(_goBricks == null || _goBricks.Count <= 0) return;

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
    private GameObject SpawnHandle(GameObject prefab, Transform parent, Vector3 position, string name)
    {
        GameObject goTile = Instantiate(prefab, position, Quaternion.identity, parent);

        int key = HashName(name);
        if (_goBricks.ContainsKey(key))
            _goBricks[key].Add(goTile);
        else
            _goBricks[key] = new List<GameObject>() { goTile };

        goTile.name = $"{name} #{_goBricks[key].Count}";

        return goTile;
    }

    public void GenerateMap()
    {
        if(LevelManager.Instance.CurrentLevel > _data.Count)
            LevelManager.Instance.ResetLevel();

        MapMatrix map = CheckLevel(LevelManager.Instance.CurrentLevel);

        Rotation = map.direction;

        int rows = map.rows;
        int cols = map.cols;

        int key = 0;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                Vector3 pos = new Vector3(x, 0, y);
                switch (map.GetValue(x, y))
                {
                    case -30:
                        SpawnHandle(_goEndBrick, _goParPointBrick, pos, "End");
                        break;
                    case -15:
                        SpawnHandle(_goStartedBrick, _goParPointBrick, pos, "Started");
                        key = HashName("Started");
                        GoStarted = _goBricks[key].First();

                        SpawnHandle(_goGround, _goParGround, new Vector3(x, -1, y), "Ground");
                        break;
                    case 1:
                        GameObject goBrick = SpawnHandle(_goBlockedBrick, _goParBlockedBrick, pos, "Blocked");
                        goBrick.GetComponent<BoxCollider>().enabled = false;
                        break;
                    case 3:
                        SpawnHandle(_goBlockedBrick, _goParBlockedBrick, pos, "Blocked");
                        break;
                    case 6:
                        SpawnHandle(_goNormalBrick, _goParNormalBrick, pos, "Brick");
                        SpawnHandle(_goGround, _goParGround, new Vector3(x, -1, y), "Ground");
                        break;
                    case 9:
                        SpawnHandle(_goBridgeBrick, _goParBridgeBrick, new Vector3(x, -0.2f, y), "Bridge");
                        break;
                    case 10:
                        key = HashName("EndLine");
                        int num = 0;

                        if (_goBricks.ContainsKey(key))
                            num = _goBricks[key].Count;


                        Vector3 dir = new Vector3(x, 0, y + (2 * num));

                        SpawnHandle(_goEndLine, _goParEndLine, dir, "EndLine");
                        break;
                    case 11:
                        key = HashName("EndLine");
                        int num1 = _goBricks.ContainsKey(key) ? _goBricks[key].Count : 0;
                        Vector3 dir1 = new Vector3(x, -0.2f, y + (2 * num1));
                        
                        GameObject obj = SpawnHandle(_goTreasure, _goParTreasure, dir1, "Treasure");
                        break;
                }
            }
        }
    }

    #endregion

    #region --- Properties ---

    public GameObject GoStarted { get; private set; }
    public EDirection Rotation { get; private set; }

    #endregion

    #region --- Fields ---

    [Header("--- Parent ---")]
    [SerializeField] private Transform _goParPointBrick;
    [SerializeField] private Transform _goParNormalBrick;
    [SerializeField] private Transform _goParBridgeBrick;
    [SerializeField] private Transform _goParBlockedBrick;
    [SerializeField] private Transform _goParGround;
    [SerializeField] private Transform _goParEndLine;
    [SerializeField] private Transform _goParTreasure; 

    [Header("--- Brick ---")]
    [SerializeField] private GameObject _goStartedBrick;
    [SerializeField] private GameObject _goEndBrick;
    [SerializeField] private GameObject _goNormalBrick;
    [SerializeField] private GameObject _goBridgeBrick;
    [SerializeField] private GameObject _goBlockedBrick;
    [SerializeField] private GameObject _goGround;
    [SerializeField] private GameObject _goEndLine;
    [SerializeField] private GameObject _goTreasure;

    private Dictionary<int, List<GameObject>> _goBricks;

    [Header("--- Matrix Data ---")]
    [SerializeField] private List<MapMatrix> _data;

    #endregion
}
