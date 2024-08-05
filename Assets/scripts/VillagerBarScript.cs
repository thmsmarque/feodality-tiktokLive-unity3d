using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillagerBarScript : MonoBehaviour
{

    GameManagerScript gm;
    public GameObject panel;
    public Button buttonPrefab;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
    }

    public void updateVillagers()
    {
        emptyPanel();
        foreach(VillagerScript v in gm.playersWaiting)
        {
            Button temp = Instantiate(buttonPrefab, panel.transform);
            temp.GetComponent<VillagerButtonScript>().setButton(v);
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
