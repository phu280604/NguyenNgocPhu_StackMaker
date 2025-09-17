using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapMatrix))]
public class MapMatrixEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapMatrix map = (MapMatrix)target;

        EditorGUILayout.LabelField("--- Level ---", EditorStyles.boldLabel);
        map.lvl = EditorGUILayout.IntField("Lvl", map.lvl);

        EditorGUILayout.LabelField("--- Direction Map ---", EditorStyles.boldLabel);
        map.direction = (EDirection)EditorGUILayout.EnumPopup("Direction", map.direction);

        EditorGUILayout.LabelField("--- Rows and Columns ---", EditorStyles.boldLabel);
        map.rows = EditorGUILayout.IntField("Rows", map.rows);
        map.cols = EditorGUILayout.IntField("Cols", map.cols);

        EditorGUILayout.LabelField("--- Matrix ---", EditorStyles.boldLabel);
        int newSize = map.rows * map.cols;
        if (map.data == null || map.data.Length != newSize)
        {
            map.data = new int[newSize];
        }

        // Vẽ dạng bảng
        for (int r = 0; r < map.rows; r++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int c = 0; c < map.cols; c++)
            {
                int index = r * map.cols + c;
                map.data[index] = EditorGUILayout.IntField(map.data[index], GUILayout.Width(50));
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(map);
        }
    }
}
