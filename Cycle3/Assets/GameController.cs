using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
    [SerializeField]
    private float roomWidth;
    [SerializeField]
    private float roomHeight;

    private float minXRoomPos;
    private float minYRoomPos;

    [SerializeField]
    private Vector4[] neighborValues;
    [SerializeField]
    private GameObject[] mazeObjects;
    Dictionary<Vector4, int> keyIndexDict = new Dictionary<Vector4, int>();
    private MazeConstructor generator;

    [SerializeField]
    private int mazeYCount = 13;
    [SerializeField]
    private int mazeXCount = 15;
    [SerializeField]
    private GameObject playerObject;
    void Start()
    {
        InitIndexDict();
        generator = GetComponent<MazeConstructor>();      // 2
        generator.GenerateNewMaze(mazeYCount, mazeXCount);
        GenerateRooms(generator.data);



    }
    private void InitIndexDict()
    {
        for (int i = 0; i < neighborValues.Length; i++)
        {
            keyIndexDict.Add(neighborValues[i], i);
        }
        minXRoomPos = -(((mazeXCount - 3)) * roomWidth / 2);// - (roomWidth / 2);
        minYRoomPos = -(((mazeYCount - 3)) * roomHeight / 2);// - (roomHeight / 2);
        Debug.Log(mazeXCount);
        Debug.Log(roomWidth);
        Debug.Log(minXRoomPos);
    }
    private void GenerateRooms(int[,] maze)
    {
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);



        float spawnPointX = minXRoomPos;
        float spawnPointY = minYRoomPos;
        for (int i = 1; i <= rMax-1; i++)
        {
            for (int j = 1; j <= cMax-1; j++)
            {
                int northN = maze[i + 1, j];
                int southN = maze[i - 1, j];
                int westN = maze[i, j+1];
                int eastN = maze[i, j-1];

                Vector4 key = new Vector4(northN, westN, southN, eastN);
                Debug.Log(key);
                if(keyIndexDict.ContainsKey(key))
                {
                    int index = keyIndexDict[key];
                    GameObject newRoom = Instantiate(mazeObjects[index], new Vector3(spawnPointX, spawnPointY, 0), Quaternion.identity);
                }
                if(generator.PlayerInitPos.x == i && generator.PlayerInitPos.y ==j)
                {
                    playerObject.transform.position = new Vector3(spawnPointX, spawnPointY, 0);
                }
                       

                spawnPointX += roomWidth;
              
            }
            spawnPointX = minXRoomPos;
            spawnPointY += roomHeight;
        }
    }

}
