using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.Json;

namespace TicTacClient
{
    public partial class Form1 : Form
    {
        private readonly string _url = "https://localhost:44356/signalr";
        HubConnection connection;
        Lobby lobby;
        public Form1()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder().WithUrl(_url, options =>
            {
                options.AccessTokenProvider = async () => await Task.FromResult("05002795-2964-05b2-9dd9-ecce57768c3b");
            }).Build();
            
            this.Load += Form1_Load;
            lobby = new Lobby(connection);
        }
       // public  GameData? currentGame { get; set; }

        private async void Form1_Load(object? sender, EventArgs e)
        {

            connection.Closed += async (error) =>
            {
                MessageBox.Show("server disconnected");                
                await connection.StartAsync();
                this.Show();
                MessageBox.Show("connected");

            };
            //connection.On<JsonElement>("getcurrentgame", (game) =>
            //{
            //   var json = game.GetRawText();
            //   currentGame = JsonConvert.DeserializeObject<GameData>(json);

            //});
           

            connection.On<List<JsonElement>>("getallgame", (games) =>
            {
                List<string> jsons = new List<string>();
                foreach (JsonElement element in games)
                {
                    jsons.Add(element.GetRawText());
                }
                lobby.allGames.Clear();
                foreach (string json in jsons)
                {
                    lobby.allGames.Add(JsonConvert.DeserializeObject<GameData>(json));
                }
                
            });

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.StartAsync();
                MessageBox.Show("you are connected!");
                lobby.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
        //public static T ToObject<T>( JsonElement element)
        //{
        //    var json = element.GetRawText();
        //    return JsonSerializer.Deserialize<T>(json);
        //}
    }
}