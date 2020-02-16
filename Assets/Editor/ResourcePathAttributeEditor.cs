using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ResourcePathAttribute))]
public class ResourcePathAttributeEditor : PropertyDrawer
{

    string path = string.Empty;


    ResourcePathAttribute attrib;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        path = DropAreaGUI(position, "Resource Path");
        if (!string.IsNullOrEmpty(path))
            property.stringValue = path;

        attrib = (ResourcePathAttribute)this.attribute;

        if(attrib.checkType)
            label.tooltip = "Drag and drop resource folder files of type "+attrib.type;
        else 
            label.tooltip = "Drag and drop resource folder files";

        EditorGUI.PropertyField(position, property, label);



    }

    public string DropAreaGUI(Rect drop_area, string title)
    {
        Event evt = Event.current;
        // Rect drop_area = GUILayoutUtility.GetRect(0.0f, 50.0f);//, GUILayout.ExpandWidth(false));
        //GUI.Box(drop_area, title);

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!drop_area.Contains(evt.mousePosition))
                    return null;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                
                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    //ResourcePathAttribute attrib = (ResourcePathAttribute)this.attribute;
                    foreach (Object dragged_object in DragAndDrop.objectReferences)
                    {
                        // Do On Drag Stuff here
                        string resourcesPath = ConvertToResourcesPath(AssetDatabase.GetAssetPath(dragged_object), true);
                        if (string.IsNullOrEmpty(resourcesPath))
                            Debug.LogError("Not valid resource file/prefab!");
                        else if (attrib.checkType)
                        {
                            //attrib.type
                            if(dragged_object.GetType() != attrib.type)
                            {
                                Debug.LogError("Invalide Type "+ dragged_object.GetType()+" - Expected type = " + attrib.type);
                                resourcesPath = string.Empty;
                            }
                            

                        }
                        return resourcesPath;
                    }
                }
                break;

        }
        return null;
    }

    private const string RESOURCES_FOLDER_NAME = "/Resources/";
    private const string RESOURCES_FOLDER_NAME_LOWER = "/resources/";
    private const string ASSETS_FOLDER_NAME = "Assets/";
    public static string ConvertToResourcesPath(string projectPath, bool extentionremove)
    {

        if (string.IsNullOrEmpty(projectPath))
        {
            return string.Empty;
        }

        int folderIndex = projectPath.IndexOf(RESOURCES_FOLDER_NAME);
        if (folderIndex == -1) folderIndex = projectPath.IndexOf(RESOURCES_FOLDER_NAME_LOWER);
        if (folderIndex == -1)
        {
            return string.Empty;
        }

        folderIndex += RESOURCES_FOLDER_NAME.Length;


        int length = projectPath.Length - folderIndex;


        string resourcesPath = projectPath.Substring(folderIndex, length);

        if (extentionremove)
        {

            int dotIndex = resourcesPath.IndexOf(System.IO.Path.GetExtension(resourcesPath));
            if (dotIndex > 0)
            {
                int count = resourcesPath.Length - dotIndex;
                resourcesPath = resourcesPath.Remove(dotIndex, count);
            }
            else return null;
        }

        return resourcesPath;
    }

}
