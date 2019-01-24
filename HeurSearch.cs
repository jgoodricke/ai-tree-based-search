using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
	abstract class InfomredSearch : TentativeSearch
	{
		//Holds the fronteer and searched nodes
		protected List<SearchNode> searchingNodes;
		protected List<SearchNode> visitedNodes;

		//Get the top node on the stack
		protected override SearchNode GetTop()
		{
			return searchingNodes[searchingNodes.Count - 1];
		}

		//Removes an element from the end of the list
		protected override void remove()
		{
			visitedNodes.Add(searchingNodes[searchingNodes.Count - 1]);
			searchingNodes.RemoveAt(searchingNodes.Count - 1);
		}

		//Checks if a node has already been visited
		protected override bool HaveSearched(int posX, int posY)
		{
			//Check the nodes that are currently being searched
			foreach (SearchNode n in searchingNodes)
			{
				if (posX == n.PosX && posY == n.PosY)
				{
					return true;
				}
			}
			//Check the nodes that are already being searched
			foreach (SearchNode n in visitedNodes)
			{
				if (posX == n.PosX && posY == n.PosY)
				{
					return true;
				}
			}
			//Node not found
			return false;
		}

		//Inserts the node into the fronteer acording to its cost
		protected void Insert(SearchNode newNode)
		{
			bool uninserted = true;

			//Search the fronteer and insert the node next to the one with a smaller cost
			foreach (SearchNode n in searchingNodes)
			{
				//If the new node has a larger cost than any of the current nodes, insert it into that position in the fronteer
				if (n.Cost < newNode.Cost)
				{
					searchingNodes.Insert(searchingNodes.IndexOf(n), newNode);
					uninserted = false;
					break;
				}
			}
			
			//If the node has a smaller cost than all the other nodes, add it to the front of the fronteer
			if (uninserted)
			{
				searchingNodes.Add(newNode);
			}

			NumNodes++;
		}

		//Used to get the cost of the current node
		protected abstract int getCost(int x, int y);

		public InfomredSearch(Map m): base(m)
		{
			//Initiate Map
			myMap = m;

			//Initiate search list with starting node
			searchingNodes = new List<SearchNode>();
			Insert(myMap.Start);

			//Initiate search list with starting node
			visitedNodes = new List<SearchNode>();
		}
	}
}
