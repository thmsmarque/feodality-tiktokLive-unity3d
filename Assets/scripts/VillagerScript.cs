using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
    [SerializeField]
    ActivityTemplate actualActivity;

    /// <summary>
    /// How strong he is
    /// </summary>
    float strongness = 2f;
    /// <summary>
    /// How efficience he is
    /// </summary>
    float efficacity = 2.5f;


    public string name;
    public long idPlayer;
    public int numberOfLikes;

    public bool isSelected;

    // Start is called before the first frame update
    void Start()
    {
        actualActivity = null;
        isSelected = true;
    }

    public void selectVillager()
    {
        isSelected = true;
        //Activer effet selection
    }

    public void deselectVillager()
    {
        isSelected = false;
        //Desactivier effet selection
    }

 
    /// <summary>
    /// Change the actual activity
    /// </summary>
    /// <param name="ac">new actual activiy</param>
    public void changeActualActivity(ActivityTemplate ac)
    {
        if(actualActivity != null)
        {
            actualActivity.removeVillager(this);
        }
        this.actualActivity = ac;
        ac.addVillager(this);
    }

    


    public ActivityTemplate getActualActivity()
    {
        return actualActivity;
    }


    public void changeStrongness(int st)
    {
        this.strongness = st;
    }

    public void changeEfficacity(int eff)
    {
        this.efficacity = eff;
    }

    public float getEfficacity()
    {
        return this.efficacity;
    }

    public float getStrongness()
    {
        return this.strongness;
    }
}
