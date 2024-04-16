using UnityEngine;

// 온전히 이해하고 쓴 코드는 아님

/// <summary>
/// Read Only attribute.
/// Attribute is use only to mark ReadOnly properties.
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute { }

// ReadOnlyDrawer 정의
#if UNITY_EDITOR
namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false; // 편집 비활성화
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true; // 편집 활성화
        }
    }
}
#endif