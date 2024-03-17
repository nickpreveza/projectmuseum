using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    public GameObject wallcube;
    public GameObject floorcube;
    public GameObject door;

    private void Start()
    {
        wallcube.name = "wall";
        floorcube.name = "floor";

    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "floor")
        {
            door.GetComponent<doorO>().Open();
            Debug.Log("detected");
        }
        Debug.Log("touched");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "floor")
        {
            door.GetComponent<doorO>().Close();
        }
    }
}
