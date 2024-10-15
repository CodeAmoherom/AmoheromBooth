using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    public GameObject floorTilePrefab; // Your tile prefab
    public int rows = 10;
    public int columns = 10;
    public float tileSize = 1.0f;

    void Start()
    {
        GenerateFloor();
    }

    void GenerateFloor()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int z = 0; z < columns; z++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, z * tileSize);
                Instantiate(floorTilePrefab, position, Quaternion.identity);
            }
        }
    }
}
