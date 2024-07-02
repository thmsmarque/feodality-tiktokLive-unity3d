using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPanel : MonoBehaviour
{
    GameObject gm;
    int numbVillager;

    [SerializeField]
    GameObject activity;

    public ActivityTemplate[] foodActivities;
    public ActivityTemplate[] materialsActivities;
    public ActivityTemplate[] defenseActivities; 
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
        GameObject temp = Instantiate(activity, gameObject.transform);
        temp.GetComponent<ActivityScript>().setActivityTemplate(foodActivities[0]);
        gm.GetComponent<GameManagerScript>().addActivity(temp.GetComponent<ActivityScript>());
    }

    public void apparitionMaterials()
    {
        GameObject temp = Instantiate(activity, gameObject.transform);
        temp.GetComponent<ActivityScript>().setActivityTemplate(materialsActivities[0]);
        gm.GetComponent<GameManagerScript>().addActivity(temp.GetComponent<ActivityScript>());
    }

    public void apparitionDefense()
    {
        GameObject temp = Instantiate(activity, gameObject.transform);
        temp.GetComponent<ActivityScript>().setActivityTemplate(defenseActivities[0]);
        gm.GetComponent<GameManagerScript>().addActivity(temp.GetComponent<ActivityScript>());
    }

    public void priorFood()
    {
        gm.GetComponent<GameManagerScript>().positionFood();
    }

    public void priorMaterials()
    {
        gm.GetComponent<GameManagerScript>().positionMaterials();
    }

    public void priorDefense()
    {
        gm.GetComponent<GameManagerScript>().positionDefense();
    }
}
