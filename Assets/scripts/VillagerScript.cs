using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerScript : MonoBehaviour
{
    [SerializeField]
    ActivityTemplate actualActivity, favoriteActivity;

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

    // Start is called before the first frame update
    void Start()
    {
        actualActivity = null;
        favoriteActivity = null;
    }

 
    /// <summary>
    /// Change the actual activity
    /// </summary>
    /// <param name="ac">new actual activiy</param>
    public void changeActualActivity(ActivityTemplate ac)
    {
        this.actualActivity = ac;
    }

    /// <summary>
    /// Change the favorite activity
    /// </summary>
    /// <param name="fav">new favorite activity</param>
    public void changeFavoriteActitvy(ActivityTemplate fav)
    {
        this.favoriteActivity = fav;
    }

    public ActivityTemplate getFavoriteActivity()
    {
        return favoriteActivity;
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
