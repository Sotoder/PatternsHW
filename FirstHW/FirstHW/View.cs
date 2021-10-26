using System;

namespace FirstHW
{
    class View
    {
        private IPresenter _app;

        public View(IPresenter app)
        {
            this._app = app;
        }

        public void ShowConsole()
        {
            Console.WriteLine("Здравствуйте, вас приветствует математическая программа.");
            Console.WriteLine("Пожалуйста введите целое положительное число.");
            Console.WriteLine("Для выхода нажмите Q.");

            GetNumber();
        }

        private void GetNumber()
        {
            var number = GetInput();
            _app.SetNumber(number);
            ShowResult(number);
        }

        private void ShowResult(int number)
        {
            var result = _app.GetCalculationResult();

            Console.Clear();
            Console.WriteLine($"\nФакториал равен: {result.factorial}");
            Console.WriteLine($"Сума от 1 до {number} равна: {result.sum}");
            Console.WriteLine($"Максимальное четное число меньше {number} равно: {result.maxEvenNumber}");
            Console.ReadLine();
        }

        private int GetInput()
        {
            var inputString = Console.ReadLine();
            var isInputSuccess = false;
            int result = 0;

            while (!isInputSuccess)
            {
                if (inputString.Equals("q"))
                {
                    _app.Exit();
                }
                else if (Int32.TryParse(inputString, out result))
                {
                    isInputSuccess = true;
                }
                else
                {
                    Console.WriteLine("Необходимо ввести целое число или Q для выхода.\nПовторите ввод");
                    inputString = Console.ReadLine();
                }
            }
            return result;
        }
    }
}
