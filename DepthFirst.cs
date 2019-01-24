using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    class DepthFirst : UninformedSearch
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
					push(new SearchNode(posX, posY - 1, Direction.up, GetTop()));
				}

				//Search Right
				else if (myMap.checkPos(posX + 1, posY) != PosType.wall &&
					!HaveSearched(posX + 1, posY))
				{
					push(new SearchNode(posX + 1, posY, Direction.right, GetTop()));
				}

				//Search Down
				else if (myMap.checkPos(posX, posY + 1) != PosType.wall &&
					!HaveSearched(posX, posY + 1))
				{
					push(new SearchNode(posX, posY + 1, Direction.down, GetTop()));
				}

				//Search Left
				else if (myMap.checkPos(posX - 1, posY) != PosType.wall &&
					!HaveSearched(posX - 1, posY))
				{
					push(new SearchNode(posX - 1, posY, Direction.left, GetTop()));
				}
				
				//If the current node has no child nodes, dequeue top node
				else
				{
					remove();
				}

				//Get position of new top node
				posX = GetTop().PosX;
				posY = GetTop().PosY;
			}
		}

		public DepthFirst(Map m) : base(m)
		{ }
	}
}
