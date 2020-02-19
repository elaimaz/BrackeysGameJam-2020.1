using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BasePowerUp : MonoBehaviour
{
    public virtual void OnPowerUpPickedUp(GameObject playerobject) { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            OnPowerUpPickedUp(collision.gameObject);
        }
    }
}
