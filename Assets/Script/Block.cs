using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum BLOCKTYPE { BAR, L, Z, T, SQUARE };
public class Block : MonoBehaviour
{
    BlockBoard blockBoard;
    int width, height;
    List<CubeInfo> cubes;
    CubeIndex stopIndex;
    float currentTime;
    CubeIndex anchor;

    public BLOCKTYPE type;
    public float deltaTime;
    public int Width
    {
        get { return width; }
        set { width = value; }
    }
    public int Height
    {
        get { return height; }
        set { height = value; }
    }
    public List<CubeInfo> Cubes
    {
        get { return cubes; }
    }
    public CubeIndex Anchor
    {
        get { return anchor; }
        set
        {
            anchor = value;
            transform.position = new Vector3(0, anchor.row, anchor.col);
        }
    }
    public BlockBoard BlockBoard
    {
        set { blockBoard = value; }
    }

    public int getWidth()
    {
        return width;
    }


    public Block()
    {
        cubes = new List<CubeInfo>();
    }
    public CubeIndex getStopIndex()
    {
        return stopIndex;
    }
    public List<CubeInfo> getCubes()
    {
        return cubes;
    }
    public void rotate()
    {

        foreach (var cube in cubes)
        {
            cube.changeCubeIndex(width - 1 - cube.index.col, cube.index.row);
        }

        var temp = width;
        width = height;
        height = temp;

        calcStopPosition();
    }


    public List<int> getListBlockCols()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < width; i++)
        {
            result.Add(anchor.col + i);
        }
        if (result.Count == 0)
        {
            throw new Exception("No cols in block");
        }
        return result;
    }
    void moveCol(int x)
    {
        CubeIndex newPos = new CubeIndex(anchor.row, anchor.col + x);
        if (inBlockBoard(newPos))
            Anchor = newPos;
    }
    public void move(string v)
    {
        if (v.Equals("Left"))
        {
            moveCol(-1);
        }
        else if (v.Equals("Right"))
        {
            moveCol(1);
        }
        else if (v.Equals("Ground"))
        {
            Anchor = stopIndex;
        }
        calcStopPosition();
    }

    private bool inBlockBoard(CubeIndex newPos)
    {
        return newPos.col >= 0 && newPos.col <= blockBoard.width - width;
    }

    //distance between peek of ground and lowest cube in the same column, return stop position(in row)
    int findDistance(int col, int groundPeek)
    {
        CubeIndex index = getLowestCubeIndexInRow(col);
        int row = anchor.row + index.row;
        return groundPeek + 1 - index.row;
    }
    //find highest stop position
    public void calcStopPosition()
    {
        List<int> cols = getListBlockCols();
        List<int> highestColsInBoard = blockBoard.getHighestCubeInCols(cols);

        int col = 0;
        int highestStop = Int32.MinValue;
        foreach (var groundPeek in highestColsInBoard)
        {
            var stopRow = findDistance(col, groundPeek);
            if (highestStop < stopRow)
            {
                highestStop = stopRow;
            }
            col++;
        }
        stopIndex = new CubeIndex(highestStop, anchor.col);
    }
    public CubeIndex getLowestCubeIndexInRow(int col)
    {
        int lowest = Int32.MaxValue;
        CubeIndex result = null;
        foreach (var cube in cubes)
        {
            if (cube.index.col == col && lowest > cube.index.row)
            {
                lowest = cube.index.row;
                result = cube.index;
            }
        }
        return result;
    }
    public List<Vector3> getListPositionCube()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (CubeInfo cube in cubes)
        {
            positions.Add(cube.transform.position);
        }
        return positions;
    }
    public bool stopFalling()
    {
        if (stopIndex.row == anchor.row)
        {
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void moveToward()
    {
        if (!stopFalling())
        {
            anchor.row--;
            gameObject.transform.position -= Vector3.up;
        }
    }
    // Use this for initialization
    void Awake()
    {
        cubes = new List<CubeInfo>();
        currentTime = 0.0f;
    }
    void Update()
    {
        if (Time.time > currentTime + deltaTime)
        {
            currentTime = Time.time;
            moveToward();
        }
    }
}
