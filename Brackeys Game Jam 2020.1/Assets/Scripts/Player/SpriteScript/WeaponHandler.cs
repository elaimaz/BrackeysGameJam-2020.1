using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public SpriteRenderer weaponSpriteRenderer;

    public void HideWeapon()
    {
        weaponSpriteRenderer.enabled = false;
    }

    public void ShowWeapon()
    {
        weaponSpriteRenderer.enabled = true;
    }
}
