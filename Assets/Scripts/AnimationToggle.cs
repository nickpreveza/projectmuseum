using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationToggle : ActionReceiver
{
    public bool moveBack = false;
    [SerializeField] float moveBackTime = -1f;
    float timeElapsed;
    float currentAnimationTime;
    bool moving;
    Vector3 initPos, currentPos, targetPos;

    [SerializeField] List<Vector3> targetPointOffsets = new List<Vector3>();
    [SerializeField] List<float> animationTimes = new List<float>();
    int targetIndex = 0;
    bool isBackwards = false;

    [SerializeField] GameObject targetObject;

    //[SerializeField] GameObject[] targetObjects;
    private void Start()
    {
        EventManager.Instance.onActionTriggered += ActionTriggered;

        initPos = targetObject.transform.localPosition;
        currentPos = initPos;

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

    private void OnDestroy()
    {
        EventManager.Instance.onActionTriggered -= ActionTriggered;
    }

    private void Update()
    {
        if (moving)
        {
            timeElapsed += Time.deltaTime / currentAnimationTime;
            targetObject.transform.localPosition = Vector3.Lerp(currentPos, targetPos, timeElapsed);

            if (Vector3.Distance(targetObject.transform.localPosition, targetPos) <= 0.001f)
            {
                if (isBackwards)
                {
                    if (targetIndex - 1 < 0)
                    {
                        moving = false;

                    }
                    else
                    {
                        targetIndex--;
                        SetDestination(targetPointOffsets[targetIndex], animationTimes[targetIndex]);
                    }
                }
                else
                {
                    if (targetIndex + 1 >= targetPointOffsets.Count)
                    {
                        moving = false;

                    }
                    else
                    {
                        targetIndex++;
                        SetDestination(targetPointOffsets[targetIndex], animationTimes[targetIndex]);
                    }
                }
            }   
        }
    }


    void SetDestination(Vector3 destination, float time)
    {
        timeElapsed = 0;
        currentAnimationTime = time;

        currentPos = targetObject.transform.localPosition;
        targetPos = destination;

        moving = true;
    }

    public override void ActionTriggered(int sourceIndex, int targetIndex, bool enabled)
    {
        if (sourceIndex == matchActionSource || targetIndex == receiverIndex)
        {
            if (enabled)
            {
                DoAction();
            }
            else 
            {
                ResetAction();
            }
        }
       
        
    }

    public override void DoAction()
    {
        targetIndex = 0;
        isBackwards = false;
        SetDestination(targetPointOffsets[targetIndex], animationTimes[targetIndex]);
    }

    public override void ResetAction()
    {
        if (noReset) { return;  }
        isBackwards = true;
        targetIndex = targetPointOffsets.Count - 1;
        SetDestination(targetPointOffsets[targetIndex], animationTimes[targetIndex]);
    }
}
