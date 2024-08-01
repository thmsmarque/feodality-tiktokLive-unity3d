using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HUD_Script : MonoBehaviour
{
    GameManagerScript gm;
    public TMP_Text mat_Text;

    public Image food_Bar;
    public Image faith_Circle;
    void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        mat_Text.text = Mathf.Floor(gm.materials) + "";

        food_Bar.fillAmount = Mathf.Clamp(gm.foodQuantity / gm.foodNeeded,0,1);
        ColorChanger();
    }


    void ColorChanger()
    {
        Color foodColor = Color.Lerp(Color.red,Color.green, food_Bar.fillAmount);
        food_Bar.color = foodColor;

        Color faithColor = Color.Lerp(Color.white, Color.yellow, (gm.faithQuantity / gm.faithNeeded));
        faith_Circle.color = faithColor;
        

    }
}
