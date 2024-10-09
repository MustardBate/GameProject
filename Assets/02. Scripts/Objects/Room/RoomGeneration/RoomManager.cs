using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private int roomWidth = 20;
    private int roomHeight = 12;
    [SerializeField] private int maxRoomsCount = 15;
    private int roomCount;

    [SerializeField] private GameObject normalRoom;
    [SerializeField] private GameObject spawnRoom;
    [SerializeField] private GameObject portalRoom;   
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject treasureRoom;

    private List<Vector2Int> roomPositions;

    private List<GameObject> roomObjects;


    private void Awake()
    {
        roomPositions = new ();
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

        // DEBUGGING
        // foreach (Vector2Int room in roomPositions)
        // {
        //     Debug.Log(room);
        // }
    }


    private void DrawRooms()
    {
        int treasureIndex = UnityEngine.Random.Range(3, roomPositions.Count - 2);
        int shopIndex = UnityEngine.Random.Range(4, roomPositions.Count - 1);
        
        if (treasureIndex == shopIndex) shopIndex = UnityEngine.Random.Range(4, roomPositions.Count - 1);

        foreach (Vector2Int roomPos in roomPositions)
        {
            // Generate spawn room as the first room of the list
            if (roomPos == new Vector2Int(0, 0))
            {
                var spawnRoomDrawn = Instantiate(spawnRoom, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
                spawnRoomDrawn.name = $"Spawn {roomPos.x}, {roomPos.y}";

                spawnRoomDrawn.GetComponent<RoomObject>().roomPosition = roomPos;
                spawnRoomDrawn.GetComponent<RoomObject>().thisRoomType = RoomObject.RoomType.SpawnRoom;
                roomObjects.Add(spawnRoomDrawn);
                continue;
            }


            // Generate boss room as the last room of the list 
            if (roomPos == roomPositions[roomPositions.Count - 1])
            {
                var bossRoomDrawn = Instantiate(portalRoom, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
                bossRoomDrawn.name = $"Boss {roomPos.x}, {roomPos.y}";

                bossRoomDrawn.GetComponent<RoomObject>().roomPosition = roomPos;
                bossRoomDrawn.GetComponent<RoomObject>().thisRoomType = RoomObject.RoomType.Portal;
                roomObjects.Add(bossRoomDrawn);
                continue;
            }


            if (roomPos == roomPositions[treasureIndex])
            {
                var treasureRoomDrawn = Instantiate(treasureRoom, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
                treasureRoomDrawn.name = $"Treasure {roomPos.x}, {roomPos.y}";

                treasureRoomDrawn.GetComponent<RoomObject>().roomPosition = roomPos;
                treasureRoomDrawn.GetComponent<RoomObject>().thisRoomType = RoomObject.RoomType.Treasure;
                roomObjects.Add(treasureRoomDrawn);
                continue;
            }


            if (roomPos == roomPositions[shopIndex])
            {
                var shopRoomDrawn = Instantiate(shop, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
                shopRoomDrawn.name = $"Shop {roomPos.x}, {roomPos.y}";

                shopRoomDrawn.GetComponent<RoomObject>().roomPosition = roomPos;
                shopRoomDrawn.GetComponent<RoomObject>().thisRoomType = RoomObject.RoomType.Shop;
                roomObjects.Add(shopRoomDrawn);
                continue;
            }


            // Generate random normal room based on the rooms left in the list
            var roomDrawn = Instantiate(normalRoom, new Vector2(roomPos.x, roomPos.y), Quaternion.identity, this.transform);
            roomDrawn.name = $"{roomPos.x}, {roomPos.y}";

            roomDrawn.GetComponent<RoomObject>().roomPosition = roomPos;
            roomDrawn.GetComponent<RoomObject>().thisRoomType = RoomObject.RoomType.Normal;
            roomObjects.Add(roomDrawn);
        }

        // DEBUGGING
        // foreach (GameObject roomObject in roomObjects)
        // {
        //     Debug.Log(roomObject.GetComponent<RoomObject>().GetRoomPosition());
        // }
        // Debug.Log(roomObjects.Count);
    }


    private void ConnectRooms()
    {
        foreach (GameObject roomObject in roomObjects)
        {
            // Each hypothetical neighbouring room to the current room 
            var topRoomPosition = roomObject.GetComponent<RoomObject>().roomPosition + new Vector2Int(0, roomHeight);
            var bottomRoomPosition = roomObject.GetComponent<RoomObject>().roomPosition + new Vector2Int(0, -roomHeight);
            var leftRoomPosition = roomObject.GetComponent<RoomObject>().roomPosition + new Vector2Int(-roomWidth, 0);
            var rightRoomPosition = roomObject.GetComponent<RoomObject>().roomPosition + new Vector2Int(roomWidth, 0);

            // The index of the 4 above neighbors (to check with the room positions list we have created above)
            var topRoomIndex = roomPositions.IndexOf(topRoomPosition);
            var bottomRoomIndex = roomPositions.IndexOf(bottomRoomPosition);
            var leftRoomIndex = roomPositions.IndexOf(leftRoomPosition);
            var rightRoomIndex = roomPositions.IndexOf(rightRoomPosition);


            // If the index is found (does exist) => returns the index in the list // else => returns -1
            // If the index != -1 => Set the boolean value to detect the corrosponding room to true
            if (topRoomIndex != -1)
            {
                roomObject.GetComponent<RoomObject>().isTopRoomExists = true;
            }

            if (bottomRoomIndex != -1)
            {
                roomObject.GetComponent<RoomObject>().isBottomRoomExists = true;
            }

            if (leftRoomIndex != -1)
            {
                roomObject.GetComponent<RoomObject>().isLeftRoomExists = true;
            }

            if (rightRoomIndex != -1)
            {
                roomObject.GetComponent<RoomObject>().isRightRoomExists = true;
            }

            // Use the SetUpDoors function from the RoomObject script to open valid doors
            roomObject.GetComponent<RoomObject>().SetUpDoors();

            // DEBUGGING
            // Debug.Log($"Room {roomObject.GetComponent<RoomObject>().GetRoomPosition()}" 
            // + $"Top: {topRoomIndex}, Bottom: {bottomRoomIndex}, Left: {leftRoomIndex}, Right: {rightRoomIndex}");
        }
    }


    private Vector2Int GetRandomDirection()
    {
        int randomIndex = UnityEngine.Random.Range(0, 4);

        return randomIndex switch
        {
            0 => new Vector2Int(roomWidth, 0),
            1 => new Vector2Int(0, roomHeight),
            2 => new Vector2Int(-roomWidth, 0),
            3 => new Vector2Int(0, -roomHeight),
            _ => new Vector2Int(0, 0),
        };
    }
}
