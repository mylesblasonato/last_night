using MPUIKIT;
using TMPro;
using UnityEngine;

namespace Game.ProjectZ{
    public class CursorManager : MonoSingleton<CursorManager>{
        [SerializeField] TextMeshProUGUI interactableNameText;
        [SerializeField] TextMeshProUGUI TutorialText;
        [SerializeField] MPImageBasic cursorImage;
        [SerializeField] float mouseChangeDuration = 1f;
        [SerializeField] float textFadeDuration = 30f;
        [SerializeField] float mouseFollowDamping = 1f;

        CanvasGroup interactableTextGroup;
        float elapsedTime;
        bool isMouseChanging = false;
        float newRadius;
        float newFade;

        void Awake(){
            interactableTextGroup = interactableNameText.transform.parent.GetComponent<CanvasGroup>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CreateSingleton(this, () => Destroy(this.gameObject));
            HideAndLockMouse(true);
        }

        void Update(){
            transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, Input.mousePosition, Time.deltaTime * mouseFollowDamping);
            if (!isMouseChanging) return;
            var radiusSize = Mathf.Lerp(cursorImage.CircleRadius, newRadius, elapsedTime);
            var textFade = Mathf.Lerp(interactableTextGroup.alpha, newFade, elapsedTime);
            elapsedTime += mouseChangeDuration * Time.deltaTime;
            if (elapsedTime > 1){
                radiusSize = newRadius;
                textFade = newFade;
                isMouseChanging = false;
                elapsedTime = 0;
            }
            cursorImage.CircleRadius = radiusSize;
            interactableTextGroup.alpha = textFade;
        }

        #region ABSTRACTIONS
        public void HideAndLockMouse(bool hideAndLockMouse){
            Cursor.visible = !hideAndLockMouse;
            Cursor.lockState = hideAndLockMouse ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void SetInteractableName(string text)
        {
            interactableNameText.text = text;
        }

        public void ChangeMouseRadius(float newRadius, float newFade, string name, string tutorial){
            interactableNameText.text = name;
            TutorialText.text = tutorial;
            this.newFade = newFade;
            if (this.newRadius == newRadius) return;
            elapsedTime = 0;
            this.newRadius = newRadius;
            isMouseChanging = true;
        }
        #endregion
    }
}