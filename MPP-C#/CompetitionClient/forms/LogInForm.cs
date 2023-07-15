using CompetitionModel.model;
using CompetitionServices.services;

namespace CompetitionClient.forms
{
    public partial class LogInForm : Form
    {
        private ICompetitionServices server;
        private MainMenuForm mainMenuForm;

        public LogInForm(ICompetitionServices server, MainMenuForm mainMenuForm)
        {
            InitializeComponent();
            this.server = server;
            this.mainMenuForm = mainMenuForm;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            String username = usernameBox.Text;
            String password = passwordBox.Text;
            User appUser = new User(username, password);
            try
            {
                usernameBox.Clear();
                passwordBox.Clear();
                server.login(appUser, mainMenuForm);
                mainMenuForm.setup(appUser,server);
                mainMenuForm.Show();
                this.Hide();
            }
            catch (CompetitionException)
            {
                MessageBox.Show("There is no account with this username or password");
                return;
            }
        }
    }
}
