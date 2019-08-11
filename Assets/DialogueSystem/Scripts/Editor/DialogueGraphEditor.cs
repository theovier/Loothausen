using Dialogue;
using XNodeEditor;

namespace DialogueEditor {
    
    [CustomNodeGraphEditor(typeof(DialogueGraph))]
    public class DialogueGraphEditor : NodeGraphEditor {
		
        public override string GetNodeMenuName(System.Type type) {
            if (type.Namespace == "Dialogue") {
                return base.GetNodeMenuName(type).Replace("Dialogue/","");
            }
            return null;
        }
    }
}