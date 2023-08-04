using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D proyectilRb;
    [SerializeField] private float destroyDeay = 10f;
    private void Awake()
    {
        proyectilRb = GetComponent<Rigidbody2D>();
    }

    public void LaunchProyectile(Vector2 Direction)
    {
        proyectilRb.velocity = Direction * speed;
        
    }
    private void Update()
    {
        Destroy(gameObject, destroyDeay);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
