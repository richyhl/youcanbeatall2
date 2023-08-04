using UnityEngine;

public class MechWaypointFollower : MonoBehaviour
{

    [Header("Properties")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distanceThreshold = .1f;
    [SerializeField] private float waitTimeBetweenPoints = 1f;
    [SerializeField] private bool isLoop = false;

    private int currentWaypointIndex = 0;
    private bool canMove = true;
    private float canMoveTimer = 0;
    private float previousXPosition = 0;
    private Dron drone;

    private void Start()
    {
        drone= GetComponent<Dron>();
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime;
        if (canMove)
        {
            var currentPosition = transform.position;
            var currentWaypointPosition = wayPoints[currentWaypointIndex].position;
            if (Vector2.Distance(currentWaypointPosition, currentPosition) < distanceThreshold)
            {
                currentWaypointIndex++;

                // If cycle completed.
                if (currentWaypointIndex >= wayPoints.Length)
                {
                    if (isLoop)
                    {
                        currentWaypointIndex = 0;
                        transform.position = spawnPoint.position;
                    }
                    else
                    {
                        drone.RemoveDrone();
                    }
                }

                if (canMove && waitTimeBetweenPoints > 0)
                {
                    canMove = false;
                }
            }
            else
            {
                var newPosition = Vector2.MoveTowards(currentPosition, currentWaypointPosition, speed * deltaTime);
                var newXPosition = newPosition.x;
                transform.position = newPosition;
                if (newXPosition != previousXPosition)
                {
                    previousXPosition = newXPosition;
                }
            }
        }
        else
        {
            if (canMoveTimer >= waitTimeBetweenPoints)
            {
                canMove = true;
                canMoveTimer = 0;
            }
            else
            {
                canMoveTimer += deltaTime;
            }
        }
    }

    internal void SetUpRoute(
        Transform spawnPoint,
        Transform[] wayPoints,
        float speed,
        bool isLoop,
        float waitTimeBetweenPoints)
    {
        this.speed = speed;
        this.wayPoints = wayPoints;
        this.waitTimeBetweenPoints = waitTimeBetweenPoints;
        this.isLoop = isLoop;
        this.spawnPoint = spawnPoint;
    }
}
