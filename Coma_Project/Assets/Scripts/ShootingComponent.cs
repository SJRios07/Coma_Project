using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingComponent : MonoBehaviour
{
    public float force;
    public GameObject bullet;
    public Transform aimer;
    public Transform rotator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateWithMouse();
        if (Input.GetMouseButtonDown(0))
        {

            Shoot();

        }
    }

    public void RotateWithMouse()
    {
        Vector3 objPos = Camera.main.WorldToScreenPoint(rotator.transform.position);
        Vector3 mousePos = Input.mousePosition;

        float x = mousePos.x - objPos.x;
        float y = mousePos.y - objPos.y;
        float aimAngle=0;

        if (x != 0 || y != 0)
        {
            aimAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        }

        rotator.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);

    }

    public void Shoot()
    {
        Debug.Log("Shot");
        GameObject bulletTemp = Instantiate(bullet);
        bulletTemp.transform.position = aimer.position;

        Rigidbody rBullet = bulletTemp.GetComponent<Rigidbody>();

        /*Vector3 forceVector = player.transform.position - aimer.transform.position;

        Debug.DrawLine(aimer.transform.position, player.transform.position, Color.green, 5f);
        forceVector.Normalize();

        forceVector *= force;*/

        //Vector3 shootForce = aimer.right * force;

        rBullet.AddForce(aimer.transform.right * force, ForceMode.Impulse);
    }
}
