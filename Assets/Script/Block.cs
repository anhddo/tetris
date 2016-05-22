using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum BLOCKTYPE { L1, L2, Z1, Z2, T, BAR, SQUARE };
public class Block : MonoBehaviour
{
    public CubeInfo cubePrefabs;
    public BLOCKTYPE type;
    public Vector2 Anchor
    {
        get { return anchor; }
        set
        {
            anchor = value;
            gameObject.transform.position = new Vector3(0, anchor.x, anchor.y);
        }
    }

    Vector2 anchor;
    int width, height;
    int rotationAngle;
    List<CubeInfo> cubes;
    CubeIndex stopIndex;
    void rotate()
    {

    }
    List<CubeInfo> getCubes()
    {
        return cubes;
    }
    void initBLock()
    {
        cubes.Clear();
        if (type == BLOCKTYPE.BAR)
        {
            for (int i = 0; i < 4; i++)
            {
                CubeInfo cubeInfo = cubePrefabs.GetComponent<CubeInfo>();
                cubeInfo.index = new CubeIndex(0, i);
                CubeInfo instance = Instantiate(cubePrefabs, Vector3.zero, Quaternion.identity) as CubeInfo;
                instance.transform.parent = gameObject.transform;
                instance.transform.localPosition = new Vector3(0, cubeInfo.index.row, cubeInfo.index.col);
                cubes.Add(instance);
            }
        }
    }
    Vector2 getCubeWithShortestDistance(List<CubeInfo> cubes, Vector2 highestGroundCube)
    {
        // TODO
        return Vector2.zero;
    }
    public void calcStopPosition(List<int> highestCols)
    {
        int i = 0;
        int minDistance = Int32.MaxValue; CubeIndex minIndex = null;
        foreach (var highestPos in highestCols)
        {
            CubeIndex index = getLowestCubeIndexInRow(i);
            var distance = index.row - highestPos;
            if (distance<minDistance) 
            {
                minDistance = distance;
                minIndex = index;
            }
            i++;
        }
        stopIndex = Vector2.zero;
    }
    public List<int> getListBlockCols()
    {
        //TODO
        return null;
    }
    public CubeIndex getLowestCubeIndexInRow(int col)
    {
        //TODO
        return null;
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
        return true;
    }

    // Use this for initialization
    void Start()
    {
        cubes = new List<CubeInfo>();
        initBLock();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
