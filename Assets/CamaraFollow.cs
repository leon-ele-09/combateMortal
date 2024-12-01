using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform player; // Reference to the player's Transform
    public Transform secondarySprite; // Reference to the secondary sprite's Transform
    public float followSpeed = 2.0f; // Speed at which the camera follows the player
    public Vector2 offset = new Vector2(0, 1); // Offset from the player's position

    private Vector3 targetPosition;
    private Vector3 previousPlayerPosition;
    private Vector3 previousSecondaryPosition;
    private Vector3 difference;
    private int factor;

    void Start()
    {
        difference = secondarySprite.position - player.position;
        factor = 1;
        // Initialize previous positions to detect movement in the first frame
        if (player != null) previousPlayerPosition = player.position;
        if (secondarySprite != null) previousSecondaryPosition = secondarySprite.position;
    }

    void Update()
    {
        // Check if either the player or the secondary sprite has moved
        if (player.position != previousPlayerPosition || secondarySprite.position != previousSecondaryPosition)
        {

            // Calculate the difference vector between the player and the secondary sprite
            float changeInX = difference.x;

            difference = secondarySprite.position - player.position;

            if (changeInX > difference.x)
            {
                factor = -1;
            }
            else
            {
                factor = 1;
            }

            // Set the target position based on player position, offset, and zoom factor
            targetPosition = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z - factor * difference.x);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Update previous positions to current positions
            previousPlayerPosition = player.position;
            previousSecondaryPosition = secondarySprite.position;
        }
    }
}
