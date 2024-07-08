using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    TourTemplate towerTemp;

    bool fightMode;

    void Start()
    {
        fightMode = false;
    }


    void Update()
    {
        if(!fightMode)
            handleLookingForFightMode()

    }


    void handleLookingForFightMode()
    {
        Vector3 origin = gameObject.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(origin, towerTemp.range, towerTemp.layerTarget);

        var sortedColliders = hitColliders
            .OrderBy(collider => Vector3.Distance(origin, collider.transform.position))
            .ToArray();

        if (hitColliders.Length > 0)
        {
            foreach (var collider in sortedColliders)
            {
                if (collider.gameObject == Nexus.gameObject)
                {
                    fight = true;
                    target = Nexus;
                    nav.SetDestination(target.transform.position);
                    break;
                }
                else
                {
                    fight = true;
                    target = sortedColliders[0].gameObject;
                    nav.SetDestination(target.transform.position);
                }
            }
            StartCoroutine(fightOneTime());

        }
    }
}