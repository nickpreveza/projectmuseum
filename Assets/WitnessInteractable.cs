using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
public class WitnessInteractable : MonoBehaviour
{
    [SerializeField] bool isFocused = false;
    [SerializeField] bool isSolved = false;

    [SerializeField] float inputSpeed = 1f;

    public UnityEvent triggerEvent; //called when puzzle solved

    Vector2 input;
    private void Update()
    {
        input.x = Input.GetAxis("Mouse X");
        input.y = Input.GetAxis("Mouse Y");
        input = input * inputSpeed;

        if (input.magnitude == 0.0f) return;
    }
    public void SetBoardFocus()
    {
        isFocused = true;
        ResetPuzzle();

    }

    public void ExitFocus()
    {
        isFocused = false;
        ResetPuzzle();
    }

    public void ResetPuzzle()
    {

    }
}
