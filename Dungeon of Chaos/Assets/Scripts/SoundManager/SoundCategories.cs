using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCategories
{
    public enum SoundCategory
    {
        Looping, 
        EnemyAmbients, 
        Attack, 
        Skill, 
        Ui, 
        Items,
        Death,
        TakeDamage
    }

    public enum Looping
    {
        CaveAmbients,
        Checkpoint,
        Torch,
        Teleport,
        Burn,
        CharacterFootsteps
    }


    public enum EnemyAmbients
    {
        Chieftain,
        DarkElemental,
        Demon,
        ElectricElemental,
        Giant,
        Imp,
        LichKing,
        OrcFighter,
        Poltergeist,
        Shaman,
        SkeletonFighter,
        SkeletonWarrior,
        StoneElemental,
        Wraith
    }

    public enum Attack
    {
        CharacterSwordImpact,
        CharacterSwordSwing,
        OrcChieftainImpact,
        OrchChieftainSmashAttack,
        OrcChieftainSwingAttack,
        DarkElementalChargedAttack,
        DarkElementalProjectile,
        DarkElementalProjectileImpact,
        DemonFireball,
        DemonFireballImpact,
        DemonFlamingSwordAttack,
        DemonStompAttack,
        DemonFlamingSwordImpact,
        ElectricElementalProjectile,
        ElectricElementalProjectileImpact,
        ElectricElementalSlashAttack,
        ElectricElementalSlashAttackImpact,
        GiantSmashAttack,
        GiantStompAttack,
        GiantSwingAttack,
        GiantSwingImpact,
        ImpAttack,
        ImpAttackImpact,
        LichKingHealAttack,
        LichKingProjectile,
        LichKingProjectileImpact,
        LichKingSummonAttack,
        OrcFighterClubHit,
        OrcFighterClubSwing,
        PoltergeistAttack,
        PoltergeistSlashImpact,
        ShamanProjectile,
        ShamanProjectileImpact,
        ShamanStaffImpact,
        ShamanStaffSwing,
        SkeletonFighterImpact,
        SkeletonFighterSwing,
        SkeletonWarriorAttackImpact,
        SkeletonWarriorSmashAttack,
        SkeletonWarriorThrustAttack,
        StoneElementalAttack,
        StoneElementalSmashImpact,
        WraithAttackImpact,
        WraithSwingAttack,
        WraithThrustAttack
    }

    //ToDo: Connect with VFX 
    public enum Skill
    {
        ApplyBurn,
        ArmourUp,
        AttackUp,
        BasicProjectile,
        Dash,
        FastAttack,
        Fireball,
        FireballExplosion,
        FireballImpact,
        FlamingSwodApplyEffect,
        Healing,
        LightningTouch,
        Regeneration,
        Resurrection,
        Shockwave,
        SpellCast,
        SpellPowerBuff,
        StrongAttack,
        ThrowingDaggers,
        ThrowingDaggersFlying,
        ThrustAttack,
        Whirlwind,
        DashImpact
    }

    public enum Ui
    {
        ButtonClick,
        ButtonHover,
        RequirementsNotMet
    }

    public enum Items
    {
        BookPickup,
        ChestOpen,
        CrateDestroy,
        EssencePickup,
        MapFragmentPickup,
        SpikeTrapDown,
        SpikeTrapUp,
        TeleportUse,
        TorchFlameOn
    }


    public enum Death
    {
        Character,
        Chieftain,
        DarkElemental,
        Demon,
        ElectricElemental,
        Giant,
        Imp,
        LichKing,
        OrcFighter,
        Poltergeist,
        Shaman,
        SkeletonFighter,
        SkeletonWarrior,
        StoneElemental,
        Wraith
    }

    public enum TakeDamage
    {
        Character,
        Chieftain,
        DarkElemental,
        Demon,
        ElectricElemental,
        Giant,
        Imp,
        LichKing,
        OrcFighter,
        Poltergeist,
        Shaman,
        SkeletonFighter,
        SkeletonWarrior,
        StoneElemental,
        Wraith
    }

}
