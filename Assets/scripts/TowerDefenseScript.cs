using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerDefenseScript : MonoBehaviour
{
    public Transform pointSpawn;

    pulic bool hasTouret;
    TowerScript touret;

    void Start()
    {
        pointSpawn = GetComponentInChildren<Transform>();
        hasTouret = false;
    }

    public void newTouret(TowerScript tw)
    {
        touret = tw;
        hasTouret = true;
    }

    public void removeTouret()
    {
        Remove(touret);
        hasTouret = false;
    }


}