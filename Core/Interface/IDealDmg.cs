using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBoGV.Core.Interface
{
	internal interface IDealDmg
	{
		int AttackSpeed { get; set; }
		int AttackDmg { get; set; }
		bool ReadyToAttack();
		Projectile Attack();
	}
}
