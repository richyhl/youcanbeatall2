using UnityEngine;

public class Cañon : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Proyectil proyectilprefab;
    [SerializeField, Range(1f, 20f)] private float rotationSpeed;
    [SerializeField] private Transform shootPosition;

    /*public float ClipLeght = 1f;
    public GameObject AudioClip;*/

    void Start()
    {

        //AudioClip.SetActive(false);
        cam= Camera.main;
    }

  
    void Update()
    {
        Vector2 mouseWorldPoint= cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPoint - (Vector2)transform.position;
        transform.up= Vector2.MoveTowards(transform.up, direction, rotationSpeed * Time.deltaTime);

        if(Input.GetMouseButtonDown(0))
        {
            Proyectil proyectile = Instantiate(proyectilprefab, shootPosition.position, transform.rotation);
            proyectile.LaunchProyectile(transform.up);
            /*AudioClip.SetActive(true);
            yield return new WaitForSeconds(ClipLeght);
            AudioClip.SetActive(false);*/
        }
    }
}
