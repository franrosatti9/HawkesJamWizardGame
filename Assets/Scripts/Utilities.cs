using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static bool ArrayAreEqual<T>(T[] array1, T[] array2) where T : Enum
    {
        // Check if arrays are of equal length
        if (array1.Length != array2.Length)
            return false;

        // Iterate through each element and compare
        for (int i = 0; i < array1.Length; i++)
        {
            if (!array1[i].Equals(array2[i]))
                return false;
        }

        // If all elements match, return true
        return true;
    }
}
