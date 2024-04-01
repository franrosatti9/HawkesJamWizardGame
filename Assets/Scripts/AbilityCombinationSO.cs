using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Combination", fileName = "New Combination")]
public class AbilityCombinationSO : ScriptableObject
{
    public DNAPartTypes[] combination;
    public AbilitySO output;
}
