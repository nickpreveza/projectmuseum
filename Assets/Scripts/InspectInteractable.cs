using UnityEngine;

public class InspectInteractable : Interactable
{
    MeshRenderer thisMeshRenderer;
    [SerializeField] GameObject prefabToInspect;

    private void Start()
    {
        thisMeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }
    public override void Interact(Transform _grabPointTransform, FixedJoint targetJoint)
    {
        //UIManager.Instance.overlayPanel?.GetComponent<OverlayPanel>().DisablePrompt();
        //thisMeshRenderer.enabled = false;
        GameManager.Instance.SetItemInspectState(true, prefabToInspect);
        EventManager.Instance.OnItemGrabbed(this.gameObject);
        //UIManager.Instance.ShowItemInspect();
    }

    public override void Drop()
    {
        thisMeshRenderer.enabled = true;
        GameManager.Instance.SetItemInspectState(false);
        EventManager.Instance.OnItemDropped(this.gameObject);
    }
}
