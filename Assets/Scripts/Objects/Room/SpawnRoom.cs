using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    public Vector2Int roomPosition;
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;

    public bool isTopRoomExists = false;
    public bool isBottomRoomExists = false;
    public bool isLeftRoomExists = false;
    public bool isRightRoomExists = false;

    private void Start()
    {
        roomPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

    }


    public Vector2Int GetRoomPosition()
    {
        return roomPosition;
    }

    private void SetUpDoors(RoomObject room)
    {
        if(topWall == null) Debug.Log("topWall is null");
        if(bottomWall == null) Debug.Log("bottomWall is null");
        if(leftWall == null) Debug.Log("leftWall is null");
        if(rightWall == null) Debug.Log("rightWall is null");

        if (isTopRoomExists == true)    this.topWall.SetActive(false);

        if (isBottomRoomExists == true) this.bottomWall.SetActive(false);

        if (isLeftRoomExists == true)   this.leftWall.SetActive(false);

        if (isRightRoomExists == true)  this.rightWall.SetActive(false);
    }
}
