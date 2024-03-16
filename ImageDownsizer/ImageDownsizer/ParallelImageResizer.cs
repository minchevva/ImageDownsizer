namespace ImageDownsizer
{
    public class ParallelImageResizer
    {  
        public static Color[][] ScaleDownWithInterpolation(Color[][] sourcePixels, double reductionFactor)
        {
            int resultWidth = (int)(sourcePixels[0].Length * reductionFactor);
            int resultHeight = (int)(sourcePixels.Length * reductionFactor);

            var newPixels = new Color[resultHeight][];

            int cpuCount = Environment.ProcessorCount; 
            var processingThreads = new List<Thread>();

            for (int index = 0; index < cpuCount; index++)
            {
                int segmentStart = index * (resultHeight / cpuCount);
                int segmentEnd = (index == cpuCount - 1) ? resultHeight : (index + 1) * (resultHeight / cpuCount);

                Thread worker = new Thread(() =>
                {
                    for (int y = segmentStart; y < segmentEnd; y++)
                    {
                        newPixels[y] = new Color[resultWidth];
                        for (int x = 0; x < resultWidth; x++)
                        {
                            double sourceCol = x / reductionFactor;
                            double sourceRow = y / reductionFactor;

                            int leftBound = (int)Math.Floor(sourceCol);
                            int topBound = (int)Math.Floor(sourceRow);

                            int rightBound = Math.Min(leftBound + 1, sourcePixels[0].Length - 1);
                            int bottomBound = Math.Min(topBound + 1, sourcePixels.Length - 1);

                            double colProximity = sourceCol - leftBound;
                            double rowProximity = sourceRow - topBound;

                            Color interpolatedPixel = InterpolatePixelColors(
                                sourcePixels[topBound][leftBound],
                                sourcePixels[topBound][rightBound],
                                sourcePixels[bottomBound][leftBound],
                                sourcePixels[bottomBound][rightBound],
                                colProximity,
                                rowProximity
                            );

                            newPixels[y][x] = interpolatedPixel;
                        }
                    }
                });
                processingThreads.Add(worker);
            }

          
            processingThreads.ForEach(worker => worker.Start());
            processingThreads.ForEach(worker => worker.Join());

            return newPixels;
        }

        private static Color InterpolatePixelColors(Color cornerTopLeft, Color cornerTopRight, Color cornerBottomLeft, Color cornerBottomRight, double proximityX, double proximityY)
        {
            int alpha = CalculateInterpolatedValue(cornerTopLeft.A, cornerTopRight.A, cornerBottomLeft.A, cornerBottomRight.A, proximityX, proximityY);
            int red = CalculateInterpolatedValue(cornerTopLeft.R, cornerTopRight.R, cornerBottomLeft.R, cornerBottomRight.R, proximityX, proximityY);
            int green = CalculateInterpolatedValue(cornerTopLeft.G, cornerTopRight.G, cornerBottomLeft.G, cornerBottomRight.G, proximityX, proximityY);
            int blue = CalculateInterpolatedValue(cornerTopLeft.B, cornerTopRight.B, cornerBottomLeft.B, cornerBottomRight.B, proximityX, proximityY);

            return Color.FromArgb(alpha, red, green, blue);
        }

        private static int CalculateInterpolatedValue(int topLeftValue, int topRightValue, int bottomLeftValue, int bottomRightValue, double weightX, double weightY)
        {
            double topInterpolation = WeightedAverage(topLeftValue, topRightValue, weightX);
            double bottomInterpolation = WeightedAverage(bottomLeftValue, bottomRightValue, weightX);
            return (int)WeightedAverage(topInterpolation, bottomInterpolation, weightY);
        }

        private static double WeightedAverage(double value1, double value2, double weight)
        {
            return value1 * (1 - weight) + value2 * weight;
        }
    }
}
