using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBoGV.Core.Interface
{
	internal interface IRecieveDmg
	{
		int Hp {  get; set; }
		void RecieveDmg();
	}
}
