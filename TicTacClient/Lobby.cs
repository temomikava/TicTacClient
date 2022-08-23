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
    public partial class Lobby : Form
    {
        HubConnection connection { get; set; }
        public static BindingList<GameData?> allGames=new BindingList<GameData?>();
        public Lobby(HubConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            this.Load += Lobby_Load;
            InitializeList();
        }
        public GameData? currentGame { get; set; }

        private void Lobby_Load(object? sender, EventArgs e)
        {
            
            connection.On<JsonElement>("getcurrentgame", (game) =>
            {
                var json = game.GetRawText();
                currentGame = JsonConvert.DeserializeObject<GameData>(json);
                GameForm gameForm = new GameForm(connection, currentGame);

            });
            connection.On<int,string,string>("ongamejoin", (errorcode,errormessage,username) =>
            {
                if (errorcode==1)
                {
                    this.Hide();
                    GameForm game = new GameForm(connection, currentGame);
                    game.messageTextBox.Text = errormessage;
                    game.usernameTextBox.Text=username;
                    game.Show();
                }
                else
                {
                    MessageBox.Show(errormessage);
                    this.Show();
                }
                return;
            });
        }
        private async void creaeGameButton_Click(object sender, EventArgs e)
        {

            GameForm.markk = "X";
            await connection.InvokeAsync("creategame",  3, 2);
            GameForm gameForm = new GameForm(connection,currentGame);
            creaeGameButton.Enabled = false;
            joinToGameButton.Enabled = false;
            MessageBox.Show("wait for opponent connection");
        }
        public void InitializeList()
        {
            
            availableGames.DataSource=allGames;
            
            availableGames.DisplayMember = "DisplayMember";                       
        }

        private async void joinToGameButton_Click(object sender, EventArgs e)
        {

            var createdGames = allGames.Where(x => x?.StateId == 1);
            if (createdGames.Count()>0)
            {
                var data = availableGames.SelectedItem as GameData;
                if (createdGames.Any(x => x?.GameId == data?.GameId))
                {
                    GameForm.markk = "O";
                    await connection.InvokeAsync("jointogame", data?.GameId);
                    
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("this game is already started");
                    return;
                }

            }
            else
            {
                MessageBox.Show("there is no games to join");               
            }
            
        }
    }
}
