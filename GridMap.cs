using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RobotNavigation
{
    class GridMap : Map
    {
		//Holds the positions of walls, start and end point
		private PosType[,] grid;

		//Imports the map from a file
		protected override void importMap(string fileName)
        {
			int counter = 0;
			string line;

			// Read the file and display it line by line.  
			System.IO.StreamReader file = new System.IO.StreamReader(fileName);
			while ((line = file.ReadLine()) != null)
			{
				//Clean String
				string[] charsToRemove = new string[] { "(", ")", "[", "]" };
				foreach (string c in charsToRemove)
				{
					line = line.Replace(c, string.Empty);
				}

				string[] lineSplit = line.Split(',');

				switch (counter)
				{
					//Grid Size
					case 0:
						int rows = int.Parse(lineSplit[0]);
						int colls = int.Parse(lineSplit[1]);

						grid = new PosType[colls, rows];

						//Filling map with empty squares
						for (int y = 0; y < rows; y++)
						{
							for (int x = 0; x < colls; x++)
							{
								grid[x, y] = PosType.empty;
							}
						}

						//Set size
						sizeX = colls;
						sizeY = rows;

						break;
					
						//Start Position
					case 1:
						int startX = int.Parse(lineSplit[0]);
						int startY = int.Parse(lineSplit[1]);

						grid[startX, startY] = PosType.start;

						//Save start position
						start = new SearchNode(startX, startY, Direction.nill, null);
						break;
					
					//Goal Position
					case 2:
						int endX = int.Parse(lineSplit[0]);
						int endY = int.Parse(lineSplit[1]);

						grid[endX, endY] = PosType.goal;

						//Save goal position
						goal = new SearchNode(endX, endY, Direction.nill, null);
						break;
					
					//Walls
					default:
						int wallX = int.Parse(lineSplit[0]);
						int wallY = int.Parse(lineSplit[1]);
						int width = int.Parse(lineSplit[2]);
						int height = int.Parse(lineSplit[3]);
					
						//Add a wall
						for (int x = wallX; x < wallX + width; x++)
						{
							for (int y = wallY; y < wallY + height; y++)
							{
								grid[x, y] = PosType.wall;
							}
						}
						break;
				}
				counter++;
			}

			//Close the File when finished
			file.Close();
		}

		//Checks the type of a position on the map
		public override PosType checkPos(int x, int y)
		{
			//If the position is within bounds, return the positon type
			if (x >= 0 &&
				x < grid.GetLength(0) &&
				y >= 0 &&
				y < grid.GetLength(1))
			{
				return grid[x, y];
			}
			//If the position is out of bounds, return wall type
			else
			{
				return PosType.wall;
			}

		}

		public GridMap(string fileName) : base(fileName)
		{ }
    }
}
