using System.Collections;
using UnityEngine;
using YouCanBeatAll.ScriptableObjects;

public class Proyectile : MonoBehaviour
{

    [SerializeField] private Sprite BulletHit;
    [SerializeField] private TipoDeBala bulletType;
    [SerializeField] private float speed;
    [SerializeField] private float destroyDelay = 2.5f;
    public bool isBulletFromPlayer = false;
    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.PLAYER && !isBulletFromPlayer)
        {
            var playerCannon = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponentInChildren<Cannon>();
            playerCannon.Die();
            StartCoroutine(SetProyectileHit());
        }

        if (collision.tag == Tags.SHOOTABLE_ITEM && isBulletFromPlayer)
        {
            Dron drone = collision.GetComponent<Dron>();
            if (drone.BulletType == TipoDeBala.Indiferente || drone.BulletType == bulletType)
            {
                drone.TakeHit();
                StartCoroutine(SetProyectileHit());
            }
        }

        if (collision.tag == Tags.SHIELD)
        {
            StartCoroutine(SetProyectileHit());
        }
    }

    public void LaunchProyectile(Vector2 Direction)
    {
        rigidBody2D.velocity = Direction * speed;
    }

    private IEnumerator SetProyectileHit()
    {
        spriteRenderer.sprite = BulletHit;
        rigidBody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
