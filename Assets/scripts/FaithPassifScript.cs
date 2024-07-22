using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaithPassifScript : MonoBehaviour
{
    LayerMask players;
    [SerializeField]
    FaithPassifTemplate temp;

    private void Start()
    {
        players = LayerMask.GetMask("Player");
    }

    public float getFaithGenerated()
    {
        return getNumberOfPeople() * temp.faith;
    }

    public int getNumberOfPeople()
    {
        Collider[] villagersDidntCollected;
        int numberOfPeople = 0;
        foreach(Collider c in getVillagersInTheRange())
        {
            if(!c.GetComponent<VillagerScript>().faithCollected)
            {
                c.GetComponent<VillagerScript>().faithCollected = true;
                numberOfPeople++;
            }
        }
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
