using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCategories
{
    public enum SoundCategory
    {
        Looping, 
        Footsteps, 
        Attack, 
        Skill, 
        Ui, 
        Items,
        Death,
        TakeDamage
    }

    public enum Looping
    {
        Firepit,
        ScaryNoises
    }

    public enum FootSteps
    {
        Character,
        Giant,
        Orc
    }

    public enum Attack
    {
        CharacterSwing,
        CharacterSwingImpact,
        GiantSwing,
        GiantSwingImpact,
        GiantSwingIndicator,
        OrcSwing,
        OrcSwingImpact
    }

    public enum Skill
    {
        SpellCast,
        FireballFlight,
        FireballImpact,
        Healing,
        Dash,
        DamagingDash,
        DashImpact
    }

    public enum Ui
    {
        ButtonClick,
        ButtonHover
    }

    public enum Items
    {
        Pickup,
        Drop
    }

    public enum Death
    {
        Character,
        Giant,
        Orc
    }

    public enum TakeDamage
    { 
        Character,
        Giant,
        Orc
    }

}
