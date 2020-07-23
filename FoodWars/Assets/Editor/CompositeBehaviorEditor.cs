//this was supposed to be a scirpt used to edit everything, but its currently a mess -- just ignore it, I might revisit it later



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(CompositeBehavior))]

//public class CompositeBehaviorEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        //setup
//        CompositeBehavior cb = (CompositeBehavior)target; // what the inspector is looking at is the 'target' object

//        Rect r = EditorGUILayout.BeginHorizontal();
//        r.height = EditorGUIUtility.singleLineHeight;

//        //check for behaviors
//        if(cb.behaviors == null || cb.behaviors.Length == 0)
//        {
//            EditorGUILayout.HelpBox("No behaviors in array.", MessageType.Warning);
//            EditorGUILayout.EndHorizontal();
//            r = EditorGUILayout.BeginHorizontal();
//            r.height = EditorGUIUtility.singleLineHeight;
//        }
//        else
//        {
            
//            r.x = 30f;
//            r.width = EditorGUIUtility.currentViewWidth - 95f;
//            EditorGUI.LabelField(r, "Behaviors");
//            r.x = EditorGUIUtility.currentViewWidth - 65f;
//            r.width = 60f;
//            EditorGUI.LabelField(r, "Weights");
//            r.y += EditorGUIUtility.singleLineHeight * 1.2f;

//        }





//    }

//}
