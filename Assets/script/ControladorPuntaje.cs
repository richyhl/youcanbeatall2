using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorPuntaje : MonoBehaviour
{
    private int puntaje = 0;
    public Text textoPuntaje;

    void Start()
    {
        // Inicializar el puntaje en cero
        puntaje = 0;

        // Actualizar el texto del puntaje en la interfaz de usuario
        ActualizarTextoPuntaje();
    }

    public void SumarPuntos(int cantidad)
    {
        // Sumar los puntos recibidos a la variable puntaje
        puntaje += cantidad;

        // Actualizar el texto del puntaje en la interfaz de usuario
        ActualizarTextoPuntaje();
    }

    void ActualizarTextoPuntaje()
    {
        // Actualizar el texto del puntaje en la interfaz de usuario con el valor actualizado
        textoPuntaje.text = "Puntaje: " + puntaje;
    }
}
