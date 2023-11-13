using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Note: refered to this StackOverflow question to get the gun rotation right: https://stackoverflow.com/a/56570217

public class GunScript : MonoBehaviour
{
    /* PUBLIC VARIABLES */
    public Transform target; // should maybe be an array?
    public float rotationSpeed; //todo
    public float projectileSpeed;
    public GameObject bullet;
    public Transform gunTipTransform;
    public bool shoot;

    /* PRIVATE VARIABLES */



    // Start is called before the first frame update
    void Start()
    {
        shoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gunTipTransform != null && target != null)
        {
            // Have the gun point at the target
            transform.LookAt(target);

            

            if (shoot)
            {
                shoot = false;
                // Create a bullet
                Instantiate(bullet);
                // set the bullet starting point
                bullet.transform.position = gunTipTransform.position;

                // pass the target to the bullet script. 
                BulletScript bulletScript = bullet.GetComponent<BulletScript>();

                // Set variables on bulletScript
                bulletScript.target = target;
                bulletScript.speed = projectileSpeed;
            }
        }
    }
}
