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
          
            connection.On<JsonElement>("getcurrentgame", (game) =>
            {
                var json = game.GetRawText();
                currentGame = JsonConvert.DeserializeObject<GameData>(json);

            });
            connection.On<int, string, string>("ongamejoin", (errorcode, errormessage, username) =>
            {
                if (errorcode == 1)
                {
                    GameForm.messageTextBox.Text = errormessage;
                    GameForm.usernameTextBox.Text = username;
                    GameForm.playerOneNameValue.Text = currentGame.PlayerOne.UserName;
                    GameForm.playerTwoNameValue.Text= currentGame.PlayerTwo.UserName;
                    GameForm.yourScoreValue.Text=currentGame.PlayerOneScore.ToString();
                    GameForm.opponentScoreValue.Text=currentGame.PlayerTwoScore.ToString();
                    GameForm.targetScoreValue.Text= currentGame.TargetScore.ToString();
                    GameForm.Show();
                }
                else
                {
                    MessageBox.Show(errormessage);
                }
                return;
            });


        }
        private async void creaeGameButton_Click(object sender, EventArgs e)
        {

            await connection.InvokeAsync("creategame", 9, 2);
            GameForm = new GameForm(connection, currentGame);
            GameForm.markk = "X";

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
                if (data.StateId == 1)
                {
                    Lobby.gamesForRejoin.Remove(data);
                    GameForm = new GameForm(connection, data);
                    GameForm.markk = "O";
                    await connection.InvokeAsync("jointogame", data?.GameId);
                   
                }
                else
                {
                    MessageBox.Show("this game is not in created state");
                }

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
