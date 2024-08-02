using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildButtonScript : MonoBehaviour
{

    public Image mainImage;
    public TMP_Text name;
    public TMP_Text cost;
    public TMP_Text capacity;
    public TMP_Text production;

    public ActivityTemplate temp;

    public void setButton(Image mainImage,string name, float cost, float capacity, float production, ActivityTemplate temp )
    {
        this.mainImage = mainImage;
        this.name.text = name + "";
        this.cost.text = cost + "";
        this.capacity.text = capacity + "";
        this.production.text = production + "";
        this.temp = temp;
    }

    public void startConstruction()
    {
        GameObject.FindWithTag("GameController").GetComponent<BuildScript>().startCoroutinebuildMode(temp);

    }
}
