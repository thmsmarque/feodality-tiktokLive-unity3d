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
    public int capacity;

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

    

    public bool isFoodActivity()
    {
        return activityType == TYPE.FOOD;
    }
    public bool isMaterialsActivity()
    {
        return activityType == TYPE.MATERIALS;
    }
    public bool isDefenseActivity()
    {
        return TYPE.DEFENSE == activityType;
    }
    public bool isRestingActivity()
    {
        return TYPE.RESTING == activityType;
    }
    public bool isTrainingActivity()
    {
        return TYPE.RESTING == activityType;
    }

}
