using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerAbilities abilities;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CastSpellEvent()
    {
        abilities.UseSpell();
    }
}
