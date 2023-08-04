using UnityEngine;
using YouCanBeatAll.ScriptableObjects;
using static UnityEngine.GraphicsBuffer;

public class Cannon : MonoBehaviour
{

    [Header("Turret Elements")]
    [SerializeField] private GameObject turretShield;
    [SerializeField] private SpriteRenderer turretBase;
    [SerializeField] private SpriteRenderer turretCannon;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private AudioSource fxExplosion;
    [SerializeField] private AudioSource fxShield;
    private bool isShieldActive = false;
    public bool IsShieldActive => isShieldActive;

    [Header("White Turret")]
    [SerializeField] private Sprite turretBaseWhite;
    [SerializeField] private Sprite turretCannonWhite;
    [SerializeField] private Proyectile proyectilBlanco;
    [SerializeField] private AudioSource fxShootWhite;

    [Header("Black Turret")]
    [SerializeField] private Sprite turretBaseBlack;
    [SerializeField] private Sprite turretCannonBlack;
    [SerializeField] private Proyectile proyectilNegro;
    [SerializeField] private AudioSource fxShootBlack;

    [SerializeField, Range(1f, 20f)] private float rotationSpeed;

    private Animator animator;

    private void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPoint - (Vector2)transform.position;
        transform.up = Vector2.MoveTowards(transform.up, direction, rotationSpeed * Time.deltaTime);
    }

    public void ToggleShield(bool isActive)
    {
        isShieldActive = isActive;
        turretShield.SetActive(isActive);
        if (isActive)
        {
            fxShield.Play();
        }
    }

    public void ShootProyectile(TipoDeBala tipoDeBala)
    {
        Proyectile proyectileToUse;
        if (tipoDeBala == TipoDeBala.Blanca)
        {
            proyectileToUse = proyectilBlanco;
            turretBase.sprite = turretBaseWhite;
            turretCannon.sprite = turretCannonWhite;
            fxShootWhite.Play();
        }
        else
        {
            proyectileToUse = proyectilNegro;
            turretBase.sprite = turretBaseBlack;
            turretCannon.sprite = turretCannonBlack;
            fxShootBlack.Play();
        }
        var mProyectile = Instantiate(proyectileToUse, shootPosition.position, transform.rotation);
        mProyectile.isBulletFromPlayer = true;
        mProyectile.LaunchProyectile(transform.up);
    }

    public void Die()
    {
        fxExplosion.Play();
        animator.SetBool("explode", true);
        GameManager.AllowInputs = false;
        StartCoroutine(LevelManager.GameCompleted());
    }
}
