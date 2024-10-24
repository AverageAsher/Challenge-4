public class RotateCameraX : MonoBehaviour
{
    // Speed at which the camera rotates
    private float speed = 200;

    // Reference to the player object
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        // Get horizontal input (A/D or arrow keys) for camera rotation
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotate the camera around the Y-axis based on horizontal input
        transform.Rotate(Vector3.up, horizontalInput * speed * Time.deltaTime);

        // Move the camera's position to follow the player's position
        transform.position = player.transform.position;
    }
}
