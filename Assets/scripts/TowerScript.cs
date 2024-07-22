using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [SerializeField]
    TourTemplate towerTemp;

    [SerializeField]
    GameManagerScript gm;
    bool fightMode;

    public float health;

    GameObject target;

    void Start()
    {
        fightMode = false;
        health = towerTemp.health;
        gm = GameObject.FindWithTag("GameController").GetComponent<GameManagerScript>();


    }


    void Update()
    {
        if(!fightMode)
        {
            handleLookingForFightMode();
        }

    }

    public float getFaithNeeded()
    {
        return towerTemp.faithNeeded;
    }


    void handleLookingForFightMode()
    {

        Vector3 origin = gameObject.transform.position;

        Collider[] hitColliders = Physics.OverlapSphere(origin, towerTemp.range, towerTemp.layerTarget);



       if (hitColliders.Length > 0)
       {
                //Debug.Log("Lancement de l'attaque");
                target = hitColliders[0].gameObject;
                fightMode = true;
                StartCoroutine(fightOneTime());
       }
 
        
        
    }

    IEnumerator fightOneTime()
    {
        //Debug.Log("Tour se prépare à attaquer");
        if (isTargetInRange())
        {
            //Debug.Log("Ennemie dans la portée");

            Vector3 origin = target.transform.position;

            Collider[] hitColliders = Physics.OverlapSphere(origin, towerTemp.degatZone, towerTemp.layerTarget);

            foreach (Collider c in hitColliders)
            {
                if (c.gameObject != target)
                {
                    c.gameObject.GetComponentInParent<EnnemyScript>().takingDamage(towerTemp.power * gm.getFaithState() * 0.5f);
                }
            }

            if (target.GetComponentInParent<EnnemyScript>().takingDamage(towerTemp.power * gm.getFaithState()))
            {
                target.GetComponentInParent<EnnemyScript>().die();
                target = null;
                fightMode = false;
            }
        }
        else
        {
            target = null;
            fightMode = false;
        }

        if (fightMode)
        {
            yield return new WaitForSeconds(towerTemp.speedAttack);
            StartCoroutine(fightOneTime());
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, towerTemp.range);
    }

    public bool takingDamage(float dmg)
    {
        //Debug.Log("tour prend des dégats : " + dmg + "   Nouvelle vie : " + (health - dmg));
        health -= dmg;
        if (health > 0)
        {
            return false;
        }
        else
        {

            return true;
        }
    }

    public void die()
    {
        gm.removeTower(this);
        Destroy(gameObject);
    }

    bool isTargetInRange()
    {
        if (target == null) return false; // V�rifiez que la cible n'est pas null
        float distanceToTarget = Vector3.Distance(transform.position, target.gameObject.transform.position);
        return distanceToTarget <= towerTemp.range;
    }

    public void setTouretTemplate(TourTemplate t)
    {
        Debug.Log("Set touret Template");
        towerTemp = t;
        if (towerTemp.prefab != null)
        {
            Instantiate(towerTemp.prefab, gameObject.GetComponent<Transform>().position, Quaternion.identity, gameObject.transform);
        }
       
    }
}