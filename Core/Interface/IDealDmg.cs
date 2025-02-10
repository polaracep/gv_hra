using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBoGV.Core.Interface
{
	internal interface IDealDmg
	{
        DateTime LastAttackTime { get; set; }
		int AttackSpeed { get; set; }
		int AttackDmg { get; set; }
		bool ReadyToAttack();
		Projectile Attack();
	}
}
