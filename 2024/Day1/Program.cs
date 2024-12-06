using System.Text.RegularExpressions;

List<string> lines = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory()+ $"..\\..\\..\\..\\..\\Inputs\\aocd1.txt")).ToList();
Regex regex = new(@"(?<number>\d+)");
int sum = 0;

List<int> List1 = new();
List<int> List2 = new();
List<int> DiffList = new();
List<int> MList = new();
var count = lines.Count;
for (int j = 0; j < count; j++)
{
    var res = lines[j].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    List1.Add(int.Parse(res[0]));
    List2.Add(int.Parse(res[1]));
}
List1 = List1.OrderBy(x => x).ToList();
List2 = List2.OrderBy(x => x).ToList();
for (int j = 0; j < count; j++)
{
    DiffList.Add(Math.Abs(List1[j] - List2[j]));
}
sum = DiffList.Sum(x => x);
Console.WriteLine($"Szumma 1A:{sum}");

for (int j = 0; j < count; j++)
{
    var RunningNumber = List1[j];
    MList.Add(List2.Where(x => x == RunningNumber).Count() * List1[j]);
}

sum = MList.Sum(x => x);
Console.WriteLine($"Szumma 1B:{sum}");
Console.ReadKey();