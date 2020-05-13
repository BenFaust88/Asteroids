using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponController : MonoBehaviour
{
    public float fireRate = 0.125f;
    public GameObject laserBlastPrefab;
    public Transform firePoint;

    private bool _isFiringPrimary = false;

    // Start is called before the first frame update
    void Start()
    {
        firePoint = GameObject.Find("FirePoint").transform;
    }

    public void FirePrimary()
    {
        if(!_isFiringPrimary)
        {
            _isFiringPrimary = true;
            StartCoroutine(ShootPrimary());
        }
    }

    IEnumerator ShootPrimary()
    {
        if(_isFiringPrimary)
        {
            GameObject laserBlast = Instantiate(laserBlastPrefab, firePoint.position, firePoint.rotation);

            yield return new WaitForSeconds(fireRate);
            _isFiringPrimary = false;
        }
    }
}
