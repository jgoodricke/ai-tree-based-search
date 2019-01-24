using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    class SearchNode
    {
        private int posX;					//x position on map
        private int posY;					//y position on map
		private Direction dir;				//Direction moved to get to this position
		private SearchNode previousNode;	//Previous visited position
		private int cost;					//Current Path cost

		public int Cost
		{
			get { return cost; }
		}

		public int PosX
        {
            get { return posX; }
        }

        public int PosY
        {
            get { return posY; }
        }

        public Direction Dir
        {
            get { return dir; }
        }
		public SearchNode PreviousNode
		{
			get { return previousNode; }
		}

		//Constructor for uninformed searches
		public SearchNode(int x, int y, Direction d, SearchNode p)
        {
            posX = x;
            posY = y;
            dir = d;
			previousNode = p;
			cost = 0;
        }

		//Constructor for informed searches
		public SearchNode(int x, int y, Direction d, SearchNode p, int c)
		{
			posX = x;
			posY = y;
			dir = d;
			previousNode = p;
			cost = c;
		}

	}
}
