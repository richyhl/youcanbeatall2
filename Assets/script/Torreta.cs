using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public GameObject disparoPrefab;
    public Transform puntoDisparo;
    public float fuerzaDisparo = 10f;
    public GameObject escudo;

    public float energiaMaxima = 100f;
    public float velocidadRecuperacion = 10f;

    private bool escudoActivado = false;
    [SerializeField] private float energiaActual;
    private float tiempoSinEnergia = 1f;
    private float tiempoRetrasoRecuperacion = 5f;

    void Start()
    {
        energiaActual = energiaMaxima;
    }

    void Update()
    {
        // Obtener la dirección hacia el puntero del mouse en 2D
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = (worldMousePosition - transform.position).normalized;

        // Rotar la torreta hacia el puntero del mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Disparar cuando se presione el botón izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }

        // Activar o desactivar el escudo al mantener presionada la tecla "Z"
        if (Input.GetKeyDown(KeyCode.Z))
        {
            escudoActivado = true;
            escudo.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            escudoActivado = false;
            escudo.SetActive(false);
        }

        // Manejar la energía del escudo
        if (escudoActivado)
        {
            if (energiaActual > 0)
            {
                energiaActual -= Time.deltaTime;
            }
        }
        else
        {
            if (energiaActual < energiaMaxima)
            {
                if (energiaActual <= 0)
                {
                    if (tiempoSinEnergia >= tiempoRetrasoRecuperacion)
                    {
                        energiaActual += velocidadRecuperacion * Time.deltaTime;
                    }
                    else
                    {
                        tiempoSinEnergia += Time.deltaTime;
                    }
                }
                else
                {
                    energiaActual += velocidadRecuperacion * Time.deltaTime;
                }
            }
        }

        // Desactivar el escudo cuando la energía llegue a cero
        if (energiaActual <= 0)
        {
            energiaActual = 0;
            escudoActivado = false;
            escudo.SetActive(false);
            tiempoSinEnergia = 1f;
        }
    }

    void Disparar()
    {
        // Verificar si el escudo está activado antes de disparar
        if (escudoActivado)
        {
            // Código para disparar con el escudo activado (opcional)
        }
        else
        {
            // Código para disparar sin el escudo activado
            GameObject disparo = Instantiate(disparoPrefab, puntoDisparo.position, transform.rotation);
            Rigidbody2D rbDisparo = disparo.GetComponent<Rigidbody2D>();
            rbDisparo.AddForce(transform.right * fuerzaDisparo, ForceMode2D.Impulse);
        }
    }
}
