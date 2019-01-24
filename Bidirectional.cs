using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
	class Bidirectional: UninformedSearch
	{
		//Lists for holding fronteer nodes and visited nodes
		protected List<SearchNode> rSearchingNodes;
		protected List<SearchNode> rVisitedNodes;

		//Reverse Search Methods
		private bool RHaveSearched(int posX, int posY)
		{
			//Check the nodes that are currently being searched
			foreach (SearchNode n in rSearchingNodes)
			{
				if (posX == n.PosX && posY == n.PosY)
				{
					return true;
				}
			}
			//Check the nodes that are already being searched
			foreach (SearchNode n in rVisitedNodes)
			{
				if (posX == n.PosX && posY == n.PosY)
				{
					return true;
				}
			}
			//Node not found
			return false;
		}

		//Adds removes an element from the end of the list
		private void REnqueue(SearchNode node)
		{
			rSearchingNodes.Insert(0, node);
			NumNodes++;
		}
		//Removes an element from the end of the list (NOTE: This functions as both pop and dequeue)
		private void RRemove()
		{
			rVisitedNodes.Add(rSearchingNodes[rSearchingNodes.Count - 1]);
			rSearchingNodes.RemoveAt(rSearchingNodes.Count - 1);
		}

		protected SearchNode RGetTop()
		{
			return rSearchingNodes[rSearchingNodes.Count - 1];
		}


		public override void DoSearch()
		{
			bool forwardSearch = true;
			while (!hasIntersected() && searchingNodes.Any() && rSearchingNodes.Any())
			{
				if (forwardSearch)
				{
					SearchForward();
					forwardSearch = false;
				}
				else
				{
					SearchBackward();
					forwardSearch = true;
				}
			}

			//If search successful, combine Searches
			if (searchingNodes.Any() && rSearchingNodes.Any())
			{
				//NOTE: The boolean needs to be inverted, as it is inverted after completing the search. 
				//TODO: Explain this better.
				combineSearches(!forwardSearch);
			}
		}

		private void combineSearches(bool forwardSearch)
		{
			//Index of intersecting node in forwards and backwards lists
			int forwardIndex;
			int backwardIndex;

			//Find intersecting node indexes
			if (forwardSearch)
			{
				forwardIndex = 0;
				backwardIndex = getIntersection(searchingNodes[0], rSearchingNodes);
			}
			else
			{
				forwardIndex = getIntersection(rSearchingNodes[0], searchingNodes);
				backwardIndex = 0;
			}

			//Holds current and previous node to be added to the list.
			SearchNode current = rSearchingNodes[backwardIndex];
			SearchNode next = rSearchingNodes[backwardIndex].PreviousNode;

			searchingNodes.Add(new SearchNode(next.PosX, next.PosY, current.Dir, searchingNodes[forwardIndex]));

			//Add each node in the backwards search to the forward search
			do
			{
				searchingNodes.Add(new SearchNode(next.PosX, next.PosY, current.Dir, GetTop()));
				current = next;
				next = next.PreviousNode;
			}
			while (next != null);

		}

		//Returns the index of the intersecting node in a list, or -1 if interesction isn't found
		private int getIntersection(SearchNode node, List<SearchNode> nodeList)
		{
			foreach (SearchNode s in nodeList)
			{
				if (s.PosX == node.PosX && s.PosY == node.PosY)
				{
					return nodeList.IndexOf(s);
				}
			}
			return -1;
		}

		//Check to see if the searches have intersected
		private bool hasIntersected()
		{
			foreach (SearchNode s in searchingNodes)
			{
				foreach (SearchNode r in rSearchingNodes)
				{
					if (s.PosX == r.PosX && s.PosY == r.PosY)
					{
						return true;
					}
				}
			}
			return false;
		}

		//Breadth-First Search from start
		private void SearchForward()
		{
			int posX = GetTop().PosX;
			int posY = GetTop().PosY;

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
		}

		//Breadth-First Search from goal
		private void SearchBackward()
		{
			int posX = RGetTop().PosX;
			int posY = RGetTop().PosY;

			//Note: Directions are reversed so it can be combined seamlessly with the other list
			//Search Up
			if (myMap.checkPos(posX, posY - 1) != PosType.wall &&
				!RHaveSearched(posX, posY - 1))
			{

				REnqueue(new SearchNode(posX, posY - 1, Direction.down, RGetTop()));
			}

			//Search Right
			if (myMap.checkPos(posX + 1, posY) != PosType.wall &&
				!RHaveSearched(posX + 1, posY))
			{
				REnqueue(new SearchNode(posX + 1, posY, Direction.left, RGetTop()));
			}

			//Search Down
			if (myMap.checkPos(posX, posY + 1) != PosType.wall &&
				!RHaveSearched(posX, posY + 1))
			{
				REnqueue(new SearchNode(posX, posY + 1, Direction.up, RGetTop()));
			}

			//Search Left
			if (myMap.checkPos(posX - 1, posY) != PosType.wall &&
				!RHaveSearched(posX - 1, posY))
			{
				REnqueue(new SearchNode(posX - 1, posY, Direction.right, RGetTop()));
			}

			//Dequeue top node
			RRemove();
		}

		public Bidirectional(Map m) : base(m)
		{
			//Initiate search list with starting node
			rSearchingNodes = new List<SearchNode>();
			rVisitedNodes = new List<SearchNode>();

			REnqueue(myMap.Goal);
		}
	}
}
