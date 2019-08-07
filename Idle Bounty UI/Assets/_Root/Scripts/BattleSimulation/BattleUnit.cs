using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    public Vector3 StartingPosition;
    public int StartingHealth;
    public int Damage;
    public float AttackDelay;
    public int StartingUnitsInSquad;

    float mCurrentHealth;
    int mCurrentSquadSize;
    float mCurrentAttackCD;
    bool mCanFight = false;

    BattleUnit mTarget;
    UnitView mUI;

    public void InitializeUnit(BattleUnit Target)
    {
        mCurrentHealth = StartingHealth;
        mCurrentSquadSize = StartingUnitsInSquad;
        mCurrentAttackCD = 0;
        mTarget = Target;
        transform.position = StartingPosition;

        mUI = GetComponent<UnitView>();
        mUI.Setup(mCurrentSquadSize);
        StartFight();
    }

    private void Update()
    {
        if (mCanFight)
        {
            if (mCurrentAttackCD >= AttackDelay )
            {
                Strike();
            }
            else
            {
                UpdateAttackCD(Time.deltaTime);
            }            
        }
    }

    private void UpdateAttackCD(float deltaTime)
    {
        mCurrentAttackCD += deltaTime;
        mUI.UpdateAttackCD(mCurrentAttackCD / AttackDelay);
    }

    private void StartFight()
    {
        mCanFight = true;
    }

    public void MyTargetDied()
    {
        mCanFight = false;
    }

    void Strike()
    {
        mTarget.GetHit(Damage * mCurrentSquadSize);
        mCurrentAttackCD = 0;
    }

    public void GetHit(int Damage)
    {
        mCurrentHealth -= Damage;
        mUI.DisplayDamage(Damage);

        if (mCurrentHealth <= 0)
        {
            UnitDies(Mathf.Abs(mCurrentHealth));
        }
        else
        {
            mUI.UpdateHealthBar(mCurrentHealth/StartingHealth);
        }
    }

    private void UnitDies(float ExtraDamage)
    {
        mCurrentSquadSize--;

        if (mCurrentSquadSize <= 0)
        {
            Die();
        }
        else
        {
            mUI.UpdateUnitsCounter(mCurrentSquadSize);
            mCurrentHealth = StartingHealth - ExtraDamage;
        }

        //GetHit(Mathf.RoundToInt(ExtraDamage));
    }

    void Die()
    {
        mCanFight = false;
        mTarget.MyTargetDied();
        mUI.DeathSequence();
    }
}
