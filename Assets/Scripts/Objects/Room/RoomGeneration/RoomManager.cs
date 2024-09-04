using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private int roomWidth = 20;
    private int roomHeight = 12;
    [SerializeField] private int maxRoomsCount = 10;
    private int roomCount;

    [SerializeField] private GameObject normalRoom;
    [SerializeField] private GameObject spawnRoom;
    [SerializeField] private GameObject bossRoom;   
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject treasureRoom;

    private Dictionary<int, Vector2Int> roomPositions;

    private List<GameObject> roomObjects;


    private void Start()
    {
        roomPositions = new Dictionary<int, Vector2Int>();
        roomObjects = new List<GameObject>();
        
        roomCount = 0;

        CreateMap();
    }

    public void CreateMap()
    {
        GenerateRoomsPositions();
        DrawRooms();
    }


    private void GenerateRoomsPositions()
    {
        Vector2Int currentPos = new (0, 0);
        Vector2Int previousPos = new (0, 0);

        roomPositions.Add(0, currentPos);

        int id = 1;
        while (roomCount < maxRoomsCount - 1)
        {
            currentPos += GetRandomDirection();

            if (roomPositions.ContainsValue(currentPos))
            {
                currentPos = previousPos;
            }

            else 
            {
                roomCount++;
                previousPos = currentPos;
                roomPositions.Add(id, currentPos);
                id++;
            }
        }

        // foreach (KeyValuePair<int, Vector2Int> room in roomPositions)
        // {
        //     Debug.Log(room.Key + ": " + room.Value);
        // }
    }


    private void DrawRooms()
    {
        foreach (KeyValuePair<int, Vector2Int> roomPos in roomPositions)
        {
            // Generate spawn room as the first room of the list
            if (roomPos.Value == new Vector2Int(0, 0))
            {
                var spawnRoomDrawn = Instantiate(spawnRoom, new Vector2(roomPos.Value.x, roomPos.Value.y), Quaternion.identity, this.transform);
                spawnRoomDrawn.name = $"Spawn {roomPos.Value.x}, {roomPos.Value.y}";

                // spawnRoomDrawn.GetComponent<RoomObject>().SetUpDoors();
                spawnRoomDrawn.GetComponent<RoomObject>().SetRoomPosition(roomPos.Value);
                spawnRoomDrawn.GetComponent<RoomObject>().SetRoomType(RoomObject.RoomType.SpawnRoom);
                spawnRoomDrawn.GetComponent<RoomObject>().SetRoomId(roomPos.Key);
                roomObjects.Add(spawnRoomDrawn);
                continue;
            }


            // Generate boss room as the last room of the list 
            if (roomPos.Value == roomPositions[roomPositions.Count - 1])
            {
                var bossRoomDrawn = Instantiate(bossRoom, new Vector2(roomPos.Value.x, roomPos.Value.y), Quaternion.identity, this.transform);
                bossRoomDrawn.name = $"Boss {roomPos.Value.x}, {roomPos.Value.y}";

                // bossRoomDrawn.GetComponent<RoomObject>().SetUpDoors();
                bossRoomDrawn.GetComponent<RoomObject>().SetRoomPosition(roomPos.Value);
                bossRoomDrawn.GetComponent<RoomObject>().SetRoomType(RoomObject.RoomType.Boss);
                bossRoomDrawn.GetComponent<RoomObject>().SetRoomId(roomPos.Key);
                roomObjects.Add(bossRoomDrawn);
                continue;
            }


            // Generate random normal room based on the rooms left in the list
            var roomDrawn = Instantiate(normalRoom, new Vector2(roomPos.Value.x, roomPos.Value.y), Quaternion.identity, this.transform);
            roomDrawn.name = $"{roomPos.Value.x}, {roomPos.Value.y}";

            // roomDrawn.GetComponent<RoomObject>().SetUpDoors();
            roomDrawn.GetComponent<RoomObject>().SetRoomPosition(roomPos.Value);
            roomDrawn.GetComponent<RoomObject>().SetRoomType(RoomObject.RoomType.Normal);
            roomDrawn.GetComponent<RoomObject>().SetRoomId(roomPos.Key);
            roomObjects.Add(roomDrawn);
        }

        ConnectRooms();

        // Debugging
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
            var topRoomPosition = roomObject.GetComponent<RoomObject>().GetRoomPosition() + new Vector2Int(0, roomHeight);
            var bottomRoomPosition = roomObject.GetComponent<RoomObject>().GetRoomPosition() + new Vector2Int(0, -roomHeight);
            var leftRoomPosition = roomObject.GetComponent<RoomObject>().GetRoomPosition() + new Vector2Int(-roomWidth, 0);
            var rightRoomPosition = roomObject.GetComponent<RoomObject>().GetRoomPosition() + new Vector2Int(roomWidth, 0);

            // The index of the 4 above neighbors (to check with the room positions list we have created above)
            var topRoomIndex = roomPositions.Values.ToList().IndexOf(topRoomPosition);
            var bottomRoomIndex = roomPositions.Values.ToList().IndexOf(bottomRoomPosition);
            var leftRoomIndex = roomPositions.Values.ToList().IndexOf(leftRoomPosition);
            var rightRoomIndex = roomPositions.Values.ToList().IndexOf(rightRoomPosition);


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

            // Debugging
            // Debug.Log($"Room {roomObject.GetComponent<RoomObject>().GetRoomPosition()}" 
            // + $"Top: {topRoomIndex}, Bottom: {bottomRoomIndex}, Left: {leftRoomIndex}, Right: {rightRoomIndex}");
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
}
