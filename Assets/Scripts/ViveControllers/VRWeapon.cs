
public class VRWeapon : VRInteractableObject
{
    public float velocity;


    public override void BeginInteraction(WandController wand)
    {
        base.BeginInteraction(wand);
        transform.position = wand.transform.position;
        transform.rotation = wand.transform.rotation;
        transform.SetParent(wand.transform);
        wand.HideControllerModel();
    }
    public override void EndInteraction(WandController wand)
    {
        base.EndInteraction(wand);
        transform.SetParent(null);
        wand.ShowControllerModel();
    }
}
