using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildScript : MonoBehaviour
{
    [SerializeField]
    GameObject activityPrefab;
    [SerializeField]
    GameObject touretPrefab;

    GameObject tempBuild;
    
    [SerializeField]
    TowerDefenseScript towerOfDefense;

    ActivityTemplate actualTemplate;
    TourTemplate tourTemplate;
    FaithPassifTemplate faithTemplate;

    public bool buildModeActivity;
    public bool buildModeTower;
    public bool buildModeFaith;

    public float radius = 1f;

    public LayerMask layerToGet;

    public LayerMask activityLayer;

    GameManagerScript gm;


    void Start()
    {
        buildModeActivity = false;
        layerToGet = LayerMask.GetMask("Activity");
        layerToGet = ~layerToGet;
        activityLayer = LayerMask.GetMask("Activity");

        gm = GetComponent<GameManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if(buildModeActivity)
        {
            handlebuildModeActivity();
        }
        if(buildModeTower)
        {
            handleBuildModeTouret();
        }
        //if(buildModeFaith)
        //{
        //    handleBuildModeFaith();
        //}
    }

    /*void handleBuildModeFaith()
    {
        handleLeavingBuildMode();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerToGet))
        {
            Vector3 elevatedPosition = hit.point;
            elevatedPosition.y += actualTemplate.elevatedHeight;
            tempBuild.transform.position = elevatedPosition;
        }
        if (checkBuildActivity())
        {
            tempBuild.GetComponent<Renderer>().material.color = Color.green;
            if (Input.GetMouseButtonDown(0))
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

    }*/

    void handleBuildModeTouret()
    {
        handleLeavingBuildMode();

        LayerMask towerMask = LayerMask.GetMask("Activity");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, towerMask))
            {
            Debug.Log("Tag du collider : " + hit.collider.tag);
                    if(hit.collider.tag == "Tower")
                    {
                        towerOfDefense = hit.collider.gameObject.GetComponent<TowerDefenseScript>();
                        tempBuild.transform.position = hit.collider.gameObject.GetComponent<TowerDefenseScript>().pointSpawn.position;
                    }
            }
            if(checkBuildTower())
            {
            if(Input.GetMouseButtonDown(0))
            {
                Debug.Log("Lancement Construction tour...");
                GameObject tempNewActivity = Instantiate(touretPrefab, tempBuild.transform.position, Quaternion.identity, hit.collider.gameObject.transform);
                tempNewActivity.GetComponent<TowerScript>().setTouretTemplate(tourTemplate);
                towerOfDefense.newTouret(tempNewActivity.GetComponent<TowerScript>());
                gm.addTower(tempNewActivity.GetComponent<TowerScript>());
                gm.updateFaithNeeded();
                gm.removeMaterials(tourTemplate.coastInMaterials);
                towerOfDefense = null;
            }
            }
    }

    void handlebuildModeActivity()
    {


        handleLeavingBuildMode();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, layerToGet))
            {
                    Vector3 elevatedPosition = hit.point;
                    elevatedPosition.y += actualTemplate.elevatedHeight;
                    tempBuild.transform.position = elevatedPosition;         
            }        
                if (checkBuildActivity())
                {
                    tempBuild.GetComponent<Renderer>().material.color = Color.green;
                    if (Input.GetMouseButtonDown(0))
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

    bool checkBuildActivity()
    {

        if(actualTemplate.costInMaterials > gm.materials)
        {
            return false;
        }

        // Origine de la sph�re, souvent la position du joueur ou de l'objet
        Vector3 origin = tempBuild.transform.position;

        // Utiliser Physics.OverlapSphere pour obtenir tous les colliders dans la sph�re
        Collider[] hitColliders = Physics.OverlapSphere(origin, radius, activityLayer);
        // Si la sph�re touche un ou plusieurs colliders, retourner false
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

        // Si la sph�re ne touche aucun collider, retourner true
        return true;
    }

    bool checkBuildTower()
    {
        if(towerOfDefense == null)
        {
            return false;
        }
        
            if (tourTemplate.coastInMaterials > gm.materials)
            {
                return false;
            }
            if(towerOfDefense.hasTouret)
            {
                return false;
            }
        return true;
    }

    //bool checkBuildPassifFaith()
    //{
    //    if (faithTemplate.cost > gm.materials)
    //    {
    //        return false;
    //    }else
    //    {
    //        return true;
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(tempBuild.transform.position, radius);
    }

    void handleLeavingBuildMode()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            buildModeActivity = false;
            buildModeTower = false;
            towerOfDefense = null;
        }
    }


    public void startCoroutinebuildMode(ActivityTemplate temp)
    {
        StartCoroutine(startbuildMode(temp));
        actualTemplate = temp;
        tourTemplate = null;
    }

    public void startCoroutinebuildMode(TourTemplate temp)
    {
        StartCoroutine(startbuildMode(temp));
        tourTemplate = temp;
        actualTemplate = null;
    }

    public IEnumerator startbuildMode(ActivityTemplate template)
    {
        if(tempBuild != null)
        {
            Destroy(tempBuild);
        }

        tempBuild = Instantiate(template.prefab, transform);

        buildModeTower = false;
        buildModeActivity = true;
        yield return new WaitWhile(()=>buildModeActivity);
        buildModeActivity = false;
        buildModeTower = false;

        Destroy(tempBuild);
    }

    public IEnumerator startbuildMode(TourTemplate template)
    {
        if (tempBuild != null)
        {
            Destroy(tempBuild);
        }

        tempBuild = Instantiate(template.prefab, transform);

        buildModeActivity = false;
        buildModeTower = true;
        yield return new WaitWhile(() => buildModeTower);
        buildModeActivity = false;
        buildModeTower = false;

        Destroy(tempBuild);
    }
}
