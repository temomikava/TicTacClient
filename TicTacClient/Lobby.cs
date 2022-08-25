using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Text.Json;

namespace TicTacClient
{
    public partial class Lobby : Form
    {
        HubConnection connection { get; set; }
        public static BindingList<GameData?> allGames = new BindingList<GameData?>();
        public static BindingList<GameData?> gamesForRejoin = new BindingList<GameData?>();
        public Lobby(HubConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            this.Load += Lobby_Load;
            InitializeList();
        }
        public GameData? currentGame { get; set; }
        public GameForm GameForm { get; set; }
        private void Lobby_Load(object? sender, EventArgs e)
        {

            connection.On<JsonElement,string>("getmovehistory", (moves,username) =>
            {
                string json = moves.GetRawText();
                var result = JsonConvert.DeserializeObject<int[]>(json);
                string[] arr=new string[result.Length];

                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i]==1)
                    {
                        arr[i] = "X";
                    }
                    if (result[i] == 0)
                    {
                        arr[i] = "0";
                    }
                    if (result[i] == -1)
                    {
                        arr[i] = "";
                    }
                }
                List<Button> buttons = new List<Button>();
                    GameForm.button1.Text = arr[0];
                    GameForm.button2.Text = arr[1];
                    GameForm.button3.Text = arr[2];
                    GameForm.button4.Text = arr[3];
                    GameForm.button5.Text = arr[4];
                    GameForm.button6.Text = arr[5];
                    GameForm.button7.Text = arr[6];
                    GameForm.button8.Text = arr[7];
                    GameForm.button9.Text = arr[8];
                buttons.Add(GameForm.button1);
                buttons.Add(GameForm.button2);
                buttons.Add(GameForm.button3);
                buttons.Add(GameForm.button4);
                buttons.Add(GameForm.button5);
                buttons.Add(GameForm.button6);
                buttons.Add(GameForm.button7);
                buttons.Add(GameForm.button8);
                buttons.Add(GameForm.button9);
                foreach (Button item in buttons)
                {
                    if (item.Text!="")
                    {
                        item.Enabled = false;
                    }
                }
                this.Hide();
                GameForm.usernameTextBox.Text = username;
                GameForm.Show();
                gamesForRejoin.Remove(gamesForRejoin.Where(x=>x.GameId==currentGame.GameId).First());
            });
            connection.On<JsonElement>("getcurrentgame", (game) =>
            {
                var json = game.GetRawText();
                currentGame = JsonConvert.DeserializeObject<GameData>(json);
                GameForm = new GameForm(connection, currentGame);

            });
            connection.On<int, string, string>("ongamejoin", (errorcode, errormessage, username) =>
            {
                if (errorcode == 1)
                {
                    this.Hide();
                    GameForm game = new GameForm(connection, currentGame);
                    game.messageTextBox.Text = errormessage;
                    game.usernameTextBox.Text = username;
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
            await connection.InvokeAsync("creategame", 3, 2);
            GameForm gameForm = new GameForm(connection, currentGame);
            creaeGameButton.Enabled = false;
            joinToGameButton.Enabled = false;
            MessageBox.Show("wait for opponent connection");
        }
        public void InitializeList()
        {

            availableGames.DataSource = allGames;

            availableGames.DisplayMember = "DisplayMember";
            gamesForReconnect.DataSource = gamesForRejoin;
            gamesForReconnect.DisplayMember = "DisplayMember";
        }

        private async void joinToGameButton_Click(object sender, EventArgs e)
        {

            var createdGames = allGames.Where(x => x?.StateId == 1);
            if (createdGames.Count() > 0)
            {
                var data = availableGames.SelectedItem as GameData;
                Lobby.gamesForRejoin.Remove(data);
                GameForm.markk = "O";
                await connection.InvokeAsync("jointogame", data?.GameId);
                this.Hide();
            }
            else
            {
                MessageBox.Show("there is no games to join");
            }

        }

        private async void rejoinbutton_click(object sender, EventArgs e)
        {
            var games = gamesForRejoin;
            if (games.Count() > 0)
            {
                var game = gamesForReconnect.SelectedItem as GameData;
                await connection.InvokeAsync("onreconnected", game.GameId);
            }
        }
    }
}
