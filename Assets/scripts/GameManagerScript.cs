using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public List<VillagerScript> playersOnBoard;
    public List<ActivityTemplate> activities;
    public float foodQuantity;
    public float foodNeeded;
    public float materials;
    public float marginFood = 0.25f;

    enum FOOD_STATE
    {
        STARVING,UNDER,UPON,ABUNDANCE 
    }

    [SerializeField]
    FOOD_STATE foodState;

    public GameObject playerPrefab;

    private void Update()
    {
        StartCoroutine(react());    
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
            foodneed += 1f + g.getStrongness() * 0.3f;
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
}
