using UltEvents;
using UnityEngine;

namespace MDI.Gamekit.Core{
    public class SEventListener : MonoBehaviour, ISEventListener{
        [SerializeField] SEvent sEvent;
        [SerializeField] UltEvent eventToInvoke;

        void Start(){
            sEvent.Register(this);
        }

        void OnDestroy(){
            sEvent.Deregister(this);
        }

        public void Invoke(){
            eventToInvoke?.Invoke();
        }
    }
}
