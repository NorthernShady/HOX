using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(BaseTypedMap), true)]
public class TypedMapDrawer : PropertyDrawer {
	bool hidden = true;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginProperty(position, label, property);

		var foldOutPosition = new Rect(position.x, position.y, position.width, GetLabelHeight(property, label));
		hidden = !EditorGUI.Foldout(foldOutPosition, !hidden, label, true);

		if (hidden) {
			EditorGUI.EndProperty();
			return;
		}

		position.y += foldOutPosition.height;
		position.x += 20;
					
		var enumType = GetEnumType(property);

		var map = RenegerateMap(property, enumType);
		foreach (int key in System.Enum.GetValues(enumType)) {
			EditorGUI.PropertyField(position, map[key], new GUIContent(System.Enum.GetName(enumType, key)), true);
			position.y += EditorGUI.GetPropertyHeight(map[key]);
		}

		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		if (hidden) {
			return GetLabelHeight(property, label);
		}

		var enumType = GetEnumType(property);
		var map = RenegerateMap(property, enumType);

		var result = GetLabelHeight(property, label);
		foreach (var pair in map)
			result += EditorGUI.GetPropertyHeight(pair.Value, new GUIContent(""));
	
		return result;
	}

	Dictionary<int, SerializedProperty> RenegerateMap(SerializedProperty property, System.Type enumType) {
		var values = property.FindPropertyRelative("values");
		var keys = property.FindPropertyRelative("keys");

		var availableKeys = new HashSet<int>();
		
		for (int i = keys.arraySize - 1; i >= 0; --i) {
			var key = keys.GetArrayElementAtIndex(i).intValue;

			if (!System.Enum.IsDefined(enumType, key) || availableKeys.Contains(key)) {
				keys.DeleteArrayElementAtIndex(i);
				values.DeleteArrayElementAtIndex(i);
				continue;
			}

			availableKeys.Add(key);
		}

		var enumValues = System.Enum.GetValues(enumType);

		int lastIndex = keys.arraySize;

		for (int i = 0, count = enumValues.Length; i < count; ++i) {
			var key = (int)enumValues.GetValue(i);

			if (availableKeys.Contains(key))
				continue;

			keys.InsertArrayElementAtIndex(lastIndex);
			values.InsertArrayElementAtIndex(lastIndex);

			keys.GetArrayElementAtIndex(lastIndex).intValue = key;
			++lastIndex;
		}

		var result = new Dictionary<int, SerializedProperty>();
		for (int i = 0, count = keys.arraySize; i < count; ++i)
			result.Add(keys.GetArrayElementAtIndex(i).intValue, values.GetArrayElementAtIndex(i));

		return result;
	}

	System.Type GetEnumType(SerializedProperty property) {
		var type = fieldInfo.FieldType;

		while (true) {
			if (type.Name == "TypedMap`2")
				break;
		
			type = type.BaseType;
		}

		return type.GetGenericArguments()[0];
	}

	float GetLabelHeight(SerializedProperty property, GUIContent label) {
		return EditorGUI.GetPropertyHeight(property, label, false);
	}
}
