using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VillagerButtonScript : MonoBehaviour
{




    public Image mainImage;
    public TMP_Text name;
    public TMP_Text efficacity;
    public TMP_Text strongness;

    public VillagerScript vs;
    public void setButton(VillagerScript vs)
    {
        this.name.text = vs.nameOfVillager;
        this.efficacity.text = vs.getEfficacity() + "";
        this.strongness.text = vs.getStrongness() + "";
        this.vs = vs;
    }

    public void handlePlacing()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>().addPlayerOnBoard(vs);
    }
}
