using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class LogicTest
{


    const int L = 0;
    const int SQUARE = 1;
    [Test]
    public void FirstTest()
    {
        int[,] currentState = { {0,0,0},
                                {0,0,0},
                                {0,0,0},
                                {0,0,0}};
        int[,] expected = { {1,0,0},
                               {1,0,0},
                               {1,0,0},
                               {1,0,0}};
        int inputType = L;
        var result = calc(currentState, inputType,new Vector2(0,0));
        Assert.AreEqual(expected, result);
    }
    [Test]
    public void SecondTest()
    {
        int[,] currentState = { {0,0,0},
                                {0,0,0},
                                {0,0,0},
                                {0,0,0}};
        int[,] expected = { {0,0,0},
                                {0,0,0},
                                {1,1,0},
                                {1,1,0}};
        int inputType = L;
        var result = calc(currentState, inputType, new Vector2(0,0));
        Assert.AreEqual(expected, result);
    }
    public int[,] getArray(int type)
    {
        if(type== L)
        {
            return new int[,] { { 1 }, { 1 }, { 1 }, { 1 } };
        }else if(type== SQUARE)
        {
            return new int[,] { { 1, 1 }, { 1, 1 } };
        }
        return null;
    }
    int[] getBottom(int[,] block)
    {
        var rows = block.GetLength(0);
        var cols = block.GetLength(1);
        return null;
    }
    private int[,] calc(int[,] currentState, int inputType, Vector2 position)
    {
        var block = getArray(inputType);
        return null;
    }
}
