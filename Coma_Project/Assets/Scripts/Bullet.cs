using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public GameObject bala;
    public AudioClip desactivar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Torreta"))
        {
            if (collision.gameObject.GetComponent<Torreta>().torretaActiva)
            {
                collision.gameObject.GetComponent<Torreta>().torretaActiva = false;
                collision.gameObject.GetComponent<Torreta>().torretAsource.PlayOneShot(desactivar);
                collision.gameObject.GetComponent<Torreta>().luz.intensity = 0;
            }
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
