using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private int roomWidth = 20;
    [SerializeField] private int roomHeight = 12;
    [SerializeField] private int maxRoomsCount = 10;
    private int roomCount;

    [SerializeField] private GameObject normalRoom;
    [SerializeField] private GameObject spawnRoom;
    [SerializeField] private GameObject bossRoom;   

    private List<Vector2Int> roomPositions;

    private List<GameObject> roomObjects;


    private void Start()
    {
        roomPositions = new List<Vector2Int>();
        roomObjects = new List<GameObject>();
        
        roomCount = 0;

        CreateMap();
    }

    public void CreateMap()
    {
        GenerateRoomsPositions();
        DrawRooms();
        ConnectRooms();
    }


    private void GenerateRoomsPositions()
    {
        Vector2Int currentPos = new (0, 0);
        Vector2Int previousPos = new (0, 0);

        roomPositions.Add(currentPos);

        while (roomCount < maxRoomsCount - 1)
        {
            currentPos += GetRandomDirection();

            if (roomPositions.Contains(currentPos))
            {
                currentPos = previousPos;
            }

            else 
            {
                roomCount++;
                previousPos = currentPos;
                roomPositions.Add(currentPos);
            }
        }
    }


    private Vector2Int GetRandomDirection()
    {
        int randomIndex = Random.Range(0, 4);

        return randomIndex switch
        {
            0 => new Vector2Int(roomWidth, 0),
            1 => new Vector2Int(0, roomHeight),
            2 => new Vector2Int(-roomWidth, 0),
            3 => new Vector2Int(0, -roomHeight),
            _ => new Vector2Int(0, 0),
        };
    }


    private void DrawRooms()
    {
        foreach (Vector2Int roomPos in roomPositions)
        {
            if (roomPos == new Vector2Int(0, 0))
            {
                var spawnRoomDrawn = Instantiate(spawnRoom, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
                spawnRoomDrawn.name = $"Spawn {roomPos.x}, {roomPos.y}";

                // spawnRoomDrawn.GetComponent<RoomObject>().SetUpDoors();
                roomObjects.Add(spawnRoomDrawn);
                continue;
            }

            if (roomPos == roomPositions[roomPositions.Count - 1])
            {
                var bossRoomDrawn = Instantiate(bossRoom, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
                bossRoomDrawn.name = $"Boss {roomPos.x}, {roomPos.y}";  

                // bossRoomDrawn.GetComponent<RoomObject>().SetUpDoors();
                roomObjects.Add(bossRoomDrawn);
                continue;
            }

            var roomDrawn = Instantiate(normalRoom, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
            roomDrawn.name = $"{roomPos.x}, {roomPos.y}";

            // roomDrawn.GetComponent<RoomObject>().SetUpDoors();
            roomObjects.Add(roomDrawn);
        }

        // Debugging
        foreach (GameObject roomObject in roomObjects)
        {
            Debug.Log(roomObject.GetComponent<RoomObject>().GetRoomPosition());
        }
        Debug.Log(roomObjects.Count);
    }


    private void ConnectRooms()
    {
        foreach (GameObject roomObject in roomObjects)
        {
            var topRoomPosition = roomObject.GetComponent<RoomObject>().GetRoomPosition() + new Vector2Int(0, roomHeight);
            var bottomRoomPosition = roomObject.GetComponent<RoomObject>().GetRoomPosition() + new Vector2Int(0, -roomHeight);
            var leftRoomPosition = roomObject.GetComponent<RoomObject>().GetRoomPosition() + new Vector2Int(-roomWidth, 0);
            var rightRoomPosition = roomObject.GetComponent<RoomObject>().GetRoomPosition() + new Vector2Int(roomWidth, 0);

            var topRoomIndex = roomPositions.IndexOf(topRoomPosition);
            var bottomRoomIndex = roomPositions.IndexOf(bottomRoomPosition);
            var leftRoomIndex = roomPositions.IndexOf(leftRoomPosition);
            var rightRoomIndex = roomPositions.IndexOf(rightRoomPosition);

            if (topRoomIndex != -1)
            {
                roomObject.GetComponent<RoomObject>().isTopRoomExists = true;
                // roomObjects[topRoomIndex].GetComponent<RoomObject>().isBottomRoomExists = true;
                // roomObject.GetComponent<RoomObject>().isBottomRoomExists = true;
            }

            if (bottomRoomIndex != -1)
            {
                roomObject.GetComponent<RoomObject>().isBottomRoomExists = true;
                // roomObjects[bottomRoomIndex].GetComponent<RoomObject>().isTopRoomExists = true;
                // bottomRoom.GetComponent<RoomObject>().isTopRoomExists = true;
            }

            if (leftRoomIndex != -1)
            {
                roomObject.GetComponent<RoomObject>().isLeftRoomExists = true;
                // roomObjects[leftRoomIndex].GetComponent<RoomObject>().isRightRoomExists = true;
                // leftRoom.GetComponent<RoomObject>().isRightRoomExists = true;
            }

            if (rightRoomIndex != -1)
            {
                roomObject.GetComponent<RoomObject>().isRightRoomExists = true;
                // roomObjects[rightRoomIndex].GetComponent<RoomObject>().isLeftRoomExists = true;
                // rightRoom.GetComponent<RoomObject>().isLeftRoomExists = true;
            }

            roomObject.GetComponent<RoomObject>().SetUpDoors();
        }
    }
   
}
