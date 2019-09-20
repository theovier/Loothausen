using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DialogueEditor {
    
    [CustomNodeEditor(typeof(Reward))]
    public class RewardEditor : NodeEditor {
        public override void OnBodyGUI() {
            serializedObject.Update();
            
            GUILayout.BeginHorizontal();
            NodeEditorGUILayout.PortField(GUIContent.none, target.GetInputPort("input"), GUILayout.MinWidth(0));
            NodeEditorGUILayout.PortField(GUIContent.none, target.GetOutputPort("output"), GUILayout.MinWidth(0));
            GUILayout.EndHorizontal();
            
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("items"));

            serializedObject.ApplyModifiedProperties();
        }

        public override int GetWidth() {
            return 336;
        }
        
        public override Color GetTint() {
            return new Color(1,0.65f,0);
        }
    }
}
