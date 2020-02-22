using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePowerUp : MonoBehaviour
{
    //0 = Jump, 1 = Shield, 2 = speed.
    public int id;

    public GameObject player;

    PlayerManager playerManagerScript;

    public GameObject powerUpBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (id == 0)
            {
                collision.GetComponent<PlayerManager>().haveJumpPowerUp = true;
            }else if (id == 1)
            {
                collision.GetComponent<PlayerManager>().haveShieldPowerUp = true;
            }else if (id == 2)
            {
                collision.GetComponent<PlayerManager>().haveSpeedPowerUp = true;
            }
            powerUpBar.SetActive(true);
            Destroy(gameObject);
        }
    }
}
