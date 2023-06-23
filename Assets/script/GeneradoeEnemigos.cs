using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradoeEnemigos : MonoBehaviour
{

    public GameObject enemigoPrefab;
    public float tiempoMinimo = 1f;
    public float tiempoMaximo = 3f;
    public float rangoX = 5f;
    public float rangoY = 5f;

    private float tiempoSiguienteGeneracion;

    void Start()
    {
        // Establecer el tiempo para la primera generación de enemigos
        tiempoSiguienteGeneracion = Time.time + ObtenerTiempoAleatorio();
    }

    void Update()
    {
        // Verificar si es el momento de generar un nuevo enemigo
        if (Time.time >= tiempoSiguienteGeneracion)
        {
            GenerarEnemigo();

            // Establecer el tiempo para la siguiente generación de enemigos
            tiempoSiguienteGeneracion = Time.time + ObtenerTiempoAleatorio();
        }
    }

    float ObtenerTiempoAleatorio()
    {
        // Generar un tiempo aleatorio dentro del rango especificado
        return Random.Range(tiempoMinimo, tiempoMaximo);
    }

    void GenerarEnemigo()
    {
        // Generar una posición aleatoria dentro del rango especificado
        Vector3 posicionGeneracion = new Vector3(Random.Range(-rangoX, rangoX), Random.Range(-rangoY, rangoY), 0f);

        // Instanciar un nuevo enemigo en la posición generada
        Instantiate(enemigoPrefab, posicionGeneracion, Quaternion.identity);
    }
}
