using UnityEngine;

public abstract class ActionReceiver : MonoBehaviour
{
    public int receiverIndex;
    public int matchActionSource;

    public bool destoryAfterAction;
    public bool noReset;

    public abstract void ActionTriggered(int sourceIndex, int targetIndex, bool enabled);

    public abstract void DoAction();

    public abstract void ResetAction();
}
