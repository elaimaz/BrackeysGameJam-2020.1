using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGateInstant : MonoBehaviour
{
    public GameObject openGate;
    public GameObject closeGate;

    public GameObject invisibleWall;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            openGate.SetActive(false);
            closeGate.SetActive(true);
            invisibleWall.SetActive(true);
        }

    }
}
