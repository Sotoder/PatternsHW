namespace FirstHW
{
    class Data
    {
        public int number;

        public (int factorial, int sum, int maxEvenNumber) CalculateResult()
        {
            int factorial = 1; 
            int sum = 0;
            int maxEvenNumber = 0;

            for (int i = 1; i <= number; i++)
            {
                factorial = factorial * i;
                sum = sum + i;
                if (i % 2 == 0)
                {
                    maxEvenNumber = i;
                }
            }
            return (factorial, sum, maxEvenNumber);
        }
    }
}
