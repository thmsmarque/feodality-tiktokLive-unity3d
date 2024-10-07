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
    public ActivityTemplate[] houses;
    public TourTemplate[] tours;

    public GameObject panel;

    public Button foodButtonTemplate;
    public Button materialsButtonTemplate;
    public Button DefenseButtonTemplate;
    public Button faithButtonTemplate;
    public Button houseButtonTemplate;

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

    public void showDefenseBuilding()
    {
        emptyPanel();
        foreach(ActivityTemplate a in defenseActivities)
        {
            Button temp = Instantiate(DefenseButtonTemplate, panel.transform);
            temp.GetComponent<BuildButtonScript>().setButton(a.shopImage, a.name, a.costInMaterials, 0, 0, a);
        }
        foreach(TourTemplate a in tours)
        {
            Button temp = Instantiate(DefenseButtonTemplate, panel.transform);
            temp.GetComponent<BuildButtonScript>().setButton(a.shopImage, a.name, a.coastInMaterials, a.power, a.faithNeeded, a);
        }
    }

    public void showFaithBuilding()
    {
        emptyPanel();
        foreach (ActivityTemplate a in cultActivies)
        {
            FaithPassifTemplate f = a.prefab.GetComponent<FaithPassifScript>().temp;
            Button temp = Instantiate(faithButtonTemplate, panel.transform);
            temp.GetComponent<BuildButtonScript>().setButton(a.shopImage, a.name, a.costInMaterials,f.rangeOfAction , f.faith, a);
        }
     
    }

    public void showHouseBuilding()
    {
        emptyPanel();
        foreach (ActivityTemplate a in houses)
        {
            Button temp = Instantiate(houseButtonTemplate, panel.transform);
            temp.GetComponent<BuildButtonScript>().setButton(a.shopImage, a.name, a.costInMaterials, a.capacity, 0, a);
        }
    }


    void emptyPanel()
    {
        // Créer une liste temporaire pour stocker les boutons à détruire
        List<GameObject> buttonsToDestroy = new List<GameObject>();

        // Parcourir tous les enfants du parent
        foreach (Transform child in panel.transform)
        {
            // Vérifier si l'enfant a un composant Button
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Ajouter l'enfant à la liste des boutons à détruire
                buttonsToDestroy.Add(child.gameObject);
            }
        }

        // Détruire tous les boutons trouvés
        foreach (GameObject button in buttonsToDestroy)
        {
            Destroy(button);
        }
    }
}
