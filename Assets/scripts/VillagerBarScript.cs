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
