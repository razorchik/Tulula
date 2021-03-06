﻿using UnityEngine;
using UnityEditor;

using System.Xml;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

public class CreateHud 
{
    [MenuItem("Editor/Load Hud")]
    static void Load()
    {
        string[] filters = { "XML Files", "xml" };
        string path = EditorUtility.OpenFilePanelWithFilters("Open XML File", Global.kSourcesPath, filters);

        XmlDocument doc = new XmlDocument();
        doc.Load(path);

        XmlNode root = doc.DocumentElement.SelectSingleNode("/hud");

        string images = Global.kAssetsPath + root.Attributes["images"].InnerText;

        GameObject hud = new GameObject();

        hud.name = "hud";
        hud.AddComponent<HudController>();

        GameObject[] objects = LoadScene.loadObjectsFromXML(root.SelectSingleNode("objects"), images);

        int z = objects.Length;

        foreach (var obj in objects)
        {
            var position = obj.transform.position;

            obj.transform.parent = hud.transform;

            position.z = z--;
            obj.transform.position = position;
        }

        hud.transform.position = new Vector3(0, 0, (float)Order.Hud);
    }
}



