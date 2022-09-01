using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacClient
{
    public partial class GameForm : Form
    {

        HubConnection connection;
        public GameData CurrentGame { get; set; }   
        public static List<GameForm>Forms = new List<GameForm>();
        public int GameId { get; set; }
        public GameForm(HubConnection connection, GameData game)
        {
            InitializeComponent();
            this.connection = connection;
            this.CurrentGame = game;
            InitializeForm(game.GameId);
            this.Load += GameForm_Load;
            this.GameId = game.GameId;
            Forms.Add(this);

        }
        OnMoveMadeResponce responce;
        public string markk;
        private void GameForm_Load(object? sender, EventArgs e)
        {
            usernameTextBox.Enabled = false;
            playerOneNameValue.Enabled = false;
            playerTwoNameValue.Enabled = false;
            yourScoreValue.Enabled = false;
            opponentScoreValue.Enabled = false;
            targetScoreValue.Enabled = false;
            messageTextBox.Enabled = false;
            connection.On<int, int, string, int, int, string>("nextturn", (errorcode, gameId, errormessage, rowcoordinate, columncoordinate, mark) =>
            {
                responce = new OnMoveMadeResponce();
                responce.Errorcode = errorcode;
                responce.ErrorMessage = errormessage;
                responce.RowCordinate = rowcoordinate;
                responce.ColumnCoordinate = columncoordinate;
                responce.MarK = mark;
                responce.GameId = gameId;

                OnMoveMade(responce,gameId);

            });
            connection.On<int, int, int, string>("matchend", (gameId, playerOneScore, playerTwoScore, message) =>
            {
                var form = Forms.Where(x => x.GameId == gameId).Single();
                form.yourScoreValue.Text = playerOneScore.ToString();
                form.opponentScoreValue.Text = playerTwoScore.ToString();
                form.messageTextBox.Text = message;
                form.button1.Enabled = false;
                form.button2.Enabled = false;
                form.button3.Enabled = false;
                form.button4.Enabled = false;
                form.button5.Enabled = false;
                form.button6.Enabled = false;
                form.button7.Enabled = false;
                form.button8.Enabled = false;
                form.button9.Enabled = false;
            });
            connection.On<int>("matchstart", (gameId) =>
            {
                var form = Forms.Where(x => x.GameId == gameId).Single();
                form.button1.Enabled = true;
                form.button2.Enabled = true;
                form.button3.Enabled = true;
                form.button4.Enabled = true;
                form.button5.Enabled = true;
                form.button6.Enabled = true;
                form.button7.Enabled = true;
                form.button8.Enabled = true;
                form.button9.Enabled = true;
                form.button1.Text = "";
                form.button2.Text = "";
                form.button3.Text = "";
                form.button4.Text = "";
                form.button5.Text = "";
                form.button6.Text = "";
                form.button7.Text = "";
                form.button8.Text = "";
                form.button9.Text = "";

            });
            connection.On<int, int, int, string>("gameend", (gameId, playerOneScore, playerTwoScore, message) =>
            {
                var form = Forms.Where(x => x.GameId == gameId).Single();
                form.yourScoreValue.Text = playerOneScore.ToString();
                form.opponentScoreValue.Text = playerTwoScore.ToString();
                form.messageTextBox.Text = message;
                Thread.Sleep(1000);
                return;
            });
            
        }

        private void InitializeForm(int gameId)
        {
            targetScoreValue.Text = CurrentGame.TargetScore.ToString();
            playerOneNameValue.Text = CurrentGame.PlayerOne.UserName;
            playerTwoNameValue.Text = CurrentGame.PlayerTwo?.UserName;

            yourScoreValue.Text = 0.ToString();
            opponentScoreValue.Text = 0.ToString();

        }

        private void OnMoveMade(OnMoveMadeResponce responce, int gameId)
        {
            GameForm form = Forms.Where(x => x.GameId == gameId).Single();
            form.messageTextBox.Text = responce.ErrorMessage;

            if (responce.Errorcode == 1 && responce.RowCordinate != -1)
            {
                if (responce.RowCordinate == 0 && responce.ColumnCoordinate == 0)
                {
                    form.button1.Text = responce.MarK;
                    form.button1.Enabled = false;

                }
                if (responce.RowCordinate == 0 && responce.ColumnCoordinate == 1)
                {
                    form.button2.Text = responce.MarK;
                    form.button2.Enabled = false;


                }
                if (responce.RowCordinate == 0 && responce.ColumnCoordinate == 2)
                {
                    form.button3.Text = responce.MarK;
                    form.button3.Enabled = false;


                }
                if (responce.RowCordinate == 1 && responce.ColumnCoordinate == 0)
                {
                    form.button4.Text = responce.MarK;
                    form.button4.Enabled = false;

                }
                if (responce.RowCordinate == 1 && responce.ColumnCoordinate == 1)
                {
                    form.button5.Text = responce.MarK;
                    form.button5.Enabled = false;

                }
                if (responce.RowCordinate == 1 && responce.ColumnCoordinate == 2)
                {
                    form.button6.Text = responce.MarK;
                    form.button6.Enabled = false;

                }
                if (responce.RowCordinate == 2 && responce.ColumnCoordinate == 0)
                {
                    form.button7.Text = responce.MarK;
                    form.button7.Enabled = false;

                }
                if (responce.RowCordinate == 2 && responce.ColumnCoordinate == 1)
                {
                    form.button8.Text = responce.MarK;
                    form.button8.Enabled = false;

                }
                if (responce.RowCordinate == 2 && responce.ColumnCoordinate == 2)
                {
                    form.button9.Text = responce.MarK;
                    form.button9.Enabled = false;

                }
                
            }                      
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", CurrentGame?.GameId, 0, 0);

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", CurrentGame?.GameId, 0, 1);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", CurrentGame?.GameId, 0, 2);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", CurrentGame?.GameId, 1, 0);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", CurrentGame?.GameId, 1, 1);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", CurrentGame?.GameId, 1, 2);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", CurrentGame?.GameId, 2, 0);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", CurrentGame?.GameId, 2, 1);
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", CurrentGame?.GameId, 2, 2);
        }



        private void button9_MouseEnter(object sender, EventArgs e)
        {

            Button button = new Button();
            button = (Button)sender;
            if (button.Enabled == true)
            {
                button.Text = markk;
            }
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            Button button = new Button();
            button = (Button)sender;
            if (button.Enabled == true)
            {
                button.Text = "";
            }
        }
    }
}
