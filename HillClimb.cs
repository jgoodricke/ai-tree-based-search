using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
	class HillClimb: Search
	{
		//Used to manage random restart
		Random rnd = new Random();
		int restartCount = 0;

		//Used to hold current node and start of plataue
		private SearchNode currentNode;

		//Used to manage plataue searching
		private SearchNode PlateauStart;
		private int plateauLoop;
		private bool onPlateau;

		//Gets top node
		protected override SearchNode GetTop()
		{
			return currentNode;
		}

		//Restart search from a random position
		private void Restart()
		{
			restartCount++;

			currentNode = myMap.Start;

			int maxX; 
			int minX; 
			int maxY;
			int minY;

			int moveX;
			int moveY;				  

			//Move a further position with each restart
			for (int i = 0; i < (restartCount); i++)
			{
				maxX = myMap.SizeX - currentNode.PosX;
				minX = 0 - currentNode.PosX;
				maxY = myMap.SizeY - currentNode.PosY;
				minY = 0 - currentNode.PosY;

				moveX = rnd.Next(minX, maxX);
				moveY = rnd.Next(minY, maxY);

				//Move left
				if (moveX > 0)
				{
					for (int j = 0; j <= moveX; j++)
					{
						int posX = GetTop().PosX;
						int posY = GetTop().PosY;

						if (myMap.checkPos(posX + 1, posY) != PosType.wall)
						{
							MoveNode(posX + 1, posY, Direction.left);
						}
						else
						{
							break;
						}
					}
				}
				//Move right
				else if (moveX < 0)
				{
					int posX = GetTop().PosX;
					int posY = GetTop().PosY;

					if (myMap.checkPos(posX - 1, posY) != PosType.wall)
					{
						MoveNode(posX - 1, posY, Direction.right);
					}
					else
					{
						break;
					}
				}

				//Move up
				if (moveY > 0)
				{
					for (int j = 0; j <= moveX; j++)
					{
						int posX = GetTop().PosX;
						int posY = GetTop().PosY;

						if (myMap.checkPos(posX, posY + 1) != PosType.wall)
						{
							MoveNode(posX, posY + 1, Direction.up);
						}
						else
						{
							break;
						}
					}
				}
				//Move Down
				else if (moveY < 0)
				{
					int posX = GetTop().PosX;
					int posY = GetTop().PosY;

					if (myMap.checkPos(posX, posY - 1) != PosType.wall)
					{
						MoveNode(posX, posY - 1, Direction.down);
					}
					else
					{
						break;
					}
				}
			}
		}

		//Used to get the cost of the current node
		private int GetCost(int x, int y)
		{
			//Get x and y distances between current position and goal
			int xCost = x - myMap.Goal.PosX;
			int yCost = y - myMap.Goal.PosY;

			//Make negative costs posative
			if (xCost < 0)
			{
				xCost = xCost * -1;
			}
			if (yCost < 0)
			{
				yCost = yCost * -1;
			}

			return xCost + yCost;
		}

		//Moves node to next position
		private void MoveNode(int x, int y, Direction dir)
		{
			currentNode = new SearchNode(x, y, dir, GetTop(), GetCost(x, y));
			NumNodes++;
		}

		//Checks if stuck on plataue
		private bool IsStuck()
		{
			if (plateauLoop == -1)
			{
				PlateauStart = GetTop();
			}
			else if (PlateauStart == GetTop())
			{
				plateauLoop++;
			}

			if (plateauLoop >= 2)
			{
				return true;
			}
			return false;
		}

		public override void DoSearch()
		{
			int posX = GetTop().PosX;
			int posY = GetTop().PosY;

			//Keep searching until position is found or you run out of search orders
			while ((myMap.checkPos(posX, posY) != PosType.goal))
			{
				onPlateau = true;

				//Search Up
				if (myMap.checkPos(posX, posY - 1) != PosType.wall
					&& GetCost(posX, posY - 1) < GetCost(posX, posY))
				{
					MoveNode(posX, posY - 1, Direction.up);
					onPlateau = false;
				}
				
				//Search Right
				else if (myMap.checkPos(posX + 1, posY) != PosType.wall &&
					GetCost(posX + 1, posY) < GetCost(posX, posY))
				{
					MoveNode(posX + 1, posY, Direction.right);
					onPlateau = false;
				}
				
				//Search Down
				else if (myMap.checkPos(posX, posY + 1) != PosType.wall &&
					GetCost(posX, posY + 1) < GetCost(posX, posY))
				{
					MoveNode(posX, posY + 1, Direction.down);
					onPlateau = false;
				}
				
				//Search Left
				else if (myMap.checkPos(posX - 1, posY) != PosType.wall &&
					GetCost(posX - 1, posY) < GetCost(posX, posY))
				{
					MoveNode(posX - 1, posY, Direction.left);
					onPlateau = false;
				}



				//If stuck on platue, search for exit
				if (onPlateau)
				{
					//Check if looping on plateu
					if (IsStuck())
					{
						Restart();
					}
					//Check directions
					//Check Up
					else if (myMap.checkPos(posX, posY - 1) != PosType.wall
						&& GetCost(posX, posY - 1) == GetCost(posX, posY))
					{
						MoveNode(posX, posY - 1, Direction.up);
					}
					//check right
					else if (myMap.checkPos(posX + 1, posY) != PosType.wall &&
						GetCost(posX + 1, posY) == GetCost(posX, posY))
					{
						MoveNode(posX + 1, posY, Direction.right);
					}
					//check down
					else if (myMap.checkPos(posX - 1, posY) != PosType.wall &&
						GetCost(posX - 1, posY) == GetCost(posX, posY))
					{
						MoveNode(posX - 1, posY, Direction.left);
					}
					//check left
					else if (myMap.checkPos(posX, posY + 1) != PosType.wall &&
						GetCost(posX, posY + 1) == GetCost(posX, posY))
					{
						MoveNode(posX, posY + 1, Direction.down);
					}
					//If there's nowhere to go, mark as unsovlable
					else
					{
						Restart();
					}
				}
				//If not on a plateu, reset loop.
				else
				{
					plateauLoop = -1;
				}

				//Get Top position of new top node
				posX = GetTop().PosX;
				posY = GetTop().PosY;
			}
		}

		public HillClimb(Map m) : base(m)
		{
			currentNode = m.Start;
			plateauLoop = -1;
		}
	}
}
