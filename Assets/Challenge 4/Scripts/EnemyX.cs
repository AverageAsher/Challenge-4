public class EnemyX : MonoBehaviour
{
    // Speed at which the enemy moves
    public float speed;

    // Rigidbody component for controlling enemy physics
    private Rigidbody enemyRb;

    // Reference to the player goal object
    private GameObject playerGoal;

    // Reference to the SpawnManagerX script to access enemy speed
    private SpawnManagerX spawnManagerX;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the enemy
        enemyRb = GetComponent<Rigidbody>();

        // Find the player goal object in the scene
        playerGoal = GameObject.Find("Player Goal");

        // Get the SpawnManagerX script from the Spawn Manager object
        spawnManagerX = GameObject.Find("Spawn Manager").GetComponent<SpawnManagerX>();

        // Set the enemy's speed based on the current speed from SpawnManagerX
        speed = spawnManagerX.enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction towards the player goal
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;

        // Move the enemy towards the player goal using the calculated direction and speed
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);
    }

    // Called when the enemy collides with another object
    private void OnCollisionEnter(Collision other)
    {
        // If the enemy collides with the "Enemy Goal", destroy the enemy
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        }
        // If the enemy collides with the "Player Goal", destroy the enemy
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }
    }
}
