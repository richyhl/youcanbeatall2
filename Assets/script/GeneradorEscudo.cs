using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEscudo : MonoBehaviour
{
    public GameObject escudos;
    public float energiaMaxima = 100f;
    public float consumoEnergia = 10f;
    public float regeneracionEnergia = 5f;

    private bool escudoActivado = false;
    private float energiaActual;

    private void Start()
    {
        escudos.SetActive(false);
        energiaActual = energiaMaxima;
    }

    private void Update()
    {
        if (energiaActual <= 0f)
        {
            escudoActivado = false;
            escudos.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Z) && energiaActual > 0f)
        {
            escudoActivado = true;
            escudos.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Z) || energiaActual <= 0f)
        {
            escudoActivado = false;
            escudos.SetActive(false);
        }

        if (escudoActivado)
        {
            energiaActual -= consumoEnergia * Time.deltaTime;
            energiaActual = Mathf.Clamp(energiaActual, 0f, energiaMaxima);
        }
        else
        {
            energiaActual += regeneracionEnergia * Time.deltaTime;
            energiaActual = Mathf.Clamp(energiaActual, 0f, energiaMaxima);
        }
    }
}
