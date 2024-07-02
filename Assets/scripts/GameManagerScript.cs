using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public List<VillagerScript> playersOnBoard;
    public List<ActivityTemplate> activities;
    public List<VillagerScript> VillagersSelected;

    public float foodQuantity;
    public float foodNeeded;
    public float materials;
    public float marginFood = 0.25f;
    public float multiplierFoodNeededByStrongness = 0.1f;

    enum FOOD_STATE
    {
        STARVING,UNDER,UPON,ABUNDANCE 
    }

    [SerializeField]
    FOOD_STATE foodState;

    public GameObject playerPrefab;

    private void Start()
    {
        playersOnBoard = new List<VillagerScript>();
        activities = new List<ActivityTemplate>();
    }

    private void Update()
    {
        StartCoroutine(react());    
        handleSelectionByClick();

    }

    void handleSelectionByClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.RayCast(ray, out hit)))
            {
                if(hit.GameObject.CompareWithTag("Villager"))
                {
                       VillagerScript vi = hit.GameObject.GetComponent<VillagerScript>();
                       if(VillagersSelected.Contains(vi))
                       {
                            VillagersSelected.Remove(vi);
                            vi.deselectVillager();
                            
                       }else
                       {
                            VillagersSelected.Add(vi);
                            vi.selectVillager();
                       }
                }
            }
        }
    }

    IEnumerator react()
    {
        foreach(ActivityTemplate a in activities)
        {
            a.react();
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(react());
    }

    public void resetFood()
    {
        foodQuantity = 0;
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

    public void addFood(float food)
    {
        this.foodQuantity += food; 
    }

    public void addMaterials(float mat)
    {
        this.materials += mat;
    }

    public void removeMaterials(int mat)
    {
        if(mat > materials)
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
        playersOnBoard.Add(tempP.GetComponentInChildren<VillagerScript>());
        this.updateFoodNeeded();
        
    }

    public List<ActivityTemplate> getFoodActivities()
    {
        List<ActivityTemplate> foodsActitivies = null;
        foreach(ActivityTemplate a in activities)
        {
            if(a.isFoodActivity())
            {
                foodsActitivies.Add(a);
            }
        }
        return foodsActitivies;
    }

    public List<ActivityTemplate> getMaterialsActivities()
    {
        List<ActivityTemplate> materialsActivities = new List<ActivityTemplate>();
        foreach (ActivityTemplate a in activities)
        {
            if (a.isMaterialsActivity())
            {
                materialsActivities.Add(a);
            }
        }
        return materialsActivities;
    }

    public List<ActivityTemplate> getDefenseActivities()
    {
        List<ActivityTemplate> defenseActivities = new List<ActivityTemplate>();
        foreach (ActivityTemplate a in activities)
        {
            if (a.isDefenseActivity())
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
        int huitieme = (int)(nbVillagers*0.8f);
        int dix = (int)(nbVillagers*0.1f);

        List<ActivityTemplate> foodAc = getFoodActivities();
        List<ActivityTemplate> materialsAc = getMaterialsActivities();
        List<ActivityTemplate> defAc = getDefenseActivities();
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
        int huitieme = (int)(nbVillagers*0.8f);
        int dix = (int)(nbVillagers*0.1f);

        List<ActivityTemplate> foodAc = getFoodActivities();
        List<ActivityTemplate> materialsAc = getMaterialsActivities();
        List<ActivityTemplate> defAc = getDefenseActivities();
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
        int huitieme = (int)(nbVillagers*0.8f);
        int dix = (int)(nbVillagers*0.1f);

        List<ActivityTemplate> foodAc = getFoodActivities();
        List<ActivityTemplate> materialsAc = getMaterialsActivities();
        List<ActivityTemplate> defAc = getDefenseActivities();
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
}
