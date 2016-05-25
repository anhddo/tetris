using UnityEngine;
using System.Collections;
using System;

public class BlockShapes : MonoBehaviour
{
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

    public Block blockPrefabs;
    public CubeInfo cubePrefabs;
    CubeInfo initNewCube()
    {
        CubeInfo instance = null;
        instance = Instantiate(cubePrefabs, Vector3.zero, Quaternion.identity) as CubeInfo;
        return instance;
    }
    String[] getBlockString(BLOCKTYPE type)
    {
        if (type == BLOCKTYPE.BAR)
        {
            return Bar;
        }
        else if (type == BLOCKTYPE.L)
        {
            return L;
        }
        else if (type == BLOCKTYPE.Z)
        {
            return Z;
        }
        else if (type == BLOCKTYPE.T)
        {
            return T;
        }
        else if (type == BLOCKTYPE.SQUARE)
        {
            return Square;
        }
        return null;
    }
    void initBlockByString(Block block)
    {
        String[] blockString = getBlockString(block.type);
        block.Height = blockString.Length;
        block.Width = blockString[0].Length;
        for (int i = 0; i < block.Height; i++)
        {
            for (int j = 0; j < block.Width; j++)
            {
                if (blockString[i][j] == '*')
                {
                    CubeInfo cube = initNewCube();
                    cube.transform.parent = block.transform;
                    cube.changeCubeIndex(i, j);
                    block.Cubes.Add(cube);
                }
            }
        }
    }
    BLOCKTYPE initTypeRandom()
    {
        var array = Enum.GetValues(typeof(BLOCKTYPE));
        return (BLOCKTYPE)array.GetValue(UnityEngine.Random.Range(0, array.Length - 1));
    }
    void randomBlockColor(Block block)
    {
        Color color = UnityEngine.Random.ColorHSV();
        foreach (var cube in block.Cubes)
        {
            Renderer render = cube.GetComponent<Renderer>();
            render.material.SetColor("_Color", color);
        }
    }
    public Block createRandomBlock()
    {
        Block block = Instantiate(blockPrefabs, Vector3.zero, Quaternion.identity) as Block;
        block.type = initTypeRandom();
        initBlockByString(block);
        randomBlockColor(block);
        return block;
    }
}
