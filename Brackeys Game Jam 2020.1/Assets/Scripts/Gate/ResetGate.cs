using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGate : MonoBehaviour
{
    public GameObject openGate;
    public GameObject closingGate;

    public GameObject invisibleWall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            openGate.SetActive(true);
            closingGate.SetActive(false);
            invisibleWall.SetActive(false);
        }
    }
}
