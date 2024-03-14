using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class TriggerInteractable : Interactable
{
    public bool moveBack = true;
    [Header("Animations")]
    [SerializeField] float moveBackTime = -1f;
    float timeElapsed;
    float currentAnimationTime;
    bool moving;
    Vector3 initPos, currentPos, targetPos;

    [SerializeField] List<Vector3> targetPointOffsets = new List<Vector3>();
    [SerializeField] List<float> animationTimes = new List<float>();
    int targetIndex = 0;

    public UnityEvent triggerEvent;

    void Start()
    {
        initPos = transform.localPosition;
        currentPos = initPos;

        /*
        for(int i = 0; i < targetPointOffsets.Count; i++)
        {
            Vector3 editedTarget = targetPointOffsets[i];
            targetPointOffsets[i] = initPos + editedTarget;
        }*/

        if (moveBack)
        {
            targetPointOffsets.Add(initPos);

            if (moveBackTime >= 0)
            {
                animationTimes.Add(moveBackTime);
            }
            else
            {
                animationTimes.Add(animationTimes[0]);
            }
        }

       
    }

    void Press()
    {

        if (moving)
        {
            return;
        }

        isInteractable = false;
        transform.localPosition = initPos;
        targetIndex = 0;
        SetDestination(targetPointOffsets[targetIndex], animationTimes[targetIndex]);
    }

    void SetDestination(Vector3 destination, float time)
    {
        timeElapsed = 0;
        currentAnimationTime = time;

        currentPos = transform.localPosition;
        targetPos = destination;

        moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            timeElapsed += Time.deltaTime / currentAnimationTime;
            transform.localPosition = Vector3.Lerp(currentPos, targetPos, timeElapsed);

            if (Vector3.Distance(transform.localPosition, targetPos) <= 0.001f)
            {
               

                if (targetIndex + 1 >= targetPointOffsets.Count)
                {
                    moving = false;
                    TriggerEffect();
                }
                else
                {
                    targetIndex++;
                    SetDestination(targetPointOffsets[targetIndex], animationTimes[targetIndex]);
                }
            }
        }
    }

    public override void Interact(Transform _grabPointTransform)
    {
        UIManager.Instance.overlayPanel?.GetComponent<OverlayPanel>().DisablePrompt();
        Press();
    }

    public void TriggerEffect()
    {
        isInteractable = true;
        Debug.Log("Button action was called");
        triggerEvent?.Invoke();
    }


    public override void Drop()
    {
        //this is invalid for this case;
    }


}
