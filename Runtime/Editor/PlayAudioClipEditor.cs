using UnityEditor;

namespace BrightLib.Animation.Runtime
{
    [CustomEditor(typeof(PlayAudioClip))]
    [CanEditMultipleObjects]
    public class PlayAudioClipEditor : UnityEditor.Editor
    {
        private SerializedProperty _clip;
        private SerializedProperty _clips;

        private void OnEnable()
        {
            _clip = serializedObject.FindProperty("clip");
            _clips = serializedObject.FindProperty("clips");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var tObject = (target as PlayAudioClip);

            EditorGUI.BeginChangeCheck();
            tObject.useMultiple = EditorGUILayout.Toggle("Use Multiple", tObject.useMultiple);
            if (!tObject.useMultiple)
            {
                EditorGUILayout.PropertyField(_clip);
            }
            else
            {
                EditorGUILayout.PropertyField(_clips, true);
            }
            
            tObject.condition = (PlayCondition)EditorGUILayout.EnumPopup("Condition", tObject.condition);
            if (tObject.condition == PlayCondition.OnEnter)
            {
                EditorGUI.indentLevel++;
                tObject.delay = EditorGUILayout.FloatField("[OLD]delay", tObject.delay);
                //tObject.delayType = (DelayType)EditorGUILayout.EnumPopup("Delay Type", tObject.delayType);
                //if (tObject.delayType == DelayType.Time)
                //{
                //    tObject.delayTimer.time = EditorGUILayout.FloatField("Time", tObject.delayTimer.time);
                //}
                //else
                //{
                //    tObject.delayFrameTimer.frame = EditorGUILayout.IntField("Frame", tObject.delayFrameTimer.frame);
                //}
                EditorGUI.indentLevel--;
            }
            else if (tObject.condition == PlayCondition.OnUpdate)
            {
                EditorGUI.indentLevel++;
                tObject.frequency = EditorGUILayout.FloatField("Frequency", tObject.frequency);
                EditorGUI.indentLevel--;
            }

            if(EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
            
        }
    }
}