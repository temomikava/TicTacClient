using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace TicTacClient
{
    public partial class Form1 : Form
    {
        private string _url = "https://localhost:44356/signalr";
        HubConnection connection;
        public Form1()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder().WithUrl(_url, options =>
            {
                options.AccessTokenProvider = async () => await Task.FromResult("05002795-2964-05b2-9dd9-ecce57768c3b");
            }).Build();
            connection.Closed += async (error) =>
            {
                MessageBox.Show("server died");
                Thread.Sleep(2000);
                this.Close();
                await connection.StartAsync();
            };
            this.Load += Form1_Load;
        }
        static GameData gameData = new GameData();

        private async void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                await connection.StartAsync();
                MessageBox.Show("signalR connected to the server!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            connection.On<JsonElement>("getcurrentgame", (game) =>
            {
               var json = game.GetRawText();
               gameData= JsonConvert.DeserializeObject<GameData>(json);
               

            });
            await connection.InvokeAsync("creategame", 3, 2);

          
        }
        //public static T ToObject<T>( JsonElement element)
        //{
        //    var json = element.GetRawText();
        //    return JsonSerializer.Deserialize<T>(json);
        //}
    }
}