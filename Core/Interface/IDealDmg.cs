using System;

namespace TBoGV;
internal interface IDealDmg
{
	DateTime LastAttackTime { get; set; }
	int AttackSpeed { get; set; }
	int AttackDmg { get; set; }
	bool ReadyToAttack();
	Projectile Attack();
}