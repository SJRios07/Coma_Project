using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalaTorreta : MonoBehaviour
{

    public int dano;
    GUIManager guimanager;
    //public GameObject bala;

    // Start is called before the first frame update
    void Start()
    {
        guimanager = FindObjectOfType<GUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {

        SctPlayerController player = collision.gameObject.GetComponent<SctPlayerController>();
        if (player)
        {
            Destroy(this.gameObject);
            guimanager.ReceiveDamage(10);
        }
        else {
            Destroy(gameObject);
        }
    }
}
