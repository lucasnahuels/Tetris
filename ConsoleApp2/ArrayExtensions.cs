public static class ArrayExtensions
{
    public static int[,] Reshape(this int[] array, int rows, int cols)
    {
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = array[i * cols + j];
            }
        }
        return result;
    }
}
