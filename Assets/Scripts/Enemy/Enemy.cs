using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Gun gun;
    public bool firing;    

    public void PreShoot()
    {
        Player player = Player.instance;
        firing = true;
        //Randomly target close to the player center


        /*
        BulletTrajectory traj = Instantiate(BulletTrajectory, gun.barrelTransform);
        traj.Initialize(gun.position);
        */
    }

    void Shoot()
    {
        gun.Shoot();
        firing = false;
    }
}
