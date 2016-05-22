using UnityEngine;
using System.Collections;
public class CubeIndex
{
    public int row, col;
    public CubeIndex(int r, int c)
    {
        row = r; col = c;
    }
}
public class CubeInfo : MonoBehaviour
{
    public CubeIndex index;
}
