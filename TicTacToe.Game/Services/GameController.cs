using System;
using System.Collections.Generic;
using System.Drawing;
using static TicTacToe.Game.Enums;

namespace TicTacToe.Game.Services
{
    public class GameController
    {
        private int _gameBoardSize;
        public GameController(int gameBoardSize)
        {
            _gameBoardSize = gameBoardSize;
            GameMap = new List<List<int>>();

            for (var i = 0; i < gameBoardSize; i++)
            {
                List<int> bufferList = new List<int>();
                for (var j = 0; j < gameBoardSize; j++)
                {
                    bufferList.Add(-1);
                }
                GameMap.Add(bufferList);
            }
        }

        public List<List<int>> GameMap { get; private set; }

        public void  MakeTurn(Point coordinates, TurnType turnType)
        {
            if (coordinates.X > _gameBoardSize || coordinates.Y > _gameBoardSize) throw new IndexOutOfRangeException("Coordinats are out of range");

            GameMap[coordinates.X][coordinates.Y] = (int)turnType;
        }

        public bool CheckState() => GameMap.CheckForTicTacToeWin() || GameMap.TransformRowsIntoColumns().CheckForTicTacToeWin() || GameMap.GetDiagonalsMatrix().CheckForTicTacToeWin();
    }
}
