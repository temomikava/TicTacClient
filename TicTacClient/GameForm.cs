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
        GameData? gameData;
        public GameForm(HubConnection connection,GameData? gameData)
        {
            InitializeComponent();
            this.connection = connection;
            this.gameData = gameData;
            InitializeForm();
            this.Load += GameForm_Load;
            
        }
        OnMoveMadeResponce responce;
        public static string markk;
        private void GameForm_Load(object? sender, EventArgs e)
        {
            usernameTextBox.Enabled = false;
            playerOneNameValue.Enabled = false;
            playerTwoNameValue.Enabled = false;
            yourScoreValue.Enabled = false;
            opponentScoreValue.Enabled = false;
            targetScoreValue.Enabled = false;
            messageTextBox.Enabled = false;
            goBackToLobbyButton.Enabled = false;
            connection.On<int,string,int,int,string>("nextturn", (errorcode,errormessage,rowcoordinate,columncoordinate,mark) =>
            {
                responce = new OnMoveMadeResponce();
                responce.Errorcode = errorcode;
                responce.ErrorMessage = errormessage;
                responce.RowCordinate = rowcoordinate;
                responce.ColumnCoordinate = columncoordinate;
                responce.MarK = mark;
                
                messageTextBox.Text = errormessage;
                OnMoveMade(responce);
                
            });
            connection.On<int,int,string>("matchend", (playerOneScore,playerTwoScore,message) =>
            {

                yourScoreValue.Text = playerOneScore.ToString();
                opponentScoreValue.Text = playerTwoScore.ToString();
                messageTextBox.Text = message;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            });
            connection.On< string>("matchstart", ( message) =>
            {
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button1.Text = "";
                button2.Text = "";
                button3.Text = "";
                button4.Text = "";
                button5.Text = "";
                button6.Text = "";
                button7.Text = "";
                button8.Text = "";
                button9.Text = "";
                
            });
            connection.On<int, int, string>("gameend", (playerOneScore, playerTwoScore, message) =>
            {
                Thread.Sleep(2000);
                yourScoreValue.Text = playerOneScore.ToString();
                opponentScoreValue.Text = playerTwoScore.ToString();
                messageTextBox.Text = message;
                Thread.Sleep(1000);
                goBackToLobbyButton.Enabled = true;
                return;
            });
            connection.On<int, string>("ondisconnected", (errorcode, erromessage) =>
            {
                MessageBox.Show(erromessage);
                Thread.Sleep(2000);
                Lobby lobby = new Lobby(connection);
                this.Hide();
                lobby.Show();
            });
        }

        private void InitializeForm()
        {
            targetScoreValue.Text=gameData?.TargetScore.ToString();
            playerOneNameValue.Text = gameData?.PlayerOne.UserName;
            playerTwoNameValue.Text = gameData?.PlayerTwo?.UserName;
            messageTextBox.Text = "wait for oppontent connection";
            yourScoreValue.Text = 0.ToString();
            opponentScoreValue.Text = 0.ToString();

        }

        private void OnMoveMade(OnMoveMadeResponce responce)
        {
            if (responce.Errorcode == 1 && responce.RowCordinate!=-1)
            {
                if (responce.RowCordinate == 0 && responce.ColumnCoordinate == 0)
                {
                    button1.Text = responce.MarK;
                    button1.Enabled = false;
                }
                if (responce.RowCordinate == 0 && responce.ColumnCoordinate == 1)
                {
                    button2.Text = responce.MarK;
                    button2.Enabled = false;

                }
                if (responce.RowCordinate == 0 && responce.ColumnCoordinate == 2)
                {
                    button3.Text = responce.MarK;
                    button3.Enabled = false;

                }
                if (responce.RowCordinate == 1 && responce.ColumnCoordinate == 0)
                {
                    button4.Text = responce.MarK;
                    button4.Enabled = false;

                }
                if (responce.RowCordinate == 1 && responce.ColumnCoordinate == 1)
                {
                    button5.Text = responce.MarK;
                    button5.Enabled = false;

                }
                if (responce.RowCordinate == 1 && responce.ColumnCoordinate == 2)
                {
                    button6.Text = responce.MarK;
                    button6.Enabled = false;

                }
                if (responce.RowCordinate == 2 && responce.ColumnCoordinate == 0)
                {
                    button7.Text = responce.MarK;
                    button7.Enabled = false;

                }
                if (responce.RowCordinate == 2 && responce.ColumnCoordinate == 1)
                {
                    button8.Text = responce.MarK;
                    button8.Enabled = false;

                }
                if (responce.RowCordinate == 2 && responce.ColumnCoordinate == 2)
                {
                    button9.Text = responce.MarK;
                    button9.Enabled = false;

                }

            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.GameId,0,0);
            
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.GameId, 0, 1);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.GameId, 0, 2);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.GameId, 1, 0);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.GameId, 1, 1);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.GameId, 1, 2);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.GameId, 2, 0);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.GameId, 2, 1);
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.GameId, 2, 2);
        }

        private void goBackToLobbyButton_Click(object sender, EventArgs e)
        {
            Lobby lobby=new Lobby(connection);
            Close();
            lobby.Show();


        }

        private void button9_MouseEnter(object sender, EventArgs e)
        {

            Button button=new Button();
            button = (Button)sender;
            if (button.Enabled==true)
            {
                button.Text =markk;

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
