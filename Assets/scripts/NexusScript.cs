using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusScript : MonoBehaviour
{

    public float health;

    GameManagerScript gm;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
    }

    public bool takingDamage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            gm.hasLost();
            return true;
        }
        else
        {
            return false;
        }
    }




}
