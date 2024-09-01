using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    // [SerializeField] private Transform weapon;
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject projectile;

    public readonly int damage = 5;
    [SerializeField] private int maxAmmo = 6;
    private int currentAmmo;

    [SerializeField] private float timeBetweenShot = .5f;
    private float nextTimeShot;
    [SerializeField] private float reloadTime = 1.05f;
    private bool isReloading = false;


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = maxAmmo;
    }


    // Update is called once per frame
    private void Update()
    {
        //Set the red axis towards the direction of the mouse 
        UnityEngine.Vector2 direction = (UnityEngine.Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        transform.right = direction;

        //Flip the gun corrosponding to the direction of the mouse
        UnityEngine.Vector2 scale = transform.localScale;
        if (direction.x < 0) 
        {
            scale.y = -1;
        }
        else if (direction.x > 0) 
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        //Reload & Firing
        AmmoCheck();
    }


    private void Shoot()
    {
        currentAmmo--;
        Debug.Log("Current ammo: " + currentAmmo);
        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }
    

    //Function for firing bullets and reloading
    private void AmmoCheck()
    {
        if (isReloading) return;

        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time > nextTimeShot)
        {
            nextTimeShot = Time.time + timeBetweenShot;
            Shoot();
        }
    }


    private IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Reloading");

        yield return new WaitForSeconds(reloadTime);

        Debug.Log("Reloaded");

        currentAmmo = maxAmmo;

        isReloading = false;
    }
}
