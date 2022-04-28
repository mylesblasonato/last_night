using System.Collections.Generic;
using UnityEngine;

namespace MDI.Gamekit.Core{
    [System.Serializable, CreateAssetMenu(fileName = "New Scriptable Event", menuName = "MDI/Event/Create New Scriptable Event ")]
    public class SEvent : ScriptableObject{
        HashSet<ISEventListener> listeners = new HashSet<ISEventListener>();
        
        public void Register(ISEventListener listener){
            listeners.Add(listener);
        }

        public void Deregister(ISEventListener listener){
            listeners.Remove(listener);
        }

        public void Raise(){
            foreach (var listener in listeners){
                listener.Invoke();
            }
        }
    }
}
