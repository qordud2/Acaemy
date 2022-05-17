using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// CustomEditorTest의 inspect를 편집이 가능
[CustomEditor(typeof(CustomEditorTest))]
public class CustomEditorTestBase : Editor
{
    CustomEditorTest _test;
    SerializedProperty _number;
    private void OnEnable()
    {
        _test = target as CustomEditorTest;
        _number = serializedObject.FindProperty("Number");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // _test.Number = 을 넣으면 수정이 가능, 안넣으면 수정이 불가능
        //_test.Number = EditorGUILayout.IntField("Number", _test.Number);

        //EditorGUILayout.PropertyField(_number);
        //// ApplyModifiedProperties를 호출해줘야 수정이 가능하다
        //if (serializedObject.ApplyModifiedProperties())
        //{
        //    Debug.Log("Change");
        //}

        // 버튼을 한 가로에 넣기위한 코드
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Create"))
        {
            _test.CreateMap();
        }
        if (GUILayout.Button("Clear"))
        {
            _test.AllClear();
        }
        GUILayout.EndHorizontal();
    }
}
