using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

    public GameObject[] terrain;
    public int width;
    public int height;
    public int averageRoomSize;
    public int averageRoomCount;
    private TileInformation[,] tiles;
    private RoomInformation[] rooms;

	// Use this for initialization
	void Start () {
        tiles = new TileInformation[width, height];
        for (int i = 0; i < width; i++) { for (int j = 0; j < height; j++) { tiles[i, j] = new TileInformation(i,j); } }

        int numRooms = Mathf.CeilToInt(Random.value * (averageRoomCount)) + 2;
        rooms = new RoomInformation[numRooms]; //(w,h),(x,y)

        for (int i = 0; i < numRooms; i++)
        {
            Vector2 size = new Vector2(Mathf.CeilToInt(RandomNormal(averageRoomSize)), Mathf.CeilToInt(RandomNormal(averageRoomSize)));
            Vector2 postion = new Vector2((Mathf.CeilToInt(Random.value * (width - size.x))) % width, (Mathf.CeilToInt(Random.value * (height - size.y))) % height);
            rooms[i] = new RoomInformation(postion, size);
        }

        GenerateRooms(rooms);
        AttachIsolatedRooms(rooms);

        for (int i = 0; i < numRooms; i++)
        {
            Debug.Log("Room " + i + ": pos(" + rooms[i].getPosition().x + ", " + rooms[i].getPosition().y
                + ") size(" + rooms[i].getSize().x + ", " + rooms[i].getSize().y
                + ") " + rooms[i].getAdjacencyState());
        }

    }

    // Update is called once per frame
    void Update () {
	
	}

    private void GenerateRooms(RoomInformation[] rooms)
    {
        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int x = 0; x < (int)rooms[i].getSize().x; x++)
            {
                for (int y = 0; y < (int)rooms[i].getSize().y; y++)
                {
                    tiles[(int)rooms[i].getPosition().x + x, (int)rooms[i].getPosition().y + y].activateTile(terrain[0]);
                }
            }
        }
    }

    private void AttachIsolatedRooms(RoomInformation[] rooms)
    {
        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for(int x = 0; x < (int)rooms[i].getSize().x && !rooms[i].getAdjacencyState(); x++)
            {
                if(!((int)rooms[i].getPosition().y - 1 < 0))
                {
                    rooms[i].setAdjacencyState(tiles[x + (int)rooms[i].getPosition().x, (int)rooms[i].getPosition().y - 1].isActive());
                }
                if (!((int)rooms[i].getPosition().y + (int)rooms[i].getSize().y >= height))
                {
                    rooms[i].setAdjacencyState(tiles[x + (int)rooms[i].getPosition().x, (int)rooms[i].getPosition().y + (int)rooms[i].getSize().y].isActive());
                }
            }
            for (int y = 0; y < (int)rooms[i].getSize().y && !rooms[i].getAdjacencyState(); y++)
            {
                if (!((int)rooms[i].getPosition().x - 1 < 0))
                {
                    rooms[i].setAdjacencyState(tiles[(int)rooms[i].getPosition().x - 1, y + (int)rooms[i].getPosition().y].isActive());
                }
                if (!((int)rooms[i].getPosition().x + (int)rooms[i].getSize().x >= width))
                {
                    rooms[i].setAdjacencyState(tiles[(int)rooms[i].getPosition().x + (int)rooms[i].getSize().x, y + (int)rooms[i].getPosition().y].isActive());
                }
            }
        }
    }

    private float RandomNormal(int mean)
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float randStdNormal = Mathf.Sqrt(-2f * Mathf.Log((float)u1)) * Mathf.Sin(2f * Mathf.PI * u2);
        return mean + Mathf.Pow(mean, 1f/3f) * randStdNormal;
    }

    private class TileInformation
    {
        private bool active;
        private GameObject tile;
        private int x, y;
        public TileInformation(int x, int y)
        {
            active = false;
            this.x = x;
            this.y = y;
        }

        public void activateTile(GameObject tileType)
        {
            active = true;
            tile = (GameObject)Instantiate(tileType);
            tile.transform.position = new Vector2(x, y);
        }

        public bool isActive() { return active; }
    }
    
    private class RoomInformation
    {
        private Vector2 position;
        private Vector2 size;
        private bool adjacencyState;

        public RoomInformation(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
            adjacencyState = false;
        }

        public void setAdjacencyState(bool adjacencyState) { this.adjacencyState = adjacencyState; }

        public bool getAdjacencyState() { return adjacencyState; }

        public Vector2 getPosition() { return position; }

        public Vector2 getSize() { return size; }
    }
}
