using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityScript : MonoBehaviour
{
    public ActivityTemplate acTemp;
    public List<VillagerScript> villagersList = new List<VillagerScript>();
    public List<EnnemyScript> ennemiesList = new List<EnnemyScript>();
    public Transform[] placeToWork;
    public VillagerScript[] hasAVillager;

    public float health;

    public GameManagerScript gm;

    private void Start()
    {
        health = acTemp.health;
       gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
        GetComponent<BoxCollider>().size = acTemp.sizeOfCollider;
       
    }

    public void react()
    {
        if (acTemp.isFoodActivity())
        {
            float food = 0;
            foreach (VillagerScript v in villagersList)
            {
                food += v.getEfficacity() * acTemp.multiplierEfficacity;
            }
            gm.addFood(food);
        } else if (acTemp.isMaterialsActivity())
        { 
            
            float materials = 0f;
            foreach (VillagerScript v in villagersList)
            {
                materials += v.getEfficacity() * 0.1f * acTemp.multiplierEfficacity;
            }
            gm.addMaterials(materials  * gm.getHungerState());
            
        }else if(acTemp.isTrainingActivity())
        {
            foreach(VillagerScript v in villagersList)
            {
                v.trainOneTime();
            }
        }else if(acTemp.isCultActivity())
        {
            float faith =0;
            FaithPassifScript ps = GetComponentInChildren<FaithPassifScript>();
            faith += ps.getFaithGenerated();
            gm.addFaith(faith * gm.getHungerState());
        }
        
    }


    public void addVillager(VillagerScript b)
    {
        if (villagersList.Count < acTemp.capacity)
        {
            this.villagersList.Add(b);
            for(int i = 0; i<placeToWork.Length; i++)
            {
                Debug.Log("tour du placeToWork");
                if (hasAVillager[i] == null)
                {
                    Debug.Log("HasAVillager passed");
                    b.changeDestination(placeToWork[i].position);
                    hasAVillager[i] = b;
                    break;
                }
            }
        }
    }

    public void removeVillager(VillagerScript b)
    {
        if (this.villagersList.Contains(b))
        {
            for(int i = 0; i<placeToWork.Length; i++)
            {
                if(hasAVillager[i] == b)
                {
                    hasAVillager[i] = null;
                }
            }
            this.villagersList.Remove(b);

        }
    }

    public void addEnnemy(EnnemyScript b)
    {
        this.ennemiesList.Add(b);
    }

    public void removeEnnemy(EnnemyScript b)
    {
        if (this.ennemiesList.Contains(b))
            this.ennemiesList.Remove(b);
    }

    /// <summary>
    /// Remove health from activity
    /// </summary>
    /// <param name="dmg"></param>
    /// <returns>If health<=0 return true else return false</returns>
    public bool removeHealth(float dmg)
    {
        //Debug.Log("Vie retir�e : " + dmg + "   Vie : " + health) ;
        this.health -= dmg;
        if(health > 0)
        {
            return false;
        }else
        {
            destroy();
            return true;

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
            GameObject temp = Instantiate(acTemp.prefab, gameObject.GetComponent<Transform>().position, Quaternion.identity, gameObject.transform);
            Transform[] trans = GetComponentsInChildren<Transform>();
            

            int nbPlace = 0;
            foreach(Transform t in trans)
            {
                if(t.CompareTag("Place"))
                {
                    nbPlace++;
                }
            }

            placeToWork = new Transform[nbPlace];
            hasAVillager = new VillagerScript[nbPlace];

            nbPlace = 0;

            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].gameObject.tag == "Place")
                {
                    placeToWork[nbPlace] = trans[i];
                    hasAVillager[nbPlace] = null;
                    nbPlace++;

                }
            }
        }
    }

    public void destroy()
    {
        villagersList.Clear();
        foreach (EnnemyScript v in ennemiesList)
        {
            v.hasDestroyActivity();
        }
        ennemiesList.Clear();
        gm.removeActitvity(this);
        Destroy(gameObject);
    }



}