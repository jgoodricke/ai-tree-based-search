using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
	class BreadthFirst : UninformedSearch
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
					enqueue(new SearchNode(posX, posY - 1, Direction.up, GetTop()));
				}

				//Search Right
				if (myMap.checkPos(posX + 1, posY) != PosType.wall &&
					!HaveSearched(posX + 1, posY))
				{
					enqueue(new SearchNode(posX + 1, posY, Direction.right, GetTop()));
				}

				//Search Down
				if (myMap.checkPos(posX, posY + 1) != PosType.wall &&
					!HaveSearched(posX, posY + 1))
				{
					enqueue(new SearchNode(posX, posY + 1, Direction.down, GetTop()));
				}

				//Search Left
				if (myMap.checkPos(posX - 1, posY) != PosType.wall &&
					!HaveSearched(posX - 1, posY))
				{
					enqueue(new SearchNode(posX - 1, posY, Direction.left, GetTop()));
				}

				//Dequeue top node
				remove();

				//Get position of new top node
				posX = GetTop().PosX;
				posY = GetTop().PosY;
			}
		}

		public BreadthFirst(Map m) : base(m)
		{ }
	}
}
