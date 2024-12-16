using System.Text.RegularExpressions;
string postfix;
var test = false;
if (test)
{
    postfix = "test";
}
else postfix = "";
var input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd14{postfix}.txt"));
var InputRegex = new Regex("p=\\s*(?<Px>\\d+),(?<Py>\\d+)\\s*v\\=(?<Vx>\\-?\\d+),\\s*(?<Vy>\\-?\\d+)");
var matches = InputRegex.Matches(input);
List<Robot> robots = new List<Robot>();
 (sbyte, sbyte) mapsize = (11, 7);
var secound = 100;
int Q1 = 0;
int Q2 = 0;
int Q3 = 0;
int Q4 = 0;
foreach (Match item in matches)
{
    (sbyte,sbyte) startPos=(sbyte.Parse(item.Groups["Px"].Value), sbyte.Parse(item.Groups["Py"].Value));
    (sbyte, sbyte) velocity = (sbyte.Parse(item.Groups["Vx"].Value), sbyte.Parse(item.Groups["Vy"].Value));
    var robot = new Robot(startPos, velocity, mapsize);
    //robot.Move(secound);
    robots.Add(robot);
    if (robot.CurrentPosition.Item1 != mapsize.Item1 / 2  || robot.CurrentPosition.Item2 != mapsize.Item2 / 2)
    {
        if (robot.CurrentPosition.Item1 < mapsize.Item1 / 2  && robot.CurrentPosition.Item2 < mapsize.Item2 / 2 )
            Q1++;

        if (robot.CurrentPosition.Item1 > mapsize.Item1 / 2 && robot.CurrentPosition.Item2 > mapsize.Item2 / 2 )
            Q4++;

        if (robot.CurrentPosition.Item1 < mapsize.Item1 / 2 && robot.CurrentPosition.Item2 > mapsize.Item2 / 2 )
            Q2++;

        if (robot.CurrentPosition.Item1 > mapsize.Item1 / 2  && robot.CurrentPosition.Item2 < mapsize.Item2 / 2 )
            Q3++;
    }
}

char[,] list = new char[101, 103];
for (int x = 0; x < mapsize.Item2; x++)
{
    for (int y = 0; y < mapsize.Item1; y++)
    {
        list[y, x] = '0';
    }
}

foreach (var robot in robots)
{
    int temp = int.Parse(list[robot.CurrentPosition.Item1, robot.CurrentPosition.Item2].ToString());
    temp++;
    list[robot.CurrentPosition.Item1, robot.CurrentPosition.Item2] = temp.ToString()[0];
}
for (int j = 0; j < 103; j++)
{
    for (int i = 0; i < 101; i++)
    {
        Console.Write(list[i, j]);
    }
    Console.WriteLine();
}
Console.WriteLine($"14B:{Q1*Q2*Q3*Q4}");

public class Robot
{
    private (sbyte, sbyte) currentPosition;
    public (sbyte, sbyte) CurrentPosition => currentPosition;
    private (sbyte, sbyte) Velocity;
    private (sbyte, sbyte) MapSize;

    public Robot((sbyte, sbyte) startPosition, (sbyte, sbyte) velocity, (sbyte, sbyte) mapSize)
    {
        this.Velocity = velocity;
        this.MapSize = mapSize;
        this.currentPosition = startPosition;
    }   
    public void Move(int iteration)
    {
        int currenPosX = (int)currentPosition.Item1 + (int)Velocity.Item1 * (int)iteration;
        int currenPosY = (int)currentPosition.Item2 + (int)Velocity.Item2 * (int)iteration;
        if (currenPosX > 0)
        {
            currentPosition.Item1 = (sbyte)((((int)currentPosition.Item1 + (int)Velocity.Item1 * (int)iteration) % (MapSize.Item1)) % MapSize.Item1);
        } else
        {
            currentPosition.Item1 = (sbyte)((MapSize.Item1+(((int)currentPosition.Item1 + (int)Velocity.Item1 * (int)iteration) % (MapSize.Item1)))%MapSize.Item1);
        }
        if (currenPosY > 0)
        {
            currentPosition.Item2 = (sbyte)((((int)currentPosition.Item2 + (int)Velocity.Item2 * (int)iteration) % (MapSize.Item2))% MapSize.Item2);
        }
        else
        {
            currentPosition.Item2 = (sbyte)((MapSize.Item2+(((int)currentPosition.Item2 + (int)Velocity.Item2 * (int)iteration) % (MapSize.Item2)))% MapSize.Item2);
        }
    }
}