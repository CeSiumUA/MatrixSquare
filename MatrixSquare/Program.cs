int columns = 40;
int rows = 40;

bool[,] matrix = new bool[rows, columns];

int[,] mapMatrix = new int[rows + 1, columns + 1];

var random = new Random();

var rightBottomCorner = new SquarePointer();

for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < columns; j++)
    {
        var val = random.Next(0, 2);
        matrix[i, j] = val == 1;
    }
}

for(int i = 1; i <= rows; i++)
{
    for(int j = 1; j <= columns; j++)
    {
        if (matrix[i - 1, j - 1])
        {
            var min = new int[] { mapMatrix[i - 1, j - 1], mapMatrix[i, j - 1], mapMatrix[i - 1, j] }.Min();
            mapMatrix[i, j] = min + 1;
            if (mapMatrix[i, j] > rightBottomCorner.EdgeLength)
            {
                rightBottomCorner.EdgeLength = mapMatrix[i, j];
                rightBottomCorner.X = i;
                rightBottomCorner.Y = j;
            }
        }
    }
}

var xRange = Enumerable.Range(rightBottomCorner.X - rightBottomCorner.EdgeLength, rightBottomCorner.EdgeLength);
var yRange = Enumerable.Range(rightBottomCorner.Y - rightBottomCorner.EdgeLength, rightBottomCorner.EdgeLength);

for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < columns; j++)
    {
        var val = matrix[i, j];
        Console.BackgroundColor = (val && xRange.Contains(i) && yRange.Contains(j))
            ? ConsoleColor.White
            : ConsoleColor.Black;

        Console.ForegroundColor = val ? ConsoleColor.Green : ConsoleColor.Red;
        Console.Write($"{(val ? 1 : 0)}{(j == columns - 1 ? string.Empty : " ")}");
        Console.ForegroundColor = ConsoleColor.White;
    }
    Console.WriteLine();
}

Console.WriteLine($"Largest square edge length: {rightBottomCorner.EdgeLength}, square: {rightBottomCorner.EdgeLength * rightBottomCorner.EdgeLength}" +
    $"{Environment.NewLine}" +
    $"X: {rightBottomCorner.X}, Y: {rightBottomCorner.Y}");

Console.WriteLine("Write something to exit...");
Console.ReadKey();

class SquarePointer
{
    public int X { get; set; }
    public int Y { get; set; }
    public int EdgeLength { get; set; }
}