using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeSolvingBot
{
    class Program
    {
        static int mazeX = 7;                     //Vertical size
        static int mazeY = 9;                     //Horizontal size
        static int[,,] mazeArray;                  //Layer 0 - 0 = Free Tile | 1 = Wall | 2 = Start | 3 = End || Layer 2 - 0 = Start
        static void Main(string[] args)
        {
            mazeArray = new int[mazeX, mazeY, 2];
            ConvertToMaze();
            PrintMaze();
            Console.ReadLine();
        }
        static void CheckNearbyTiles(int x, int y)
        {

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
                    index++;
                }
            }
        }
        static void PrintMaze()
        {
            for (int i = 0; i < mazeX; i++)
            {
                for (int j = 0; j < mazeY; j++)
                {
                    if (mazeArray[i, j, 0] == 2)
                        Console.Write('S');
                    else if (mazeArray[i, j, 0] == 3)
                        Console.Write('F');
                    else if (mazeArray[i, j, 0] == 1)
                        Console.Write('■');
                    else
                        Console.Write('o');
                }
                Console.WriteLine();
            }
        }
    }
}
