using System;

namespace FirstHW
{
    class App: IApp, IPresenter
    {
        private View _view;
        private Data _data;
        public App()
        {
            _view = new View(this);
            _data = new Data();
        }

        public void StartApp()
        {
            _view.ShowConsole();
        }

        public void SetNumber(int number)
        {
            _data.number = number;
        }

        public (int factorial, int sum, int maxEvenNumber) GetCalculationResult()
        {
            return _data.CalculateResult();
        }
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}
