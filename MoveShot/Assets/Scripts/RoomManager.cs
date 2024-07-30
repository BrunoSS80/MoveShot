using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;

public class RoomManager : MonoBehaviour
{
  [SerializeField] GameObject roomPrefab;
  [SerializeField] private int maxRooms = 15;
  [SerializeField] private int minRooms = 10;

  int roomWidth = 20;
  int roomHeight = 12;
  int gridSizeX = 10;
  int gridSizeY = 10;
  private List<GameObject> roomObjects = new List<GameObject>();
  private Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();
  private int [,] roomGrid;
  private int roomCount;

    private void Start() {
        roomGrid = new int [gridSizeX, gridSizeY];
        roomQueue = new Queue<Vector2Int>();

        Vector2Int initialRoomIndex = new Vector2Int(gridSizeX/2, gridSizeY/2);
        StartRoomGenerationFromRoom(initialRoomIndex);
    }

    private void StartRoomGenerationFromRoom( Vector2Int roomIndex){
        roomQueue.Enqueue(roomIndex);
        int x = roomIndex.x;
        int y = roomIndex.y;
        roomGrid[x,y] = 1;
        roomCount++;
        var initialRoom = Instantiate(roomPrefab, GetPositionFromGridIndex(roomIndex), Quaternion.identity);
        initialRoom.name = $"Room-{roomCount}";
        initialRoom.GetComponent<Room>().RoomIndex = roomIndex;
        roomObjects.Add(initialRoom);
    }



    private Vector3 GetPositionFromGridIndex(Vector2Int gridIndex){
        int gridX = gridIndex.x;
        int gridY = gridIndex.y;
        return new Vector3(roomWidth * (gridX - gridSizeX/2), roomHeight * (gridY - gridSizeY/2));
    }

    private void OnDrawGizmos() {
        Color gizmoColor = new Color (0, 1, 1, 0.05f);
        Gizmos.color = gizmoColor;

        for(int x = 0; x < gridSizeX; x++){
            for(int y = 0; y < gridSizeY; y++){
                Vector3 position = GetPositionFromGridIndex(new Vector2Int(x, y));
                Gizmos.DrawWireCube(position, new Vector3(roomWidth, roomHeight, 1));
            }
        }
    }
}
