using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GUIManager : MonoBehaviour
{
    
    public Image lifeBar;

    public float maxHealth = 100;
    public float currentHealth;
    public GameObject deadMessage;
    public GameObject endGame;
    public GameObject checkFinal;
    [HideInInspector]
    public bool dead;
    public bool finished;

    GameObject player;


    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");

    }
   
    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -11f)
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            player.GetComponent<SctPlayerController>().dead = true;
            deadMessage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
        else
        {
            deadMessage.SetActive(false);
        }

        if (finished)
        {
            endGame.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }

        if (checkFinal.GetComponent<SctCheckpoint>().activado)
        {
            finished = true;
        }
        else
        {
            finished = false;
        }
    }

    public void SetMaxAmount(int health)
    {
        //Image.MAxvalue = health;
        //Image.value = health;
    }



    public void ReceiveDamage(int damage)
    {
        

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }


        float myValue = Remap(currentHealth, 0, maxHealth, 0, 1);
        //Debug.Log(myValue);
        lifeBar.fillAmount = myValue;
    }


    public void AddLife(int extraLife)
    {
        
            
        currentHealth += extraLife;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;

        }

        float myValue = Remap(currentHealth, 0, maxHealth, 0, 1);
        //Debug.Log(myValue);
        lifeBar.fillAmount = myValue;
    }


    public float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}
