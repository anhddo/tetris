using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BlockBoard : MonoBehaviour
{
    public float fallSpeed;
    public int width, height;
    public BlockShapes blockShapes;
    public Block CurrentBlock
    {
        get { return currentBlock; }
    }


    private Block currentBlock;
    private List<CubeInfo> cubes;
    private bool isFalling;

    public void Start()
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
        int maxHeight = -1;
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
        currentBlock.move("Ground");
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

        if (result.Count == 0)
        {
            throw new Exception("highest cols is zero");
        }
        return result;
    }

    public void addRandomBLock()
    {
        if (canAddNewBlock())
        {
            addBlockToBoard(blockShapes.createRandomBlock());
        }
    }

    public void addBlockToBoard(Block block)
    {
        if (!isFalling)
        {
            currentBlock = block;
            block.BlockBoard = this;
            block.Anchor = new CubeIndex(height, width / 2);
            block.calcStopPosition();
            isFalling = true;
        }
    }
    void destroyCurrentBlockAndAddCubesToBlockBoard()
    {
        CubeIndex stopIndex = currentBlock.getStopIndex();
        foreach (var blockCube in currentBlock.getCubes())
        {
            blockCube.index.row += stopIndex.row; blockCube.index.col += stopIndex.col;
            blockCube.transform.parent = transform;
            cubes.Add(blockCube);
        }
        currentBlock.transform.DetachChildren();
        Destroy(currentBlock.gameObject);
    }
    public void Update()
    {
        if (currentBlock != null && currentBlock.stopFalling())
        {
            isFalling = false;
            destroyCurrentBlockAndAddCubesToBlockBoard();
            removeFullRows();
        }
    }

    //void remove
    private void removeFullRows()
    {
        int row = 0;
        while (true)
        {
            if (row >= height)
                break;

            var cubesInRow = cubes.FindAll(x => x.index.row == row);
            if (cubesInRow.Count == 0)
                break;

            if (cubesInRow.Count == width)
            {
                cubesInRow.ForEach(x => Destroy(x.gameObject));
                cubes.RemoveAll(x => x.index.row == row);//delete cubes in a row
                cubes.FindAll(x => x.index.row > row).ForEach(
                    x =>
                        {
                            x.index.row--;
                            Vector3 p = x.transform.position;
                            p.y--;
                            x.transform.position = p;
                        }
                ); //moveCubesAboveDownOneRow
            }
            else
                row++;
        }
    }
}
