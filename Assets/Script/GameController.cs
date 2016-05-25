using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public BlockBoard blockBoard;
    public float keyPressTimeDelay;
    private float currentTime;

    // Use this for initialization
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void keyHandling()
    {
        if (Input.anyKey && Time.time > currentTime + keyPressTimeDelay)
        {
            currentTime = Time.time;
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                blockBoard.moveBlockToTheGround();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                blockBoard.rotateBlock();
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                blockBoard.moveFallingBlock("Left");
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                blockBoard.moveFallingBlock("Right");
            }
        }
    }
    void Update()
    {
        blockBoard.addRandomBLock();
        keyHandling();

    }
}
