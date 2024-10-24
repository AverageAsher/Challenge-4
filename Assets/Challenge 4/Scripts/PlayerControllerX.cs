public class PlayerControllerX : MonoBehaviour
{
    // Rigidbody component for controlling the player's physics
    private Rigidbody playerRb;

    // Speed of the player movement
    private float speed = 500;

    // Reference to the focal point object
    private GameObject focalPoint;

    // Boolean to track if player has the powerup
    public bool hasPowerup;

    // Powerup indicator game object
    public GameObject powerupIndicator;

    // Duration of the powerup in seconds
    public int powerUpDuration = 5;

    // Strength of the player's hit without a powerup
    private float normalStrength = 10;

    // Strength of the player's hit with a powerup
    private float powerupStrength = 25;

    // Speed boost when the turbo is activated
    float turboBoost = 10f;

    // Particle system for the turbo boost effect
    public ParticleSystem turboSmoke;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the player
        playerRb = GetComponent<Rigidbody>();

        // Find the focal point object in the scene
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        // Get vertical input from the player (W/S or arrow keys) for movement
        float verticalInput = Input.GetAxis("Vertical");

        // Add force to the player in the direction of the focal point, moving forward or backward
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);

        // Position the powerup indicator below the player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        // Check if the spacebar is pressed for a turbo boost
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Apply a turbo boost force to the player in the direction of the focal point
            playerRb.AddForce(focalPoint.transform.forward * turboBoost, ForceMode.Impulse);

            // Play the turbo boost smoke effect
            turboSmoke.Play();
        }

    }

    // Called when the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with a powerup object
        if (other.gameObject.CompareTag("Powerup"))
        {
            // Start the powerup cooldown coroutine
            StartCoroutine(PowerupCooldown());

            // Destroy the powerup object
            Destroy(other.gameObject);

            // Set the powerup status to true
            hasPowerup = true;

            // Activate the powerup indicator
            powerupIndicator.SetActive(true);
        }
    }

    // Coroutine for handling the powerup duration countdown
    IEnumerator PowerupCooldown()
    {
        // Wait for the powerup duration to expire
        yield return new WaitForSeconds(powerUpDuration);

        // Set the powerup status to false
        hasPowerup = false;

        // Deactivate the powerup indicator
        powerupIndicator.SetActive(false);
    }

    // Called when the player collides with another object
    private void OnCollisionEnter(Collision other)
    {
        // Check if the player collided with an enemy object
        if (other.gameObject.CompareTag("Enemy"))
        {
            // Get the Rigidbody component of the enemy
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();

            // Calculate the direction away from the player
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            // If the player has the powerup, apply the powerup force to the enemy
            if (hasPowerup)
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            // Otherwise, apply the normal force to the enemy
            else
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
}
