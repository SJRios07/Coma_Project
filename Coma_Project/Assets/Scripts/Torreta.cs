using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    public float force;
    public GameObject Tbullet;
    public GameObject ObjTorreta;
    public Transform Taimer;
    public Transform rotator;
    GameObject player;

    public Vector3 torretToPlayer;
    public float distanceToShoot;

    public float velocidadDisparo = 2f;
    private float proximoDisparo = 0f;


    public AudioSource torretAsource;
    public AudioClip shootClip;

    public Light luz;

    [HideInInspector]
    public bool torretaActiva;

    // Start is called before the first frame update
    void Start()
    {
        torretAsource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        torretaActiva = true;
    }

    // Update is called once per frame
    void Update()
    {
        RotateWithPlayer();

        if (torretToPlayer.magnitude <= distanceToShoot )
        {
            if (Time.time < proximoDisparo || !torretaActiva)
                return;

            proximoDisparo = Time.time + velocidadDisparo;
            Shoot();
        }

        if (torretaActiva)
        {
            luz.intensity = 30;
        }
        else
        {
            luz.intensity = 0;
        }

    }

    public void RotateWithPlayer()
    {
        Vector3 objPos = ObjTorreta.transform.position;
        Vector3 playerPos = player.transform.position;

        torretToPlayer = playerPos - ObjTorreta.transform.position;

        float x = playerPos.x - objPos.x;
        float y = playerPos.y - objPos.y;
        float aimAngle = 0;

        if (x != 0 || y != 0)
        {
            aimAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        }

        rotator.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);

    }

    public void Shoot()
    {
        torretAsource.clip = shootClip;
        torretAsource.PlayOneShot(shootClip);

        GameObject bulletTemp = Instantiate(Tbullet);
        bulletTemp.transform.position = Taimer.position;

        Rigidbody rBullet = bulletTemp.GetComponent<Rigidbody>();

        rBullet.AddForce(Taimer.transform.right * force, ForceMode.Impulse);
        
    }
}
