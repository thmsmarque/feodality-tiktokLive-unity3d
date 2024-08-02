using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Activity")]
public class ActivityTemplate : ScriptableObject
{

    public enum TYPE{
        FOOD,MATERIALS,DEFENSE,TRAINING,RESTING,CULT
    };

    public string name;
    public TYPE activityType;
    public GameObject prefab;
    public int capacity;
    public float multiplierEfficacity;
    public float costInMaterials;

    public Vector3 sizeOfCollider;
    public float elevatedHeight = 0f;

    public float health;
    public float radiusChecking;

    public Image shopImage;

    

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
    public bool isCultActivity()
    {
        return TYPE.CULT == activityType;
    }

}
