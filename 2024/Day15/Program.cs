using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
string postfix;
var test = false;
if (test)
{
    postfix = "test";
}
else postfix = "";
var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd15{postfix}.txt")).ToList();
var inputString = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd15{postfix}.txt"));
var Directions = new Regex("^[<>\\\\^v\\s]+",RegexOptions.Multiline);
int endOfMap = 0;
List<(int, int)> Wall = new List<(int, int)>();
var xRange=input[0].Count(x => x == '#');
var yRange = 1;
while (xRange != input[yRange++].Count(x => x == '#'))
{

}
char[,] Map= new char[xRange, yRange];
(int, int) LaternFishPosition=(-1,-1);
for (int i = 0; i < yRange; i++)
{
    for (int j = 0; j < xRange; j++)
    {
        Map[i, j] = input[i][j];
        if (Map[i,j]== '@')
        {
            LaternFishPosition = (i, j);
        }
    }
}
//Console.ReadKey();
var instructions=Directions.Match(inputString).Value.ToList();
instructions.RemoveAll(x => x.Equals('\r') || x.Equals('\n'));
foreach (var item in instructions)
{
 if (item== '^')
    {
        //attemp to move left
        if (Map[LaternFishPosition.Item1-1,LaternFishPosition.Item2]=='.')
        {
            Map[LaternFishPosition.Item1 - 1, LaternFishPosition.Item2] = '@';
            Map[LaternFishPosition.Item1, LaternFishPosition.Item2] = '.';
            LaternFishPosition.Item1--;
        }
        //attemp to push left
        else if (Map[LaternFishPosition.Item1 - 1, LaternFishPosition.Item2] == 'O')
        {
            var i = 0;
            List<char> ElementsTillTheWall=new List<char>();
            while (Map[LaternFishPosition.Item1 - i++, LaternFishPosition.Item2]!='#')
            {
                ElementsTillTheWall.Add(Map[LaternFishPosition.Item1 - i, LaternFishPosition.Item2]);
            }
            //yes babe, we can push it
            if (ElementsTillTheWall.Contains('.'))
            {
                var index=ElementsTillTheWall.FindIndex(x => x.Equals('.'));
                for (int j = index; j >=0; j--)
                {
                    Map[LaternFishPosition.Item1 - j - 1, LaternFishPosition.Item2] = Map[LaternFishPosition.Item1 - j, LaternFishPosition.Item2];
                }
                Map[LaternFishPosition.Item1, LaternFishPosition.Item2] = '.';
                LaternFishPosition.Item1--;
            }
        }
    }
    else if (item == 'v')
    {
        //attemp to move right
        if (Map[LaternFishPosition.Item1 + 1, LaternFishPosition.Item2] == '.')
        {
            Map[LaternFishPosition.Item1 + 1, LaternFishPosition.Item2] = '@';
            Map[LaternFishPosition.Item1, LaternFishPosition.Item2] = '.';
            LaternFishPosition.Item1++;
        }
        //attemp to push right
        else if (Map[LaternFishPosition.Item1 + 1, LaternFishPosition.Item2] == 'O')
        {
            var i = 0;
            List<char> ElementsTillTheWall = new List<char>();
            while (Map[LaternFishPosition.Item1 + i++, LaternFishPosition.Item2] != '#')
            {
                ElementsTillTheWall.Add(Map[LaternFishPosition.Item1 + i, LaternFishPosition.Item2]);
            }
            //yes babe, we can push it
            if (ElementsTillTheWall.Contains('.'))
            {
                var index = ElementsTillTheWall.FindIndex(x => x.Equals('.'));
                for (int j = index; j >= 0; j--)
                {
                    Map[LaternFishPosition.Item1 +j + 1, LaternFishPosition.Item2] = Map[LaternFishPosition.Item1 + j, LaternFishPosition.Item2];
                }
                Map[LaternFishPosition.Item1, LaternFishPosition.Item2] = '.';
                LaternFishPosition.Item1++;
            }
        }
    }
    else if (item == '>')
    {
        //attemp to move down
        if (Map[LaternFishPosition.Item1 , LaternFishPosition.Item2+1] == '.')
        {
            Map[LaternFishPosition.Item1 , LaternFishPosition.Item2+1] = '@';
            Map[LaternFishPosition.Item1, LaternFishPosition.Item2] = '.';
            LaternFishPosition.Item2++;
        }
        //attemp to push down
        else if (Map[LaternFishPosition.Item1, LaternFishPosition.Item2 + 1] == 'O')
        {
            var i = 0;
            List<char> ElementsTillTheWall = new List<char>();
            while (Map[LaternFishPosition.Item1 , LaternFishPosition.Item2 + i++] != '#')
            {
                ElementsTillTheWall.Add(Map[LaternFishPosition.Item1, LaternFishPosition.Item2 + i]);
            }
            //yes babe, we can push it
            if (ElementsTillTheWall.Contains('.'))
            {
                var index = ElementsTillTheWall.FindIndex(x => x.Equals('.'));
                for (int j = index; j >= 0; j--)
                {
                    Map[LaternFishPosition.Item1 , LaternFishPosition.Item2 + j + 1] = Map[LaternFishPosition.Item1, LaternFishPosition.Item2 + j];
                }
                Map[LaternFishPosition.Item1, LaternFishPosition.Item2] = '.';
                LaternFishPosition.Item2++;
            }
        }
    }
    else if (item == '<')
    {
        //attemp to up
        if (Map[LaternFishPosition.Item1, LaternFishPosition.Item2 - 1] == '.')
        {
            Map[LaternFishPosition.Item1, LaternFishPosition.Item2 - 1] = '@';
            Map[LaternFishPosition.Item1, LaternFishPosition.Item2] = '.';
            LaternFishPosition.Item2--;
        }
        //attemp to push up
        else if (Map[LaternFishPosition.Item1, LaternFishPosition.Item2 - 1] == 'O')
        {
            var i = 0;
            List<char> ElementsTillTheWall = new List<char>();
            while (Map[LaternFishPosition.Item1, LaternFishPosition.Item2 - i++] != '#')
            {
                ElementsTillTheWall.Add(Map[LaternFishPosition.Item1, LaternFishPosition.Item2 - i]);
            }
            //yes babe, we can push it
            if (ElementsTillTheWall.Contains('.'))
            {
                var index = ElementsTillTheWall.FindIndex(x => x.Equals('.'));
                for (int j = index; j >= 0; j--)
                {
                    Map[LaternFishPosition.Item1, LaternFishPosition.Item2 - j - 1] = Map[LaternFishPosition.Item1, LaternFishPosition.Item2 - j];
                }
                Map[LaternFishPosition.Item1, LaternFishPosition.Item2] = '.';
                LaternFishPosition.Item2--;
            }
        }
    }



}
for (int j = 0; j < yRange; j++)
{
    for (int i = 0; i < xRange; i++)
    {
        Console.Write(Map[j, i]);
    }
    Console.WriteLine();
}
//Console.ReadKey();
var sum = 0;
for (int j = 0; j < yRange; j++)
{
    for (int i = 0; i < xRange; i++)
    {
        if (Map[i,j]== 'O')
        {
            sum += i * 100 + j;   
        }
    }
}
Console.WriteLine($"15A:{sum}");
Console.ReadKey();
//var InputRegex = new Regex("p=\\s*(?<Px>\\d+),(?<Py>\\d+)\\s*v\\=(?<Vx>\\-?\\d+),\\s*(?<Vy>\\-?\\d+)");
//var matches = InputRegex.Matches(input);