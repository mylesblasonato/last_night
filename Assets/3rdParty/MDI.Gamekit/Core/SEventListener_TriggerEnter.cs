using UltEvents;
using UnityEngine;

namespace MDI.Gamekit.Core{
    public class SEventListener_TriggerEnter : MonoBehaviour
    {
        [SerializeField] SEvent sEvent;

        void OnTriggerEnter(Collider other){
            if(other.CompareTag("Player")){
                sEvent.Raise();
            }
        }
    }
}
