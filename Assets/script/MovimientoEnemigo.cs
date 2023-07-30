using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public float velocidad = 5f;
    private Vector2 direccion;
    private Rigidbody2D rbEnemigo;
    private Collider2D colEnemigo;
    private Camera cam;

    void Start()
    {
        rbEnemigo = GetComponent<Rigidbody2D>();
        colEnemigo = GetComponent<Collider2D>();
        cam = Camera.main;

        // Iniciar el movimiento aleatorio del enemigo
        CambiarDireccionAleatoria();
    }

    void FixedUpdate()
    {
        // Mover el enemigo en la dirección actual
        rbEnemigo.velocity = direccion * velocidad;

        // Verificar si el enemigo ha llegado a los límites de la pantalla
        Vector3 puntoInferiorIzquierdo = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 puntoSuperiorDerecho = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));
        float limiteIzquierdo = puntoInferiorIzquierdo.x + colEnemigo.bounds.extents.x;
        float limiteDerecho = puntoSuperiorDerecho.x - colEnemigo.bounds.extents.x;
        float limiteInferior = puntoInferiorIzquierdo.y + colEnemigo.bounds.extents.y;
        float limiteSuperior = puntoSuperiorDerecho.y - colEnemigo.bounds.extents.y;

        if (transform.position.x < limiteIzquierdo && direccion.x < 0f)
        {
            // Invertir la dirección en el eje X
            direccion.x = -direccion.x;
        }
        else if (transform.position.x > limiteDerecho && direccion.x > 0f)
        {
            // Invertir la dirección en el eje X
            direccion.x = -direccion.x;
        }

        if (transform.position.y < limiteInferior && direccion.y < 0f)
        {
            // Invertir la dirección en el eje Y
            direccion.y = -direccion.y;
        }
        else if (transform.position.y > limiteSuperior && direccion.y > 0f)
        {
            // Invertir la dirección en el eje Y
            direccion.y = -direccion.y;
        }
    }

    void CambiarDireccionAleatoria()
    {
        // Generar una dirección aleatoria utilizando valores entre -1 y 1 en cada componente
        direccion = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
