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
        public GameData CurrentGame { get; set; }
        GameForm gameForm { get; set; }
        public static BindingList<GameData?> allGames = new BindingList<GameData?>();
        
        public Lobby(HubConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            this.Load += Lobby_Load;
            InitializeList();
        }
       
        private void Lobby_Load(object? sender, EventArgs e)
        {

            connection.On<JsonElement,int>("getcurrentgame", (game,id) =>
            {
                var json = game.GetRawText();
                CurrentGame = JsonConvert.DeserializeObject<GameData>(json);
                gameForm = new GameForm(connection, CurrentGame);
                if (id==CurrentGame.PlayerOne.Id)
                {
                    gameForm.markk = "X";
                }
                else
                {
                    gameForm.markk = "O";
                }


            });
            connection.On<int, int, string, string>("ongamejoin", (errorcode, gameId, errormessage, username) =>
            {
                if (errorcode == 1)
                {

                    gameForm.messageTextBox.Text = errormessage;
                    gameForm.usernameTextBox.Text = username;
                    gameForm.playerOneNameValue.Text = CurrentGame.PlayerOne.UserName;
                    gameForm.playerTwoNameValue.Text = CurrentGame.PlayerTwo.UserName;
                    gameForm.yourScoreValue.Text = CurrentGame.PlayerOneScore.ToString();
                    gameForm.opponentScoreValue.Text = CurrentGame.PlayerTwoScore.ToString();
                    gameForm.targetScoreValue.Text = CurrentGame.TargetScore.ToString();
                    gameForm.Show();
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
        }
        public void InitializeList()
        {

            availableGames.DataSource = allGames;

            availableGames.DisplayMember = "DisplayMember";
            
        }

        private async void joinToGameButton_Click(object sender, EventArgs e)
        {

            var createdGames = allGames.Where(x => x?.StateId == 1);
            if (createdGames.Count() > 0)
            {
                var data = availableGames.SelectedItem as GameData;

                
                await connection.InvokeAsync("jointogame", data?.GameId);
            }
            else
            {
                MessageBox.Show("there is no games to join");
            }

        }

        private async void rejoinbutton_click(object sender, EventArgs e)
        {
            
        }
    }
}
