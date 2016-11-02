﻿using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] terrain;
    public int width;
    public int height;
    public int averageRoomSize;
    private GameObject[,] tiles;

	// Use this for initialization
	void Start () {
        tiles = new GameObject[width, height];
        int numRooms = Mathf.CeilToInt(Random.value * 4) + 2;
        Vector2[,] rooms = new Vector2[numRooms, 2]; //(w,h),(x,y)
        
        for (int i = 0; i < numRooms; i++)
        {
            rooms[i, 0] = new Vector2(Mathf.CeilToInt(RandomNormal(averageRoomSize)), Mathf.CeilToInt(RandomNormal(averageRoomSize)));
            rooms[i, 1] = new Vector2((Mathf.CeilToInt(Random.value * (width - rooms[i, 0].x))) % width, (Mathf.CeilToInt(Random.value * (height - rooms[i, 0].y))) % height);
            Debug.Log(rooms[i, 0].x + " " + rooms[i, 0].y + " " + rooms[i, 1].x + " " + rooms[i, 1].y);
        }

        for (int i = 0; i < numRooms; i++)
        {
            int[] size = { (int)rooms[i, 0].x, (int)rooms[i, 0].y };
            int[] coords = { (int)rooms[i, 1].x, (int)rooms[i, 1].y };
            for(int x = 0; x < size[0]; x++)
            {
                for (int y = 0; y < size[1]; y++)
                {
                    tiles[coords[0] + x, coords[1] + y] = (GameObject)Instantiate(terrain[0]);
                    tiles[coords[0] + x, coords[1] + y].transform.position = new Vector2(coords[0] + x, coords[1] + y);
                }
            }
        }
        /*for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i, j] = (GameObject)Instantiate(terrain[0]);
                tiles[i, j].transform.position = new Vector2(i, j);
            }
        }*/
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private float RandomNormal(int mean)
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float randStdNormal = Mathf.Sqrt(-2f * Mathf.Log((float)u1)) * Mathf.Sin(2f * Mathf.PI * u2);
        return mean + Mathf.Pow(mean, 1f/3f) * randStdNormal;
    }
}