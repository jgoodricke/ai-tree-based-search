using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
	class AStar: InfomredSearch
	{
		public override void DoSearch()
		{
			//Coordinates of current position
			int posX = GetTop().PosX;
			int posY = GetTop().PosY;

			//Keep searching until position is found or you run out of new nodes to search
			while (myMap.checkPos(posX, posY) != PosType.goal
				&& searchingNodes.Any())
			{
				//Search Up
				if (myMap.checkPos(posX, posY - 1) != PosType.wall &&
					!HaveSearched(posX, posY - 1))
				{
					Insert(new SearchNode(posX, posY - 1, Direction.up, GetTop(), getCost(posX, posY - 1)));
				}

				//Search Right
				else if (myMap.checkPos(posX + 1, posY) != PosType.wall &&
					!HaveSearched(posX + 1, posY))
				{
					Insert(new SearchNode(posX + 1, posY, Direction.right, GetTop(), getCost(posX + 1, posY)));
				}

				//Search Down
				else if (myMap.checkPos(posX, posY + 1) != PosType.wall &&
					!HaveSearched(posX, posY + 1))
				{
					Insert(new SearchNode(posX, posY + 1, Direction.down, GetTop(), getCost(posX, posY + 1)));
				}

				//Search Left
				else if (myMap.checkPos(posX - 1, posY) != PosType.wall &&
					!HaveSearched(posX - 1, posY))
				{
					Insert(new SearchNode(posX - 1, posY, Direction.left, GetTop(), getCost(posX - 1, posY)));
				}
				//If child nodes exist, dequeue top node
				else
				{
					remove();
				}

				//Get Top position of new top node
				posX = GetTop().PosX;
				posY = GetTop().PosY;
			}
		}

		//Returns the distance between the current position and the goal
		protected override int getCost(int x, int y)
		{
			//Get x and y distances between current position and goal
			int xCost = x - myMap.Goal.PosX;
			int yCost = y - myMap.Goal.PosY;

			//Get cost of previous node
			int preCost = 0;
			if (searchingNodes.Any())
			{
				preCost = GetTop().Cost;
			}

			//Make negative costs posative
			if (xCost < 0)
			{
				xCost = xCost * -1;
			}
			if (yCost < 0)
			{
				yCost = yCost * -1;
			}

			return xCost + yCost + preCost;
		}

		public AStar(Map m) : base(m)
		{ }
	}
}
