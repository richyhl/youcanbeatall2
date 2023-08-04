using UnityEngine;
using YouCanBeatAll.ScriptableObjects;

public class Dron : MonoBehaviour
{
    [SerializeField] private SODron sODron;
    [SerializeField] private GameObject prefabProyectile;
    [SerializeField] private Transform proyectileSpawnPoint;
    public TipoDeBala BulletType => sODron.tipoDeBala;

    private int hitPoints;
    private float timeBetweenShotsTimer = 0;
    private Animator animator;

    public void Start()
    {
        hitPoints = sODron.vida;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!MainGameUI.IsCountDownHappening && !GameManager.IsGamePaused)
        {
            if (timeBetweenShotsTimer > sODron.timeBetweenShots)
            {
                ShootProyectile();
                timeBetweenShotsTimer = 0;
            }
            else
            {
                timeBetweenShotsTimer += Time.deltaTime;
            }
        }
    }

    public void TakeHit()
    {
        hitPoints--;
        if (hitPoints == 0)
        {
            animator.SetBool("explode", true);
            GameManager.DroneKilled(sODron.puntos);
            LevelManager.DroneKilled();
        }
    }

    public void RemoveDrone()
    {
        LevelManager.DroneKilled();
        Destroy(gameObject);
    }

    public void DestroyDrone() => Destroy(gameObject);

    private void ShootProyectile()
    {
        Transform playerGOTransform = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        Vector3 proyectileDirection = (playerGOTransform.position - proyectileSpawnPoint.position).normalized;
        GameObject proyectile = Instantiate(prefabProyectile, proyectileSpawnPoint.position, Quaternion.identity);
        Proyectile mProyectile = proyectile.GetComponent<Proyectile>();
        mProyectile.LaunchProyectile(proyectileDirection);
    }
}
