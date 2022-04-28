using UnityEngine;

namespace MDI.Gamekit.Input{
    public abstract class OldUnityInput : MonoSingleton<OldUnityInput>, IInputManager{
        public bool isActive { get; set; } = true;

        void Awake(){
            CreateSingleton(this, () => Destroy(this.gameObject));
        }

        public virtual void ActivateInput(bool activate){
            isActive = activate;
        }

        public virtual float GetAxis(string axis){
            if (isActive)
                return UnityEngine.Input.GetAxis(axis);
            return 0;
        }

        public virtual Vector3 GetMousePosition(){
            if (isActive)
                return UnityEngine.Input.mousePosition;
            else
                return Vector3.zero;
        }

        public virtual bool GetButtonUp(string axis){
            if (isActive)
                return UnityEngine.Input.GetButtonUp(axis);
            else
                return false;
        }

        public virtual bool GetButton(string axis){
            if (isActive)
                return UnityEngine.Input.GetButton(axis);
            else
                return false;
        }

        public virtual bool GetButtonDown(string axis){
            if (isActive)
                return UnityEngine.Input.GetButtonDown(axis);
            else
                return false;
        }
    }
}
