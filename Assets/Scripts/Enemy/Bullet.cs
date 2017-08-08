using UnityEngine;

public class Bullet : MonoBehaviour {

    public delegate void CollisionEvent();
    public event CollisionEvent Reflect;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Katana")
        {
            float velocity = GetComponent<Collider>().GetComponent<VRWeapon>().velocity;
            if (velocity > 10f)
            {

            }
        }
    }
}
