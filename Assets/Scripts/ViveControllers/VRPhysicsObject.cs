using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPhysicsObject : VRInteractableObject {

    private Transform interactionPoint;

    private Vector3 posDelta;
    private float velocityFactor = 20000f;

    private Quaternion rotationDelta;
    private float rotationFactor = 400f;
    private float angle;
    private Vector3 axis;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        interactionPoint = new GameObject().transform;
        velocityFactor /= rigidBody.mass;
        rotationFactor /= rigidBody.mass;
    }

    public void FixedUpdate()
    {
        if(attachedWand && CurrentlyInteracting)
        {
            posDelta = attachedWand.transform.position - interactionPoint.position;
            this.rigidBody.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;

            rotationDelta = attachedWand.transform.rotation * Quaternion.Inverse(interactionPoint.rotation);
            rotationDelta.ToAngleAxis(out angle, out axis);
            
            if(angle > 180)
            {
                angle -= 360;
            }

            this.rigidBody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
        }
    }

    public override void BeginInteraction(WandController wand)
    {
        attachedWand = wand;
        interactionPoint.transform.position = wand.transform.position;
        interactionPoint.transform.rotation = wand.transform.rotation;
        interactionPoint.SetParent(transform, true);

        CurrentlyInteracting = true;
    }

    public override void EndInteraction(WandController wand)
    {
        if (wand == attachedWand)
        {
            attachedWand = null;
            CurrentlyInteracting = false;
        }
    }   
}
