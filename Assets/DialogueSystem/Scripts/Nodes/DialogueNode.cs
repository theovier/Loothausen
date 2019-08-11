using XNode;

namespace Dialogue {
    public abstract class DialogueNode : Node {
        [Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public DialogueNode input;
        [Output(backingValue = ShowBackingValue.Never)] public DialogueNode output;
        
        public abstract void Trigger();

        protected static void TriggerSuccessors(NodePort port) {
            if (port == null) {
                return;
            }
            for (var i = 0; i < port.ConnectionCount; i++) {
                var connection = port.GetConnection(i);
                (connection.node as DialogueNode).Trigger();
            }
        }
    }
}

