using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class VRInteractableObject : MonoBehaviour
{
    public Rigidbody rigidBody;
    public WandController attachedWand;

    public bool CurrentlyInteracting { get { return _interacting; } set { _interacting = value; } }
    private bool _interacting;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        CurrentlyInteracting = false;
    }

    public virtual void BeginInteraction(WandController wand)
    {
        CurrentlyInteracting = true;
        attachedWand = wand;
    }      

    public virtual void EndInteraction(WandController wand)
    {
        CurrentlyInteracting = false;
        attachedWand = null;
    }
}
