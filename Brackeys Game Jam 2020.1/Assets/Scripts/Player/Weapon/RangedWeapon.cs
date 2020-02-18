using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public float rps;
    public int damage;
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


    // Update is called once per frame
    void Update()
    {
        tLastFire += Time.deltaTime;

        lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
     
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetButton("Fire1"))
        {
            if (tLastFire >= tNextFire)
            {
                tNextFire = 1f / rps;
                tLastFire = 0.0F;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        anime.SetTrigger("isAttacking");
        playerAnime.SetTrigger("isAttacking");
    }   
    

    public void ShootBullet()
    {
        Instantiate(BulletPrefab, AttackOrigin.position, AttackOrigin.rotation).GetComponent<BulletScript>().SetBullete(damage,BulleteSpeed);
    }
}
