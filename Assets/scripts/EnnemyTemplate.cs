using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ennemy")]
public class EnnemyTemplate : ScriptableObject
{
    public GameObject prefab;

    public float rangeOfSearch;
    public float rangeOfAttack;
    public float power;
    public float speedAttack;
    public float speed;
    public float health;

    public LayerMask layerTarget;

}
