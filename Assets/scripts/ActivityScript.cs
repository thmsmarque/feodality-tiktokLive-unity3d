using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ActivityScript : MonoBehaviour
{
    public ActivityTemplate acTemp;
    public List<VillagerScript> villagersList = new List<VillagerScript>();



    public void addVillager(VillagerScript b)
    {
        this.villagersList.Add(b);
    }

    public void removeVillager(VillagerScript b)
    {
        if(this.villagersList.Contains(b))
            this.villagersList.Remove(b);
    }

    public List<VillagerScript> getVillagerList()
    {
        return villagersList;
    }

    public ActivityTemplate getActivityTemplate()
    {
        return acTemp;
    }

}