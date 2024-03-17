using UnityEngine;

public class NameUpdater : MonoBehaviour
{
    public string setName;

    public void UpdateName()
    {
        this.name = setName + " (" + transform.position.x + "," + transform.position.y + "," + transform.position.z + ")";
    }
}
