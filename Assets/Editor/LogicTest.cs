using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System;

public class LogicTest
{
    [Test]
    public void FirstTest()
    {
        BlockBoard blockBoard = new BlockBoard();
        blockBoard.Start();
        blockBoard.width = 10; blockBoard.height = 20;

        Block z = new Block();
        Assert.NotNull(z);
        z.type = BLOCKTYPE.Z;
        z.initBlock();
        blockBoard.addBlockToBoard(z);

        Assert.AreEqual(blockBoard.height, z.Anchor.row);

        blockBoard.rotateBlock();
        blockBoard.moveBlockToTheGround();

        Assert.True(z.stopFalling());
        Assert.NotNull(blockBoard.CurrentBlock);
        blockBoard.Update();
		Assert.AreEqual(0, z.getStopIndex().row);

        Block anotherZ= new Block();
        anotherZ.type = BLOCKTYPE.Z;
        anotherZ.initBlock();
        blockBoard.addBlockToBoard(anotherZ);
        Debug.Log(anotherZ.getStopIndex());
        blockBoard.rotateBlock();
        blockBoard.moveBlockToTheGround();

        var stop = anotherZ.getStopIndex();
        Debug.Log(stop);
        Assert.AreSame(new CubeIndex(0, 0), stop);
    }
}
