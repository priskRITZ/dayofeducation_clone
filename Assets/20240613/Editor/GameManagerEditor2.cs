using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// 어떤 컴포넌트를 수정할지 정함
[CustomEditor(typeof(GameManager))]

// Editor 상속
public class GameManagerEditor2 : Editor
{
    private string AnimationName;
    private float SliderValue;
    
    public override void OnInspectorGUI()
    {
        // 주석 처리후 컴포넌트 인스펙터 확인
        // base.OnInspectorGUI();

        AnimationName = EditorGUILayout.TextField("AnimationName", AnimationName);
        SliderValue = EditorGUILayout.Slider(SliderValue, 0, 1);
        GameManager manager = (GameManager)target;
        Animator animator = manager.GetComponent<Animator>();
        animator.Play(AnimationName, 0, SliderValue);
        animator.Update(Time.deltaTime);
    }
}