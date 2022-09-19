using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCategories
{
    public enum SoundCategory
    {
        Ambient, 
        Footsteps, 
        Attack, 
        Skill, 
        Ui, 
        Items
    }

    public enum Ambient
    {
        Firepit,
        ScaryNoises
    }

    public enum FootSteps
    {
        Character,
        Giant,
        Ogre
    }

    public enum Attack
    {
        Character,
        Giant,
        Ogre
    }

    public enum Skill
    {
        FireballCast,
        FireballImpact
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

}
