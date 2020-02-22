using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(BoxCollider2D))]
public class CheckPoint : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.instance.SetCheckPoint(transform.position);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
