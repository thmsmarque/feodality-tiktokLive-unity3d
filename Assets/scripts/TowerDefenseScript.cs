using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerDefenseScript : MonoBehaviour
{
    public Transform pointSpawn;

    public bool hasTouret;

    [SerializeField]
    TowerScript touret;

    void Start()
    {
        //pointSpawn = GetComponentInChildren<Transform>();
        hasTouret = false;
    }

    public void newTouret(TowerScript tw)
    {
        touret = tw;
        hasTouret = true;
    }

    public void removeTouret()
    {
        Destroy(touret);
        hasTouret = false;
    }


}