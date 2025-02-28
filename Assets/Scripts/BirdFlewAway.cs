using UnityEngine;

public class BirdFlewAway : MonoBehaviour
{
    public float flySpeed = 5f;             // Speed at which the bird flies
    public float maxDistance = 20f;         // Maximum distance before the bird is destroyed
    public Vector2 randomDirectionRange = new Vector2(0.5f, 2.5f); // Range for random flight direction

    private Vector3 startPosition;
    private Vector3 flyDirection;

    void Start()
    {
        // Save the starting position of the bird
        startPosition = transform.position;

        // Generate a random direction for the bird to fly in
        flyDirection = new Vector3(
            Random.Range(-randomDirectionRange.x, randomDirectionRange.x),
            Random.Range(randomDirectionRange.y, randomDirectionRange.y),
            0).normalized;

        AudioManager.instance.PlaySFX("FlyAway");
    }

    void Update()
    {
        // Move the bird in the generated direction
        transform.position += flyDirection * flySpeed * Time.deltaTime;

        // Check if the bird has flown far enough to be destroyed
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
