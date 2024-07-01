using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Activity")]
public class ActivityTemplate : ScriptableObject
{

    public enum TYPE{
        FOOD,MATERIALS,DEFENSE,TRAINING,RESTING
    };

    public string name;
    public TYPE activityType;
    public GameObject prefab;
    public List<VillagerScript> villagersList;

    public void react()
    {
        GameManagerScript gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        switch(activityType)
        {
            case TYPE.FOOD:
                float food = 0;
                foreach(VillagerScript v in villagersList)
                {
                    food += v.getEfficacity();
                }
                gm.addFood(food);
                break;
            case TYPE.MATERIALS:
                float materials = 0f;
                foreach(VillagerScript v in villagersList)
                {
                    materials += v.getEfficacity() * 0.1f;
                }
                gm.addMaterials(materials);
                break;
            case TYPE.DEFENSE: break;
            case TYPE.TRAINING: 

                break;
            case TYPE.RESTING: break;
        }
    }

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
}
