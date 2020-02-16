
using UnityEngine;
using System;

//TODO: rename file to be CustomAttributes
public class ResourcePathAttribute : PropertyAttribute
{
    public readonly Type type;
    public bool checkType = false;

    public ResourcePathAttribute()
    {
        checkType = false;
    }
    public ResourcePathAttribute(Type ptype)
    {
        type = ptype;
        checkType = true;
    }

}

public class ResourceFolderPathAttribute : PropertyAttribute
{


    public ResourceFolderPathAttribute()
    {

    }

}

