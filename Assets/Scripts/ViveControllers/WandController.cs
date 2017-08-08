using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SphereCollider))]
public class WandController : MonoBehaviour {

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;    
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_RenderModel model { get { return trackedObj.transform.GetChild(0).GetComponent<SteamVR_RenderModel>(); } }

    List<VRInteractableObject> objectsHoveringOver = new List<VRInteractableObject>();    
    private VRInteractableObject interactingItem;
    private VRInteractableObject closestItem;

    // Use this for initialization
    void Start ()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();        
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(controller == null)
        {
            Debug.Log("Controller not initialized!");
            return;
        }

        if (controller.GetPressDown(triggerButton))
        {            
            float minDistance = float.MaxValue;
            float distance;
            
            foreach (VRInteractableObject item in objectsHoveringOver)
            {
                distance = (item.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestItem = item;
                }
            }           

            interactingItem = closestItem;
            if(interactingItem)
            {
                if(interactingItem.CurrentlyInteracting)
                {
                    interactingItem.EndInteraction(this);
                }
                interactingItem.BeginInteraction(this);
            }

        }
        if (controller.GetPressUp(triggerButton) && interactingItem != null)
        {
            interactingItem.EndInteraction(this);
            interactingItem = null;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger Entered");
        VRInteractableObject collidedItem = collider.GetComponent<VRInteractableObject>();
        if(collidedItem)
        {
            objectsHoveringOver.Add(collidedItem);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        Debug.Log("Trigger Exited");
        VRInteractableObject collidedItem = collider.GetComponent<VRInteractableObject>();
        if (collidedItem)
        {
            objectsHoveringOver.Remove(collidedItem);
        }

        if (objectsHoveringOver.Count == 0)
        {
            closestItem = null;
        }
    }

    public void HideControllerModel()
    {
        model.gameObject.SetActive(false);
    }

    public void ShowControllerModel()
    {
        model.gameObject.SetActive(true);
    }
}
