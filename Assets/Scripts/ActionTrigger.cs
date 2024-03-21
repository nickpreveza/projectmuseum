using UnityEngine;

public interface IActionTrigger 
{
    public int sourceIndex { get; set; }
    public int targetIndex { get; set; }
    void TriggerAction(bool enabled);

}
