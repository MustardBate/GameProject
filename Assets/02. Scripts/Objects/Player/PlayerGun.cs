using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform shotPoint;
    [SerializeField] private GameObject projectile;

    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip reload;
    [SerializeField] private AnimationClip shoot;

    // Everything related to the damage (Projectile script will use this)
    private readonly float baseDamage = 5;
    [HideInInspector] public float currentDamage;
    [HideInInspector] public int killCount;
    
    [Range(3f, 50f)]
    [HideInInspector] public float bulletSpeed = 18f;

    [SerializeField] private int maxAmmo = 6;
    private int currentAmmo;

    private float timeBetweenShot;
    private float nextTimeShot;
    [HideInInspector] public bool isReloading = false;

    [SerializeField] private BulletCountUI bulletCountUI;


    // Start is called before the first frame update
    void Start()
    {
        bulletCountUI = GameObject.FindGameObjectWithTag("BulletCountUI").GetComponent<BulletCountUI>();

        timeBetweenShot = shoot.length;

        nextTimeShot = timeBetweenShot;

        currentDamage = baseDamage;
        currentAmmo = maxAmmo;

        GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUIContainer>().SetDamageUI(baseDamage);
        GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUIContainer>().SetBulletSpeedUI(bulletSpeed);
        GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUIContainer>().SetKillsUI(killCount);

        bulletCountUI.SetBulletCountUI(currentAmmo, maxAmmo, isReloading);
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


    //Function for firing bullets and reloading
    private void AmmoCheck()
    {
        nextTimeShot -= Time.deltaTime;

        if (isReloading) return;

        if (Input.GetMouseButton(0) && nextTimeShot <= 0)
        {
            nextTimeShot = timeBetweenShot;
            currentAmmo--;
            StartCoroutine(Shoot());
        }

        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }
    }


    IEnumerator Shoot()
    {
        bulletCountUI.SetBulletCountUI(currentAmmo, maxAmmo, isReloading);
        animator.SetBool("isShooting", true);

        Instantiate(projectile, shotPoint.position, shotPoint.rotation);

        yield return new WaitForSeconds(.1f);

        animator.SetBool("isShooting", false);
    }


    private IEnumerator Reload()
    {
        isReloading = true;
        bulletCountUI.SetBulletCountUI(currentAmmo, maxAmmo, isReloading);

        animator.SetTrigger("Reloading");

        yield return new WaitForSeconds(reload.length - .25f);

        animator.SetBool("isReloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
        bulletCountUI.SetBulletCountUI(currentAmmo, maxAmmo, isReloading);
    }


    public float SetNewDamage(float damageUp, float damageScale)
    {
        currentDamage = (currentDamage + damageUp) * damageScale;
        return currentDamage;
    }
}
