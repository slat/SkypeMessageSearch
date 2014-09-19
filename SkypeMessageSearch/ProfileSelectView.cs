using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace SkypeMessageSearch
{
    public partial class ProfileSelectView : Form
    {
        private bool _safeToExit;
        public ProfileSelectViewModel ViewModel { get; private set; }

        public ProfileSelectView()
        {
            InitializeComponent();
            WireupControls();

            LoadViewModel();
        }

        private void WireupControls()
        {
            Closing += OnClosing;
            _comboProfile.SelectedIndexChanged += (sender, args) =>
                ViewModel.Profile = ViewModel.ProfileDirectories[_comboProfile.SelectedIndex];
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = !_safeToExit;
        }

        private void LoadViewModel()
        {
            ViewModel = new ProfileSelectViewModel();

            _comboProfile.Items.Clear();
            _comboProfile.Items.AddRange(ViewModel.ProfileDirectories.Cast<object>().ToArray());
            if (_comboProfile.Items.Count > 0)
                _comboProfile.SelectedIndex = 0;
        }

        private void _linkCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _safeToExit = true;
            Application.Exit();
        }

        private void _btnOk_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.Profile))
                return;

            _safeToExit = true;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ProfileSelectView_Load(object sender, System.EventArgs e)
        {
            Text = string.Format("{0} | {1}", Application.ProductName, "Profile Select");
        }
    }
}
