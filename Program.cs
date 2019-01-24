using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotNavigation
{
    enum PosType { empty, start, goal, wall, nill };
    enum Direction { up, right, down, left, nill };


    class Program
    {
		
		static void Main(string[] args)
        {
			//Get Arguments from command line
			String[] arguments = Environment.GetCommandLineArgs();
			string filename = arguments[1];
			string searchType = arguments[2];

			Search s = null;					//Holds search object
			bool validInput = true;				//Used to check if searchType input is valid
			Map m = new GridMap(filename);		//Used to hold map
			string output = "";					//Output string

			//Get the search type
			if (searchType == "breadth-first")
			{
				s = new BreadthFirst(m);
			}
			else if (searchType == "depth-first")
			{
				s = new DepthFirst(m);
			}
			else if (searchType == "greedy-best-first")
			{
				s = new GreedyBestFirst(m);
			}
			else if (searchType == "a-star")
			{
				s = new AStar(m);
			}
			else if (searchType == "hill-climb")
			{
				s = new HillClimb(m);
			}
			else if (searchType == "bidirectional")
			{
				s = new Bidirectional(m);
			}
			else
			{
				Console.WriteLine("ERROR: " + searchType + " Isn't a valid search type.");
				validInput = false;
			}

			//Perform search and output results
			if (validInput)
			{
				if(s != null)
				{
					s.DoSearch();
				}
							
				output = s.OutputString();
				output = filename + " " + " " + searchType + " " + output;
				Console.WriteLine(output);
				Console.Read();
			}
		}
    }
}
