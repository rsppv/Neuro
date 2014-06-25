using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Game
{
    public class GameEnvironment
    {
        private const int EMPTY = -1;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int PipeLength { get; private set; }
        public List<Cell> FinalPipes { get; set; }

        private Cell[,] _field;
        public Cell NextCell { get; private set; }

        public GameEnvironment(int width, int height)
        {
            Height = height;
            Width = width;
            InitField();
        }

        private void InitField()
        {
            FinalPipes = new List<Cell>();
            PipeLength = 0;
            _field = new Cell[Height, Width];
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    _field[row, col] = new Cell(row, col, EMPTY);
                }
            }
            NextCell = _field[0, 0];
        }

        public List<int> GetObstacles(int row, int col)
        {
            if (row < 0 || row > Height - 1 || col < 0 || col > Width - 1)
                throw new ArgumentOutOfRangeException("Ошибка в ф-ции получения препятствий. Передаваемые аргументы должны находиться в пределах поля");

            /*  Список целых - это список на входы сети
             *  Порядок значений в листе. Начиная с верхнего и по часовой стрелке
             */

            List<int> obstacles = new List<int>();

            if (row == 0)
            {
                obstacles.Add(1);
            }
            else
            {
                obstacles.Add(IsFree(_field[row - 1, col]) ? 0 : 1);
            }

            if (col == Width - 1)
            {
                obstacles.Add(1);
            }
            else
            {
                obstacles.Add(IsFree(_field[row, col + 1]) ? 0 : 1);
            }

            if (row == Height - 1)
            {
                obstacles.Add(1);
            }
            else
            {
                obstacles.Add(IsFree(_field[row + 1, col]) ? 0 : 1);
            }

            if (col == 0)
            {
                obstacles.Add(1);
            }
            else
            {
                obstacles.Add(IsFree(_field[row, col - 1]) ? 0 : 1);
            }

            if (obstacles.Count != 4)
            {
                throw new Exception("GetObstacles() почему то возвращает список не из 4-х параметров");
            }

            return obstacles;
        }

        //public Cell checkFreeCells(Cell cell)
        //{
        //    /* TODO доделать. Алгоритм следующий:
        //     * Передаю в функцию клетку, 
        //     * ф-я возвращает в зависимости от Value две клетки которые рядом с концами
        //     * Проверяю эти клетки. Та, которая свободная и будет следующая.
        //     * Перед этой функцией проверить на IsCompatible и если совместимы то выбирать уже следующую клетку. 
        //     * Если не совместимы то break()
        //    */
        //    Cell end1;
        //    Cell end2;

        //    switch (cell.Value)
        //    {
        //        case EMPTY:

        //        case 0:
        //            end1 = GetCell(cell.Row - 1, cell.Col);
        //            end2 = GetCell(cell.Row + 1, cell.Col);
        //            break;
        //        case 1:
        //            end1 = GetCell(cell.Row, cell.Col - 1);
        //            end2 = GetCell(cell.Row, cell.Col + 1);
        //            break;
        //        case 2:
        //            end1 = GetCell(cell.Row + 1, cell.Col);
        //            end2 = GetCell(cell.Row, cell.Col + 1);
        //            break;
        //        case 3:
        //            end1 = GetCell(cell.Row, cell.Col-1);
        //            end2 = GetCell(cell.Row + 1, cell.Col);
        //            break;
        //        case 4:
        //            end1 = GetCell(cell.Row - 1, cell.Col);
        //            end2 = GetCell(cell.Row, cell.Col+1);
        //            break;
        //        case 5:
        //            end1 = GetCell(cell.Row - 1, cell.Col);
        //            end2 = GetCell(cell.Row, cell.Col-1);
        //            break;
        //    }
        //}

        public void SetCellValue(int row, int col, int value)
        {
            if (value > 5 || value < -1)
                throw new ArgumentOutOfRangeException("Некорректное значение в ячейке поля. Значение должно быть в диапазоне [-1 ; 5]");

            if (row < 0 || row > Height - 1 || col < 0 || col > Width - 1)
                throw new ArgumentOutOfRangeException("Передаваемые аргументы должны находиться в пределах поля");

            if (!IsFree(_field[row, col]))
            {
                throw new Exception("Нельзя изменить ячейку, она уже занята");
            }

            _field[row, col].Value = value;

            if (row == 0 && col == 0)
            {
                FinalPipes.Add(_field[row, col]);

                if (_field[row, col].Value == 0 || _field[row, col].Value == 3)
                    NextCell = _field[row + 1, col];
                if (_field[row, col].Value == 1 || _field[row, col].Value == 4)
                    NextCell = _field[row, col + 1];
                if (_field[row, col].Value == 2 || _field[row, col].Value == 5)
                    NextCell = null;
            }
            else if (IsCompatible(FinalPipes.Last(), _field[row, col])) 
            {
                FinalPipes.Add(_field[row, col]);
            }
            
            PipeLength = FinalPipes.Count;
        }

        public bool CheckNextCell()
        {
            return NextCell != null;
        }

        private bool IsFree(Cell cell)
        {
            return cell.Value == EMPTY;
        }

        private bool IsUpEdge(Cell cell)
        {
            return cell.Row == 0;
        }
        private bool IsDownEdge(Cell cell)
        {
            return cell.Row == Height - 1;
        }
        private bool IsLeftEdge(Cell cell)
        {
            return cell.Col == 0;
        }
        private bool IsRightEdge(Cell cell)
        {
            return cell.Col == Width - 1;
        }

        public Cell GetCell(int row, int col)
        {
            if (row < 0 || row > Height - 1 || col < 0 || col > Width - 1)
                throw new ArgumentOutOfRangeException("Передаваемые аргументы должны находиться в пределах поля");
            return _field[row, col];
        }

        //private bool CheckLinked(Cell from, Cell to)
        //{
        //    // Если на одной строке и столбцы соседние и концы трубы совмещены
        //    if (IsNear(from, to) && IsCompatible(from, to))
        //    {
        //        return true;
        //    }
        //    // Если на одном столбце и строки соседние и концы трубы совмещены 
        //    if (IsNear(from, to) && IsCompatible(from, to))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        private bool IsNear(Cell c1, Cell c2)
        {
            if ((c1.Row == c2.Row) && (Math.Abs(c1.Col - c2.Col) == 1))
                return true;
            if ((c1.Col == c2.Col) && (Math.Abs(c1.Row - c2.Row) == 1))
                return true;
            return false;
        }

        public bool IsCompatible(Cell cell1, Cell cell2)
        {
            if (cell1.Value == 0) // Проверка для !
            {
                if (cell2.Value == 0)
                {
                    if (cell1.Col == cell2.Col && Math.Abs(cell1.Row - cell2.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsUpEdge(cell2))
                        {
                            if (IsFree(_field[cell2.Row - 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row - 1, cell2.Col];
                            }
                        }
                        if (!IsDownEdge(cell2))
                        {
                            if (IsFree(_field[cell2.Row + 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row + 1, cell2.Col];
                            }
                        }

                        #endregion
        
                        return true;
                    }
                }
                else if (cell2.Value == 2 || cell2.Value == 3)
                {
                    if (cell1.Col == cell2.Col && (cell1.Row - cell2.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsRightEdge(cell2) && cell2.Value == 2)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col + 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col + 1];
                            }
                        }
                        if (!IsLeftEdge(cell2) && cell2.Value == 3)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col - 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col - 1];
                            }
                        }

                        #endregion
                        
                        return true;
                    }
                }
                else if (cell2.Value == 4 || cell2.Value == 5)
                {
                    if (cell1.Col == cell2.Col && (cell2.Row - cell1.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsRightEdge(cell2) && cell2.Value == 4)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col + 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col + 1];
                            }
                        }
                        if (!IsLeftEdge(cell2) && cell2.Value == 5)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col - 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col - 1];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
            }
            else if (cell1.Value == 1) // Проверка для - 
            {
                if (cell2.Value == 1)
                {
                    if (cell1.Row == cell2.Row && Math.Abs(cell1.Col - cell2.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if ( !IsRightEdge(cell2) )
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col + 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col + 1];
                            }
                        }
                        if ( !IsLeftEdge(cell2) )
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col - 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col - 1];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 2 || cell2.Value == 4)
                {
                    if (cell1.Row == cell2.Row && (cell1.Col - cell2.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsUpEdge(cell2) && cell2.Value == 4)
                        {
                            if (IsFree(_field[cell2.Row - 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row - 1, cell2.Col];
                            }
                        }
                        if (!IsDownEdge(cell2) && cell2.Value == 2)
                        {
                            if (IsFree(_field[cell2.Row + 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row + 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 3 || cell2.Value == 5)
                {
                    if (cell1.Row == cell2.Row && (cell2.Col - cell1.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsUpEdge(cell2) && cell2.Value == 5)
                        {
                            if (IsFree(_field[cell2.Row - 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row - 1, cell2.Col];
                            }
                        }
                        if (!IsDownEdge(cell2) && cell2.Value == 3)
                        {
                            if (IsFree(_field[cell2.Row + 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row + 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
            }
            else if (cell1.Value == 2) // Проверка для Г 
            {
                if (cell2.Value == 5)
                {
                    if (cell1.Col == cell2.Col && (cell2.Row - cell1.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsLeftEdge(cell2))
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col - 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col - 1];
                            }
                        }

                        #endregion

                        return true;
                    }
                    if (cell1.Row == cell2.Row && (cell2.Col - cell1.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsUpEdge(cell2))
                        {
                            if (IsFree(_field[cell2.Row - 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row - 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 0 || cell2.Value == 4)
                {
                    if (cell1.Col == cell2.Col && (cell2.Row - cell1.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsRightEdge(cell2) && cell2.Value == 4)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col + 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col + 1];
                            }
                        }
                        if (!IsDownEdge(cell2) && cell2.Value == 0)
                        {
                            if (IsFree(_field[cell2.Row + 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row + 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 1 || cell2.Value == 3)
                {
                    if (cell1.Row == cell2.Row && (cell2.Col - cell1.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsRightEdge(cell2) && cell2.Value == 1)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col + 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col + 1];
                            }
                        }
                        if (!IsDownEdge(cell2) && cell2.Value == 3)
                        {
                            if (IsFree(_field[cell2.Row + 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row + 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
            }
            else if (cell1.Value == 3) // Проверка для 7 
            {
                if (cell2.Value == 4)
                {
                    if (cell1.Col == cell2.Col && (cell2.Row - cell1.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsRightEdge(cell2))
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col + 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col + 1];
                            }
                        }

                        #endregion

                        return true;
                    }
                    if (cell1.Row == cell2.Row && (cell1.Col - cell2.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsUpEdge(cell2))
                        {
                            if (IsFree(_field[cell2.Row - 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row - 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 1 || cell2.Value == 2)
                {
                    if (cell1.Row == cell2.Row && (cell1.Col - cell2.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsLeftEdge(cell2) && cell2.Value == 1)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col - 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col - 1];
                            }
                        }
                        if (!IsDownEdge(cell2) && cell2.Value == 2)
                        {
                            if (IsFree(_field[cell2.Row + 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row + 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 0 || cell2.Value == 5)
                {
                    if (cell1.Col == cell2.Col && (cell2.Row - cell1.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsLeftEdge(cell2) && cell2.Value == 5)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col - 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col - 1];
                            }
                        }
                        if (!IsDownEdge(cell2) && cell2.Value == 0)
                        {
                            if (IsFree(_field[cell2.Row + 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row + 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
            }
            else if (cell1.Value == 4) // Проверка для L 
            {
                if (cell2.Value == 3)
                {
                    if (cell1.Col == cell2.Col && (cell1.Row - cell2.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsLeftEdge(cell2))
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col - 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col - 1];
                            }
                        }

                        #endregion

                        return true;
                    }
                    if (cell1.Row == cell2.Row && (cell2.Col - cell1.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsDownEdge(cell2) && cell2.Value == 0)
                        {
                            if (IsFree(_field[cell2.Row + 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row + 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 0 || cell2.Value == 2)
                {
                    if (cell1.Col == cell2.Col && (cell1.Row - cell2.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsRightEdge(cell2) && cell2.Value == 2)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col + 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col + 1];
                            }
                        }
                        if (!IsUpEdge(cell2) && cell2.Value == 0)
                        {
                            if (IsFree(_field[cell2.Row - 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row - 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 1 || cell2.Value == 5)
                {
                    if (cell1.Row == cell2.Row && (cell2.Col - cell1.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsRightEdge(cell2) && cell2.Value == 1)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col + 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col + 1];
                            }
                        }
                        if (!IsUpEdge(cell2) && cell2.Value == 5)
                        {
                            if (IsFree(_field[cell2.Row - 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row - 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
            }
            else if (cell1.Value == 5) // Проверка для j 
            {
                if (cell2.Value == 2)
                {
                    if (cell1.Col == cell2.Col && (cell1.Row - cell2.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsRightEdge(cell2))
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col + 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col + 1];
                            }
                        }

                        #endregion

                        return true;
                    }
                    if (cell1.Row == cell2.Row && (cell1.Col - cell2.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsDownEdge(cell2) && cell2.Value == 0)
                        {
                            if (IsFree(_field[cell2.Row + 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row + 1, cell2.Col];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 0 || cell2.Value == 3)
                {
                    if (cell1.Col == cell2.Col && (cell1.Row - cell2.Row) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsUpEdge(cell2) && cell2.Value == 0)
                        {
                            if (IsFree(_field[cell2.Row - 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row - 1, cell2.Col];
                            }
                        }
                        if (!IsLeftEdge(cell2) && cell2.Value == 3)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col - 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col - 1];
                            }
                        }                        

                        #endregion

                        return true;
                    }
                }
                else if (cell2.Value == 1 || cell2.Value == 4)
                {
                    if (cell1.Row == cell2.Row && (cell1.Col - cell2.Col) == 1)
                    {
                        #region Выбор следующей ячейки

                        NextCell = null;
                        if (!IsUpEdge(cell2) && cell2.Value == 4)
                        {
                            if (IsFree(_field[cell2.Row - 1, cell2.Col]))
                            {
                                NextCell = _field[cell2.Row - 1, cell2.Col];
                            }
                        }
                        if (!IsLeftEdge(cell2) && cell2.Value == 1)
                        {
                            if (IsFree(_field[cell2.Row, cell2.Col - 1]))
                            {
                                NextCell = _field[cell2.Row, cell2.Col - 1];
                            }
                        }

                        #endregion

                        return true;
                    }
                }
            }
            NextCell = null;
            return false;
        }

        private string CodeToSymbol(int code)
        {
            /* Символы для трубы
     * { "007С |", "2014 -", "2308 Г", "2309 верз прав угол", "230A ниж лев", "230B ниж прав" };
     * Консоль не выводит эти символы почему то
     */
            switch (code)
            {
                case EMPTY:
                    return " ";
                case 0:
                    return "!";
                case 1:
                    return "—";
                case 2:
                    return "г";
                case 3:
                    return "7";
                case 4:
                    return "L";
                case 5:
                    return "j";
                default:
                    return "&";
            }
        }

        public void Print()
        {
            Console.Out.WriteLine("Игровое поле\n");
            for (int row = 0; row < Height + 2; row++)
            {
                for (int col = 0; col < Width + 2; col++)
                {
                    if (row == 0 || row == Height + 1)
                    {
                        Console.Out.Write("*");
                    }
                    else if (col == 0 || col == Width + 1)
                    {
                        Console.Out.Write("*");
                    }
                    else
                    {
                        Console.Out.Write(CodeToSymbol(_field[row - 1, col - 1].Value));
                    }
                }
                Console.Out.WriteLine();
            }
        }

    }
}
