﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public  BlockBoard blockBoard;
    public Block blockPrefabs;
    public int initHeight;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            Block block = Instantiate(blockPrefabs, Vector3.zero, Quaternion.identity) as Block;
            Debug.Log(block.getWidth());
            block.Anchor = new CubeIndex(initHeight, 3);
            blockBoard.addBlockToBoard(block);
        }
	
	}
}