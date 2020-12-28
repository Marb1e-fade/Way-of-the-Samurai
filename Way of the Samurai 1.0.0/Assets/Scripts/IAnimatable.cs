using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IAnimatable
{
    void AnimateIdling();
    void AnimateWalking(sightDirection direction);
    void AnimateRunning(sightDirection direction);
    void AnimateJumping(sightDirection direction);
    void AnimateJumpingInAir(sightDirection direction);
    void AnimateJumpingLanding(sightDirection direction);
    void AnimateAttackingMelee(sightDirection direction);
    void AnimateAttackingRanged(sightDirection direction);
    void AnimateRecievingDamage(sightDirection direction);
    void AnimateDodgingEnemyAttack(sightDirection direction);
    IEnumerator AnimateDeath();
}
