namespace ImageDownsizer
{
   public class Resizer
    { 
        public static Color[][] TransformImage(Color[][] sourceColors, double resizeFactor)
        {
            int targetWidth = (int)(sourceColors[0].Length * resizeFactor);
            int targetHeight = (int)(sourceColors.Length * resizeFactor);

            var transformedColors = new Color[targetHeight][];

            for (int verticalPos = 0; verticalPos < targetHeight; verticalPos++)
            {
                transformedColors[verticalPos] = new Color[targetWidth];

                for (int horizontalPos = 0; horizontalPos < targetWidth; horizontalPos++)
                {
                    double originalColumn = horizontalPos / resizeFactor;
                    double originalRow = verticalPos / resizeFactor;

                    int leftColumn = (int)Math.Floor(originalColumn);
                    int topRow = (int)Math.Floor(originalRow);

                    int rightColumn = Math.Min(leftColumn + 1, sourceColors[0].Length - 1);
                    int bottomRow = Math.Min(topRow + 1, sourceColors.Length - 1);

                    double columnFraction = originalColumn - leftColumn;
                    double rowFraction = originalRow - topRow;

                    Color interpolated = InterpolateColors(
                        sourceColors[topRow][leftColumn],     
                        sourceColors[topRow][rightColumn],   
                        sourceColors[bottomRow][leftColumn],  
                        sourceColors[bottomRow][rightColumn],
                        columnFraction,
                        rowFraction
                    );

                    transformedColors[verticalPos][horizontalPos] = interpolated;
                }
            }

            return transformedColors;
        }

        private static Color InterpolateColors(Color upperLeft, Color upperRight, Color lowerLeft, Color lowerRight, double colFraction, double rowFraction)
        {
            int alphaChannel = Mix(upperLeft.A, upperRight.A, lowerLeft.A, lowerRight.A, colFraction, rowFraction);
            int redChannel = Mix(upperLeft.R, upperRight.R, lowerLeft.R, lowerRight.R, colFraction, rowFraction);
            int greenChannel = Mix(upperLeft.G, upperRight.G, lowerLeft.G, lowerRight.G, colFraction, rowFraction);
            int blueChannel = Mix(upperLeft.B, upperRight.B, lowerLeft.B, lowerRight.B, colFraction, rowFraction);

            return Color.FromArgb(alphaChannel, redChannel, greenChannel, blueChannel);
        }

        private static int Mix(int upperLeftValue, int upperRightValue, int lowerLeftValue, int lowerRightValue, double colWeight, double rowWeight)
        {
            double upperAvg = WeightedAverage(upperLeftValue, upperRightValue, colWeight);
            double lowerAvg = WeightedAverage(lowerLeftValue, lowerRightValue, colWeight);
            return (int)WeightedAverage(upperAvg, lowerAvg, rowWeight);
        }

        private static double WeightedAverage(double startValue, double endValue, double weight)
        {
            return startValue + (endValue - startValue) * weight;
        }
    }
}
