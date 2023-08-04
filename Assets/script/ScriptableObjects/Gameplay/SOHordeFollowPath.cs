using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SOHordeFollowPath_", menuName = "ScriptableObjects/Sequence Follow Path Horde", order = 4)]
public class SOHordeFollowPath : SOHorde
{
    public GameObject pathToFollow;
    public bool isLoop = false;
    public float waitTimeBetweenPoints = 1f;
    public float speed = 2.5f;

    public override IEnumerator RunHorde()
    {
        Transform spawnPoint = this.FindWithTag(pathToFollow.transform, Tags.SPAWN);
        Transform[] wayPoints = this.FindArrayWithTag(pathToFollow.transform, Tags.WAYPOINT);

        if (spawnPoint == null || wayPoints.Length == 0)
        {
            throw new System.Exception("Missing information to create waypoint horde.");
        }

        for (var i = 0; i < amountOfEnemies; i++)
        {
            var mEnemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
            var mechWaypointFollower = mEnemy.GetOrAddComponent<MechWaypointFollower>();
            mechWaypointFollower.SetUpRoute(spawnPoint, wayPoints, speed, isLoop, waitTimeBetweenPoints);
            yield return new WaitForSeconds(waitTimeBetween);
        }
    }
}
