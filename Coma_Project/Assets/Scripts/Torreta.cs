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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RotateWithPlayer();

        if (torretToPlayer.magnitude <= distanceToShoot )
        {
            if (Time.time < proximoDisparo)
                return;

            proximoDisparo = Time.time + velocidadDisparo;
            Shoot();

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
        //Debug.Log("Shot");
        GameObject bulletTemp = Instantiate(Tbullet);
        bulletTemp.transform.position = Taimer.position;

        Rigidbody rBullet = bulletTemp.GetComponent<Rigidbody>();

        //Vector3 forceVector = player.transform.position - Taimer.transform.position;

        //Debug.DrawLine(Taimer.transform.position, player.transform.position, Color.green, 5f);
        //forceVector.Normalize();

        //forceVector *= force;

        //Vector3 shootForce = Taimer.right * force;

        rBullet.AddForce(Taimer.transform.right * force, ForceMode.Impulse);

        
    }
}
