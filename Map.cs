using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
	abstract class Map
	{
		protected SearchNode start;
		protected SearchNode goal;

		//The height and width of the map
		protected int sizeX;
		protected int sizeY;

		public SearchNode Start
		{
			get { return start; }
		}

		public SearchNode Goal
		{
			get { return goal; }
		}

		public int SizeX
		{
			get { return sizeX; }
		}

		public int SizeY
		{
			get { return sizeY; }
		}

		public abstract PosType checkPos(int x, int y);

		protected abstract void importMap(string fileName);

		public Map(string fileName)
		{
			importMap(fileName);
		}
	}
}
