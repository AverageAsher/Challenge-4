public class SpawnManagerX : MonoBehaviour
{
    // Reference to the enemy prefab to spawn
    public GameObject enemyPrefab;

    // Reference to the powerup prefab to spawn
    public GameObject powerupPrefab;

    // X-axis range for spawning enemies and powerups
    private float spawnRangeX = 10;

    // Minimum Z-axis value for spawning enemies and powerups
    private float spawnZMin = 15;

    // Maximum Z-axis value for spawning enemies and powerups
    private float spawnZMax = 25;

    // Current count of enemies in the scene
    public int enemyCount;

    // Current wave number (number of enemies increases with wave)
    public int waveCount = 1;

    // Speed of the enemies, which increases with each wave
    public float enemySpeed = 50;

    // Reference to the player game object
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        // Get the current number of enemies in the scene by counting all objects with the "Enemy" tag
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // If there are no enemies left, spawn the next wave
        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
        }

    }

    // Generate a random spawn position within the defined range
    Vector3 GenerateSpawnPosition()
    {
        // Generate random X position within spawn range
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);

        // Generate random Z position within min and max spawn Z values
        float zPos = Random.Range(spawnZMin, spawnZMax);

        // Return a new Vector3 with the random X and Z values and Y set to 0
        return new Vector3(xPos, 0, zPos);
    }

    // Method to spawn a wave of enemies and possibly a powerup
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Define an offset to make powerups spawn closer to the player
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15);

        // If there are no powerups remaining in the scene, spawn a new powerup
        if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
        {
            // Instantiate a powerup at a random position with the offset
            Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
        }

        // Spawn a number of enemies equal to the current wave count
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Instantiate an enemy at a random position
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        // Increase the wave count for the next round
        waveCount++;

        // Reset the player's position after spawning the enemies
        ResetPlayerPosition();

        // Increase the speed of enemies with each wave
        enemySpeed += 25;
    }

    // Reset the player's position to the starting point and stop any movement
    void ResetPlayerPosition()
    {
        // Set the player's position to the starting coordinates
        player.transform.position = new Vector3(0, 1, -7);

        // Reset the player's velocity to zero to stop movement
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // Reset the player's angular velocity to stop rotation
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

}
