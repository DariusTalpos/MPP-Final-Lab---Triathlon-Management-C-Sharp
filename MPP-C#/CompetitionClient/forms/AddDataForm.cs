using CompetitionModel.model;
using CompetitionServices.services;

namespace CompetitionClient.forms
{
    public partial class AddDataForm : Form
    {
        ICompetitionServices server;
        User appUser;


        public AddDataForm(ICompetitionServices server,User appUser)
        {
            InitializeComponent();
            this.server = server;
            this.appUser = appUser;
            reloadData();
        }

        private void saveDataButton_Click(object sender, EventArgs e)
        {
            if ((roundBox.Text == "") || (pointsBox.Text == ""))
            {
                MessageBox.Show("The fields must be completed!");
                return;
            }
            String roundName = roundBox.Text; 
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
            if (participantGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("No participant selected!");
                return;
            }
            Participant participant = (Participant)participantGridView.CurrentRow.DataBoundItem;
            server.addRoundScore(roundName, participant, points);
            MessageBox.Show("Data saved successfully!");
            reloadData();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenuForm mainMenuForm = new MainMenuForm();
            mainMenuForm.setup(appUser, server);
            mainMenuForm.Show();
        }

        private void reloadData()
        {
            List<Participant> list = server.getParticipantList();
            participantGridView.DataSource = list;
            participantGridView.Columns[2].Visible = false;
        }
    }
}
