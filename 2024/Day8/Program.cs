using System.Text.RegularExpressions;

List<string> lines = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd8.txt")).ToList();
List<(int, int, char)> Positions = new();
Regex LetterNumber = new(@"[\d\w]");

for (int i = 0; i < lines.Count; i++)
{
    var line = lines[i];
    var matches = LetterNumber.Matches(line);
    foreach (Match item in matches)
    {
        Positions.Add((i, item.Index, item.Value[0]));
    }
}

int sum = 0;
List<(int, int, int, int)> positionPairs = new();
for (int i = 0; i < Positions.Count; i++)
{
    for (int j = i; j < Positions.Count; j++)
    {
        if (Positions[i].Item3 == Positions[j].Item3 && i!=j)
        {
            positionPairs.Add((Positions[i].Item1, Positions[i].Item2, Positions[j].Item1, Positions[j].Item2));
        }
    }
}
int MaxX = lines.Count;
int MaxY = lines.Max().Length;

List<(int,int)> FinalPositions=new();
foreach (var pairs in positionPairs)
{
    (int, int) distance = (pairs.Item3 - pairs.Item1, pairs.Item4 - pairs.Item2);
    var newpoint1 = ((pairs.Item1 - distance.Item1), (pairs.Item2 - distance.Item2));
    var newpoint2 = ((pairs.Item3 + distance.Item1), (pairs.Item4+ distance.Item2));
    if(newpoint1.Item1>=0 && newpoint1.Item2>=0 && newpoint1.Item1<MaxX && newpoint1.Item2<MaxY)
    {
        FinalPositions.Add(newpoint1);
        sum++;
    }
    else
    {

    }
    if (newpoint2.Item1 >= 0 && newpoint2.Item2 >= 0 && newpoint2.Item1 < MaxX && newpoint2.Item2 < MaxY)
    {
        FinalPositions.Add(newpoint2);
        sum++;
    }
    else
    {

    }
}

sum = FinalPositions.Distinct().Count();
Console.WriteLine($"Szumma 8A:{sum}");

FinalPositions = new();
foreach (var pairs in positionPairs)
{
    (int, int) distance = (pairs.Item3 - pairs.Item1, pairs.Item4 - pairs.Item2);
    for (int i = 0; pairs.Item1 - distance.Item1 * i >= 0 && pairs.Item2 - distance.Item2 * i>=0 &&
        pairs.Item1 - distance.Item1 * i <MaxX && pairs.Item2 - distance.Item2 * i <MaxY; i++)
    {
        var newpoint1 = ((pairs.Item1 - distance.Item1*i), (pairs.Item2 - distance.Item2*i));
        if (newpoint1.Item1 >= 0 && newpoint1.Item2 >= 0 && newpoint1.Item1 < MaxX && newpoint1.Item2 < MaxY)
        {
            FinalPositions.Add(newpoint1);
        }
        else
        {

        }
    }
    for (int i = 0; pairs.Item1 + distance.Item1 * i >= 0 && pairs.Item2 + distance.Item2 * i >= 0 &&
    pairs.Item1 + distance.Item1 * i < MaxX && pairs.Item2 + distance.Item2 * i < MaxY; i++)
    {
        var newpoint1 = ((pairs.Item1 + distance.Item1 * i), (pairs.Item2 + distance.Item2 * i));
        if (newpoint1.Item1 >= 0 && newpoint1.Item2 >= 0 && newpoint1.Item1 < MaxX && newpoint1.Item2 < MaxY)
        {
            FinalPositions.Add(newpoint1);
        }
        else
        {

        }
    }
}

sum = FinalPositions.Distinct().Count();
Console.WriteLine($"Szumma 8B:{sum}");
Console.ReadKey();