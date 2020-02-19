using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : BasePowerUp
{
    public int AddHealth;
    public override void OnPowerUpPickedUp(GameObject playerObject)
    {
        playerObject.GetComponent<PlayerManager>().GainHealth(AddHealth);
        Destroy(gameObject);
    }
}
