using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbstractDungeonGen), true)]
public class AbstractDungeonGenEditor : Editor
{
   private AbstractDungeonGen generator;

   private void Awake()
   {
      generator = (AbstractDungeonGen)target;
   }

   public override void OnInspectorGUI()
   {
      base.OnInspectorGUI();
      if (GUILayout.Button("Create Dungeon0"))
      {
         generator.GenerateDungeon(ePlayerLocation.Dungeon0);
      }
   }
}
