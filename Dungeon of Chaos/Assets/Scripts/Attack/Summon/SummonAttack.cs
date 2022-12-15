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
        IIndicator indicator = CreateIndicator(transform);
        if (indicator)
        {
            indicator.transform.localPosition = Vector3.zero;
            indicator.Use();
            yield return null;
        }

        //SoundManager.instance.PlaySound(swingSFX);

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
