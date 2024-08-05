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
    public ActivityTemplate[] cultActivies;
    public TourTemplate[] tours; 
    private void Start()
    {
       gm = GameObject.FindWithTag("GameController");
       numbVillager = 0;
    }
    public void apparitionVillager()
    {
        gm.GetComponent<GameManagerScript>().addPlayerWaiting("test"+numbVillager, 0000);
        numbVillager++;
    }

    public void apparitionFood()
    {
        GameObject temp = Instantiate(activity, gameObject.transform);
        temp.GetComponent<ActivityScript>().setActivityTemplate(foodActivities[0], Quaternion.identity);
        gm.GetComponent<GameManagerScript>().addActivity(temp.GetComponent<ActivityScript>());
    }

    public void apparitionMaterials()
    {
        GameObject temp = Instantiate(activity, gameObject.transform);
        temp.GetComponent<ActivityScript>().setActivityTemplate(materialsActivities[0], Quaternion.identity);
        gm.GetComponent<GameManagerScript>().addActivity(temp.GetComponent<ActivityScript>());
    }

    public void apparitionDefense()
    {
        GameObject temp = Instantiate(activity, gameObject.transform);
        temp.GetComponent<ActivityScript>().setActivityTemplate(defenseActivities[0], Quaternion.identity);
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

    public void construcChamp()
    {
        gm.GetComponent<BuildScript>().startCoroutinebuildMode(foodActivities[0]);
    }

    public void construcArbre()
    {
        gm.GetComponent<BuildScript>().startCoroutinebuildMode(materialsActivities[0]);
    }

    public void construcTour()
    {
        gm.GetComponent<BuildScript>().startCoroutinebuildMode(defenseActivities[0]);
    }

    public void construcTouret()
    {
        gm.GetComponent<BuildScript>().startCoroutinebuildMode(tours[0]);
    }

    public void construcTotem()
    {
        gm.GetComponent<BuildScript>().startCoroutinebuildMode(cultActivies[0]);
    }
}
