using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
	abstract class Search
	{
		protected Map myMap;

		//Number of nodes visited
		protected int NumNodes;

		public abstract void DoSearch();

		protected abstract SearchNode GetTop();

		//Outputs result of search
		public string OutputString()
		{
			bool firstDir = true;
			string result = NumNodes.ToString();

			int x = GetTop().PosX;
			int y = GetTop().PosY;

			//If search successful, output result
			if (myMap.checkPos(x, y) == PosType.goal)
			{
				//Get the goal node from the top of the queue
				SearchNode currentNode = GetTop();
				Direction dir = currentNode.Dir;

				//Stores the list of directions so it can be reveresed
				List<Direction> dirList = new List<Direction>();

				do
				{
					currentNode = currentNode.PreviousNode;
					dirList.Add(dir);
					dir = currentNode.Dir;
				} while (dir != Direction.nill);

				//Reverse List and output to string
				dirList.Reverse();

				foreach (Direction d in dirList)
				{
					if (firstDir)
					{
						result += " " + d.ToString();
						firstDir = false;
					}
					else
					{
						result += "; " + d.ToString();
					}
				}
				result += ";";
			}
			//If search unsuccessful, output error
			else
			{
				result = "Search unsuccessful: The goal could not be reached.";
			}
			return result;
		}

		public Search(Map m)
		{
			//Initiate Map
			myMap = m;
			NumNodes = 0;
		}
	}
}
