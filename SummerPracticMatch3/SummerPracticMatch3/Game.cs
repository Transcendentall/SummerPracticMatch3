using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Match3
{
    public partial class Game : Form // сама игра "три-в-ряд"
    {

        const byte GameMatrixSize = 8; // размер игрового поля
        private Cell[,] _CellsMatrix = new Cell[GameMatrixSize, GameMatrixSize];  // матрица, наше игровое поле
        Random rnd = new Random(); // рандом. Нужен будет для генерации рандомных фигур.

        const int TimeLapseBetweenActionsInMilliseconds = 100; // промежуток между действиями
        const int TimeLapseGravitationInMilliseconds = 1; // промежуток между падением фигур

        System.Media.SoundPlayer GameSoundtrack; // музыка, воспроизводящаяся во время игры
        System.Media.SoundPlayer EndSound; // финальный звук; воспроизводится, когда заканчивается время

        int Timer = 60; // время игры
        int ScorePoints = 0; // сколько очков заработал игрок

        Cell SelectedCell1 = null; // выделенные мышкой фигуры
        Cell SelectedCell2 = null;




        private void LabelIntNumberOfScorePoints_Click(object sender, EventArgs e) // тут игрок видит, сколько очков он заработал
        {
            LabelIntNumberOfScorePoints.Text = Convert.ToString(ScorePoints);
        }

        public void EnableAllFigures()  // разрешает пользователю производить манипуляции с фигурами
        {
            for (int i = 0; i < GameMatrixSize; i++)
            {
                for (int j = 0; j < GameMatrixSize; j++)
                {
                    _CellsMatrix[i, j].SetIsEnableToTheUser(true);
                }
            }
        }

        public void DisableAllFigures()  // запрещает пользователю производить манипуляции с фигурами
        {
            for (int i = 0; i < GameMatrixSize; i++)
            {
                for (int j = 0; j < GameMatrixSize; j++)
                {
                    _CellsMatrix[i, j].SetIsEnableToTheUser(false);
                }
            }
        }

        public void VisualisateFigure(Cell bl)  // отрисовывает фигуру в зависимости от того, что это за фигура
        {
            string n = bl.GetContainedFigure();
            switch (n)
            {
                case "Empty":
                    bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackground;
                    bl.Image = null;
                    break;
                case "Figure1":
                    bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackground;
                    bl.Image = global::SummerPracticMatch3.Properties.Resources.Figure1WithoutBackground;
                    break;
                case "Figure2":
                    bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackground;
                    bl.Image = global::SummerPracticMatch3.Properties.Resources.Figure2WithoutBackground;
                    break;
                case "Figure3":
                    bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackground;
                    bl.Image = global::SummerPracticMatch3.Properties.Resources.Figure3WithoutBackground;
                    break;
                case "Figure4":
                    bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackground;
                    bl.Image = global::SummerPracticMatch3.Properties.Resources.Figure4WithoutBackground;
                    break;
                case "Figure5":
                    bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackground;
                    bl.Image = global::SummerPracticMatch3.Properties.Resources.Figure5WithoutBackground;
                    break;
                default:
                    bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackground;
                    bl.Image = null;
                    break;
            }
        }

        public void SetCellBackgroundGreen(Cell bl)  // устанавливает зелёный фон клетки (используется, когда всё хорошо)
        {
            bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackgroundGood;
        }

        public void SetCellBackgroundYellow(Cell bl)  // устанавливает жёлтый фон клетки (используется, когда после свапа нечего уничтожать)
        {
            bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackgroundNotMatch;
        }


        public void SetCellBackgroundRed(Cell bl)  // устанавливает красный фон клетки (используется, когда выделенные пользователем иконки не соседние)
        {
            bl.BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackgroundNotNeighbor;
        }

        public void GenerateFigure(Cell bl) // рандомная генерация фигуры в клетке
        {
            int value = rnd.Next(1, 6);
            switch (value) // в зависимости от выпавшего числа, генерируется соответствующая фигура
            {
                case 1:
                    bl.SetContainedFigure("Figure1");
                    break;
                case 2:
                    bl.SetContainedFigure("Figure2");
                    break;
                case 3:
                    bl.SetContainedFigure("Figure3");
                    break;
                case 4:
                    bl.SetContainedFigure("Figure4");
                    break;
                case 5:
                    bl.SetContainedFigure("Figure5");
                    break;
            }
            VisualisateFigure(bl); // показывает фигуру в клетке
        }

        public bool FigureDestructionCheck() // проверяет, есть ли чего разрушать, то бишь есть ли >=3 одинаковых фигур подряд где-нибудь на поле
        {
            bool CellsMustBeDestroyed = false; // по умолчанию считаем, что разрушать нечего
            for (int j = 0; j < GameMatrixSize; j++) // ищем по горизонали три в ряд
            {
                for (int i = 0; i < GameMatrixSize; i++)
                {
                    int a = i;
                    int count = 1;

                    while (((a + 1) < GameMatrixSize) && (_CellsMatrix[a, j].GetContainedFigure() == _CellsMatrix[a + 1, j].GetContainedFigure())) 
                        // бегаем и ищем места, где идут подряд несколько фигур
                    {
                        ++a;
                        ++count;
                    }
                    if (count >= 3) // если находим три в ряд фигуры, то уничтожать нам есть чего
                    {
                        CellsMustBeDestroyed = true;
                    }
                }
            }

            for (int j = 0; j < GameMatrixSize; j++) // ищем по вертикали три в ряд. Всё аналогично
            {
                for (int i = 0; i < GameMatrixSize; i++)
                {
                    int a = i;
                    int count = 1;

                    while (((a + 1) < GameMatrixSize) && (_CellsMatrix[j, a].GetContainedFigure() == _CellsMatrix[j, a + 1].GetContainedFigure()))
                    {
                        ++a;
                        ++count;
                    }
                    if (count >= 3)
                    {
                        CellsMustBeDestroyed = true;
                    }
                }
            }

            return CellsMustBeDestroyed; // возвращаем, существует ли три в ряд сейчас

        }

        public void FigureDestruction() // разрушаем все случаи три и более в ряд
        {
            for (int j = 0; j < GameMatrixSize; j++) // проводим зачистку по горизонтали
            {
                for (int i = 0; i < GameMatrixSize; i++)
                {
                    int a = i;
                    int count = 1;

                    while (((a + 1) < GameMatrixSize) && (_CellsMatrix[a, j].GetContainedFigure() == _CellsMatrix[a + 1, j].GetContainedFigure()))
                    {
                        ++a;
                        ++count;
                    }
                    if (count >= 3)  // если есть матч, т.е. хотя бы 3 в ряд
                    {
                        for (int y = i; y < (i + count); y++)
                        {
                            if (_CellsMatrix[y, j].GetContainedFigure() != "Empty")
                            {
                                _CellsMatrix[y, j].SetContainedFigure("Empty");
                                VisualisateFigure(_CellsMatrix[y, j]);
                                ++ScorePoints;  // за каждую уничтоженную фигуру игрок получает +1 очко
                                LabelIntNumberOfScorePoints.Text = Convert.ToString(ScorePoints);  // вывод очков на экран
                            }
                        }
                    }
                }
            }

            for (int j = 0; j < GameMatrixSize; j++) // проводим зачистку по вертикали; всё аналогично
            {
                for (int i = 0; i < GameMatrixSize; i++)
                {
                    int a = i;
                    int count = 1;

                    while (((a + 1) < GameMatrixSize) && (_CellsMatrix[j, a].GetContainedFigure() == _CellsMatrix[j, a + 1].GetContainedFigure()))
                    {
                        ++a;
                        ++count;
                    }
                    if (count >= 3)  // если есть матч, т.е. хотя бы 3 в ряд
                    {
                        for (int y = i; y < (i + count); y++)
                        {
                            if (_CellsMatrix[j, y].GetContainedFigure() != "Empty")
                            {
                                _CellsMatrix[j, y].SetContainedFigure("Empty");
                                VisualisateFigure(_CellsMatrix[j, y]);
                                ++ScorePoints;  // за каждую уничтоженную фигуру игрок получает +1 очко
                                LabelIntNumberOfScorePoints.Text = Convert.ToString(ScorePoints);  // вывод очков на экран
                            }
                        }
                    }
                }
            }
        }

        public void FigureDestuctionAndFall() // мгновенно разрушает все матчи и опускает после этого все фигуры опускает на
                                                     // самый низ, если под ними ячейки пусты (т.е. содержат "Empty"); действует пока есть
                                                     // что разрушать
        {
            while (FigureDestructionCheck())
            {
                FigureDestruction();
                FiguresFallByMax();
            }
        }

        public void FiguresFallByMax() // мгновенно опускает все фигуры на самый низ, если под ними ячейки пусты (т.е. содержат "Empty")
        {

            for (int t = 0; t < GameMatrixSize; t++)
            {
                FiguresFallBy1();
            }
        }

        public void FiguresFallBy1() // мгновенно опускает все фигуры на ряд ниже, если под ними ячейки пусты (т.е. содержат "Empty")
        {
            for (int i = (GameMatrixSize - 1); i >= 0; i--)
            {
                for (int j = 1; j < GameMatrixSize; j++)
                {
                    if (_CellsMatrix[i, j].GetContainedFigure() == "Empty")
                    {
                        CellSwap(_CellsMatrix[i, j], _CellsMatrix[i, j - 1]);
                    }
                }
            }

            for (int i = 0; i < GameMatrixSize; i++)
            {
                if (_CellsMatrix[i, 0].GetContainedFigure() == "Empty")
                {
                    GenerateFigure(_CellsMatrix[i, 0]);
                    VisualisateFigure(_CellsMatrix[i, 0]);
                }
            }

        }

        public Image FigureMoveToUp(Cell b) // возвращает изображение фигуры, идущей вверх, в зависимости от того, что это за фигура
        {
            string n = b.GetContainedFigure();
            switch (n)
            {
                case "Figure1":
                    return global::SummerPracticMatch3.Properties.Resources.Figure1Up;
                case "Figure2":
                    return global::SummerPracticMatch3.Properties.Resources.Figure2Up;
                case "Figure3":
                    return global::SummerPracticMatch3.Properties.Resources.Figure3Up;
                case "Figure4":
                    return global::SummerPracticMatch3.Properties.Resources.Figure4Up;
                case "Figure5":
                    return global::SummerPracticMatch3.Properties.Resources.Figure5Up;
                default:
                    return global::SummerPracticMatch3.Properties.Resources.CellBackground;
            }
        }

        public Image FigureMoveToLeft(Cell b) // возвращает изображение фигуры, идущей влево, в зависимости от того, что это за фигура
        {
            string n = b.GetContainedFigure();
            switch (n)
            {
                case "Figure1":
                    return global::SummerPracticMatch3.Properties.Resources.Figure1Left;
                case "Figure2":
                    return global::SummerPracticMatch3.Properties.Resources.Figure2Left;
                case "Figure3":
                    return global::SummerPracticMatch3.Properties.Resources.Figure3Left;
                case "Figure4":
                    return global::SummerPracticMatch3.Properties.Resources.Figure4Left;
                case "Figure5":
                    return global::SummerPracticMatch3.Properties.Resources.Figure5Left;
                default:
                    return global::SummerPracticMatch3.Properties.Resources.CellBackground;
            }
        }
        public Image FigureMoveToDown(Cell b) // возвращает изображение фигуры, идущей вниз, в зависимости от того, что это за фигура
        {
            string n = b.GetContainedFigure();
            switch (n)
            {
                case "Figure1":
                    return global::SummerPracticMatch3.Properties.Resources.Figure1Down;
                case "Figure2":
                    return global::SummerPracticMatch3.Properties.Resources.Figure2Down;
                case "Figure3":
                    return global::SummerPracticMatch3.Properties.Resources.Figure3Down;
                case "Figure4":
                    return global::SummerPracticMatch3.Properties.Resources.Figure4Down;
                case "Figure5":
                    return global::SummerPracticMatch3.Properties.Resources.Figure5Down;
                default:
                    return global::SummerPracticMatch3.Properties.Resources.CellBackground;
            }
        }

        public Image FigureMoveToRight(Cell b) // возвращает изображение фигуры, идущей вправо, в зависимости от того, что это за фигура
        {
            string n = b.GetContainedFigure();
            switch (n)
            {
                case "Figure1":
                    return global::SummerPracticMatch3.Properties.Resources.Figure1Right;
                case "Figure2":
                    return global::SummerPracticMatch3.Properties.Resources.Figure2Right;
                case "Figure3":
                    return global::SummerPracticMatch3.Properties.Resources.Figure3Right;
                case "Figure4":
                    return global::SummerPracticMatch3.Properties.Resources.Figure4Right;
                case "Figure5":
                    return global::SummerPracticMatch3.Properties.Resources.Figure5Right;
                default:
                    return global::SummerPracticMatch3.Properties.Resources.CellBackground;
            }
        }

        public void CellSwap(Cell blo1, Cell blo2) // меняет местами две фигуры
        {
            string MergeBox2 = blo1.GetContainedFigure();
            blo1.SetContainedFigure(blo2.GetContainedFigure());
            blo2.SetContainedFigure(MergeBox2);

            VisualisateFigure(blo1);  // отображает новое содержимое фигур
            VisualisateFigure(blo2);

        }



        async public void ClickOnTheCell(object sender, System.EventArgs e) // нажатие на фигуру
        {
            LabelIntNumberOfScorePoints.Text = Convert.ToString(ScorePoints);

            if ((sender is Cell))  // если мы выделили клетку, а не абы что
            {

                if ((((Cell)sender).GetIsEnableToTheUser()) && (((Cell)sender).GetContainedFigure() != "Empty") && 
                    ((((Cell)sender) != SelectedCell1)))  // выделять дважды одну и ту же фигуру нельзя, пустые тоже нельзя трогать
                {
                    if (SelectedCell1 == null)
                    {
                        SelectedCell1 = ((Cell)sender);
                        SetCellBackgroundGreen(SelectedCell1);  // если проблем у выделенной фигуры нет, то она обводится зелёным
                    }
                    else
                    {
                        if ((SelectedCell1 != null) && (SelectedCell2 == null))
                        {
                            SelectedCell2 = ((Cell)sender);
                            SetCellBackgroundGreen(SelectedCell2);  // если две выбранные фигуры не имеют проблем и соседние, то они
                                                                    // обводятся зелёным
                        }
                    }


                    if ((SelectedCell1 != null) && (SelectedCell2 != null))   // если пользователь уже выделил две фигуры
                    {
                        if ((Math.Abs(SelectedCell1.GetX() - SelectedCell2.GetX()) > 1) || (Math.Abs(SelectedCell1.GetY()
                            - SelectedCell2.GetY()) > 1) || ((Math.Abs(SelectedCell1.GetX() - SelectedCell2.GetX()) == 1) 
                            && (Math.Abs(SelectedCell1.GetY() - SelectedCell2.GetY()) == 1)))   // если две выделенные фигуры не соседние
                        {
                            DisableAllFigures();  // если две фигуры выделены, то пользователь не может выделять что-то третье
                            SetCellBackgroundRed(SelectedCell1);  // если две выделенные фигуры не соседние, они обводятся красным
                            SetCellBackgroundRed(SelectedCell2);
                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);

                            VisualisateFigure(SelectedCell1);
                            VisualisateFigure(SelectedCell2);


                            SelectedCell1 = null;
                            SelectedCell2 = null;
                            EnableAllFigures();  // выделение сбрасывается, а пользователь может выделить новую пару фигур

                        }
                        else // если две выделенные фигуры являются соседними
                        {
                            DisableAllFigures();  // если две фигуры выделены, то пользователь не может выделять что-то третье
                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);


                            if ((SelectedCell1.GetX() < SelectedCell2.GetX())) // определяем координаты фигур относительно друг друга,
                                                                               // а потом запускаем анимацию их передвижения
                            {
                                await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                SelectedCell1.Image = FigureMoveToRight(SelectedCell1);
                                SelectedCell2.Image = FigureMoveToLeft(SelectedCell2);
                                await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                SelectedCell1.Image = FigureMoveToRight(SelectedCell2);
                                SelectedCell2.Image = FigureMoveToLeft(SelectedCell1);
                                await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                            }
                            else
                            {
                                if ((SelectedCell1.GetX() > SelectedCell2.GetX()))
                                {
                                    await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                    SelectedCell1.Image = FigureMoveToLeft(SelectedCell1);
                                    SelectedCell2.Image = FigureMoveToRight(SelectedCell2);
                                    await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                    SelectedCell1.Image = FigureMoveToLeft(SelectedCell2);
                                    SelectedCell2.Image = FigureMoveToRight(SelectedCell1);
                                    await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                }
                                else
                                {
                                    if ((SelectedCell1.GetY() < SelectedCell2.GetY()))
                                    {
                                        await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                        SelectedCell1.Image = FigureMoveToDown(SelectedCell1);
                                        SelectedCell2.Image = FigureMoveToUp(SelectedCell2);
                                        await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                        SelectedCell1.Image = FigureMoveToDown(SelectedCell2);
                                        SelectedCell2.Image = FigureMoveToUp(SelectedCell1);
                                        await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                    }
                                    else // если ((SelectedCell1.y > SelectedCell2.y))
                                    {
                                        await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                        SelectedCell1.Image = FigureMoveToUp(SelectedCell1);
                                        SelectedCell2.Image = FigureMoveToDown(SelectedCell2);
                                        await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                        SelectedCell1.Image = FigureMoveToUp(SelectedCell2);
                                        SelectedCell2.Image = FigureMoveToDown(SelectedCell1);
                                        await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                    }
                                }
                            }

                            CellSwap(SelectedCell1, SelectedCell2); // реально меняем фигуры местами, когда анимация уже проигралась

                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);



                            if (FigureDestructionCheck() == false) // если перестановка фигур не привела к уничтожению одной из них, то
                                                                   // надо передвинуть их обратно
                            {
                                SetCellBackgroundYellow(SelectedCell1); // если фигуры передвигаем обратно, то показываем это игроку жёлтой рамкой
                                SetCellBackgroundYellow(SelectedCell2);

                                await Task.Delay(TimeLapseBetweenActionsInMilliseconds);

                                if ((SelectedCell2.GetX() < SelectedCell1.GetX()))  // определяем координаты фигур относительно друг друга,
                                                                                    // а потом запускаем анимацию их передвижения
                                {
                                    await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                    SelectedCell2.Image = FigureMoveToRight(SelectedCell2);
                                    SelectedCell1.Image = FigureMoveToLeft(SelectedCell1);
                                    await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                    SelectedCell2.Image = FigureMoveToRight(SelectedCell1);
                                    SelectedCell1.Image = FigureMoveToLeft(SelectedCell2);
                                    await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                }
                                else
                                {
                                    if ((SelectedCell2.GetX() > SelectedCell1.GetX()))
                                    {
                                        await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                        SelectedCell2.Image = FigureMoveToLeft(SelectedCell2);
                                        SelectedCell1.Image = FigureMoveToRight(SelectedCell1);
                                        await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                        SelectedCell2.Image = FigureMoveToLeft(SelectedCell1);
                                        SelectedCell1.Image = FigureMoveToRight(SelectedCell2);
                                        await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                    }
                                    else
                                    {
                                        if ((SelectedCell2.GetY() < SelectedCell1.GetY()))
                                        {
                                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                            SelectedCell2.Image = FigureMoveToDown(SelectedCell2);
                                            SelectedCell1.Image = FigureMoveToUp(SelectedCell1);
                                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                            SelectedCell2.Image = FigureMoveToDown(SelectedCell1);
                                            SelectedCell1.Image = FigureMoveToUp(SelectedCell2);
                                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                        }
                                        else // если ((SelectedCell2.y > SelectedCell1.y))
                                        {
                                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                            SelectedCell2.Image = FigureMoveToUp(SelectedCell2);
                                            SelectedCell1.Image = FigureMoveToDown(SelectedCell1);
                                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                            SelectedCell2.Image = FigureMoveToUp(SelectedCell1);
                                            SelectedCell1.Image = FigureMoveToDown(SelectedCell2);
                                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                        }
                                    }
                                }

                                CellSwap(SelectedCell1, SelectedCell2);

                                await Task.Delay(TimeLapseBetweenActionsInMilliseconds);

                            }
                            else  // если после перестановки двух фигур появились случаи три и более в ряд, которые можно и нужно уничтожить
                            {
                                await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                while (FigureDestructionCheck())    // пока есть что уничтожать по горизонтали и вертикали
                                {
                                    for (int j = 0; j < GameMatrixSize; j++)  // идём по горизонтали
                                    {
                                        for (int i = 0; i < GameMatrixSize; i++)
                                        {
                                            int a = i;
                                            int count = 1;

                                            while (((a + 1) < GameMatrixSize) && (_CellsMatrix[a, j].GetContainedFigure()
                                                == _CellsMatrix[a + 1, j].GetContainedFigure()))  // пока мы не дошли до края и
                                                                                                  // идём по клеткам с повторяющимися фигурами внутри
                                            {
                                                ++a;
                                                ++count;
                                            }
                                            if (count >= 3)
                                            {

                                                for (int j1 = 0; j1 < GameMatrixSize; j1++) // проверяем наличие пересекающихся матчей
                                                                                            // вроде изображённого ниже: 
                                                                                            //     x
                                                                                            //   x x x
                                                                                            //     x
                                                {
                                                    for (int i1 = 0; i1 < GameMatrixSize; i1++)
                                                    {
                                                        int a1 = i1;
                                                        int count1 = 1;

                                                        while (((a1 + 1) < GameMatrixSize) && (_CellsMatrix[j1, a1].GetContainedFigure() 
                                                            == _CellsMatrix[j1, a1 + 1].GetContainedFigure()))
                                                        {
                                                            ++a1;
                                                            ++count1;
                                                        }
                                                        if (count1 >= 3)
                                                        {
                                                            for (int y1 = i1; y1 < (i1 + count1); y1++)
                                                            {
                                                                if (_CellsMatrix[j1, y1].GetContainedFigure() != "Empty")
                                                                {

                                                                    _CellsMatrix[j1, y1].BackgroundImage 
                                                                        = global::SummerPracticMatch3.Properties.Resources.CellBackgroundMATCH3;
                                                                    _CellsMatrix[j1, y1].SetIsNeedsToDelete(true); // метим то, что уничтожим позже

                                                                }
                                                            }
                                                            await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                                        }
                                                    }
                                                }


                                                for (int y = i; y < (i + count); y++)
                                                {
                                                    if (_CellsMatrix[y, j].GetContainedFigure() != "Empty")
                                                    {

                                                        _CellsMatrix[y, j].BackgroundImage
                                                            = global::SummerPracticMatch3.Properties.Resources.CellBackgroundMATCH3;
                                                        // подсвечиваем три и более в ряд, чтобы игрок видел, что именно сейчас уничтожится
                                                        _CellsMatrix[y, j].SetIsNeedsToDelete(true);
                                                    }
                                                }


                                                await Task.Delay(TimeLapseBetweenActionsInMilliseconds);

                                                for (int i3 = 0; i3 < GameMatrixSize; i3++)
                                                {
                                                    for (int j3 = 0; j3 < GameMatrixSize; j3++)
                                                    {
                                                        if (_CellsMatrix[i3, j3].GetIsNeedsToDelete())
                                                        {
                                                            _CellsMatrix[i3, j3].SetContainedFigure("Empty"); // разрушаем матчи
                                                            ++ScorePoints;  // начисляем очки игроку
                                                            LabelIntNumberOfScorePoints.Text = Convert.ToString(ScorePoints); // выводим очки на экран
                                                            _CellsMatrix[i3, j3].SetIsNeedsToDelete(false);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    for (int j = 0; j < GameMatrixSize; j++)  // по вертикали тоже устраиваем зачистку
                                    {
                                        for (int i = 0; i < GameMatrixSize; i++)
                                        {
                                            int a = i;
                                            int count = 1;

                                            while (((a + 1) < GameMatrixSize) && (_CellsMatrix[j, a].GetContainedFigure() == _CellsMatrix[j, a + 1].GetContainedFigure()))
                                            {
                                                ++a;
                                                ++count;
                                            }
                                            if (count >= 3)
                                            {
                                                for (int y = i; y < (i + count); y++)
                                                {
                                                    if (_CellsMatrix[j, y].GetContainedFigure() != "Empty")
                                                    {

                                                        _CellsMatrix[j, y].BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackgroundMATCH3;
                                                    }
                                                }
                                                await Task.Delay(TimeLapseBetweenActionsInMilliseconds);
                                                for (int y = i; y < (i + count); y++)
                                                {

                                                    if (_CellsMatrix[j, y].GetContainedFigure() != "Empty")
                                                    {
                                                        _CellsMatrix[j, y].SetContainedFigure("Empty");
                                                        ++ScorePoints;
                                                        LabelIntNumberOfScorePoints.Text = Convert.ToString(ScorePoints);
                                                    }
                                                }
                                            }
                                        }
                                    }


                                    for (int t = 0; t < GameMatrixSize; t++)  // тут анимированное падение фигур вниз, если есть, куда падать
                                    {
                                        for (int i = (GameMatrixSize - 1); i >= 0; i--)
                                        {
                                            for (int j = 1; j < GameMatrixSize; j++)
                                            {
                                                if (_CellsMatrix[i, j].GetContainedFigure() == "Empty")
                                                {

                                                    if ((_CellsMatrix[i, j - 1].GetY() < _CellsMatrix[i, j].GetY()))
                                                    {
                                                        _CellsMatrix[i, j - 1].Image = FigureMoveToDown(_CellsMatrix[i, j - 1]);
                                                        _CellsMatrix[i, j].Image = FigureMoveToUp(_CellsMatrix[i, j]);
                                                        await Task.Delay(TimeLapseGravitationInMilliseconds);
                                                        _CellsMatrix[i, j - 1].Image = FigureMoveToDown(_CellsMatrix[i, j]);
                                                        _CellsMatrix[i, j].Image = FigureMoveToUp(_CellsMatrix[i, j - 1]);
                                                        await Task.Delay(TimeLapseGravitationInMilliseconds);
                                                    }


                                                    CellSwap(_CellsMatrix[i, j - 1], _CellsMatrix[i, j]);

                                                }
                                            }
                                        }

                                        for (int i = 0; i < GameMatrixSize; i++) // генерация новых фигур в верхней строчке, если там есть
                                                                                 // свободное место
                                        {
                                            if (_CellsMatrix[i, 0].GetContainedFigure() == "Empty")
                                            {
                                                GenerateFigure(_CellsMatrix[i, 0]);
                                                VisualisateFigure(_CellsMatrix[i, 0]);
                                            }
                                        }

                                    }
                                }
                            }
                            SelectedCell1 = null; // сбрасываем выделение, раз уж уничтожение произошло
                            SelectedCell2 = null;
                            EnableAllFigures(); // раз все действия с фигурами закончены, то пользователь может выделить новую пару фигур
                        }
                    }
                }
            }



        }



        public Game() // сама игра
        {

            InitializeComponent();
            GameSoundtrack = new System.Media.SoundPlayer(SummerPracticMatch3.Properties.Resources.game); // саундтрек
            EndSound = new System.Media.SoundPlayer(SummerPracticMatch3.Properties.Resources.endgame); // звук окончания игры
            GameSoundtrack.Load();
            GameSoundtrack.PlayLooping(); // подрубаем музыку
            for (int i = 0; i < GameMatrixSize; i++) // создаём игровое поле
            {
                for (int j = 0; j < GameMatrixSize; j++)
                {
                    _CellsMatrix[i, j] = new Cell();
                    _CellsMatrix[i, j].SetX(i);
                    _CellsMatrix[i, j].SetY(j);
                    _CellsMatrix[i, j].BackgroundImage = global::SummerPracticMatch3.Properties.Resources.CellBackground; // задаём фон клеткам
                    _CellsMatrix[i, j].Location = new Point(290 + 51 * i, 190 + 51 * j); // вычисляем, где располагать клетки
                    _CellsMatrix[i, j].Size = new Size(50, 50);
                    _CellsMatrix[i, j].SizeMode = PictureBoxSizeMode.AutoSize;
                    this.Controls.Add(_CellsMatrix[i, j]);
                    GenerateFigure(_CellsMatrix[i, j]); // генерируем рандомную фигуру и помещаем её в клетку
                    _CellsMatrix[i, j].Click += ClickOnTheCell;
                }
            }

            LabelIntNumberOfTimeInSeconds.Text = Convert.ToString(Timer);  // выводим время игры
            FigureDestuctionAndFall(); // очищаем поле от случаев три и более в ряд до начала игры
            ScorePoints = 0; // обнуляем очки игрока перед началом игры
            LabelIntNumberOfScorePoints.Text = Convert.ToString(ScorePoints); // выводим очки игрока
            TimerOfGameover(); // запускаем главный таймер


        }


        async public void TimerOfGameover()
        {

            while (Timer > 0)  // обратный отсчёт времени
            {
                await Task.Delay(1000);
                --Timer;
                LabelIntNumberOfTimeInSeconds.Text = Convert.ToString(Timer);
            }

            EndSound.Load();  // запускаем звук конца игры, когда время истекает
            EndSound.Play();
            MessageBox.Show("   GAME OVER!   ", "Три-в-ряд", MessageBoxButtons.OK, MessageBoxIcon.Information); // сообщаем игроку о геймовере


            GameSoundtrack.Stop();
            Game.ActiveForm.Hide();
            Menu MyMenu = new Menu();
            MyMenu.ShowDialog();
            Close();
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            this.FormBorderStyle = FormBorderStyle.None; // убираем границы
            this.WindowState = FormWindowState.Maximized; // запускаем на полный экран
            this.TopMost = true;
            LabelIntNumberOfScorePoints.Text = Convert.ToString(ScorePoints);
        }

        private void Cell11_Click(object sender, EventArgs e)
        {

        }

        private void Cell33_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabelNameOfGame_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void LabelNameOfGame_Click(object sender, EventArgs e)
        {
        }

        private void LabelNameOfScoreBalls_Click(object sender, EventArgs e)
        {
        }

        private void LabelIntNumberOfTimeInSeconds_Click(object sender, EventArgs e)
        {
            LabelIntNumberOfTimeInSeconds.Text = Convert.ToString(Timer);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }



}