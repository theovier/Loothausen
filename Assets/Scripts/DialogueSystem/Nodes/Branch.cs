using System;

namespace Dialogue {
    
    [NodeTintAttribute("#CCCCFF")]
    
    public class Branch : DialogueNode {

        public Condition[] conditions;
        [OutputAttribute] public DialogueNode pass;
        [OutputAttribute] public DialogueNode fail;
        
        public override void Trigger() {
            var success = EvaluateConditions();
            var port = success ? GetOutputPort("pass") : GetOutputPort("fail");
            TriggerSuccessors(port);
        }

        private bool EvaluateConditions() {
            foreach (var condition in conditions) {
                var conditionViolated = !condition.Invoke();
                if (conditionViolated) {
                    return false;
                }
            }
            return true;
        }

    }

    [Serializable]
    public class Condition : SerializableCallback<bool> { }
}