using System;
using System.Windows.Forms;

namespace Match3
{


    static class Program // сама программа
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu()); // запуск меню
        }


    }

    public class Cell : PictureBox // Клетка. Из таких клеток состоит игровое поле.
    {
        protected int x = 0; // номер клетки по оси OX
        protected int y = 0; // номер клетки по оси OY
        protected string ContainedFigure = "Empty"; // фигура, которая содержится в данной клетке
        protected bool isEnableToTheUser = true; // может ли тыкнуть на клетку игрок
        protected bool isNeedsToDelete = false; // помечает клетку, которую надо будет удалить

        // ниже идут геттеры и сеттеры для переменных

        public void SetX(int number)
        {
            this.x = number;
        }
        public void SetY(int number)
        {
            this.y = number;
        }
        public void SetContainedFigure(string str)
        {
            this.ContainedFigure = str;
        }
        public void SetIsEnableToTheUser(bool boolean)
        {
            this.isEnableToTheUser = boolean;
        }
        public void SetIsNeedsToDelete(bool boolean)
        {
            this.isEnableToTheUser = boolean;
        }
        public int GetX()
        {
            return this.x;
        }
        public int GetY()
        {
            return this.y;
        }
        public string GetContainedFigure()
        {
            return this.ContainedFigure;

        }
        public bool GetIsEnableToTheUser()
        {
            return this.isEnableToTheUser;
        }
        public bool GetIsNeedsToDelete()
        {
            return this.isEnableToTheUser;
        }

    }


}
