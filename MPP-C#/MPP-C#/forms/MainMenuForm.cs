using MPP_C_.domain;
using MPP_C_.repository.repos;
using MPP_C_.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPP_C_.forms
{
    internal partial class MainMenuForm : Form
    {
        IDictionary<string, string> props;
        User appUser;
        ParticipantService participantService;
        RoundService roundService;
        ScoreService scoreService;



        public MainMenuForm(User user, IDictionary<string, string> _)
        {
            InitializeComponent();
            props = _;
            appUser = user;
            participantService = new ParticipantService(new ParticipantDBRepo(props));
            roundService = new RoundService(new RoundDBRepo(props));
            scoreService = new ScoreService(new ScoreDBRepo(props));

            userLabel.Text = "Hello, " + appUser.Username + "!";
            List<Participant> participants = participantService.getParticipantList();
            participantsGridView.DataSource = participants;
            participantsGridView.Columns[2].Visible = false;
            List<Round> rounds = roundService.getRoundList();
            roundsGridView.DataSource = rounds;
            roundsGridView.Columns[1].Visible = false;
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm logInForm = new LogInForm(props);
            logInForm.Show();
        }

        private void loadDataButton_Click(object sender, EventArgs e)
        {
            if (roundsGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("No round has been selected!");
                return;
            }
            List<Score> scores = scoreService.getScoreListFromRound(Convert.ToInt64(roundsGridView.Rows[roundsGridView.CurrentRow.Index].Cells["id"].Value));
            scoresGridView.DataSource = scores;
            scoresGridView.Columns[1].Visible = false;
            scoresGridView.Columns[3].Visible = false;
        }

        private void addDataButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddDataForm addDataForm = new AddDataForm(appUser, props);
            addDataForm.Show();
        }
    }
}
