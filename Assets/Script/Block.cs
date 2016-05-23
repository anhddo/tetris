using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum BLOCKTYPE { BAR, L1, L2, Z1, Z2, T, SQUARE };
public class Block : MonoBehaviour
{
    public CubeInfo cubePrefabs;
    public BLOCKTYPE type;
    public  float deltaTime;
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
    void initBLock()
    {
        cubes.Clear();
        if (type == BLOCKTYPE.BAR)
        {
            height = 1;
            width = 4;
            for (int i = 0; i < 4; i++)
            {
                CubeInfo instance = Instantiate(cubePrefabs, Vector3.zero, Quaternion.identity) as CubeInfo;
                instance.index = new CubeIndex(0, i);
                instance.transform.parent = gameObject.transform;
                instance.transform.localPosition = new Vector3(0, instance.index.row, instance.index.col);
                cubes.Add(instance);
            }
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
            return;
        }
        int i = 0;
        int minDistance = Int32.MaxValue; CubeIndex minIndex = null;
        foreach (var highestPos in highestCols)
        {
            CubeIndex index = getLowestCubeIndexInRow(i);
            var distance = index.row - highestPos;
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = index;
            }
            i++;
        }
        //
        stopIndex = new CubeIndex(minIndex.row + minDistance, 0);
        Debug.Log(stopIndex);
    }
    public CubeIndex getLowestCubeIndexInRow(int col)
    {
        int maxIndex = Int32.MinValue;
        CubeIndex result = null;
        foreach (var cube in cubes)
        {
            if (cube.index.col == col && maxIndex < cube.index.row)
            {
                maxIndex = cube.index.row;
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
        type = BLOCKTYPE.BAR;
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
            Debug.Log(anchor.row);
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
