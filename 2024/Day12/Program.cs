
using System;
using System.Text.RegularExpressions;

string postfix;
var test = true;
if (test)
{
    postfix = "test";
}
else postfix = "";
var lines = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd12{postfix}.txt")).ToList();
var input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd12{postfix}.txt"));
input = input.Replace("\r", string.Empty);
input = input.Replace("\n", string.Empty);
//var valami = lines.ToArray().Cast<char[]>().Distinct().;
Dictionary<int, List<(int, int)>> gardens = new();
List<char> chars = new List<char>();
chars = input.ToList().Distinct().ToList();
chars.Remove('\r');
chars.Remove('\n');
Dictionary<string, List<(int, int)>> charCoordinates = new();
//for (int i = 0; i < lines.Count; i++)
//{
//    chars.Add()
//}
//char FirstChar = lines.Distinct()
//var list = new List<(int, int)>();
for (int i = 0; i < chars.Count; i++)
{
    char Actual = chars[i];
    Regex regex = new Regex(Actual.ToString());
    var matches = regex.Matches(input);
    var list = new List<(int, int)>();
    for (int j = 0; j < matches.Count; j++)
    {
        list.Add((matches[j].Index / lines.Count, matches[j].Index % lines.Count));
    }
    charCoordinates.Add(Actual.ToString(), list);
}
Dictionary<string, List<(int, int)>> keyValuePairs = new();
foreach (var item in charCoordinates)
{
    var temp = GetGardens(item.Value, new List<(int, int)>(), new List<List<(int, int)>>());
    for (int i = 0; i < temp.Count; i++)
    {
        keyValuePairs.Add(item.Key + i, temp[i]);
    }
}
var sum = 0;
foreach (var item in keyValuePairs)
{
    sum += item.Value.Count * CalculatePermiter(item.Value);
}
Console.WriteLine($"Day12A:{sum}");
sum = 0;
foreach (var item in keyValuePairs)
{
    sum += item.Value.Count * CalculateSides(item.Value);
}
Console.WriteLine($"Day12B:{sum}");
Console.ReadKey();

int CalculatePermiter(List<(int, int)> Originallist)
{
    int sum = 0;
    for (int i = 0; i < Originallist.Count; i++)
    {
        sum += (4 - Originallist.FindAll(x =>
                          x.Item1 == Originallist[i].Item1 && x.Item2 == Originallist[i].Item2 + 1
                       || x.Item1 == Originallist[i].Item1 && x.Item2 == Originallist[i].Item2 - 1
                       || x.Item1 == Originallist[i].Item1 + 1 && x.Item2 == Originallist[i].Item2
                       || x.Item1 == Originallist[i].Item1 - 1 && x.Item2 == Originallist[i].Item2).Count);
    }
    return sum;
}

int CalculateSides(List<(int, int)> Originallist)
{
    int sum = 0;
    var AllHLines = new List<List<(int, int)>>();
    for (int x = 0; x <= Originallist.MaxBy(x => x.Item1).Item1; x++)
    {
        var HLinePoints = Originallist.FindAll(point => x == point.Item1);
        if (HLinePoints.Count == 1)
        {
            AllHLines.Add(HLinePoints);
        }
        else
        {
            AllHLines.AddRange(CalculateHorizontalBlocks(HLinePoints.OrderBy(x => x.Item2).ToList(), new List<List<(int, int)>>()));
        }
    }
    foreach (var item in AllHLines)
    {
        //check also,felso barmelyik pontra a soron
        sum = sum + CalculateUD(item, Originallist);
    }
    var AllVLines = new List<List<(int, int)>>();
    for (int x = 0; x <= Originallist.MaxBy(x => x.Item2).Item2; x++)
    {
        var VLinePoints = Originallist.FindAll(point => x == point.Item2);
        if (VLinePoints.Count == 1)
        {
            AllVLines.Add(VLinePoints);
        }
        else
        {
            AllVLines.AddRange(CalculateVerticalBlocks(VLinePoints.OrderBy(x => x.Item1).ToList(), new List<List<(int, int)>>()));
        }
    }
    foreach (var item in AllVLines)
    {
        //check also,felso barmelyik pontra a soron
        sum = sum + CalculateLR(item, Originallist);
    }
    //for (int i = 0; i < Originallist.Count; i++)
    //{
    //    sum += (4 - Originallist.FindAll(x =>
    //                      x.Item1 == Originallist[i].Item1 && x.Item2 == Originallist[i].Item2 + 1
    //                   || x.Item1 == Originallist[i].Item1 && x.Item2 == Originallist[i].Item2 - 1
    //                   || x.Item1 == Originallist[i].Item1 + 1 && x.Item2 == Originallist[i].Item2
    //                   || x.Item1 == Originallist[i].Item1 - 1 && x.Item2 == Originallist[i].Item2).Count);
    //}
    return sum;
}

