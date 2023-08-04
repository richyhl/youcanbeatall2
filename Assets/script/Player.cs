using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool isDamage;
    public static int vidaref;
    public int vida;
    public int vidaMaxima;
    public Animator anim;
    SpriteRenderer sprite;
    Blink material;
    
    private bool terminar = false;
    public SpriteRenderer fuego;




    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        vida = vidaMaxima;
        // Reiniciar.gameObject.SetActive(false);      
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bala") && !isDamage)
        {
            vida -= 1;
            
            StartCoroutine(Efecto());
            StartCoroutine(Dañado());


        }
        if (collision.CompareTag("Enemigo") && !isDamage)
        {
            vida -= 1;
            
            StartCoroutine(Efecto());
            StartCoroutine(Dañado());


        }


    }

    IEnumerator Dañado()
    {
        isDamage = true;
        yield return new WaitForSeconds(1);
        isDamage = false;
    }

    IEnumerator Efecto()
    {
        sprite.material = material.blink;
        yield return new WaitForSeconds(0.2f);
        sprite.material = material.orginal;
        yield return new WaitForSeconds(0.2f);
        sprite.material = material.blink;
        yield return new WaitForSeconds(0.2f);
        sprite.material = material.orginal;
        yield return new WaitForSeconds(0.2f);
        sprite.material = material.blink;
        yield return new WaitForSeconds(0.2f);
        sprite.material = material.orginal;


    }
    void Update()
    {

        vidaref = vida;
        if (vida <= 0)
        {
            Destroy(gameObject);
            Muerte();

        }

        if (vida <= 200)
        {
            fuego.enabled = true;
        }
        else
        {
            fuego.enabled = false;
        }
    }

    private void Muerte()
    {

        anim.SetBool("muerte", true);

        StartCoroutine(Load());




       // GameMAnager.Puntuacion = 0;





    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(.8f);
        Destroy(gameObject);

        terminar = true;
        if (terminar == true)
        {

            SceneManager.LoadScene(0);
            terminar = false;
        }




    }

}
