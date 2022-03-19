using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalaTorreta : MonoBehaviour
{

    public int dano;
    //public GameObject bala;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        SctPlayerController player = collision.gameObject.GetComponent<SctPlayerController>();
        if (player)
        {
            Debug.Log("Shoke con player");
            //player.Die();
            Destroy(this.gameObject);
        }

        //Destroy(this.gameObject);
    }
}
