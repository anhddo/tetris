﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum BLOCKTYPE { BAR, L, Z, T, SQUARE };
public class Block : MonoBehaviour
{
    public CubeInfo cubePrefabs;
    public BLOCKTYPE type;
    public float deltaTime;
    public bool flip;
    public CubeIndex Anchor
    {
        get { return anchor; }
        set
        {
            anchor = value;
            if (cubePrefabs != null)
                gameObject.transform.position = new Vector3(0, anchor.row, anchor.col);
        }
    }

    private BlockBoard blockBoard;

    public void setReferenceBLockBoard(BlockBoard blockBoard)
    {
        this.blockBoard = blockBoard;
    }

    CubeIndex anchor;

    public int getWidth()
    {
        return width;
    }

    int width, height;
    int rotationAngle;
    List<CubeInfo> cubes;
    CubeIndex stopIndex;
    private float currentTime;

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
            changeCubeIndex(cube, width - 1 - cube.index.col, cube.index.row);
        }

        var temp = width;
        width = height;
        height = temp;

        calcStopPosition();
    }

    void changeCubeIndex(CubeInfo cube, int row, int col)
    {
        cube.index = new CubeIndex(row, col);
        //if prefab is null, it's used for testing
        if (cubePrefabs != null)
        {
            cube.transform.parent = gameObject.transform;
            cube.transform.localPosition = new Vector3(0, row, col);
        }
    }
    CubeInfo initNewCube(int row, int col)
    {
        CubeInfo instance = null;
        //for testing
        if (cubePrefabs == null)
            instance = new CubeInfo();
        //
        //for rendering
        else
            instance = Instantiate(cubePrefabs, Vector3.zero, Quaternion.identity) as CubeInfo;
        changeCubeIndex(instance, row, col);
        return instance;
    }
    void initBlockByString(string[] blockString)
    {
        height = blockString.Length;
        width = blockString[0].Length;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (blockString[i][j] == '*')
                {
                    cubes.Add(initNewCube(i, j));
                }
            }
        }
    }
    public void initBlock()
    {
        cubes.Clear();
        string[] Bar = new string[] { "****" };
        string[] L = new string[] { "*--",
                                    "***"
        };
        string[] Z = new string[] { "**-",
                                    "-**"
        };
        string[] T = new string[] { "***",
                                    "-*-"
        };
        string[] Square = new string[] { "**",
                                         "**"
        };
        if (type == BLOCKTYPE.BAR)
        {
            initBlockByString(Bar);
        }
        else if (type == BLOCKTYPE.L)
        {
            initBlockByString(L);
        }
        else if (type == BLOCKTYPE.Z)
        {
            initBlockByString(Z);
        }
        else if (type == BLOCKTYPE.T)
        {
            initBlockByString(T);
        }
        else if (type == BLOCKTYPE.SQUARE)
        {
            initBlockByString(Square);
        }
    }

    public List<int> getListBlockCols()
    {
        List<int> result = new List<int>();
        for (int i = 0; i < width; i++)
        {
            result.Add(anchor.col + i);
        }
        return result;
    }
    public void move(string v)
    {
        if (v.Equals("Left"))
        {
            Anchor = new CubeIndex(anchor.row, anchor.col - 1);
        }
        else if (v.Equals("Right"))
        {
            Anchor = new CubeIndex(anchor.row, anchor.col + 1);
        }
        else if (v.Equals("Ground"))
        {
            Anchor = stopIndex;
        }
        calcStopPosition();
    }
    public void calcStopPosition()
    {
        List<int> cols = getListBlockCols();
        List<int> highestColsInBoard = blockBoard.getHighestCubeInCols(cols);
        string a = "";
        foreach (var item in highestColsInBoard)
        {
            a = a + item + " ";
        }
        Debug.Log(a);
        if (highestColsInBoard.Count == 0)
        {
            throw new Exception("highest cols is zero");
        }
        int i = 0;
        int minDistance = Int32.MaxValue; CubeIndex minIndex = null; int stopRow = 0;
        foreach (var highestPos in highestColsInBoard)
        {
            CubeIndex index = getLowestCubeIndexInRow(i);
            int row = anchor.row + index.row;
            var distance = row - highestPos;
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = index;
                stopRow = highestPos+1-index.row;
            }
            i++;
        }

        Debug.Log(stopRow);
        stopIndex = new CubeIndex(stopRow, anchor.col);
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

    // Use this for initialization
    void Awake()
    {
        cubes = new List<CubeInfo>();
        currentTime = 0.0f;
        initTypeRandom();
        initBlock();
    }

    public void initTypeRandom()
    {
        var array = Enum.GetValues(typeof(BLOCKTYPE));
        type = (BLOCKTYPE)array.GetValue(UnityEngine.Random.Range(0, array.Length - 1));
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
    void Update()
    {
        if (Time.time > currentTime + deltaTime)
        {
            currentTime = Time.time;
            moveToward();
        }
    }
}
