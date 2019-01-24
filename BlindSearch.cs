using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    abstract class UninformedSearch: TentativeSearch
    {
		//Used to hold fronteer and searched nodes
		protected List<SearchNode> searchingNodes;
		protected List<SearchNode> visitedNodes;

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

		//Adds an element to the end of the list
		protected void push(SearchNode node)
		{
			searchingNodes.Add(node);
			NumNodes++;
		}
		//Adds an element at the end of the list
		protected void enqueue(SearchNode node)
		{
			searchingNodes.Insert(0, node);
			NumNodes++;
		}
		//Removes an element from the end of the list (NOTE: This functions as both pop and dequeue)
		protected override void remove()
		{
			visitedNodes.Add(searchingNodes[searchingNodes.Count - 1]);
			searchingNodes.RemoveAt(searchingNodes.Count - 1);
		}

		//Gets the top node from the searching list
		protected override SearchNode GetTop()
		{
			return searchingNodes[searchingNodes.Count - 1];
		}

		public UninformedSearch(Map m): base(m)
		{
			//Initiate search list with starting node
			searchingNodes = new List<SearchNode>();
			push(myMap.Start);

			//Initiate search list with starting node
			visitedNodes = new List<SearchNode>();
		}
	}
}
