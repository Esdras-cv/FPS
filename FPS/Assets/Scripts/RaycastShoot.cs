using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.01f);
    private LineRenderer shotLine;
    private float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        shotLine = GetComponent<LineRenderer>();
        fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            shotLine.SetPosition(0, gunEnd.position);

            if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                shotLine.SetPosition(1, hit.point);
                TesseractScript health = hit.collider.GetComponent<TesseractScript>();

                if(health != null)
                {
                    health.Damage(gunDamage);
                }

                if(hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }               
            }
            else
            {
                shotLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        shotLine.enabled = true;
        yield return shotDuration;
        shotLine.enabled = false;
    }
}
