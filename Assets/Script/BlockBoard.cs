using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockBoard : MonoBehaviour
{
    public float fallSpeed;
    public int width, height;

    private Block currentBlock;
    private List<CubeInfo> cubes;
    private bool isFalling;
    void Start()
    {
        cubes = new List<CubeInfo>();
        isFalling = false;
    }
    public List<Vector2> getFullRow()
    {
        return null;
    }
    public List<int > getHighestCubeInCol(int col)
    {
        return null;
    }
    public void addBlockToBoard(Block block)
    {
        if (!isFalling)
        {
            currentBlock = block;
            isFalling = true;
            List<int> cols = block.getListBlockCols();
            block.calcStopPosition();
        }
    }
    void Update()
    {
        if (isFalling)
        {
            currentBlock.transform.Translate(-Vector3.up * fallSpeed);
        }
        if (currentBlock.stopFalling())
        {
            
        }
    }
}
