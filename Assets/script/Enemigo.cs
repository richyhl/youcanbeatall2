using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int puntosDeVida = 2;
    public GameObject disparoPrefab;
    public Transform puntoDisparo;
    public float velocidadDisparo = 5f;
    public float tiempoEntreDisparos = 3f;
    private Rigidbody2D rbEnemigo;
    public float fuerzaMovimiento = 3f;
    public float tiempoCambioDireccion = 2f;
    private float tiempoSiguienteDisparo;
    private float tiempoSiguienteCambioDireccion;

   
    [SerializeField] private Animator anim;

    private void Start()
    {
        rbEnemigo = GetComponent<Rigidbody2D>();
        anim =GetComponent<Animator>();
        tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
        tiempoSiguienteCambioDireccion = Time.time + tiempoCambioDireccion;
      
        
    }

    void Update()
    {
        

        // Verificar si es el momento de disparar
        if (Time.time >= tiempoSiguienteDisparo)
        {
            Disparar();

            // Establecer el tiempo para el siguiente disparo
            tiempoSiguienteDisparo = Time.time + tiempoEntreDisparos;
        }

        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala"))
        {
            // Restar 1 punto de vida al enemigo
            puntosDeVida--;

            // Destruir la bala
            Destroy(collision.gameObject);

            // Verificar si el enemigo ha muerto
            if (puntosDeVida <= 0)
            {
               
                // Realizar acciones de muerte del enemigo
                Morir();
                
            }



        }
    }
  



    void Morir()
    {
        // Acciones a realizar cuando el enemigo muere

        // Obtener el controlador de puntaje
        ControladorPuntaje controladorPuntaje = FindObjectOfType<ControladorPuntaje>();

        // Verificar si se encontró el controlador de puntaje
        if (controladorPuntaje != null)
        {
            // Sumar los puntos correspondientes al controlador de puntaje
            controladorPuntaje.SumarPuntos(10); // Por ejemplo, sumamos 10 puntos al matar un enemigo
        }

        // Destruir el GameObject del enemigo

        anim.SetTrigger("morir");

        Destroy(gameObject);

    }
    void Disparar()
    {
        // Instanciar el proyectil del enemigo desde el prefab
        GameObject disparo = Instantiate(disparoPrefab, puntoDisparo.position, Quaternion.identity);

        // Obtener la dirección hacia el jugador
        Vector3 direccion = (GameObject.FindGameObjectWithTag("Player").transform.position - disparo.transform.position).normalized;

        // Obtener el componente Rigidbody2D del proyectil del enemigo
        Rigidbody2D rbDisparo = disparo.GetComponent<Rigidbody2D>();

        // Aplicar velocidad al proyectil en la dirección hacia el jugador
        rbDisparo.velocity = direccion * velocidadDisparo;
    }
}
