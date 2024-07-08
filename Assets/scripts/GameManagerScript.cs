using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public List<VillagerScript> playersOnBoard;
    public List<ActivityScript> activities;
    public List<VillagerScript> VillagersSelected;
    public List<TowerScript> towers;

    public float foodQuantity;
    public float foodNeeded;
    public float faithQuantity;
    public float faithNeeded;
    public float materials;
    public float marginFood = 0.25f;
    public float marginFaith = 0.25f;
    public float multiplierFoodNeededByStrongness = 0.1f;
    public float priorityProportion = 0.6f;

    enum FOOD_STATE
    {
        STARVING = 0.1f ,UNDER = 0.8f ,UPON =  1.0f,ABUNDANCE = 1.45f
    }

    enum FAITH_STATE
    {
        LACK = 0.3f ,BALANCE = 1f ,FAITHFUL = 1.5f
    }

    [SerializeField]
    FOOD_STATE foodState;
    [SerializeField
    FAITH_STATE faithState;]

    public GameObject playerPrefab;

    private void Start()
    {
        playersOnBoard = new List<VillagerScript>();
        activities = new List<ActivityScript>();
        StartCoroutine(react());
    }

    private void Update()
    {
        
        handleSelectionByClick();

    }

    void handleSelectionByClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Selection villageois ou activité
            //Debug.Log("Envoi du ray");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Le raycast a touch� un collider");
                if(hit.collider.gameObject.CompareTag("Villager"))
                {
                    //Debug.Log("Le ray a touch� un Villager");
                       VillagerScript vi = hit.collider.gameObject.GetComponentInParent<VillagerScript>();
                       if(VillagersSelected.Contains(vi))
                       {
                            VillagersSelected.Remove(vi);
                            vi.deselectVillager();
                            
                       }else
                       {
                            VillagersSelected.Add(vi);
                            vi.selectVillager();
                       }
                }else if(hit.collider.gameObject.CompareTag("Activity"))
                {
                    if(VillagersSelected.Count > 0)
                    {
                        foreach(VillagerScript vi in VillagersSelected)
                        {
                            vi.changeActualActivity(hit.collider.gameObject.GetComponent<ActivityScript>());
                            
                        }
                        for (int i = VillagersSelected.Count - 1; i >= 0; i--)
                        {
                            VillagerScript vi = VillagersSelected[i];
                            VillagersSelected.RemoveAt(i);
                            vi.deselectVillager();
                        }
                    }
                }else if(hit.collider.gameObject.CompareTag("Floor"))
                {
                    if(VillagersSelected.Count > 0)
                    {
                        foreach(VillagerScript v in VillagersSelected)
                        {
                            v.setNewDestination(hit.point);
                        }
                    }
                }
                /*else
                {
                    Debug.Log("Le ray n'a pas touch� un joueur : " + hit.collider.tag);
                }*/
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            //Affiche les informations sur le villageois ou l'activité
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Le raycast a touch� un collider");
                if(hit.collider.gameObject.CompareTag("Villager"))
                {
                    //Ouverture de la fenêtre
                }
            }
        }
    }

    IEnumerator react()
    {
        foreach(ActivityScript a in activities)
        {
            a.react();
        }
        updateFoodState();
        UpdateFaithState();
        yield return new WaitForSeconds(0.1f);
        resetFood();
        resetFaith();
        StartCoroutine(react());
    }

    public void resetFood()
    {
        foodQuantity = 0;
    }

    public void resetFaith()
    {
        faithQuantity = 0;
        foreach(VillagerScript v in villagersList)
        [
            v.faithCollected = false;
        ]
    }

    public void updateFoodState()
    {
        
        if (foodQuantity >= foodNeeded && foodQuantity < foodNeeded * (1f + marginFood))
        {
            //Enough
            foodState = FOOD_STATE.UPON;
        }
        else if (foodQuantity < foodNeeded && foodQuantity >= foodNeeded * (1f - marginFood))
        {
            //Not enough
            foodState = FOOD_STATE.UNDER;
        }else if (foodQuantity <= foodNeeded * (1f - marginFood))
        {
            //Starving
            foodState = FOOD_STATE.STARVING;
        }else if(foodQuantity >= foodNeeded * (1f + marginFood))
        {
            //A lot of food
            foodState = FOOD_STATE.ABUNDANCE;
        }

    }

    public void UpdateFaithState()
    {
        if (faithQuantity >= faithneed *(1f-marginFaith) && faithQuantity <= faithneed * (1f + marginFaith))
        {
            //Enough
            faithState = FAITH_STATE.BALANCE;
        }
        else if (faithQuantity < faithneed * (1f-marginFaith))
        {
            //Not enough
            faithState = FAITH_STATE.LACK;

        }else if (faithQuantity > faithneed * (1f + marginFaith))
        {
            //Starving
            foodState = FAITH_STATE.FAITHFUL;
        }
    }

  

    public void updateFoodNeeded()
    {
        float foodneed = 0;
        foreach(VillagerScript g in playersOnBoard)
        {
            foodneed += 1f + g.getStrongness() * multiplierFoodNeededByStrongness;
        }
        this.foodNeeded = foodneed;
        this.updateFoodState();
    }

    public float getHungerState()
    {
        return foodState;
    }

    public float getFaithState()
    {
        return faithState;
    }

    public void updateFaithNeeded()
    {
        float faithneed =0 ;
        foreach(TowerScript as in activities)
        {
            faithneed += as.towerTemp.faithneed;
        }
        this.faithneed = faithneed;
        this.UpdateFaithState();
    }

    public void addFood(float food)
    {
        this.foodQuantity += food; 
    }

    public void addMaterials(float mat)
    {
        this.materials += mat;
    }

    public void addFaith(float faith)
    {
        this.faithQuantity += faith;
    }


    public void removeMaterials(float mat)
    {
        if(mat < materials)
        {
            materials -= mat;
        }else
        {
            materials = 0;
        }
    }

    public void addPlayerOnBoard(string name, long id)
    {
        GameObject tempP = Instantiate(playerPrefab,GetComponent<Transform>());
        tempP.GetComponent<VillagerScript>().setName(name);
        playersOnBoard.Add(tempP.GetComponentInChildren<VillagerScript>());
        this.updateFoodNeeded();
        
    }

    public List<ActivityScript> getFoodActivities()
    {
        List<ActivityScript> foodsActitivies = new List<ActivityScript>();
        foreach(ActivityScript a in activities)
        {
            if(a.getActivityTemplate().isFoodActivity())
            {
                foodsActitivies.Add(a);
            }
        }
        return foodsActitivies;
    }

    public List<ActivityScript> getMaterialsActivities()
    {
        List<ActivityScript> materialsActivities = new List<ActivityScript>();
        foreach (ActivityScript a in activities)
        {
            if (a.getActivityTemplate().isMaterialsActivity())
            {
                materialsActivities.Add(a);
            }
        }
        return materialsActivities;
    }

    public List<ActivityScript> getDefenseActivities()
    {
        List<ActivityScript> defenseActivities = new List<ActivityScript>();
        foreach (ActivityScript a in activities)
        {
            if (a.getActivityTemplate().isDefenseActivity())
            {
                defenseActivities.Add(a);
            }
        }
        return defenseActivities;
    }






    public void positionFood()
    {
        playersOnBoard.Sort(new IComparerEfficacity());
        float nbVillagers = (float)playersOnBoard.Count;
        int huitieme = (int)(nbVillagers*priorityProportion);
        int dix = (int)(nbVillagers*(1 - priorityProportion)/2);

        List<ActivityScript> foodAc = getFoodActivities();
        List<ActivityScript> materialsAc = getMaterialsActivities();
        List<ActivityScript> defAc = getDefenseActivities();
        System.Random rand = new System.Random();
        for (int i = 0; i<huitieme; i++)
        {
            int aleaInt = rand.Next(foodAc.Count);
            playersOnBoard[i].changeActualActivity(foodAc[aleaInt]);

        }
        for(int i = huitieme; i< huitieme+dix; i++)
        {
            int aleaInt = rand.Next(materialsAc.Count);
            playersOnBoard[i].changeActualActivity(materialsAc[aleaInt]);
        }
        for(int i = huitieme + dix; i< huitieme + 2*dix; i++)
        {
            int aleaInt = rand.Next(defAc.Count);
            playersOnBoard[i].changeActualActivity(defAc[aleaInt]);
        }



    }

    public void positionMaterials()
    {
        playersOnBoard.Sort(new IComparerEfficacity());
        float nbVillagers = (float)playersOnBoard.Count;
        int huitieme = (int)(nbVillagers*priorityProportion);
        int dix = (int)(nbVillagers*(1 - priorityProportion)/2);

        List<ActivityScript> foodAc = getFoodActivities();
        List<ActivityScript> materialsAc = getMaterialsActivities();
        List<ActivityScript> defAc = getDefenseActivities();
        System.Random rand = new System.Random();

        for (int i = 0; i < huitieme; i++)
        {
            int aleaInt = rand.Next(materialsAc.Count);
            playersOnBoard[i].changeActualActivity(materialsAc[aleaInt]);

        }
        for (int i = huitieme; i < huitieme + dix; i++)
        {
            int aleaInt = rand.Next(foodAc.Count);
            playersOnBoard[i].changeActualActivity(foodAc[aleaInt]);
        }
        for (int i = huitieme + dix; i < huitieme + 2 * dix; i++)
        {
            int aleaInt = rand.Next(defAc.Count);
            playersOnBoard[i].changeActualActivity(defAc[aleaInt]);
        }
    }

    public void positionDefense()
    {
        playersOnBoard.Sort(new IComparerStrongness());
        float nbVillagers = (float)playersOnBoard.Count;
        int huitieme = (int)(nbVillagers*priorityProportion);
        int dix = (int)(nbVillagers*(1 - priorityProportion)/2);

        List<ActivityScript> foodAc = getFoodActivities();
        List<ActivityScript> materialsAc = getMaterialsActivities();
        List<ActivityScript> defAc = getDefenseActivities();
        System.Random rand = new System.Random();

        for (int i = 0; i<huitieme; i++)
        {
            int aleaInt = rand.Next(defAc.Count);
            playersOnBoard[i].changeActualActivity(defAc[aleaInt]);
        }
        for(int i = huitieme; i< huitieme+dix; i++)
        {
            int aleaInt = rand.Next(foodAc.Count);
            playersOnBoard[i].changeActualActivity(foodAc[aleaInt]);
        }
        for(int i = huitieme + dix; i< huitieme + 2*dix; i++)
        {
            int aleaInt = rand.Next(materialsAc.Count);
            playersOnBoard[i].changeActualActivity(materialsAc[aleaInt]);
        }
    }

    public void addActivity(ActivityScript v)
    {
        activities.Add(v);
    }
    
    
    public void removeActitvity(ActivityScript v)
    {
        if(activities.Contains(v))
        {
            activities.Remove(v);
        }
    }

    public void addTower(TowerScript t)
    {
        towers.Add(t);
    }

    public void removeTower(TowerScript t)
    {
        if(towers.Contains(t))
        {
            towers.Remove(t);
        }
    }


}
