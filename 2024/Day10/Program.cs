
string postfix;
var test = false;
if (test)
{
    postfix = "test";
}
else postfix = "";
var lines = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd10{postfix}.txt")).ToList();
List<List<byte>> list = new List<List<byte>>();
var count = lines.Count;
for (int j = 0; j < count; j++)
{
    var listitem = new List<byte>();
    for (int i = 0; i < lines[0].Length; i++)
    {
        byte value = byte.TryParse(lines[j][i].ToString(), out byte val) ? val : (byte)10;
        listitem.Add(value);
    }
    list.Add(listitem);
}
long sum = 0;
long sum2 = 0;
for (int i = 0; i < count; i++)
{
    for (int j = 0; j < list[0].Count; j++)
    {
        if (list[i][j] == 0)
        {
            sum += GetGoodPoints(list, new List<List<(int, int)>> { new List<(int, int)> { (i, j) } }, 1).Last().Distinct().Count();
            sum2 += GetGoodPoints(list, new List<List<(int, int)>> { new List<(int, int)> { (i, j) } }, 1).Last().Count;
        }
    }
}
Console.WriteLine($"10A:{sum}");
Console.WriteLine($"10B:{sum2}");
Console.ReadKey();

List<List<(int, int)>> GetGoodPoints(List<List<byte>> matrix, List<List<(int,int)>> positions, byte expectedValue)
{
    var list=new List<(int,int)>();
    if (expectedValue == 10)
    {
        return positions;
    }
    foreach (var startPosition in positions[expectedValue-1])
    {
        if (startPosition.Item1 - 1 >= 0 && matrix[startPosition.Item1 - 1][startPosition.Item2] == expectedValue)
        {
            list.Add((startPosition.Item1 - 1, startPosition.Item2));
        }
        if (startPosition.Item2 - 1 >= 0 && matrix[startPosition.Item1][startPosition.Item2 - 1] == expectedValue)
        {
            list.Add((startPosition.Item1, startPosition.Item2 - 1));
        }
        if (startPosition.Item2 + 1 < matrix[0].Count && matrix[startPosition.Item1][startPosition.Item2 + 1] == expectedValue)
        {
            list.Add((startPosition.Item1, startPosition.Item2 + 1));
        }
        if (startPosition.Item1 + 1 < matrix.Count && matrix[startPosition.Item1 + 1][startPosition.Item2] == expectedValue)
        {
            list.Add((startPosition.Item1 + 1, startPosition.Item2));
        }
    }
    positions.Add(list);

    return GetGoodPoints(matrix, positions,++expectedValue);
}