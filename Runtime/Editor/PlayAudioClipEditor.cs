using UnityEditor;
using UnityEngine;

namespace BrightLib.Animation
{
    [CustomEditor(typeof(PlayAudioClip))]
    [CanEditMultipleObjects]
    public class PlayAudioClipEditor : UnityEditor.Editor
    {
        private SerializedProperty _clip;
        private SerializedProperty _clips;

        private void OnEnable()
        {
            if (serializedObject == null) return;
            _clip = serializedObject.FindProperty("clip");
            _clips = serializedObject.FindProperty("clips");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var tObject = (target as PlayAudioClip);

            tObject.useMultiple = EditorGUILayout.Toggle("Use Multiple", tObject.useMultiple);
            if (!tObject.useMultiple)
            {
                EditorGUILayout.PropertyField(_clip, true);
            }
            else
            {
                EditorGUILayout.PropertyField(_clips, true);
            }
            
            tObject.condition = (PlayCondition)EditorGUILayout.EnumPopup("Condition", tObject.condition);
            if (tObject.condition == PlayCondition.OnEnter)
            {
                EditorGUI.indentLevel++;
                tObject.delay = EditorGUILayout.FloatField("Delay", tObject.delay);
                EditorGUI.indentLevel--;
            }
            else if (tObject.condition == PlayCondition.OnUpdate)
            {
                EditorGUI.indentLevel++;
                tObject.frequency = EditorGUILayout.FloatField("Frequency", tObject.frequency);
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}