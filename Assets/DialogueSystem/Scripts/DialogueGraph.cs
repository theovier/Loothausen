using System.Linq;
using UnityEngine;
using XNode;

namespace Dialogue {
    [CreateAssetMenu(menuName = "Dialogue/Graph", order = 0)]
    
    public class DialogueGraph : NodeGraph {
        [HideInInspector] public Chat current;

        public void Restart() {
            //Find the first DialogueNode without any inputs. This is the starting node.
            current = nodes.Find(x => x is Chat && x.Inputs.All(y => !y.IsConnected)) as Chat;
        }
        
        public Chat GiveAnswerToCurrentQuestion(int answerIndex) {
            current.GiveAnswer(answerIndex);
            return current;
        }
    }
}


