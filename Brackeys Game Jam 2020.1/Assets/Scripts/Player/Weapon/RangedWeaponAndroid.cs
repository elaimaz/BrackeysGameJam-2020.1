using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponAndroid : MonoBehaviour
{
    public float rps;
    private int damage;
    public float BulleteSpeed;
    public Transform AttackOrigin;
    public GameObject BulletPrefab;
    public LayerMask ememyLayer;

    public Animator playerAnime;
    public Animator anime;
    public float angle;
    private float tLastFire;
    private float tNextFire;
    private Vector3 mousePos;
    private Vector3 lookDir;

    public Joystick joystick;
    
    // Update is called once per frame
    void Update()
    {
        tLastFire += Time.deltaTime;

//        lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        lookDir.x = joystick.Horizontal;
        lookDir.y = joystick.Vertical;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Shoot(float damage)
    {
        print("Recieved command to fire");
        this.damage = (int)damage / 2;// This needs to be changed cuz 2  bullets were spawning.
        anime.SetTrigger("isAttacking");
        playerAnime.SetTrigger("isAttacking");
    }   
    

    public void ShootBullet()
    {
        Instantiate(BulletPrefab, AttackOrigin.position, AttackOrigin.rotation).GetComponent<BulletScript>().SetBullete(damage, BulleteSpeed);
    }
}
