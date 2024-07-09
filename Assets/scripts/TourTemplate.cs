using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tower")]
public class TourTemplate : ScriptableObject
{
    public GameObject prefab;
    public float speedAttack;
    public float power;
    public float range;
    public float faithNeeded;
    public float degatZone;
    public float health;
    public float coastInMaterials;

    public LayerMask layerTarget; 
}
