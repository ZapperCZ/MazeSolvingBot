using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolvingBot
{
    class Program
    {
        static int mazeX = 7;                       //Vertical size
        static int mazeY = 9;                       //Horizontal size
        static int[,,] mazeArray;                   //Layer 0 - 0 = Free Tile | 1 = Wall | 2 = Start | 3 = End || Layer 1 - Stores what tile it was uncovered from || Layer 2 - Stores global tile index
        static List<int> Path = new List<int>();
        static void Main(string[] args)
        {
            Console.WriteLine("Legend: 0 = Free Tile | 1 = Wall | 2 = Start | 3 = End | 5 = Path");
            mazeArray = new int[mazeX, mazeY, 3];
            ConvertToMaze();
            PrintMaze(0);
            for (int i = 0; i < mazeX; i++)
            {
                for (int j = 0; j < mazeY; j++)
                {
                    if (mazeArray[i, j, 0] == 2)        //Start
                        CheckNearbyTiles(i,j);
                }
            }

            ExtractPath();

            VisualizePath();

            PrintMaze(0);

            Console.ReadLine();
        }
        static void CheckNearbyTiles(int x, int y)
        {
            if (mazeArray[x, y, 1] == -1)
            {
                mazeArray[x, y, 1] = -2;
            }
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && j >= 0 && i < mazeX && j < mazeY)                         //Withing maze boundaries
                    {
                        if (!((i == x - 1 && j == y + 1) || (i == x+1 && j == y+1) || (i == x - 1 && j == y - 1) || (i == x + 1 && j == y - 1)))    
                        {
                            if (mazeArray[i, j, 1] == -1 && mazeArray[i, j, 0] != 1)        //Hasn't been checked yet and isn't a wall
                            {
                                mazeArray[i, j, 1] = mazeArray[x, y, 2];
                                CheckNearbyTiles(i, j);
                            }
                        }
                    }
                }
            }
        }
        static void ConvertToMaze()
        {
            String s = "Z0000000001111100010000001100001001111111101111111000000K000000";
            int index = 0;
            for(int i = 0; i < mazeX; i++)
            {
                for(int j = 0; j < mazeY; j++)
                {
                    if (s[index] == 'Z')
                        mazeArray[i, j, 0] = 2;
                    else if (s[index] == 'K')
                        mazeArray[i, j, 0] = 3;
                    else
                        mazeArray[i, j, 0] = Convert.ToInt32(s[index].ToString());

                    mazeArray[i, j, 1] = -1;
                    mazeArray[i, j, 2] = index;
                    index++;
                }
            }
        }
        static void ExtractPath()
        {
            bool startFound = false;
            int x, y;
            x = y = 0;
            for (int i = 0; i < mazeX; i++)
            {
                for (int j = 0; j < mazeY; j++)
                {
                    if (mazeArray[i, j, 0] == 3)        //End
                    {        
                        x = i;
                        y = j;
                        Path.Add(mazeArray[x, y, 1]);
                    }
                }
            }
            while (!startFound)
            {
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (i >= 0 && j >= 0 && i < mazeX && j < mazeY)
                        {
                            if (mazeArray[x, y, 1] == mazeArray[i, j, 2] && !Path.Contains(mazeArray[i, j, 1]))
                            {
                                x = i;
                                y = j;
                                Path.Add(mazeArray[i, j, 1]);
                                if(mazeArray[x,y,1] == -2)
                                {
                                    startFound = true;
                                }
                            }
                        }
                    }
                }
            }
            Path.Reverse();
        }
        static void VisualizePath()
        {
            int index = 0;
            bool endFound = false;

            while (!endFound)
            {
                for (int i = 0; i < mazeX; i++)
                {
                    for (int j = 0; j < mazeY; j++)
                    {
                        if (mazeArray[i, j, 1] == Path[index])
                        {
                            if (mazeArray[i, j, 0] != 2 && mazeArray[i, j, 0] != 3)
                                mazeArray[i, j, 0] = 5;
                            else if (mazeArray[i, j, 0] == 3)
                                endFound = true;
                            
                           
                        }
                    }
                }
                index++;
            }
        }
        static void PrintMaze(int layer)
        {
            Console.WriteLine($"\nLayer: {layer} \n");
            for (int i = 0; i < mazeX; i++)
            {
                for (int j = 0; j < mazeY; j++)
                {
                    Console.Write(mazeArray[i,j,layer] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
