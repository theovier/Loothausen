using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

namespace Dialogue {
    
    [NodeTint("#CCFFCC")]
    
    public class Chat: DialogueNode {
        
        public CharacterInfo character;
        [TextArea] public string text;
        [Output(dynamicPortList = true)] public List<Choice> choices = new List<Choice>();
        
        /// <summary> Triggers the successor node for the given choice.
        /// Returns true if there is a successor node (i.e. dialogue continues)
        /// Returns false if the dialogue is aborted after selecting this option.
        /// </summary>
        public bool GiveAnswer(int chosenAnswerIndex) {
            if (IsInvalidIndex(chosenAnswerIndex) || !OffersChoices()) {
                return false;
            }
            var port = GetOutputPort("choices " + chosenAnswerIndex);
            TriggerSuccessors(port);
            return port.IsConnected;
        }

        /// <summary> Triggers the (only) successor node in case no choices were given </summary>
        public void TriggerNext() {
            if (OffersChoices()) {
                return;
            }
            var port = GetOutputPort("output");
            TriggerSuccessors(port);
        }

        public bool HasSuccessors() {
            return Outputs.Any(x => x.IsConnected);
        }

        private bool IsInvalidIndex(int chosenAnswerIndex) {
            return !IsValidIndex(chosenAnswerIndex);
        }

        private bool IsValidIndex(int chosenAnswerIndex) {
            return choices.Count >= chosenAnswerIndex && chosenAnswerIndex >= 0;
        }
        
        public bool OffersChoices() {
            return choices.Count > 0;
        }
        
        public override void Trigger() {
            (graph as DialogueGraph).current = this;
        }
        
        [System.Serializable] public class Choice {
            public string text;
            public AudioClip voiceClip;
        }
    }
}
