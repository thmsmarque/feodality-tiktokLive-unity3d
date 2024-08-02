using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopBarScript : MonoBehaviour
{
    public ActivityTemplate[] foodActivities;
    public ActivityTemplate[] materialsActivities;
    public ActivityTemplate[] defenseActivities;
    public ActivityTemplate[] cultActivies;
    public TourTemplate[] tours;

    public GameObject panel;

    public Button foodButtonTemplate;
    public Button materialsButtonTemplate;

    public void showFoodBuildings()
    {
        emptyPanel();
        foreach(ActivityTemplate a in foodActivities)
        {
            Button temp = Instantiate(foodButtonTemplate, panel.transform);
            temp.GetComponent<BuildButtonScript>().setButton(a.shopImage,a.name,a.costInMaterials,a.capacity,a.multiplierEfficacity,a);
        }
    }

    public void showMaterialsBuildings()
    {
        emptyPanel();
        foreach (ActivityTemplate a in materialsActivities)
        {
            Button temp = Instantiate(materialsButtonTemplate, panel.transform);
            temp.GetComponent<BuildButtonScript>().setButton(a.shopImage, a.name, a.costInMaterials, a.capacity, a.multiplierEfficacity,a);
        }
    }

    void emptyPanel()
    {
        // Cr�er une liste temporaire pour stocker les boutons � d�truire
        List<GameObject> buttonsToDestroy = new List<GameObject>();

        // Parcourir tous les enfants du parent
        foreach (Transform child in panel.transform)
        {
            // V�rifier si l'enfant a un composant Button
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Ajouter l'enfant � la liste des boutons � d�truire
                buttonsToDestroy.Add(child.gameObject);
            }
        }

        // D�truire tous les boutons trouv�s
        foreach (GameObject button in buttonsToDestroy)
        {
            Destroy(button);
        }
    }
}
