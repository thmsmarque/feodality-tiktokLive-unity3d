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

    public LayerMask layerToGet;

    public LayerMask activityLayer;

    GameManagerScript gm;


    void Start()
    {
        buildMode = false;
        layerToGet = LayerMask.GetMask("Activity");
        layerToGet = ~layerToGet;
        activityLayer = LayerMask.GetMask("Activity");

        gm = GetComponent<GameManagerScript>();

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
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, layerToGet))
            {
                Vector3 elevatedPosition = hit.point;
                elevatedPosition.y += actualTemplate.elevatedHeight;
                tempBuild.transform.position = elevatedPosition;
            }

            if (checkBuild())
            {
                tempBuild.GetComponent<Renderer>().material.color = Color.green;
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Lancement Construction...");
                    GameObject tempNewActivity = Instantiate(activityPrefab, tempBuild.transform.position, Quaternion.identity, transform);
                    tempNewActivity.GetComponent<ActivityScript>().setActivityTemplate(actualTemplate);
                    gm.addActivity(tempNewActivity.GetComponent<ActivityScript>());
                    gm.removeMaterials(actualTemplate.costInMaterials);
                    
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

        if(actualTemplate.costInMaterials > gm.materials)
        {
            return false;
        }

        // Origine de la sphère, souvent la position du joueur ou de l'objet
        Vector3 origin = tempBuild.transform.position;

        // Utiliser Physics.OverlapSphere pour obtenir tous les colliders dans la sphère
        Collider[] hitColliders = Physics.OverlapSphere(origin, radius, activityLayer);
        // Si la sphère touche un ou plusieurs colliders, retourner false
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject != tempBuild)
                {
                    return false;
                }
            }
        }
        else
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
        if(tempBuild != null)
        {
            Destroy(tempBuild);
        }

        tempBuild = Instantiate(template.prefab, transform);

        buildMode = true;
        yield return new WaitWhile(()=>buildMode);
        buildMode = false;
        Destroy(tempBuild);
    }
}
