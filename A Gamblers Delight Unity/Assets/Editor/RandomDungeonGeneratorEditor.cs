using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeonGenerator), true)]  // True allows child classes of this base class to also see the custom editor.
// This script is a custom editor for level generation from within the inspector.
public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    // On awake, create a new generator variable which finds the AbstractDungeonGenerator object.
    private void Awake()
    {
        // Upcast it so the target of this object matches the type AbstractDungeonGenerator.
        generator = (AbstractDungeonGenerator)target;
    }

    // Create a custom button in the inspector.
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        // Create a custom button in this inspector and run the code when that button is pressed.
        if(GUILayout.Button("Create Dungeon"))
        {
            // Call the GenerateDungeon method from the AbstractDungeonGenerator class.
            generator.GenerateDungeon();
        }
    }
}
