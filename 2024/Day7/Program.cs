using System.Text.RegularExpressions;

List<string> lines = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd7.txt")).ToList();
List<List<ulong>> eqotations = new();
Regex Number = new(@"\d+");

foreach (string line in lines)
{
    List<ulong> temp = new();
    temp.Clear();
    var splitted = line.Split(':');
    temp.Add(ulong.Parse(Number.Match(splitted[0]).Value));
    var values = Number.Matches(splitted[1]).ToList();
    foreach (var value in values)
    {
        temp.Add(ulong.Parse(value.Value));
    }
    eqotations.Add(temp);
}

ulong sum = 0;
ulong res = 0;
foreach (var eq in eqotations)
{
    sum = eq[0];

    //Generate all possible operations
    var alphabet = "+*|";
    var q = alphabet.Select(x => x.ToString());
    int size = eq.Count - 2;
    for (int i = 0; i < size - 1; i++)
        q = q.SelectMany(x => alphabet, (x, y) => x + y);

    //Check all possible calculation against sum
    foreach (var item in q)
    {
        var sumCalc = eq[1];
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i] == '+')
            {
                sumCalc += eq[i + 2];
            }
            else if (item[i] == '*')
            {
                sumCalc *= eq[i + 2];
            }
            else if (item[i] == '|')
            {
                sumCalc = ulong.Parse(sumCalc.ToString() + eq[i + 2].ToString());
            }
            if (sumCalc > sum)
                break;
        }
        if (sumCalc == sum)
        {
            res += sum;
            break;
        }
    }
}

Console.WriteLine($"Szumma 7B:{res}");
Console.ReadKey();