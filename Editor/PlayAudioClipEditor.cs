using BrightLib.Animation.Runtime;
using UnityEditor;

namespace BrightLib.Animation.Editor
{
    [CustomEditor(typeof(PlayAudioClip))]
    [CanEditMultipleObjects]
    public class PlayAudioClipEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var tObject = (target as PlayAudioClip);

            EditorGUI.BeginChangeCheck();
            tObject.useMultiple = EditorGUILayout.Toggle("Use Multiple", tObject.useMultiple);
            if (!tObject.useMultiple)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("clip"));
            }
            else
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("clips"), true);
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
                else if (tObject.delayer.delayType == DelayType.Frame)
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

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(tObject);
            }

        }
    }
}