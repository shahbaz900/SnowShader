using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public Terrain terrain;
    public int terrainWidth = 256;
    public int terrainHeight = 256;
    public int chunkSize = 16;
    public int viewDistance = 10;

    private Transform player;
    private Vector3 lastPlayerPosition;
    private float terrainWidthHalf;
    private float terrainHeightHalf;

    void Start()
    {
        player = Camera.main.transform;
        lastPlayerPosition = player.position;
        terrainWidthHalf = terrainWidth / 2f;
        terrainHeightHalf = terrainHeight / 2f;

        GenerateInitialTerrain();
    }

    void Update()
    {
        if (Vector3.Distance(player.position, lastPlayerPosition) >= chunkSize)
        {
            lastPlayerPosition = player.position;
            UpdateTerrain();
        }
    }

    void GenerateInitialTerrain()
    {
        // This method should generate the initial terrain.
        for (int x = -viewDistance; x < viewDistance; x++)
        {
            for (int y = -viewDistance; y < viewDistance; y++)
            {
                GenerateTerrainChunk(x, y);
            }
        }
    }

    void UpdateTerrain()
    {
        // Remove and regenerate terrain chunks based on player's position.
        // This is a placeholder implementation. You'll need to track and manage active chunks.
        for (int x = -viewDistance; x < viewDistance; x++)
        {
            for (int y = -viewDistance; y < viewDistance; y++)
            {
                GenerateTerrainChunk(x, y);
            }
        }
    }

    void GenerateTerrainChunk(int x, int y)
    {
        TerrainData terrainData = new TerrainData();
        terrainData.heightmapResolution = 256;
        terrainData.size = new Vector3(chunkSize, 50, chunkSize);
        terrainData.SetHeights(0, 0, GenerateHeights(x, y));

        Terrain chunkTerrain = Terrain.CreateTerrainGameObject(terrainData).GetComponent<Terrain>();
        chunkTerrain.transform.position = new Vector3(x * chunkSize, 0, y * chunkSize);
    }

    float[,] GenerateHeights(int x, int y)
    {
        float[,] heights = new float[terrainWidth, terrainHeight];
        for (int i = 0; i < terrainWidth; i++)
        {
            for (int j = 0; j < terrainHeight; j++)
            {
                heights[i, j] = Mathf.PerlinNoise((i + x * chunkSize) / 50f, (j + y * chunkSize) / 50f);
            }
        }
        return heights;
    }
}
