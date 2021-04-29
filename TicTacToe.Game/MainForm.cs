using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicTacToe.Game.Services;

namespace TicTacToe.Game
{
    public partial class MainForm : Form
    {
        private GameController _gameController;
        private List<Button> _boardButtons;
        private Dictionary<string, Point> _buttonsMap;
        const int sizeGameMap = 3;
        const int sizeCell = 150;
        int currentPlayer = 2;
        int[,] gameMap = new int[sizeGameMap, sizeGameMap];
        Image crossImage;
        Image zeroImage;

        public MainForm()
        {
            BackColor = Color.Black;
            Size = new Size(sizeCell, sizeCell);
            crossImage = new Bitmap(GameResources.CrossImage, new Size(sizeCell, sizeCell));
            zeroImage = new Bitmap(GameResources.ZeroImage, new Size(sizeCell, sizeCell));
            InitializeComponent();
            RenderTableWithButtons();
        }

        private void RenderTableWithButtons()
        {
            var buttons = Controls.OfType<Button>().ToList();
            foreach (Button btn in buttons)
            {
                Controls.Remove(btn);
                btn.Dispose();
            }
            _boardButtons = new List<Button>();
            _buttonsMap = new Dictionary<string, Point>();
            _gameController = new GameController(3);
            
            var i = 0;
            foreach (var outerItem in _gameController.GameMap)
            {
                var j = 0;
                foreach (var innerItem in outerItem)
                {
                    var button = new Button();
                    button.Name = $"GameButton{i}{j}";
                    button.Location = new Point(j * sizeCell, i * sizeCell);
                    button.Size = new Size(sizeCell, sizeCell);
                    button.BackColor = Color.Yellow;
                    button.Click += new EventHandler(OnPress);
                    Controls.Add(button);
                    _boardButtons.Add(button);
                    _buttonsMap.Add(button.Name, new Point(i, j));
                    j++;
                }
                i++;
            }
        }

       
        public void OnPress(object sender, EventArgs e)
        {
            var pressedButton = sender as Button;
            Enums.TurnType turnType = default;

            currentPlayer = currentPlayer == 1 ? 2 : 1;
            
            switch(currentPlayer)
            {
                case 1:
                    turnType = Enums.TurnType.Cross;
                    pressedButton.BackgroundImage = crossImage;
                    pressedButton.Enabled = false; 
                    break;
                case 2:
                    turnType = Enums.TurnType.Zero;
                    pressedButton.BackgroundImage = zeroImage;
                    pressedButton.Enabled = false;
                    break;
            }
            if (_buttonsMap.TryGetValue(pressedButton.Name, out var turnPoint))
            {
                _gameController.MakeTurn(turnPoint, turnType);
            }
            if (_gameController.CheckState())
            {
                MessageBox.Show("Game ended. New game started a few moment later!");
                RenderTableWithButtons();
            }    
        }


    }
}
