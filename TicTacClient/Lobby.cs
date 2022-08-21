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
        public GameData game { get; set; }
        public BindingList<GameData?> allGames=new BindingList<GameData?>();
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

            });
        }

        private async void creaeGameButton_Click(object sender, EventArgs e)
        {

            await connection.InvokeAsync("creategame", 3, 2);
            GameForm gameForm = new GameForm(connection,currentGame);

            this.Hide();
            gameForm.Show();
        }
        public void InitializeList()
        {
            
            availableGames.DataSource=allGames;
            
            availableGames.DisplayMember = "DisplayMember";                       
        }
        
    }
}
