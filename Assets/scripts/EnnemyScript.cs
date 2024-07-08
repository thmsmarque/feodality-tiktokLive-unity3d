using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnnemyScript : MonoBehaviour
{
    [SerializeField]
    EnnemyTemplate typeOfEnnemy;

    GameObject Nexus;
    NavMeshAgent nav;

    GameObject target;
    bool fight;

    void Start()
    {
        Nexus = GameObject.FindWithTag("Nexus");
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(Nexus.transform.position);
        nav.speed = typeOfEnnemy.speed;


    }

    // Update is called once per frame
    void Update()
    {
        if(!fight)
        {
            handleSearchingForFight();
        }
    }

    void handleSearchingForFight()
    {
        Vector3 origin = gameObject.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(origin, typeOfEnnemy.rangeOfSearch, typeOfEnnemy.layerTarget);

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, typeOfEnnemy.rangeOfSearch);
    }

    IEnumerator fightOneTime()
    {
        Debug.Log("Part attaquer");
        yield return new WaitUntil(() => isTargetInRange());
        Debug.Log("Dans la portée d'attaque");
        nav.SetDestination(transform.position);
        
        if(target.GetComponentInParent<ActivityScript>().removeHealth(typeOfEnnemy.power))
        {
            hasDestroyActivity();
        }else
        {
            yield return new WaitForSeconds(typeOfEnnemy.speedAttack);
            StartCoroutine(fightOneTime());
        }

    }

    void hasDestroyActivity()
    {
        target = null;
        fight = false;
        nav.SetDestination(Nexus.transform.position);
    }

    bool isTargetInRange()
    {
        if (target == null) return false; // Vérifiez que la cible n'est pas null
        float distanceToTarget = Vector3.Distance(transform.position, target.gameObject.transform.position);
        return distanceToTarget <= typeOfEnnemy.rangeOfAttack;
    }

}
