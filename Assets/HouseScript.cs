using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    VillagerScript[] villagers;
    int capacity;
    int nbOfVillager = 0;

    private void Start()
    {

        capacity = GetComponentInParent<ActivityScript>().acTemp.capacity;
        villagers = new VillagerScript[capacity];
    }

    public void addVillagerToHouse(VillagerScript vs)
    {
        villagers[nbOfVillager] = vs;
    }


}
