using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    [SerializeField]
    GameObject activityPrefab;

    GameObject tempBuild;

    ActivityTemplate actualTemplate;

    public bool buildMode;

    public float radius = 1f;

    void Start()
    {
        buildMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        handleBuildMode();
    }


    void handleBuildMode()
    {
        if (buildMode)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                buildMode = false;
            }

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Floor")
                {
                    tempBuild.transform.position = hit.point;
                }
            }

            if (checkBuild())
            {
                tempBuild.GetComponent<Renderer>().material.color = Color.green;
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Lancement Construction...");
                    GameObject tempNewActivity = Instantiate(activityPrefab, hit.point, Quaternion.identity, transform);
                    tempNewActivity.GetComponent<ActivityScript>().setActivityTemplate(actualTemplate);
                    GetComponent<GameManagerScript>().addActivity(tempNewActivity.GetComponent<ActivityScript>());
                    
                }
            }
            else
            {
                tempBuild.GetComponent<Renderer>().material.color = Color.red;

            }
        }
    }

    bool checkBuild()
    {
        // Origine de la sphère, souvent la position du joueur ou de l'objet
        Vector3 origin = tempBuild.transform.position;

        // Utiliser Physics.OverlapSphere pour obtenir tous les colliders dans la sphère
        Collider[] hitColliders = Physics.OverlapSphere(origin, radius);
        // Si la sphère touche un ou plusieurs colliders, retourner false
        if (hitColliders.Length > 0)
        {
            //Debug.Log("Les collisions : " + hitColliders+ "  Le nombre de colliders : " + hitColliders.Length);
            foreach(Collider c in hitColliders)
            {
                if (c.tag != "Floor" && !c.Equals(tempBuild.GetComponent<Collider>()))
                {
                    return false;
                }
            }
        }else
        {
            return true;
        }

        // Si la sphère ne touche aucun collider, retourner true
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(tempBuild.transform.position, radius);
    }


    public void startCoroutineBuildMode(ActivityTemplate temp)
    {
        StartCoroutine(startBuildMode(temp));
        actualTemplate = temp;
    }

    IEnumerator startBuildMode(ActivityTemplate template)
    {
        tempBuild = Instantiate(template.prefab, transform);
        buildMode = true;
        yield return new WaitWhile(()=>buildMode);
        buildMode = false;
        Destroy(tempBuild);
    }
}
