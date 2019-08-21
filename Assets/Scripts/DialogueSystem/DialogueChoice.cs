using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;

public class DialogueChoice {
    public Chat.Choice choice;
    public int index;

    public DialogueChoice(Chat.Choice choice, int index) {
        this.choice = choice;
        this.index = index;
    }
}
