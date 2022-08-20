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
        public  List<GameData?>? gameDatas { get; set; }
        public Lobby(HubConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            gameDatas = new List<GameData>();
            InitializeList();
        }

       

        private async void creaeGameButton_Click(object sender, EventArgs e)
        {
          await connection.InvokeAsync("creategame", 3, 2);     
        }
        public void InitializeList()
        {
            availableGames.DataSource = null;
            availableGames.DataSource = gameDatas;
            availableGames.DisplayMember = "DisplayMember";
        }
        
    }
}
