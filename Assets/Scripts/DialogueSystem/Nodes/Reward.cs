using System.Collections.Generic;
using UnityEngine;

namespace Dialogue {
    public class Reward : DialogueNode {

        public List<Item> items;
        
        public override void Trigger() {
            var inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
            foreach(var item in items)  {
                inventory.AddItem(Instantiate(item));
            }
            TriggerSuccessor();
        }

        private void TriggerSuccessor() {
            var port = GetOutputPort("output");
            TriggerSuccessors(port);
        }
    }
}

