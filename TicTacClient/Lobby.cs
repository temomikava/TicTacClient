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
        HubConnection connection;
        List<GameData?> gameDatas;
        public Lobby(HubConnection connection,List<GameData?> gameDatas)
        {
            InitializeComponent();
            this.connection = connection;
            this.Load += Lobby_Load;
            this.gameDatas = gameDatas;
        }

        private void Lobby_Load(object? sender, EventArgs e)
        {
            connection.On<List<JsonElement>>("getallgame", (games) =>
            {
                List<string> jsons = new List<string>();
                foreach (JsonElement element in games)
                {
                    jsons.Add(element.GetRawText());
                }
                List<GameData?> datas = new List<GameData?>();
                foreach (string json in jsons)
                {
                    datas.Add(JsonConvert.DeserializeObject<GameData>(json));
                }
            });
        }

        private void creaeGameButton_Click(object sender, EventArgs e)
        {
            connection.InvokeAsync("creategame", 3, 2);         
        }
        public void ClientEvents(object sender, EventArgs e)
        {
            
        }
        
    }
}