int CalculateUD(List<(int, int)> PointsToCheck, List<(int, int)> AllPoints)
{
    var hasUp = true;
    var hasDown = true;
    for (int i = 0; i < PointsToCheck.Count; i++)
    {
        hasUp &= AllPoints.Exists(x => x.Item1 == PointsToCheck[i].Item1 - 1 && x.Item2 == PointsToCheck[i].Item2);
        hasDown &= AllPoints.Exists(x => x.Item1 == PointsToCheck[i].Item1 + 1 && x.Item2 == PointsToCheck[i].Item2);
    }
    return (hasUp ? 0 : 1) + (hasDown ? 0 : 1);
}

int CalculateLR(List<(int, int)> PointsToCheck, List<(int, int)> AllPoints)
{
    var hasLeft = true;
    var hasRight = true;
    for (int i = 0; i < PointsToCheck.Count; i++)
    {
        hasLeft &= AllPoints.Exists(x => x.Item2 == PointsToCheck[i].Item2 - 1 && x.Item1== PointsToCheck[i].Item1);
        hasRight &= AllPoints.Exists(x => x.Item2 == PointsToCheck[i].Item2 + 1 && x.Item1 == PointsToCheck[i].Item1);
    }


    return (hasLeft ? 0 : 1) + (hasRight ? 0 : 1);
}
List<List<(int, int)>> CalculateHorizontalBlocks(List<(int, int)> OriginalLine, List<List<(int, int)>> Lines)
{
    var SingleLine = OriginalLine.Count > 0;
    if (!SingleLine)
    {
        return Lines;
    }
    if (OriginalLine.Count == 1)
    {
        Lines.Add(OriginalLine);
        return Lines;
    }
    for (int i = 0; i < OriginalLine.Count - 1; i++)
    {
        if (OriginalLine[i].Item2 + 1 == OriginalLine[i + 1].Item2)
        {
            SingleLine = true;
        }
        else
        {
            SingleLine = false;
            var tempLine = new List<(int, int)>();
            tempLine.AddRange(OriginalLine.TakeWhile(x => x.Item2 + 1 != OriginalLine[i + 1].Item2 && x.Item2 < OriginalLine[i].Item2 + 1));
            if (tempLine.Count == 1)
            {
                Lines.Add(tempLine);
            }
            else
            {
                Lines.Add(new List<(int, int)>() { tempLine.MaxBy(x => x.Item2), tempLine.MinBy(x => x.Item2) });
            }
            OriginalLine = OriginalLine.SkipWhile(x => x.Item2 + 1 != OriginalLine[i + 1].Item2 && x.Item2 < OriginalLine[i].Item2 + 1).ToList();
            break;
        }
    }

    //Only one line
    if (SingleLine)
    {
        if (OriginalLine.Count == 1)
        {
            Lines.Add(OriginalLine);
        }
        else
        {
            Lines.Add(new List<(int, int)>() { OriginalLine.MaxBy(x => x.Item2), OriginalLine.MinBy(x => x.Item2) });
        }
        return Lines;
    }
    return CalculateHorizontalBlocks(OriginalLine, Lines);
}

