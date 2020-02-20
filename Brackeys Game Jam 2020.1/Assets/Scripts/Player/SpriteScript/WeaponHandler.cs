using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public SpriteRenderer weaponSpriteRenderer;
    public SpriteRenderer groundPortal;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void HideWeapon()
    {
        weaponSpriteRenderer.enabled = false;
    }

    public void ShowWeapon()
    {
        weaponSpriteRenderer.enabled = true;
    }

    public void HidePortal()
    {
        groundPortal.enabled = false;
    }

    public void ShowPortal()
    {
        groundPortal.enabled = true;
    }

    public void StopPlayerMove()
    {
        playerController.canMove = false;
    }

    public void StartPlayerMove()
    {
        playerController.canMove = true;
    }
}
