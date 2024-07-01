using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    public void apparitionVillager()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>().addPlayerOnBoard("test",0000);
    }
}
