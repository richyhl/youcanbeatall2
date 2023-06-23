using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int puntosDeVida = 2;
   [SerializeField] private Animator anim;

    private void Start()
    {
        anim=GetComponent<Animator>();
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
}
