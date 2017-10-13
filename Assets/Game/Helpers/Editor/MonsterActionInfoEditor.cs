﻿using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonsterActionInfo))]
public class MonsterActionInfoEditor : EditorWithSubEditors<MonsterConditionCollectionEditor, MonsterConditionCollection>
{
    MonsterActionInfo actionManager;

    SerializedProperty collectionsProperty;

    void OnEnable()
    {
        if (target == null)
        {
            DestroyImmediate(this);
            return;
        }

        actionManager = (MonsterActionInfo)target;

        collectionsProperty = serializedObject.FindProperty("conditionCollections");

        CheckAndCreateSubEditors(actionManager.conditionCollections);
    }

    private void OnDisable()
    {
        CleanupEditors();
    }
    
    protected override void SubEditorSetup(MonsterConditionCollectionEditor editor, int index)
    {
        editor.parent = this;
        editor.subEditorIndex = index;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CheckAndCreateSubEditors(actionManager.conditionCollections);
        
        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI();
            EditorGUILayout.Space();
        }

        //EditorGUILayout.PropertyField(collectionsProperty);

        if (GUILayout.Button("Add Move Set", GUILayout.Width(150f)))
        {
            MonsterConditionCollection newCollection = MonsterConditionCollectionEditor.CreateConditionCollection();
            newCollection.name = "Condition Collection";
            newCollection.actionCollection.name = "Action Collection";

            AssetDatabase.AddObjectToAsset(newCollection, target);
            AssetDatabase.AddObjectToAsset(newCollection.actionCollection, target);
            collectionsProperty.AddToObjectArray(newCollection);

            //Debug.Log(collectionsProperty.GetArrayElementAtIndex(collectionsProperty.arraySize - 1).objectReferenceValue);
            
        }

        
        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }

    public void RemoveFromCollection(int index)
    {
        var subasset = actionManager.conditionCollections[index];
        collectionsProperty.RemoveFromObjectArrayAt(index);

        foreach(var condition in subasset.conditions)
        {
            DestroyImmediate(condition, true);
        }

        foreach(var action in subasset.actionCollection.actions)
        {
            DestroyImmediate(action, true);
        }

        DestroyImmediate(subasset.actionCollection, true);
        DestroyImmediate(subasset, true);
    }
}
