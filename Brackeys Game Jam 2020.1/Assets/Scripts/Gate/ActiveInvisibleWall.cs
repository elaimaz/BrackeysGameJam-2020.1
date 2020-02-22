using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInvisibleWall : MonoBehaviour
{
    public GameObject invisibleWall;

    public void InvisibleWall()
    {
        invisibleWall.SetActive(true);
    }
}
