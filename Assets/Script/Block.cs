using UnityEngine;
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
            gameObject.transform.position = new Vector3(0, anchor.row, anchor.col);
        }
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

    public CubeIndex getStopIndex()
    {
        return stopIndex;
    }
    public List<CubeInfo> getCubes()
    {
        return cubes;
    }
    void rotate()
    {

    }
    void addToCubes(CubeInfo instance)
    {
        instance.transform.parent = gameObject.transform;
        instance.transform.localPosition = new Vector3(0, instance.index.row, instance.index.col);
        cubes.Add(instance);
    }
    CubeInfo initNewCube(int row, int col)
    {
        CubeInfo instance = Instantiate(cubePrefabs, Vector3.zero, Quaternion.identity) as CubeInfo;
        instance.index = new CubeIndex(row, col);
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
                    addToCubes(initNewCube(i, j));
                }
            }
        }
    }
    void initBLock()
    {
        cubes.Clear();
        string[] Bar = new string[] { "*****" };
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

    public void calcStopPosition(List<int> highestCols)
    {
        if (highestCols.Count == 0)
        {
            throw new Exception("highest cols is zero");
        }
        int i = 0;
        int minDistance = Int32.MaxValue; CubeIndex minIndex = null; int stopRow = 0;
        foreach (var highestPos in highestCols)
        {
            CubeIndex index = getLowestCubeIndexInRow(i);
            int row = anchor.row + index.row;
            var distance = row - highestPos;
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = index;
                stopRow = highestPos + 1;
            }
            i++;
        }
        //
        stopIndex = new CubeIndex(stopRow , anchor.col);
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
        initBLock();
        currentTime = 0.0f;
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
