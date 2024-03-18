using UnityEngine;
using UnityEngine.UI;

public class WitnessLineSegment : MonoBehaviour
{

    bool endFound;
    [SerializeField] Image background;
    [SerializeField] Image fillImage;
    public float fillValue;
    void Start()
    {
        SetFill(0);
    }

    public void SetFill(float newFill)
    {
        fillValue = newFill;
        fillImage.fillAmount = fillValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
