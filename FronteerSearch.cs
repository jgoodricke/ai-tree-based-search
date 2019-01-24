using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
	abstract class TentativeSearch: Search
	{
		protected abstract void remove();
		
		protected abstract bool HaveSearched(int posX, int posY);

		public TentativeSearch(Map m): base(m)
		{ }
	}
}
