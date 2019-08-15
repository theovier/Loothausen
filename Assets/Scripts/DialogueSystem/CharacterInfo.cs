using UnityEngine;

namespace Dialogue {
    [CreateAssetMenu(menuName = "Dialogue/CharacterInfo")]
    
    public class CharacterInfo : ScriptableObject {
        public Color color;

        public static CharacterInfo PlayerCharacterInfo() {
            return Resources.Load<CharacterInfo>("DialogueSystem/Characters/Player");
        }
    }
}