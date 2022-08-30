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
                options.AccessTokenProvider = async () => await Task.FromResult("8b49d9dc-7758-8890-bc64-b634c764dfda");
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
                Lobby.allGames.Clear();
                foreach (string json in jsons)
                {
                    Lobby.allGames.Add(JsonConvert.DeserializeObject<GameData>(json));
                }

            });

            connection.On<List<JsonElement>, Dictionary<int, int[]>, int,int>("onreconnected", (games, moves, userid,currentplayerid) =>
            {
                List<string> jsons = new List<string>();
                List<GameData> Games = new List<GameData>();
                foreach (var game in games)
                {
                    jsons.Add(game.GetRawText());
                }
                foreach (var json in jsons)
                {
                    var game=JsonConvert.DeserializeObject<GameData>(json);
                    Games.Add(game);
                }
                foreach (var game in Games)
                {
                    GameForm form = new GameForm(connection, game);
                    var array = moves.Where(x => x.Key == game.GameId).Single().Value;
                    string[] arr = new string[array.Length];

                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (array[i] == 1)
                        {
                            arr[i] = "X";
                        }
                        if (array[i] == 0)
                        {
                            arr[i] = "0";
                        }
                        if (array[i] == -1)
                        {
                            arr[i] = "";
                        }
                    }
                    List<Button> buttons = new List<Button>();
                    form.button1.Text = arr[0];
                    form.button2.Text = arr[1];
                    form.button3.Text = arr[2];
                    form.button4.Text = arr[3];
                    form.button5.Text = arr[4];
                    form.button6.Text = arr[5];
                    form.button7.Text = arr[6];
                    form.button8.Text = arr[7];
                    form.button9.Text = arr[8];
                    buttons.Add(form.button1);
                    buttons.Add(form.button2);
                    buttons.Add(form.button3);
                    buttons.Add(form.button4);
                    buttons.Add(form.button5);
                    buttons.Add(form.button6);
                    buttons.Add(form.button7);
                    buttons.Add(form.button8);
                    buttons.Add(form.button9);
                    foreach (Button item in buttons)
                    {
                        if (item.Text != "")
                        {
                            item.Enabled = false;
                        }
                    }
                    this.Hide();
                    form.usernameTextBox.Text = userid == game.PlayerOne.Id ? game.PlayerOne.UserName : game.PlayerTwo.UserName;
                    form.markk = userid == game.PlayerOne.Id ? "X" : "O";
                    form.Show();
                }


            });


        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                await connection.StartAsync();
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