List<List<(int, int)>> CalculateVerticalBlocks(List<(int, int)> OriginalLine, List<List<(int, int)>> Lines)
{
    var SingleLine = OriginalLine.Count > 0;
    if (!SingleLine)
    {
        return Lines;
    }
    if (OriginalLine.Count == 1)
    {
        Lines.Add(OriginalLine);
        return Lines;
    }
    for (int i = 0; i < OriginalLine.Count - 1; i++)
    {
        if (OriginalLine[i].Item1 + 1 == OriginalLine[i + 1].Item1)
        {
            SingleLine = true;
        }
        else
        {
            SingleLine = false;
            var tempLine = new List<(int, int)>();
            tempLine.AddRange(OriginalLine.TakeWhile(x => x.Item1 + 1 != OriginalLine[i + 1].Item1 && x.Item1 < OriginalLine[i].Item1 + 1));
            if (tempLine.Count == 1)
            {
                Lines.Add(tempLine);
            }
            else
            {
                Lines.Add(new List<(int, int)>() { tempLine.MaxBy(x => x.Item1), tempLine.MinBy(x => x.Item1) });
            }
            OriginalLine = OriginalLine.SkipWhile(x => x.Item1 + 1 != OriginalLine[i + 1].Item1 && x.Item1 < OriginalLine[i].Item1 + 1).ToList();
            break;
        }
    }

    //Only one line
    if (SingleLine)
    {
        if (OriginalLine.Count == 1)
        {
            Lines.Add(OriginalLine);
        }
        else
        {
            Lines.Add(new List<(int, int)>() { OriginalLine.MaxBy(x => x.Item1), OriginalLine.MinBy(x => x.Item1) });
        }
        return Lines;
    }
    return CalculateHorizontalBlocks(OriginalLine, Lines);
}
List<List<(int, int)>> GetGardens(List<(int, int)> Originallist, List<(int, int)> currentlist, List<List<(int, int)>> finalResult, int index = 0)
{
    if (Originallist.Count == 0)
    {
        if (currentlist.Count > 0)
        {
            finalResult.Add(currentlist);
        }
        return finalResult;
    }
    List<(int, int)> found = new();
    if (index == 0)
    {
        currentlist.Add(Originallist[0]);
        found = Originallist.FindAll(x =>
                          x.Item1 == Originallist[0].Item1 && x.Item2 == Originallist[0].Item2 + 1
                       || x.Item1 == Originallist[0].Item1 && x.Item2 == Originallist[0].Item2 - 1
                       || x.Item1 == Originallist[0].Item1 + 1 && x.Item2 == Originallist[0].Item2
                       || x.Item1 == Originallist[0].Item1 - 1 && x.Item2 == Originallist[0].Item2);
        Originallist.Remove(Originallist[0]);
    }
    else
    {
        if (currentlist.Count > index)
        {
            found = Originallist.FindAll(x =>
                      x.Item1 == currentlist[index].Item1 && x.Item2 == currentlist[index].Item2 + 1
                   || x.Item1 == currentlist[index].Item1 && x.Item2 == currentlist[index].Item2 - 1
                   || x.Item1 == currentlist[index].Item1 + 1 && x.Item2 == currentlist[index].Item2
                   || x.Item1 == currentlist[index].Item1 - 1 && x.Item2 == currentlist[index].Item2);
        }
        else
        {
            List<(int, int)> temp = new List<(int, int)>();
            temp.AddRange(currentlist);
            finalResult.Add(temp);
            currentlist.Clear();
            index = 0;
        }

    }
    //first item is a seperate garden
    if (found.Count == 0 && index == 0)
    {
        if (currentlist.Count > 0)
        {
            List<(int, int)> temp = new List<(int, int)>();
            temp.AddRange(currentlist);
            finalResult.Add(temp);
            currentlist.Clear();
        }
        // first item removed, so we start again after incrementing by one the minus 1, at 0 element
        index = -1;
    }
    if (found.Count > 0)
    {
        currentlist.AddRange(found);
        Originallist.RemoveAll(x => found.Exists(y => x == y));

    }
    index++;

    return GetGardens(Originallist, currentlist, finalResult, index);
}