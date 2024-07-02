using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
    [SerializeField]
    ActivityScript actualActivity;

    /// <summary>
    /// How strong he is
    /// </summary>
    float strongness = 2f;
    /// <summary>
    /// How efficience he is
    /// </summary>
    float efficacity = 2.5f;


    public string nameOfVillager;
    public long idPlayer;
    public int numberOfLikes;

    public bool isSelected;

    // Start is called before the first frame update
    void Start()
    {
        actualActivity = null;
        isSelected = false;
    }

    public void selectVillager()
    {
        isSelected = true;
        //Activer effet selection
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
    }

    public void deselectVillager()
    {
        isSelected = false;
        //Desactivier effet selection
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;

    }


    /// <summary>
    /// Change the actual activity
    /// </summary>
    /// <param name="ac">new actual activiy</param>
    public void changeActualActivity(ActivityScript ac)
    {
        if(actualActivity != null)
        {
            actualActivity.removeVillager(this);
        }
        this.actualActivity = ac;
        ac.addVillager(this);
    }

    public void doingNothing()
    {
        actualActivity = null;
    }

    public void trainOneTime()
    {
        efficacity += 0.003f;
        strongness += 0.001f;
    }


    public ActivityScript getActualActivity()
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

    public void setName(string name)
    {
        this.nameOfVillager = name;
    }
}
