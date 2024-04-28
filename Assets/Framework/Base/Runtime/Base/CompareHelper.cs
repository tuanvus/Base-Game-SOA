using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeCompare
{
    public const string BIGGER = "Bigger";
    public const string SMALLER = "Smaller";
    public const string EQUAL = "Equal";
    public const string DIFFERENT = "Different";
}

[System.Serializable]
public class CompareValue
{
    public string typeCompare;
    public float valueCompare;
}
[System.Serializable]
public class CompareDirectionInfo
{
    public CompareValue x;
    public CompareValue y;
    public CompareValue z;
}

public class CompareHelper
{
    public static bool CompareDirection(Vector3 direction,CompareDirectionInfo info)
    {
        bool result = true;
        if (info.x != null)
        {
            result = result && Compare(info.x, direction.x);
        }
        if (info.y != null)
        {
            result = result && Compare(info.y, direction.y);
        }
        if (info.z != null)
        {
            result = result && Compare(info.z, direction.z);
        }
        return result;
    }

    public static bool Compare(CompareValue compareValue, float value)
    {
        if (compareValue.typeCompare == null) return true;
        switch (compareValue.typeCompare)
        {
            case TypeCompare.BIGGER:
                if (value <= compareValue.valueCompare) return false;
                return true;
            case TypeCompare.SMALLER:
                if (value >= compareValue.valueCompare) return false;
                return true;
            case TypeCompare.EQUAL:
                if (value != compareValue.valueCompare) return false;
                return true;
            case TypeCompare.DIFFERENT:
                if (value == compareValue.valueCompare) return false;
                return true;
        }
        return true;
    }
}
