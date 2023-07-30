using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectilenemigo : MonoBehaviour
{
    public GameObject player;
    public float velocidadMovimiento;

    private float distancia;
    private float tiempodestroy = 5f;

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(TimerDestroy());

    }


    private void Update()
    {
        distancia = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, velocidadMovimiento * Time.deltaTime);
    }

    private IEnumerator TimerDestroy()
    {
        yield return new WaitForSeconds(tiempodestroy);
        Destroy(gameObject);


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Escudo"))
        {
            Destroy(gameObject);
        }
    }
}
