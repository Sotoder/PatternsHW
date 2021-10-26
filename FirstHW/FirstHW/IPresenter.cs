namespace FirstHW
{
    public interface IPresenter
    {
        public void SetNumber(int number);
        public void Exit();
        public (int factorial, int sum, int maxEvenNumber) GetCalculationResult();
    }
}