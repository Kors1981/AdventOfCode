using System.Text;
using System.Text.RegularExpressions;

var test = false;
var input = test ? File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd9test.txt"))
    : File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd9.txt"));
var isFile = true;
StringBuilder stringBuilder = new StringBuilder();
List<int> lines = new List<int>();
for (int i = 0; i < input.Length; i++)
{
    isFile = (i % 2 == 0);
    var count = int.Parse(input[i].ToString());
    if (isFile)
    {
        for (int j = 0; j < count; j++)
        {
            lines.Add((i / 2));
        }
    }
    else
    {
        for (int j = 0; j < count; j++)
        {
            lines.Add(-1);
        }
    }
}
var length = stringBuilder.Append(lines.AsEnumerable());
var temp = stringBuilder.ToString().ToCharArray();
var matces = new Regex($"\\.").Matches(stringBuilder.ToString());
List<int> emptyPlaces=new List<int>();
List<int> emptyPlacesWithLength = new List<int>();
var previousNumber = -1;
for (int i = 0; i < lines.Count; i++)
{

    if (lines[i] ==-1)
    {
        if (previousNumber==-1)
        {
            previousNumber = i;
        }
        if (previousNumber!=i)
        {

        }
        emptyPlaces.Add(i);
    }
}
var EmptyPointer = 0;
Console.WriteLine("Initial:" + stringBuilder.ToString());
//for (int i = lines.Count - 1; i >= 0; i--)
//{
//    if (lines[i] != ".")
//    {
//        if (emptyPlaces.Count != EmptyPointer + 1)
//        {
//            var pointer = emptyPlaces[EmptyPointer++];
//            if (pointer > i) 
//                break;
//            lines[pointer] = lines[i];
//            lines[i] = ".";
//        }
//        else
//        {
//            break;
//        }
//    }
//}
//decimal checksum = 0;
//for (int i = 0; lines[i] != "."; i++)
//{
//    checksum = checksum + decimal.Parse(lines[i].ToString()) * (decimal)i;
//}

 
var max = lines.Max();
for (int i = max ; i >= 0; i--)
{
    var currentFileLength=lines.Where(x => x == i).Count();
    bool ok=true;
    var index = -1;
    for (int running = 0; running < lines.Count - currentFileLength; running++)
    {

        ok= true;
        for (int j = 0; j < currentFileLength; j++)
        {
            if (lines[j+running] != -1)
            {
                ok = false;
                break;
            }
        }
        if (ok)
        {
            index = running;
            break;
        }
    }
    if (ok)
    {
        var firstIndex = lines.IndexOf(i);
        if (index < firstIndex)
        {
            for (int j = 0; j < currentFileLength; j++)
            {
                lines[index + j] = i;
                lines[firstIndex + j] = -1;
            }
        }
        //EmptyPointer += currentFileLength;
    }

}
decimal checksum = 0;
for (int i = 0; i<lines.Count; i++)
{
    if (lines[i]!=-1)
    checksum = checksum + decimal.Parse(lines[i].ToString()) * i;
}
Console.WriteLine("Ordered:" + stringBuilder.ToString());
Console.WriteLine("9B:" + checksum.ToString());
Console.ReadLine();