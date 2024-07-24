using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithPassifScript : MonoBehaviour
{
    LayerMask players;
    [SerializeField]
    public FaithPassifTemplate temp;

    private void Start()
    {
        players = LayerMask.GetMask("Player");
    }

    public float getFaithGenerated()
    {
        //Debug.Log("Activation passif faith");
        return getNumberOfPeople() * temp.faith;
    }

    public int getNumberOfPeople()
    {
        int numberOfPeople = 0;
        Collider[] peoples = getVillagersInTheRange();
        //Debug.Log("Nombre de villageois dans la zone :" + peoples.Length);
        foreach(Collider c in peoples)
        {
            if(!c.GetComponentInParent<VillagerScript>().faithCollected)
            {
                c.GetComponentInParent<VillagerScript>().faithCollected = true;
                numberOfPeople++;
            }
        }
        //Debug.Log("Villagers not collected : " + numberOfPeople);
        return numberOfPeople;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, temp.rangeOfAction);
    }

    public Collider[] getVillagersInTheRange()
    {
        Vector3 origin = transform.position;

        return Physics.OverlapSphere(origin, temp.rangeOfAction, players);
    }
}
