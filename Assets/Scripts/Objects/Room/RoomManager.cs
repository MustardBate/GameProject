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

    private List<RoomObject> roomObjects;


    private void Start()
    {
        roomPositions = new List<Vector2Int>();
        roomObjects = new List<RoomObject>();
        
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

                RoomObject spawn = gameObject.AddComponent<RoomObject>();
                // spawn.SetRoomPosition(roomPos);
                roomObjects.Add(spawn);
                continue;
            }

            if (roomPos == roomPositions[roomPositions.Count - 1])
            {
                var bossRoomDrawn = Instantiate(bossRoom, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
                bossRoomDrawn.name = $"Boss {roomPos.x}, {roomPos.y}";

                RoomObject boss = gameObject.AddComponent<RoomObject>();
                // boss.SetRoomPosition(roomPos);
                roomObjects.Add(boss);
                continue;
            }

            var roomDrawn = Instantiate(normalRoom, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
            roomDrawn.name = $"{roomPos.x}, {roomPos.y}";

            RoomObject room = gameObject.AddComponent<RoomObject>();
            // room.SetRoomPosition(roomPos);
            roomObjects.Add(room);
        }

        // Debugging
        foreach (RoomObject roomObject in roomObjects)
        {
            Debug.Log(roomObject.GetRoomPosition());
        }
        Debug.Log(roomObjects.Count);
    }


    private void ConnectRooms()
    {
        foreach (RoomObject roomObject in roomObjects)
        {
            var topRoomPosition = roomObject.GetRoomPosition() + new Vector2Int(0, roomHeight);
            var bottomRoomPosition = roomObject.GetRoomPosition() + new Vector2Int(0, -roomHeight);
            var leftRoomPosition = roomObject.GetRoomPosition() + new Vector2Int(-roomWidth, 0);
            var rightRoomPosition = roomObject.GetRoomPosition() + new Vector2Int(roomWidth, 0);

            RoomObject bottomRoom = roomObjects.Find(room => room.GetRoomPosition() == bottomRoomPosition);
            RoomObject topRoom = roomObjects.Find(room => room.GetRoomPosition() == topRoomPosition);
            RoomObject leftRoom = roomObjects.Find(room => room.GetRoomPosition() == leftRoomPosition);
            RoomObject rightRoom = roomObjects.Find(room => room.GetRoomPosition() == rightRoomPosition);

            if (roomPositions.IndexOf(topRoomPosition) != -1)
            {
                roomObject.isTopRoomExists = true;
                topRoom.isBottomRoomExists = true;
            }

            if (roomPositions.IndexOf(bottomRoomPosition) != -1)
            {
                roomObject.isBottomRoomExists = true;
                bottomRoom.isTopRoomExists = true;
            }

            if (roomPositions.IndexOf(leftRoomPosition) != -1)
            {
                roomObject.isLeftRoomExists = true;
                leftRoom.isRightRoomExists = true;
            }

            if (roomPositions.IndexOf(rightRoomPosition) != -1)
            {
                roomObject.isRightRoomExists = true;
                rightRoom.isLeftRoomExists = true;
            }
            roomObject.SetUpDoors(roomObject);
        }
    }
   
}
