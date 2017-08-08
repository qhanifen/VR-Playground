using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRKatana : VRWeapon {

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet") { }
    }
}
