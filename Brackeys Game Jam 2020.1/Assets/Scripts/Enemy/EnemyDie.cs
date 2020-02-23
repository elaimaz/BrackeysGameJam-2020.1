using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public GameObject healthPrefab;
    
    public void OnDestroy()
     {
        Instantiate(healthPrefab, gameObject.transform.position, Quaternion.identity);
     }
}
