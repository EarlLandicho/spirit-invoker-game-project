﻿using UnityEngine;

public interface IEnemyAttack
{
    void Attack(GameObject player);

    void OutOfAttackRange();

    void SetLockAttack(bool isLocked);

}