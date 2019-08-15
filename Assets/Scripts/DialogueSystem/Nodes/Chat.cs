using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

namespace Dialogue {
    
    [NodeTint("#CCFFCC")]
    
    public class Chat: DialogueNode {
        
        public CharacterInfo character;
        [TextArea] public string text;
        public AudioClip voiceClip;
        [Output(dynamicPortList = true)] public List<Choice> choices = new List<Choice>();
        
        
        /// <summary> Triggers the (only) successor node in case no choices were given </summary>
        public void TriggerNext() {
            if (OffersChoices()) {
                return;
            }
            var port = GetDefaultOutputPort();
            TriggerSuccessors(port);
        }

        public bool HasSuccessors() {
            return Outputs.Any(x => x.IsConnected);
        }
        
        public virtual bool OffersChoices() {
            return choices.Count > 0;
        }

        public NodePort GetChoiceOutputPort(int choiceIndex) {
            return GetOutputPort("choices " + choiceIndex);
        }

        protected NodePort GetDefaultOutputPort() {
            return GetOutputPort("output");
        }
        
        public override void Trigger() {
            (graph as DialogueGraph).current = this;
        }

        public void SelectChoice(int index) { 
            var choiceChat = ChoiceChat.CreateInstance(this, index);
            choiceChat.Trigger();
        }
        
        [System.Serializable] public class Choice {
            public string text;
            public AudioClip voiceClip;
        }
    }
}
