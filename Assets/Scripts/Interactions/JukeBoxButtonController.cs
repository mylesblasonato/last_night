using MDI.Gamekit.MadBox;
using UnityEngine;
using UltEvents;
using TMPro;

namespace Game.ProjectZ
{
    public class JukeBoxButtonController : MonoBehaviour
    {
        [SerializeField] JukeboxComponentSample jukebox;
        [SerializeField] TextMeshPro text;
        [SerializeField] string appendedText = "+ Paused...";
        [SerializeField] string useAxis = "Use";
        [SerializeField] float distance = 1f;
        [SerializeField] string layerMask;
        [SerializeField] UltEvent onClick;
        [SerializeField, Multiline] string tutorial = "INTERACT [A]";

        new Camera camera;

        void Awake()
        {
            camera = Camera.main;
        }

        void Update()
        {
            UseOnInput(this.useAxis);
        }

        void UseOnInput(string useAxis)
        {
            if (InputManager.Instance.GetButtonDown(useAxis) && DetectInteractableAtDistance(distance))
            {
                onClick?.Invoke();
                text.text = jukebox.GetTrackTitle() + appendedText;
            }
        }

        bool DetectInteractableAtDistance(float distance)
        {
            if (InteractableManager.Instance.ActiveInteractable != null) return false;
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(InputManager.Instance.GetMousePosition());
            if (Physics.Raycast(ray, out hit, distance, ~(1 >> LayerMask.NameToLayer(layerMask))) && hit.transform == transform)
                return true;

            if (InteractableManager.Instance.ActiveInteractable == transform)
                return true;
            return false;
        }

        void OnMouseOver()
        {
            CursorManager.Instance.ChangeMouseRadius(20f, 1f, name.ToUpper(), tutorial);
        }

        void OnMouseExit()
        {
            CursorManager.Instance.ChangeMouseRadius(4f, 0f, name.ToUpper(), tutorial);
        }
    }
}