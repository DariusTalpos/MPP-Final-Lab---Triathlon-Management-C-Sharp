using CompetitionModel.model;
using CompetitionServices.services;

namespace CompetitionClient.forms
{
    public partial class MainMenuForm : Form, ICompetitionObserver
    {
        User appUser;
        ICompetitionServices server;

        public MainMenuForm()
        {
        }

        public void setup(User user, ICompetitionServices server)
        {
            InitializeComponent();
            appUser = user;
            this.server = server;
            userLabel.Text = "Hello, " + appUser.Username + "!";
            refreshParticipantTable();
            refreshRoundTable();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            server.logout(appUser, this);
            Application.Exit();
        }

        private void loadDataButton_Click(object sender, EventArgs e)
        {
            if (roundsGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("No round has been selected!");
                return;
            }
            refreshScoreTable();
        }

        public void refreshParticipantTable()
        {
            List<Participant> participants = server.getParticipantList();
            participantsGridView.DataSource = participants;
            participantsGridView.Columns[2].Visible = false;
        }

        public void refreshRoundTable()
        {
            List<Round> rounds = server.getRoundList();
            roundsGridView.DataSource = rounds;
            roundsGridView.Columns[1].Visible = false;
        }

        public void refreshScoreTable()
        {
            //List<Score> scores = server.getScoreListFromRound(Convert.ToInt64(roundsGridView.Rows[roundsGridView.CurrentRow.Index].Cells["id"].Value));
            //scoresGridView.DataSource = scores;
            scoresGridView.Columns[1].Visible = false;
            scoresGridView.Columns[3].Visible = false;
        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddDataForm addDataForm = new AddDataForm(server, appUser);
            addDataForm.Show();
        }

        public void newRound()
        {
            roundsGridView.BeginInvoke((Action)delegate { refreshRoundTable(); });
        }

        public void newScore(Score score)
        {
            participantsGridView.BeginInvoke((Action)delegate { refreshParticipantTable(); });
            if (score.Round.Id == Convert.ToInt64(roundsGridView.Rows[roundsGridView.CurrentRow.Index].Cells["id"].Value))
            {
                scoresGridView.BeginInvoke(delegate { refreshScoreTable(); });
            }
        }

        private void MainMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            server.logout(appUser, this);
            Application.Exit();
        }
    }
}
