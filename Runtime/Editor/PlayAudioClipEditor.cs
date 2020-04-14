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
                tObject.delayer.delayType = (DelayType)EditorGUILayout.EnumPopup("Delay Type", tObject.delayer.delayType);
                if (tObject.delayer.delayType == DelayType.Time)
                {
                    tObject.delayer.timer.time = EditorGUILayout.FloatField("Time", tObject.delayer.timer.time);
                }
                else
                {
                    tObject.delayer.frameTimer.frame = EditorGUILayout.IntField("Frame", tObject.delayer.frameTimer.frame);
                }
                EditorGUI.indentLevel--;
            }
            else if (tObject.condition == PlayCondition.OnUpdate)
            {
                EditorGUI.indentLevel++;
                tObject.frequencyTimer.time = EditorGUILayout.FloatField("Frequency", tObject.frequencyTimer.time);
                EditorGUI.indentLevel--;
            }

            if(EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(tObject);
            }
            
        }
    }
}