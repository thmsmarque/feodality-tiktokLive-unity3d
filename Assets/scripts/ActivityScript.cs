using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityScript : MonoBehaviour
{
    public ActivityTemplate acTemp;
    public List<VillagerScript> villagersList = new List<VillagerScript>();

    public float health;

    public GameManagerScript gm;

    private void Start()
    {
        health = acTemp.health;
       gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
       
    }

    public void react()
    {
        if (acTemp.isFoodActivity())
        {
            float food = 0;
            foreach (VillagerScript v in villagersList)
            {
                food += v.getEfficacity();
            }
            gm.addFood(food);
        } else if (acTemp.isMaterialsActivity())
        { 
            
            float materials = 0f;
            foreach (VillagerScript v in villagersList)
            {
                materials += v.getEfficacity() * 0.1f;
            }
            gm.addMaterials(materials);
            
        }else if(acTemp.isTrainingActivity())
        {
            foreach(VillagerScript v in villagersList)
            {
                v.trainOneTime();
            }
        }else if(acTemp.isCultActivity())
        {
            faith =0;
            foreach(VillagerScript v in villagersList)
            {
                faith += v.getEfficacity();
            }
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

    /// <summary>
    /// Remove health from activity
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns>If health<=0 return true else return false</returns>
    public bool removeHealth(float dmg)
    {
        Debug.Log("Vie retir�e : " + dmg + "   Vie : " + health) ;
        this.health -= dmg;
        if(health > 0)
        {
            return false;
        }else
        {
            return false;
        }

    }

    public List<VillagerScript> getVillagerList()
    {
        return villagersList;
    }

    public ActivityTemplate getActivityTemplate()
    {
        return acTemp;
    }

    public void setActivityTemplate(ActivityTemplate ac)
    {
        this.acTemp = ac;
        if (acTemp.prefab != null)
        {
            Instantiate(acTemp.prefab, gameObject.GetComponent<Transform>().position, Quaternion.identity, gameObject.transform);
        }
    }


}