using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
        currentBlock = null;
    }
    public int getHighestCubeInCol(int col)
    {
        int maxHeight = Int32.MinValue;
        foreach (var cube in cubes)
        {
            if (cube.index.col == col && maxHeight < cube.index.row)
                maxHeight = cube.index.row;
        }
        return maxHeight;
    }

    public List<int> getHighestCubeInCols(List<int> cols)
    {
        List<int> result = new List<int>();
        foreach (var col in cols)
        {
            result.Add(getHighestCubeInCol(col));
        }
        return result;
    }
    public void addBlockToBoard(Block block)
    {
        if (!isFalling)
        {
            currentBlock = block;
            isFalling = true;
            List<int> cols = block.getListBlockCols();
            List<int> highests = getHighestCubeInCols(cols);
            block.calcStopPosition(highests);
        }
        else
        {
            CubeIndex stopIndex = block.getStopIndex();
            List<CubeInfo> blockCubes = block.getCubes();
            foreach (var blockCube in blockCubes)
            {
                blockCube.index = new CubeIndex(stopIndex.row + blockCube.index.row, stopIndex.col + blockCube.index.col);
            }
        }
    }
    void Update()
    {
        if (currentBlock!=null && currentBlock.stopFalling())
        {
            isFalling = false;
        }
    }
}
