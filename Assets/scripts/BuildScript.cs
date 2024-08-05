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

    public bool buildModeActivity;
    public bool buildModeTower;
    public bool buildModeFaith;

    public float radius = 1f;
    public float angleOfRotation = 5f;

    public LayerMask layerToGet;

    public LayerMask activityLayer;

    GameManagerScript gm;

    float actionRadius = 0f;
    public int resolution = 20; // Nombre de points pour la grille
    public GameObject actionZonePrefab;
    private GameObject actionZoneInstance;


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
            handleRotation();
            UpdateActionZone();

        }
        if (buildModeTower)
        {
            handleBuildModeTouret();
            handleRotation();
            UpdateActionZone();
        }
  
    }

    void handleRotation()
    {
        Vector3 rot = new Vector3(0, 1, 0);
        if(Input.GetKey(KeyCode.Q))
        {
            tempBuild.transform.Rotate(rot, -angleOfRotation * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            tempBuild.transform.Rotate(rot, angleOfRotation * Time.deltaTime);
        }
    }
    void UpdateActionZone()
    {
        MeshFilter meshFilter = actionZoneInstance.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];

        float step = (actionRadius * 2) / (resolution - 1);
        int triIndex = 0;

        for (int z = 0; z < resolution; z++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float xPos = -actionRadius + x * step;
                float zPos = -actionRadius + z * step;
                Vector3 worldPos = tempBuild.transform.position + new Vector3(xPos, 0, zPos);

                if (Physics.Raycast(worldPos + Vector3.up * 50, Vector3.down, out RaycastHit hit))
                {
                    vertices[z * resolution + x] = hit.point - tempBuild.transform.position;
                }
                else
                {
                    vertices[z * resolution + x] = new Vector3(xPos, 0, zPos);
                }

                if (x < resolution - 1 && z < resolution - 1)
                {
                    triangles[triIndex] = z * resolution + x;
                    triangles[triIndex + 1] = z * resolution + x + 1;
                    triangles[triIndex + 2] = (z + 1) * resolution + x;

                    triangles[triIndex + 3] = (z + 1) * resolution + x;
                    triangles[triIndex + 4] = z * resolution + x + 1;
                    triangles[triIndex + 5] = (z + 1) * resolution + x + 1;
                    triIndex += 6;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        actionZoneInstance.transform.position = tempBuild.transform.position;
    }



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
                GameObject tempNewActivity = Instantiate(touretPrefab, tempBuild.transform.position,Quaternion.identity, hit.collider.gameObject.transform);
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
                if (checkBuildActivity(hit.point))
                {
                    tempBuild.GetComponent<Renderer>().material.color = Color.green;
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("Lancement Construction...");
                        GameObject tempNewActivity = Instantiate(activityPrefab, tempBuild.transform.position, Quaternion.identity, transform);
                        tempNewActivity.GetComponent<ActivityScript>().setActivityTemplate(actualTemplate,tempBuild.transform.rotation);
                        gm.addActivity(tempNewActivity.GetComponent<ActivityScript>());
                        gm.removeMaterials(actualTemplate.costInMaterials);

                    }
                }
                else
                {
                    tempBuild.GetComponent<Renderer>().material.color = Color.red;
                }        
        
    }

    bool checkBuildActivity(Vector3 pos)
    {

        if(actualTemplate.costInMaterials > gm.materials)
        {
            return false;
        }


        Collider[] hitColliders = Physics.OverlapSphere(pos, actualTemplate.radiusChecking, activityLayer);
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
            Destroy(actionZoneInstance);
            actionZoneInstance = null;
            gm.updateCursor(0);
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
        actionZoneInstance = Instantiate(actionZonePrefab, Vector3.zero, Quaternion.identity);
        gm.updateCursor(1);

        if (template.isCultActivity())
        {
            actionRadius = tempBuild.GetComponentInChildren<FaithPassifScript>().temp.rangeOfAction;
        }
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
        gm.updateCursor(1);
        tempBuild = Instantiate(template.prefab, transform);
        actionZoneInstance = Instantiate(actionZonePrefab, Vector3.zero, Quaternion.identity);

        actionRadius = template.range;
        buildModeActivity = false;
        buildModeTower = true;
        yield return new WaitWhile(() => buildModeTower);
        buildModeActivity = false;
        buildModeTower = false;

        Destroy(tempBuild);
    }
}
