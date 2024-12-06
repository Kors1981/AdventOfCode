using System.Text.RegularExpressions;

var input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd3.txt"));
Regex regMul = new Regex(@"mul\((?<Number1>\d{1,3})\,(?<Number2>\d{1,3})\)");
var matches = regMul.Matches(input);
long result = 0;
foreach (Match a in matches)
{
    result += int.Parse(a.Groups["Number1"].Value) * int.Parse(a.Groups["Number2"].Value);
}
Console.WriteLine($"Szumma 3A:{result}");
result = 0;
Regex regDoNot = new Regex(@"don\'t\(\)|do\(\)");
var DoDontMatches = regDoNot.Matches(input);
bool isFirst = true;
foreach (Match a in matches)
{
    var Instruction = DoDontMatches.Where(x => x.Index < a.Index).LastOrDefault();
    if (Instruction?.Value == "do()" || isFirst)
    {
        result += int.Parse(a.Groups["Number1"].Value) * int.Parse(a.Groups["Number2"].Value);
    }
    if (isFirst) isFirst = false;
}
Console.WriteLine($"Szumma 3B:{result}");
Console.ReadKey();