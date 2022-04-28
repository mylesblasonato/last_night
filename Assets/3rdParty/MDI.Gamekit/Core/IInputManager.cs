using UnityEngine;

public interface IInputManager
{
    public void ActivateInput(bool activate);
    public float GetAxis(string axis);

    public Vector3 GetMousePosition();

    public bool GetButtonUp(string axis);

    public bool GetButton(string axis);

    public bool GetButtonDown(string axis);
}
