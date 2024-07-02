using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    GameObject gm;
    int numbVillager;
    private void Start()
    {
       gm = GameObject.FindWithTag("GameController");
       numbVillager = 0;
    }
    public void apparitionVillager()
    {
        gm.GetComponent<GameManagerScript>().addPlayerOnBoard("test"+numbVillager, 0000);
        numbVillager++;
    }

    public void apparitionFood()
    {

    }
}
