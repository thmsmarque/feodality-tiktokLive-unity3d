using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    TourTemplate towerTemp;

    GameManagerScript gm;
    bool fightMode;

    GameObjet target;

    void Start()
    {
        fightMode = false;
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();
 
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
            target = sortedColliders[0];
            fightMode = true;
            StartCoroutine(fightOneTime());
        }
    }

    IEnumerator fightOneTime()
    {     
        if(isTargetInRange())
        {
            Vector3 origin = target.transform.position;

            Collider[] hitColliders = Physics.OverlapSphere(origin, towerTemp.degatZone, towerTemp.layerTarget);

            foreach(Collider c in hitColliders)
            {
                if(c.gameObject != target)
                {
                    c.gameObject.GetComponentInParent<EnnemyScript>().takingDamage(towerTemp.power * gm.getFaithState * 0.5f);
                }
            }

            if(target.GetComponentInParent<EnnemyScript>().takingDamage(towerTemp.power * gm.getFaithState))
            {
                target = null;
                fightMode = false;
            }
        }else
        {
            target = null;
            fightMode = false;
        }
        
    }

    bool isTargetInRange()
    {
        if (target == null) return false; // V�rifiez que la cible n'est pas null
        float distanceToTarget = Vector3.Distance(transform.position, target.gameObject.transform.position);
        return distanceToTarget <= towerTemp.range;
    }
}