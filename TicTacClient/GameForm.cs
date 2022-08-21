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
        private void GameForm_Load(object? sender, EventArgs e)
        {
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
        }

        private void InitializeForm()
        {
            targetScoreValue.Text=gameData?.TargetScore.ToString();
            playerOneNameValue.Text = gameData?.PlayerOne.UserName;
            messageTextBox.Text = "wait for oppontent connection";
            yourScoreValue.Text = 0.ToString();
            opponentScoreValue.Text = 0.ToString();

        }

        private void OnMoveMade(OnMoveMadeResponce responce)
        {
            if (responce.Errorcode == 1)
            {
                if (responce.RowCordinate == 0 && responce.ColumnCoordinate == 0)
                {
                    button1.Text = responce.MarK;
                }
                if (responce.RowCordinate == 0 && responce.ColumnCoordinate == 1)
                {
                    button2.Text = responce.MarK;

                }
                if (responce.RowCordinate == 0 && responce.ColumnCoordinate == 2)
                {
                    button3.Text = responce.MarK;

                }
                if (responce.RowCordinate == 1 && responce.ColumnCoordinate == 0)
                {
                    button4.Text = responce.MarK;

                }
                if (responce.RowCordinate == 1 && responce.ColumnCoordinate == 1)
                {
                    button5.Text = responce.MarK;

                }
                if (responce.RowCordinate == 1 && responce.ColumnCoordinate == 2)
                {
                    button6.Text = responce.MarK;

                }
                if (responce.RowCordinate == 2 && responce.ColumnCoordinate == 0)
                {
                    button7.Text = responce.MarK;

                }
                if (responce.RowCordinate == 2 && responce.ColumnCoordinate == 1)
                {
                    button8.Text = responce.MarK;

                }
                if (responce.RowCordinate == 2 && responce.ColumnCoordinate == 2)
                {
                    button9.Text = responce.MarK;

                }

            }
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.Id,0,0);

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.Id, 0, 2);
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.Id, 0, 3);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.Id, 1, 0);
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.Id, 1, 1);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.Id, 1, 2);
        }

        private async void button7_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.Id, 2, 0);
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.Id, 2, 1);
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            await connection.InvokeAsync("makemove", gameData?.Id, 2, 2);
        }
    }
}
