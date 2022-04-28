using Fungus;
using System.Collections;
using UnityEngine;

namespace MDI.Gamekit.Interactables
{
    public class OpenInteractable : IInteractable, IOpenable
    {
        string[] mousePosition = { "", "" };
        bool isOpen = false;

        public OpenInteractable(string xAxis, string yAxis)
        {
            mousePosition[0] = xAxis;
            mousePosition[1] = yAxis;
        }

        public void Open(float force, Rigidbody rigidbody, Vector3 mouseLastFrame, bool isFront)
        {
            var pos = new Vector3(Input.OldUnityInput.Instance.GetAxis(mousePosition[0]), Input.OldUnityInput.Instance.GetAxis(mousePosition[1]), 0f) - mouseLastFrame;
            Vector3 finalPos = Vector3.zero;

            if (isFront)
                finalPos = new Vector3(0, 0, -pos.y + -pos.x);
            else
                finalPos = new Vector3(0, 0, -pos.y + pos.x);
            rigidbody.AddRelativeForce(finalPos * force * Time.deltaTime, ForceMode.Impulse);
        }

        public void Open(float degrees, float duration, Transform transform, Vector3 axis)
        {
            var rotateToVector = new Vector3(axis.x * degrees, axis.y * degrees, axis.z * degrees);

            if(axis.y == 1)
                LeanTween.rotateY(transform.gameObject, degrees, duration);
            else if (axis.x == 1)
                LeanTween.rotateX(transform.gameObject, degrees, duration);
        }

        public void OpenDrawer(float force, Rigidbody rigidbody, Vector3 mouseLastFrame, bool isFront)
        {
            var pos = new Vector3(Input.OldUnityInput.Instance.GetAxis(mousePosition[0]), Input.OldUnityInput.Instance.GetAxis(mousePosition[1]), 0f) - mouseLastFrame;
            Vector3 finalPos = Vector3.zero;

            if (isFront)
                finalPos = new Vector3(0, 0, -pos.y + -pos.x);
            else
                finalPos = new Vector3(0, 0, -pos.y + pos.x);
            rigidbody.AddRelativeForce(finalPos * force * Time.deltaTime, ForceMode.Impulse);
        }

        public void OpenDrawer(float amount, float duration, Transform transform)
        {
            LeanTween.moveLocalZ(transform.gameObject, amount, duration);
        }
    }
}