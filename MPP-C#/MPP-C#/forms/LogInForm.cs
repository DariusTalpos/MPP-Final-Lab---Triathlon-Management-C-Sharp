using MPP_C_.domain;
using MPP_C_.forms;
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

namespace MPP_C_
{
    public partial class LogInForm : Form
    {
        User appUser;
        UserService userService;
        IDictionary<string, string> props;

        public LogInForm(IDictionary<string, string> _)
        {
            InitializeComponent();
            props = _;
            userService = new UserService(new UserDBRepo(props));
        }



        private void loginButton_Click(object sender, EventArgs e)
        {
            String username = usernameBox.Text;
            String password = passwordBox.Text;
            appUser = userService.userExists(username, password);
            if (appUser == null)
            {
                MessageBox.Show("There is no account with this username or password");
                return;
            }
            this.Hide();
            MainMenuForm mainMenuForm = new MainMenuForm(appUser, props);
            mainMenuForm.Show();
        }
    }
}
