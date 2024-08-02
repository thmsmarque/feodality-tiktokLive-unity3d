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
