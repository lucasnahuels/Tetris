/* 
 
A construction game uses cubes to build a structure
on a table. The game progresses by dropping more
cubes onto the structure. The cubes have to be cleared as they fall down.
 
Implement the ConstructionGame class that has
the following methods and constructor:


* The constructor accepts the length and width
of the table.
* The Addcubes method accepts a 2d boolean
array that indicates the coordinates of a set of
cubes to be dropped on the table,
* The GetHeight method returns the structure's
maximum vertical height as number of cubes.


The following code represents the expected behavior.

ConstructionGame game = new ConstructionGame(2, 2);

game.AddCubes(new bool[,]
{ true, true },
{ false, false }
}); 

game.AddCubes(new bool[,]
{ true, true },
{ false, true }
});

Console.WriteLine(game.GetHeight()); //should print 2

game.AddCubes(new bool[,]
{ false, false },
{ true, true }
});

Console.WriteLine(game.GetHeight()); //should print 1
 */


public class ConstructionGame
{
    private int[,] table;
    private int length;
    private int width;
    public ConstructionGame(int length, int width)
    {
        this.length = length;
        this.width = width;
        table = new int[length, width];
    }

    public void AddCubes(bool[,] cubes)
    {
        for (int j = 0; j < width; j++)
        {
            int columnHeight = 0;
            for (int i = 0; i < length; i++)
            {
                if (cubes[i, j])
                {
                    columnHeight++;
                }
            }

            for (int i = length - 1; i >= 0; i--)
            {
                if (columnHeight > 0)
                {
                    table[i, j] = 1;
                    columnHeight--;
                }
                else
                {
                    table[i, j] = 0;
                }
            }
        }
    }

    public void AddCubesNotSubmitted(bool[,] cubes)
    {
        Parallel.For(0, length, i =>
        {
            for (int j = 0; j < width; j++)
            {
                if (cubes[i, j])
                {
                    table[i, j]++;
                }
            }
        });
        ClearRows();
    }

    public int GetHeight()
    {
        int maxHeight = 0;
        for (int j = 0; j < width; j++)
        {
            int columnHeight = 0;
            for (int i = 0; i < length; i++)
            {
                if (table[i, j] == 1)
                {
                    columnHeight++;
                }
            }
            if (columnHeight > maxHeight)
            {
                maxHeight = columnHeight;
            }
        }
        return maxHeight;
    }

    public int GetHeightNotSubmitted()
    {
        return table.Cast<int>().Max();
    }

    public void ClearRows()
    {
        if (!table.Cast<int>().ToList().Contains(0)) 
        {
            table = table.Cast<int>().Select(x => x - 1).ToArray().Reshape(length, width);
        }
    }

    public static void Main(string[] args)
    {
        ConstructionGame game = new ConstructionGame(2, 2);

        game.AddCubesNotSubmitted(new bool[,]
        {
            { true, true },
            { false, false }
        });
        // { 1, 1 },
        // { 0, 0 }
        // not cleaning row

        game.AddCubesNotSubmitted(new bool[,]
        {
            { true, true },
            { false, true }
        });
        Console.WriteLine(game.GetHeightNotSubmitted()); // should print 2 
        // { 2, 2 },
        // { 0, 1 }
        // not cleaning row

        game.AddCubesNotSubmitted(new bool[,]
        {
            { true, false },
            { false, false }
        });
        Console.WriteLine(game.GetHeightNotSubmitted()); // should print 3
        // { 3, 2 },
        // { 0, 1 }
        // not cleaning row

        game.AddCubesNotSubmitted(new bool[,]
        {
            { false, false },
            { true, true }
        });
        Console.WriteLine(game.GetHeightNotSubmitted()); // should print 2
        // { 3, 2 },
        // { 1, 2 }
        // has to clean row
        // After cleaning row:
        // { 2, 1 },
        // { 0, 1 }

        game.AddCubesNotSubmitted(new bool[,]
        {
            { false, false },
            { true, false }
        });
        Console.WriteLine(game.GetHeightNotSubmitted()); // should print 1
        // { 2, 1 },
        // { 1, 1 } 
        // has to clean row
        // After cleaning row:
        // { 1, 0 },
        // { 0, 0 }

        game.AddCubesNotSubmitted(new bool[,]
        {
            { false, true },
            { true, true }
        });
        Console.WriteLine(game.GetHeightNotSubmitted()); // should print 0
        // { 1, 1 },
        // { 1, 1 } 
        // has to clean row
        // After cleaning row:
        // { 0, 0 },
        // { 0, 0 }
    }
}
