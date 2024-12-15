// See https://aka.ms/new-console-template for more information
//Button A - 3 tokens
//Button B - 1 token
using System.Text.RegularExpressions;
string postfix;
var test = false;
if (test)
{
    postfix = "test";
}
else postfix = "";
var input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory() + $"..\\..\\..\\..\\..\\Inputs\\aocd13{postfix}.txt"));
var InputRegex = new Regex("Button\\s*A:\\s*X\\+(?<ButtonAX>\\d+)\\s*,\\s*Y\\+(?<ButtonAY>\\d+)\\s*" +
    "Button\\s*B:\\s*X\\+(?<ButtonBX>\\d+)\\s*,\\s*Y\\+(?<ButtonBY>\\d+)\\s*Prize:\\s*X=(?<PrizeX>\\d+),\\s*Y=(?<PrizeY>\\d+)");
var matches = InputRegex.Matches(input);
int sum = 0;
int round = 0;
Dictionary<Match, (int, int)> result = new Dictionary<Match, (int, int)>();
foreach (Match match in matches)
{
    int ButtonAX = int.Parse(match.Groups["ButtonAX"].Value);
    int ButtonAY = int.Parse(match.Groups["ButtonAY"].Value);
    int ButtonBX = int.Parse(match.Groups["ButtonBX"].Value); 
    int ButtonBY = int.Parse(match.Groups["ButtonBY"].Value);
    int PrizeX = (int)int.Parse(match.Groups["PrizeX"].Value); 
    int PrizeY = (int)int.Parse(match.Groups["PrizeY"].Value);
    int MaxButtonB = Math.Max(PrizeX / ButtonBX, PrizeY / ButtonBY);
    int MaxButtonA = Math.Max(PrizeX / ButtonAX, PrizeY / ButtonAY);
    int ButtonBPressed = 0;
    int ButtonAPressed = 0;
    var found = false;
    for (int i = MaxButtonB; i >= 0; i--)
    {
        int tempBX = i * ButtonBX;
        int tempBY = i * ButtonBY;
        ButtonBPressed = i;
        for (int j = 0; j < MaxButtonA; j++)
        {
            int tempAX = j * ButtonAX;
            int tempAY = j * ButtonAY;
            ButtonAPressed = j;
            if (PrizeX == tempAX + tempBX && PrizeY == tempAY + tempBY)
            {
                found = true;
                break;
            }
            if (PrizeX < tempAX + tempBX || PrizeY < tempAY + tempBY)
            {
                //went too far :)
                break;
            }
        }
        if (found) break;
    }
    if (found)
    {
        Console.WriteLine($"{++round}");
        sum += (int)(ButtonAPressed * 3 + ButtonBPressed);
        //result.Add(match, (ButtonAPressed, ButtonBPressed));
    }
    result.Add(match, (ButtonAPressed, ButtonBPressed));
}
Console.WriteLine($"13A:{sum}");
Dictionary<Match, (ulong, ulong)> result2 = new Dictionary<Match, (ulong, ulong)>();
round = 0;
sum = 0;
foreach (var item in result)
{
    ulong ButtonAX = ulong.Parse(item.Key.Groups["ButtonAX"].Value);
    ulong ButtonAY = ulong.Parse(item.Key.Groups["ButtonAY"].Value);
    ulong ButtonBX = ulong.Parse(item.Key.Groups["ButtonBX"].Value);
    ulong ButtonBY = ulong.Parse(item.Key.Groups["ButtonBY"].Value);
    ulong PrizeX = 10000000000000+ (ulong)int.Parse(item.Key.Groups["PrizeX"].Value); ;
    ulong PrizeY = 10000000000000 + (ulong)int.Parse(item.Key.Groups["PrizeY"].Value); ; ;
    ulong MaxButtonB = Math.Max(PrizeX / ButtonBX, PrizeY / ButtonBY);
    ulong MaxButtonA = Math.Max(PrizeX / ButtonAX, PrizeY / ButtonAY);
    ulong ButtonBPressed = 0;
    ulong ButtonAPressed = 0;
    var found = false;
    for (ulong i = MaxButtonB; i > 0; i--)
    {
        ulong tempBX = i * ButtonBX;
        ulong tempBY = i * ButtonBY;
        ButtonBPressed = i;
        if ((long)PrizeX - (long)tempBX < 0 || (long)PrizeY - (long)tempBY < 0) continue;
        var MaxButtonAMin = Math.Min((PrizeX - tempBX) / ButtonAX, (PrizeY - tempBY) / ButtonAY);
        var MaxButtonAMax = Math.Max((PrizeX - tempBX) / ButtonAX, (PrizeY - tempBY) / ButtonAY);
        for (ulong j = MaxButtonAMin; j <= MaxButtonAMax; j++)
        {
            ulong tempAX = j * ButtonAX;
            ulong tempAY = j * ButtonAY;
            ButtonAPressed = j;
            if ( PrizeY % (tempAY + tempBY)==0 && PrizeX % (tempAX + tempBX) == 0 && PrizeX / (tempAX + tempBX)== PrizeY / (tempAY + tempBY))
            { 
                var value = tempAX + tempBX;
                found = true;
                break;
            }
            //if (PrizeX < tempAX + tempBX || PrizeY < tempAY + tempBY)
            //{
            //    //went too far :)
            //    break;
            //}
        }
        if (found) break;
    }
    Console.WriteLine($"{++round}");
    if (found)
    {
        sum += (int)(ButtonAPressed * 3 + ButtonBPressed);
        result2.Add(item.Key, (((ulong)ButtonAPressed*(ulong)item.Value.Item1), (ulong)ButtonBPressed * (ulong)item.Value.Item2));
    }
}
ulong sum2 = 0;
foreach (var item in result2)
{
    sum2 += (ulong)(item.Value.Item1 * 3 + item.Value.Item2);
}

Console.WriteLine($"13B:{sum2}");