int columns = 10;
int rows = 10;

bool[,] matrix = new bool[rows, columns];

int[,] mapMatrix = new int[rows + 1, columns + 1];

var random = new Random();

var rightUpperCorner = new SquarePointer();

for (int i = 0; i < rows; i++)
{
    for (int j = 0; j < columns; j++)
    {
        var val = random.Next(0, 2);
        matrix[i, j] = val == 1;
        mapMatrix[i, j] = val;
    }
}

for(int i = 0; i < rows; i++)
{
    for(int j = 0; j < columns; j++)
    {
        if (matrix[i, j])
        {
            var min = new int[] { mapMatrix[i + 1, j + 1], mapMatrix[i, j + 1], mapMatrix[i + 1, j] }.Min();
            mapMatrix[i, j] = min + 1;
            if (mapMatrix[i, j] > rightUpperCorner.EdgeLength)
            {
                rightUpperCorner.EdgeLength = mapMatrix[i, j];
                rightUpperCorner.X = i;
                rightUpperCorner.Y = j;
            }
        }
    }
}

var xRange = Enumerable.Range(rightUpperCorner.X, rightUpperCorner.EdgeLength);
var yRange = Enumerable.Range(rightUpperCorner.Y, rightUpperCorner.EdgeLength);

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

Console.WriteLine($"Largest square edge length: {rightUpperCorner.EdgeLength}, square: {rightUpperCorner.EdgeLength * rightUpperCorner.EdgeLength}" +
    $"{Environment.NewLine}" +
    $"X: {rightUpperCorner.X}, Y: {rightUpperCorner.Y}");

class SquarePointer
{
    public int X { get; set; }
    public int Y { get; set; }
    public int EdgeLength { get; set; }
}