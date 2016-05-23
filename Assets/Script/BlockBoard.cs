using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BlockBoard : MonoBehaviour
{
    public float fallSpeed;
    public int width, height;
    public Block blockPrefabs;

    private Block currentBlock;
    private List<CubeInfo> cubes;
    private bool isFalling;
    void Start()
    {
        cubes = new List<CubeInfo>();
        isFalling = false;
        currentBlock = null;
    }

    public bool canAddNewBlock()
    {
        return !isFalling;
    }

    public int getHighestCubeInCol(int col)
    {
        int maxHeight = 0;
        foreach (var cube in cubes)
        {
            if (cube.index.col == col && maxHeight < cube.index.row)
                maxHeight = cube.index.row;
        }
        return maxHeight;
    }

    public void rotateBlock()
    {
        currentBlock.rotate();
    }

    public void moveBlockToTheGround()
    {
        //todo
    }

    public void moveFallingBlock(string v)
    {
        currentBlock.move(v);
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

    public void addRandomBLock()
    {
        if (canAddNewBlock())
        {
            Block block = Instantiate(blockPrefabs, Vector3.zero, Quaternion.identity) as Block;
            block.setReferenceBLockBoard(this);
            addBlockToBoard(block);
        }
    }

    void addBlockToBoard(Block block)
    {
        if (!isFalling)
        {
            currentBlock = block;
            block.Anchor = new CubeIndex(height, width / 2);
            block.calcStopPosition();
            isFalling = true;
        }
    }
    void Update()
    {
        if (currentBlock != null && currentBlock.stopFalling())
        {
            isFalling = false;
            CubeIndex stopIndex = currentBlock.getStopIndex();
            foreach (var blockCube in currentBlock.getCubes())
            {
                blockCube.index.row += stopIndex.row; blockCube.index.col += stopIndex.col;
                cubes.Add(blockCube);
            }
            currentBlock.transform.DetachChildren();
            Destroy(currentBlock.gameObject);
        }
    }
}
