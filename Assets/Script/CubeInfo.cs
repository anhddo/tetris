using UnityEngine;
using System.Collections;
public class CubeIndex
{
    public int row, col;
    public CubeIndex(int r, int c)
    {
        row = r; col = c;
    }
    public override string ToString()
    {
        return "CUBE INDEX: " + row + "," + col;
    }
	public override int GetHashCode()
	{
		return 0;
	}
    public override bool Equals(object obj)
    {
        var o = obj as CubeIndex;
        return o.row == row && o.col == col;
    }
}
public class CubeInfo : MonoBehaviour
{
    public CubeIndex index;
}
