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
    internal partial class AddDataForm : Form
    {
        User appUser;
        IDictionary<string, string> props;
        ParticipantService participantService;
        RoundService roundService;
        ScoreService scoreService;


        public AddDataForm(User user, IDictionary<string, string> _)
        {
            InitializeComponent();
            props = _;
            appUser = user;
            participantService = new ParticipantService(new ParticipantDBRepo(props));
            roundService = new RoundService(new RoundDBRepo(props));
            scoreService = new ScoreService(new ScoreDBRepo(props));
            reloadData();
        }

        private void saveDataButton_Click(object sender, EventArgs e)
        {
            if ((roundBox.Text == "") || (pointsBox.Text == ""))
            {
                MessageBox.Show("The fields must be completed!");
                return;
            }
            int points;
            try
            {
                points = Convert.ToInt32(pointsBox.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("Point amount must be a number");
                return;
            }
            string roundName = Convert.ToString(roundBox.Text);
            Round round = roundService.getRoundWithName(roundName);
            if(round == null) 
            {
                roundService.save(roundName);
                round = roundService.getRoundWithName(roundName);
            }
            if(participantGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("No participant selected!");
                return;
            }
            Participant participant = (Participant)participantGridView.CurrentRow.DataBoundItem;
            scoreService.save(round, participant, points);
            participantService.updatePoints(participant, points);
            MessageBox.Show("Data saved successfully!");
            reloadData();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenuForm mainMenuForm = new MainMenuForm(appUser, props);
            mainMenuForm.Show();
        }

        private void reloadData()
        {
            List<Participant> list = participantService.getParticipantList();
            participantGridView.DataSource = list;
            participantGridView.Columns[2].Visible = false;
        }
    }
}
