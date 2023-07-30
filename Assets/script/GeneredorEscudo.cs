using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneredorEscudo : MonoBehaviour
{
    public GameObject escudos;
    private bool escudoActivado = false;


    private void Start()
    {
        escudos.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            escudoActivado = true;
            escudos.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            escudoActivado = false;
            escudos.SetActive(false);
        }
    }
}
