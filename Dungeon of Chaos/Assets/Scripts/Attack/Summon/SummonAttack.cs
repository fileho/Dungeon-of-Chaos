using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAttack : IAttack
{

    protected GameObject minion;
    protected int minAmountOfMinions = 2;
    protected int maxAmountOfMinions = 10;
    protected float timeBetweenSpawns = 0.5f;
    protected float spawnRadius = 3f;


    protected override void ApplyConfigurations()
    {
        base.ApplyConfigurations();
        SummonAttackConfiguration _attackConfiguration = attackConfiguration as SummonAttackConfiguration;
        minion = _attackConfiguration.minion;
        minAmountOfMinions = _attackConfiguration.minAmountOfMinions;
        maxAmountOfMinions = _attackConfiguration.maxAmountOfMinions;
        timeBetweenSpawns = _attackConfiguration.timeBetweenSpawns;
        spawnRadius = _attackConfiguration.spawnRadius;
    }


    protected override IEnumerator StartAttackAnimation()
    {
        Vector3 weaponPos = Weapon.transform.position;
        Vector3 targetDirection = (GetTargetPosition() - (Vector2)weaponPos).normalized;

        Vector3 startPos = Weapon.transform.localPosition;
        Vector3 endPos = startPos + (targetDirection);
        endPos = Weapon.transform.lossyScale.x > 0 ? endPos : -Vector3.Reflect(endPos, Vector2.up);
        Vector3 midPos = (endPos + startPos) / 2f;

        IIndicator indicator = CreateIndicator(transform);
        if (indicator)
        {
            indicator.transform.localPosition = Weapon.GetWeaponTipOffset();
            indicator.Use();
            yield return new WaitForSeconds(indicator.Duration);
        }

        // Cache weapon rotation to restore after the animation
        var initialAssetRotation = Weapon.Asset.localRotation;
        Weapon.Asset.localRotation = Quaternion.Euler(0, 0, Weapon.GetUprightAngle());


        float attackAnimationDurationOneWay = AttackAnimationDuration / 2f;

        // Forward
        float time = 0;
        midPos -= new Vector3(0, 0.1f);
        while (time <= 1)
        {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseInCubic(time);
            Weapon.transform.localPosition = Vector3.Slerp(startPos - midPos, endPos - midPos, currentPos) + midPos;
            yield return null;
        }

        // Backward
        time = 0;
        midPos += new Vector3(0, 0.2f);
        while (time <= 1)
        {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutCubic(time);
            Weapon.transform.localPosition = Vector3.Slerp(endPos - midPos, startPos - midPos, currentPos) + midPos;
            yield return null;
        }

        SoundManager.instance.PlaySound(swingSFX);

        // Reset
        Weapon.Asset.localRotation = initialAssetRotation;
        Weapon.transform.localPosition = startPos;

        yield return SpawnMinions();
        isAttacking = false;
    }


    private IEnumerator SpawnMinions()
    {
        int amount = Random.Range(minAmountOfMinions, maxAmountOfMinions);

        for (int i = 0; i < amount; i++)
        {
            Vector2 pos = Random.insideUnitCircle * spawnRadius;
            Instantiate(minion, pos + (Vector2)transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public override string ToString()
    {
        return base.ToString() + "_Ranged";
    }
}
