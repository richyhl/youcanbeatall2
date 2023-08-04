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

    [Header("White Turret")]
    [SerializeField] private Sprite turretBaseWhite;
    [SerializeField] private Sprite turretCannonWhite;
    [SerializeField] private Proyectile proyectilBlanco;

    [Header("Black Turret")]
    [SerializeField] private Sprite turretBaseBlack;
    [SerializeField] private Sprite turretCannonBlack;
    [SerializeField] private Proyectile proyectilNegro;

    [SerializeField, Range(1f, 20f)] private float rotationSpeed;

    void Update()
    {
        Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPoint - (Vector2)transform.position;
        transform.up = Vector2.MoveTowards(transform.up, direction, rotationSpeed * Time.deltaTime);
    }

    public void ToggleShield(bool isActive)
    {
        turretShield.SetActive(isActive);
    }

    public void ShootProyectile(TipoDeBala tipoDeBala)
    {
        Proyectile proyectileToUse;
        if (tipoDeBala == TipoDeBala.Blanca)
        {
            proyectileToUse = proyectilBlanco;
            turretBase.sprite = turretBaseWhite;
            turretCannon.sprite = turretCannonWhite;
        }
        else
        {
            proyectileToUse = proyectilNegro;
            turretBase.sprite = turretBaseBlack;
            turretCannon.sprite = turretCannonBlack;
        }
        var mProyectile = Instantiate(proyectileToUse, shootPosition.position, transform.rotation);
        mProyectile.isBulletFromPlayer = true;
        mProyectile.LaunchProyectile(transform.up);
    }
}
