using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFightable
{
    void AttackMelee(float damage);
    void AttackRanged(float range, float damage);
    void RecieveDamage(float damage);
    void DodgeEnemyAttack();
    void Die();
}
