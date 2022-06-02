using Fungus;
using MDI.Gamekit.MadBox;
using MPUIKIT;
using UnityEngine;

namespace Game.GameProductionOne
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] CanvasGroup pauseScreen;
        [SerializeField] string pauseAxis = "Cancel";
        [SerializeField] Flowchart mainFlow;
        [SerializeField] MPImageBasic relationshipMeter;
        [SerializeField] float scaleDuration = 0.5f;
        [SerializeField] float scaleAmount = 1.5f;
        [SerializeField] JukeboxComponentSample jbc;

        AudioSource audioSource;
        float maxRelationship = 100;
        float relationship = 0;

        public bool IsPaused { get; private set; } = false;

        void Awake()
        {
            CreateSingleton(this, () => Destroy(this.gameObject));

            if (mainFlow != null)
                relationship = mainFlow.GetFloatVariable("relationship");
            
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (Input.GetButtonDown(pauseAxis))
                Pause(!IsPaused);
        }

        #region ABSTRACTIONS
        public void Pause(bool pause)
        {
            if (pause)
            {
                audioSource.Pause();
                if (jbc != null)
                    jbc.StopTrack();
            }
            else
            {
                audioSource.Play();
                if (jbc != null)
                    jbc.PlayTrack();
            }

            Time.timeScale = pause ? 0 : 1f;
            InputManager.Instance.ActivateInput(!pause);
            IsPaused = pause;
            pauseScreen.alpha = pause ? 1 : 0;
        }

        public void UpdateRelationship(int amountToAddOrSubtract)
        {
            if (mainFlow == null) return;
            relationship += amountToAddOrSubtract;
            float percentage = relationship / maxRelationship;
            relationshipMeter.fillAmount = percentage;
            var ltDesc = LeanTween.scale(relationshipMeter.transform.parent.gameObject, new Vector3(scaleAmount, scaleAmount, scaleAmount), scaleDuration);
            ltDesc.loopType = LeanTweenType.pingPong;
            ltDesc.setLoopPingPong(1);
        }
        #endregion
    }
}