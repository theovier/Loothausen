using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEngine;
using CharacterInfo = Dialogue.CharacterInfo;

public class ChoiceChat : Chat {
  // Dynamically generated (intermediate) chat node that is being placed between a selected choice and its destination.

  private Chat origin;
  private int selectedChoiceIndex;

  public static ChoiceChat CreateInstance(Chat origin, int selectedChoiceIndex) {
    var obj = CreateInstance<ChoiceChat>();
    obj.Init(origin, selectedChoiceIndex);
    return obj;
  }
  
  public void Init(Chat origin, int selectedChoiceIndex) {
    this.origin = origin;
    this.selectedChoiceIndex = selectedChoiceIndex;
    this.name = "Intermediate (Choice) Chat";
    this.character = CharacterInfo.PlayerCharacterInfo();
    this.text = origin.choices[selectedChoiceIndex].text;
    this.graph = origin.graph;
    this.choices = new List<Choice>();
    CreateConnectionToDestination();
  }
  
 private void CreateConnectionToDestination() {
    var originOutputPort = origin.GetChoiceOutputPort(selectedChoiceIndex);
    if (originOutputPort.IsConnected) {
      var destinationInputPort = originOutputPort.GetConnection(0);
      var defaultOutputPort = GetDefaultOutputPort();
      defaultOutputPort.Connect(destinationInputPort);
    }
  }

  public override bool OffersChoices() {
    return false;
  }
}
