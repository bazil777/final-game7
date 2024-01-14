using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject matchesPrefab; // Assign Matches prefab in the inspector
    public GameObject plasticShardsPrefab; // Assign Plastic Shards prefab in the inspector
    public GameObject treePrefab; // Assign Tree prefab in the inspector
    public GameObject bearPrefab; // Assign Bear prefab in the inspector
    public int numberOfBearsToSpawn = 2; 

    public int numberOfDesertItemsToSpawn = 50; // Set the number of desert items to spawn
    public int numberOfForestItemsToSpawn = 50; // Set the number of forest items to spawn

    public Terrain desertTerrain; // Assign your desert terrain
    public Terrain forestTerrain; // Assign your forest terrain

    void Start()
    {
        SpawnItemsInDesert(matchesPrefab, numberOfDesertItemsToSpawn);
        SpawnItemsInDesert(plasticShardsPrefab, numberOfDesertItemsToSpawn);
        SpawnItemsInForest(treePrefab, numberOfForestItemsToSpawn);
        SpawnBearsInForest(numberOfBearsToSpawn);
    }

    void SpawnItemsInDesert(GameObject itemPrefab, int numberOfItems)
{
    int numberOfPlasticShards = numberOfItems * 4 / 5; // 4:1 ratio of plastic shards to matches

    for (int i = 0; i < numberOfPlasticShards; i++)
    {
        // Generate random x and z coordinates within the desert terrain boundaries
        float randomX = Random.Range(desertTerrain.transform.position.x, desertTerrain.transform.position.x + desertTerrain.terrainData.size.x);
        float randomZ = Random.Range(desertTerrain.transform.position.z, desertTerrain.transform.position.z + desertTerrain.terrainData.size.z);
        float y = GetTerrainHeight(randomX, randomZ, desertTerrain);

        Vector3 randomPosition = new Vector3(randomX, y, randomZ);

        // Instantiate the plastic shards at the random position
        Instantiate(plasticShardsPrefab, randomPosition, Quaternion.identity, transform);
    }

    int numberOfMatches = numberOfItems - numberOfPlasticShards; // Calculate the number of matches

    for (int i = 0; i < numberOfMatches; i++)
    {
        // Generate random x and z coordinates within the desert terrain boundaries
        float randomX = Random.Range(desertTerrain.transform.position.x, desertTerrain.transform.position.x + desertTerrain.terrainData.size.x);
        float randomZ = Random.Range(desertTerrain.transform.position.z, desertTerrain.transform.position.z + desertTerrain.terrainData.size.z);
        float y = GetTerrainHeight(randomX, randomZ, desertTerrain);

        Vector3 randomPosition = new Vector3(randomX, y, randomZ);

        // Instantiate the matches at the random position
        Instantiate(matchesPrefab, randomPosition, Quaternion.identity, transform);
    }
}


    void SpawnItemsInForest(GameObject itemPrefab, int numberOfItems)
    {
        // Generate and spawn trees in the forest terrain
        for (int i = 0; i < numberOfItems; i++)
        {
            // Generate random x and z coordinates within the forest terrain boundaries
            float randomX = Random.Range(forestTerrain.transform.position.x, forestTerrain.transform.position.x + forestTerrain.terrainData.size.x);
            float randomZ = Random.Range(forestTerrain.transform.position.z, forestTerrain.transform.position.z + forestTerrain.terrainData.size.z);
            float y = GetTerrainHeight(randomX, randomZ, forestTerrain);

            Vector3 randomPosition = new Vector3(randomX, y, randomZ);

            // Instantiate the tree at the random position
            Instantiate(itemPrefab, randomPosition, Quaternion.identity, transform);
        }
    }

    void SpawnBearsInForest(int numberOfBears)
    {
        for (int i = 0; i < numberOfBears; i++)
        {
            float randomX = Random.Range(forestTerrain.transform.position.x, forestTerrain.transform.position.x + forestTerrain.terrainData.size.x);
            float randomZ = Random.Range(forestTerrain.transform.position.z, forestTerrain.transform.position.z + forestTerrain.terrainData.size.z);
            float y = GetTerrainHeight(randomX, randomZ, forestTerrain);

            Vector3 randomPosition = new Vector3(randomX, y, randomZ);

            Instantiate(bearPrefab, randomPosition, Quaternion.identity, transform);
        }
    }

    float GetTerrainHeight(float x, float z, Terrain terrain)
    {
        // Convert world coordinates x and z to terrain coordinates
        Vector3 terrainPos = terrain.transform.position;
        float relativeX = (x - terrainPos.x) / terrain.terrainData.size.x;
        float relativeZ = (z - terrainPos.z) / terrain.terrainData.size.z;

        // Get the height at the terrain coordinates
        float y = terrain.terrainData.GetHeight(Mathf.RoundToInt(relativeX * terrain.terrainData.heightmapResolution),
                                                       Mathf.RoundToInt(relativeZ * terrain.terrainData.heightmapResolution));

        // Convert the relative height back to world height
        return y + terrainPos.y;
    }
}
