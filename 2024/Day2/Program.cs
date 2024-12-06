var lines = File.ReadLines(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd2.txt")).ToList();
List<List<int>> list = new List<List<int>>();
var count = lines.Count;
for (int j = 0; j < count; j++)
{
    var res = lines[j].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var listitem = new List<int>();
    for (int i = 0; i < res.Count(); i++)
    {
        listitem.Add(int.Parse(res[i]));
    }
    list.Add(listitem);
}
int actualValue = -1;
var sum = 0;
bool IsAsc = false;
bool isValid = false;
foreach (var item in list)
{
    for (int i = 0; i < item.Count(); i++)
    {
        if (i == 0)
        {
            actualValue = item[i];
        }
        else if (i == 1)
        {
            if (actualValue < item[i] && actualValue + 4 > item[i])
            {
                IsAsc = true;
                isValid = true;
            }
            else if (actualValue > item[i] && actualValue - 4 < item[i])
            {
                IsAsc = false;
                isValid = true;
            }
            if (!isValid) break;
            actualValue = item[i];
        }
        else
        {
            if ((!IsAsc && actualValue > item[i] && actualValue - 4 < item[i]) ||
                    (IsAsc && actualValue < item[i] && actualValue + 4 > item[i]))
            {
                actualValue = item[i];
            }
            else break;
            //last item
            if (i + 1 == item.Count())
            {
                sum++;
            }
        }
    }
}
Console.WriteLine($"Szumma 2A:{sum}");
bool FoundOneOk = false;
sum = 0;
foreach (var item in list)
{
    FoundOneOk = false;
    for (int k = 0; k <= item.Count(); k++)
    {
        if (FoundOneOk)
        {
            sum++; break;
        }
        var temp = new List<int>();

        for (int g = 0; g < item.Count(); g++)
        {
            if (k == item.Count)
            {
                temp.Add(item[g]);
            }
            else if (k != g)
            {
                temp.Add(item[g]);
            }
        }
        isValid = false;

        for (int i = 0; i < temp.Count(); i++)
        {
            if (i == 0)
            {
                actualValue = temp[i];
            }
            else if (i == 1)
            {
                if (actualValue < temp[i] && actualValue + 4 > temp[i])
                {
                    IsAsc = true;
                    isValid = true;
                }
                else if (actualValue > temp[i] && actualValue - 4 < temp[i])
                {
                    IsAsc = false;
                    isValid = true;
                }
                if (!isValid) break;
                actualValue = temp[i];
            }
            else
            {
                if ((!IsAsc && actualValue > temp[i] && actualValue - 4 < temp[i]) ||
                        (IsAsc && actualValue < temp[i] && actualValue + 4 > temp[i]))
                {
                    actualValue = temp[i];
                }
                else break;
                //last temp
                if (i + 1 == temp.Count())
                {
                    FoundOneOk = true;
                }
            }
        }
    }
}
Console.WriteLine($"Szumma 2B:{sum}");
Console.ReadKey();