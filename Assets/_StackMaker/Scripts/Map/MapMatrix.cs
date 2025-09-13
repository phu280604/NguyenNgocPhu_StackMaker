using UnityEngine;

[CreateAssetMenu(fileName = "MapMatrix", menuName = "Custom/Map Matrix", order = 1)]
public class MapMatrix : ScriptableObject
{
    // Ví dụ: lưu map bằng ma trận số nguyên
    public EDirection direction; // Hướng xây dựng
    public int lvl;
    public int rows;
    public int cols;

    // Mảng 2 chiều không hiển thị được trực tiếp
    // Ta lưu thành mảng 1 chiều
    public int[] data;

    public int GetValue(int r, int c) => data[r * cols + c];
}
