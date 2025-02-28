using UnityEngine;

public class CloudSystem : MonoBehaviour
{
    public GameObject[] cloudPrefabs;   // Array of cloud prefabs
    public int cloudCount = 20;         // Number of clouds to spawn
    public float cloudSpeed = 2f;       // Speed at which clouds move
    public float spawnRange = 100f;     // Range in which clouds are spawned
    public float cloudLifetime = 20f;   // How long each cloud stays before being recycled
    public float minDistanceBetweenClouds = 10f; // Minimum distance between clouds

    private GameObject[] clouds;        // Array to keep track of cloud instances

    private void Start()
    {
        clouds = new GameObject[cloudCount];
        for (int i = 0; i < cloudCount; i++)
        {
            SpawnCloud(i);
        }
    }

    private void Update()
    {
        MoveClouds();
    }

    // Spawn a cloud at a random position with a random prefab
    private void SpawnCloud(int index)
    {
        GameObject cloudPrefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];
        Vector3 spawnPosition;

        // Ensure the cloud spawns at a position far enough from other clouds
        do
        {
            float xPos = Random.Range(-spawnRange, spawnRange);
            float yPos = Random.Range(-spawnRange, spawnRange);
            spawnPosition = new Vector3(xPos, yPos, 0f);
        }
        while (IsTooCloseToOtherClouds(spawnPosition));

        clouds[index] = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
        Destroy(clouds[index], cloudLifetime); // Destroy the cloud after a certain lifetime
    }

    // Check if the position is too close to existing clouds
    private bool IsTooCloseToOtherClouds(Vector3 position)
    {
        foreach (GameObject cloud in clouds)
        {
            if (cloud != null)
            {
                if (Vector3.Distance(cloud.transform.position, position) < minDistanceBetweenClouds)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Move clouds across the screen
    private void MoveClouds()
    {
        foreach (GameObject cloud in clouds)
        {
            if (cloud != null)
            {
                cloud.transform.Translate(Vector3.left * cloudSpeed * Time.deltaTime);

                // Recycle cloud if it moves out of bounds
                if (cloud.transform.position.x < -spawnRange)
                {
                    float newX = spawnRange;
                    float newY = Random.Range(-spawnRange, spawnRange);
                    cloud.transform.position = new Vector3(newX, newY, 0f);
                }
            }
        }
    }
}
