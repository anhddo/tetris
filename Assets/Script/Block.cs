using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BLOCKTYPE { L1, L2, Z1, Z2, T, BAR, SQUARE };
public class Block : MonoBehaviour
{
    public GameObject cubeObject;
    public BLOCKTYPE type;
    Vector2 anchor;
    int rotationAngle;
    List<Object> cubes;
    List<Object> getBlock(BLOCKTYPE type)
    {
        //Vector2[] blockArray = null;
        List<Object> cubes = new List<Object>();
        if (type == BLOCKTYPE.BAR)
        {
            for (int i = 0; i < 4; i++)
            {
                CubeInfo cubeInfo = cubeObject.GetComponent<CubeInfo>();
                cubeInfo.index = new Vector2(0, i);

                GameObject instance = Instantiate(cubeObject, new Vector3(cubeInfo.index.x, 0, cubeInfo.index.y), Quaternion.identity) as GameObject;
                instance.transform.parent = gameObject.transform;
                cubes.Add(instance);
            }
        }

        return cubes;
    }
    // Use this for initialization
    void Start()
    {
        cubes = getBlock(type);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
