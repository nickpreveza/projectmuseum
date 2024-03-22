using UnityEngine;

public class doorO : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject doorR;
    public GameObject doorL;

    public void Open()
    {
        doorR.transform.position += new Vector3(-2f, 0f, 0f);
        doorL.transform.position += new Vector3(2f, 0f, 0f);
    }

    public void Close()
    {
        doorR.transform.position += new Vector3(2f, 0f, 0f);
        doorL.transform.position += new Vector3(-2f, 0f, 0f);
    }
}
