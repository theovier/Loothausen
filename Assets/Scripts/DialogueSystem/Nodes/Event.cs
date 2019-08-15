
using UnityEngine.Events;

namespace Dialogue {
    
    [NodeTintAttribute("#FFFFAA")]
    public class Event : DialogueNode {

        public UnityEvent[] trigger;

        public override void Trigger() {
            foreach (var e in trigger) {
                e.Invoke();
            }
        }
    }
}