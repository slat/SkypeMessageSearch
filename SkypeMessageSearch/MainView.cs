using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SkypeMessageSearch
{
    public partial class MainView : Form, IMessageFilter
    {
        private readonly MainViewModel _viewModel;

        public MainView()
        {
            InitializeComponent();
            InitializeGrid();

            _viewModel = new MainViewModel();
            _viewModel.Ready(this);

            _comboContact.Items.Add("All");
            _comboContact.SelectedIndex = 0;

            _txtSearch.DelayTextChanged += TxtSearchOnDelayTextChanged;
            _comboContact.TextChanged += ComboContactOnTextChanged;

            Application.AddMessageFilter(this);
        }

        /// <summary>
        /// Ensure Mouse Wheel is used on Grid even if not focused.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public virtual bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x20a:
                    // WM_MOUSEWHEEL, find the control at screen position
                    IntPtr hWnd = WindowFromPoint(MousePosition);
                    if (hWnd != IntPtr.Zero && hWnd != m.HWnd && FromHandle(hWnd) != null)
                    {
                        SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
                        return true;
                    }
                    break;
            }

            return false;
        }

        // P/Invoke declarations
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pt);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("user32.dll")]
        private extern static int SendMessage(IntPtr hwnd, uint msg, int wParam, int lParam); 

        private void InitializeGrid()
        {
            _grid.VirtualMode = true;
            _grid.ShowCellToolTips = true;
            _grid.DoubleBuffered(true);
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToResizeRows = false;
            _grid.ReadOnly = true;
            _grid.RowHeadersVisible = false;
            _grid.CellValueNeeded += GridOnCellValueNeeded;
            _grid.CellMouseEnter += GridOnCellMouseEnter;
            _grid.Columns.Add(new DataGridViewTextBoxColumn { Visible = false });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Conversation", Width = 200, SortMode = DataGridViewColumnSortMode.NotSortable });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Timestamp", Width = 140, SortMode = DataGridViewColumnSortMode.NotSortable });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Author", Width = 130, SortMode = DataGridViewColumnSortMode.NotSortable });
            _grid.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Message", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, SortMode = DataGridViewColumnSortMode.NotSortable });
        }

        public int Limit
        {
            get { return _grid.DisplayedRowCount(true); }
        }

        public int Offset
        {
            get { return _grid.FirstDisplayedCell != null ? _grid.FirstDisplayedCell.RowIndex : 0; }
        }

        public int RowCount
        {
            set
            {
                _grid.Rows.Clear();
                _grid.RowCount = value;
                _grid.Invalidate();
            }
        }

        public void DisplayFooter(int count)
        {
            _labelFooter.Text = string.Format("Showing {0} Messages", count);
        }

        private void TxtSearchOnDelayTextChanged(object sender, EventArgs e)
        {
            _viewModel.FilterMessage = _txtSearch.Text;
            _viewModel.Reload();
        }

        /// <summary>
        /// Render tooltips for Message Column.
        /// todo: Consider alternative tooltip rendering? (Like a unobtrusive window?)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridOnCellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            _grid.ShowCellToolTips = true;
            _toolTip.Hide(this);

            if (e.RowIndex < 0 || e.ColumnIndex != 4)
                return;

            if (!_viewModel.Cache.ContainsKey(e.RowIndex))
                return;

            var val = _viewModel.Cache[e.RowIndex][e.ColumnIndex].ToString();

            _grid.ShowCellToolTips = false;
            _toolTip.Show(val,
                _grid,
                // Show top left of Grid.
                0, 0,
                (int)TimeSpan.FromSeconds(60).TotalMilliseconds);

            // Set maximum width for tooltip and wordwrapping.
            // 385px is approximately the standard width in Skype window.
            const int maxWidth = 385;
            var o = typeof(ToolTip).InvokeMember("Handle",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty,
                null, _toolTip, null);
            var hwnd = (IntPtr)o;
            SendMessage(hwnd, 0x0418, 0, maxWidth);
        }

        private void GridOnCellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (!_viewModel.Cache.ContainsKey(e.RowIndex))
                _viewModel.LoadPage();

            e.Value = _viewModel.Cache.ContainsKey(e.RowIndex) ? _viewModel.Cache[e.RowIndex][e.ColumnIndex] : "???";
        }

        private void MainView_Load(object sender, EventArgs e)
        {
            Text = string.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);
            _dateFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            _dateFrom.Checked = false;
            _viewModel.FilterFrom = null;
            
            var profileSelectView = new ProfileSelectView();
            if (profileSelectView.ShowDialog() == DialogResult.OK)
            {
                _viewModel.Load(profileSelectView.ViewModel.Profile, 
                    profileSelectView.ViewModel.ProfileDatabasePath);
            }
        }

        private void _checkMessageToggle_CheckedChanged(object sender, EventArgs e)
        {
            _checkMessageToggle.Text = _checkMessageToggle.Checked ? "And" : "Any";
            _viewModel.FilterMessageAnd = _checkMessageToggle.Checked;
            _viewModel.Reload();
        }

        private void _dateFrom_ValueChanged(object sender, EventArgs e)
        {
            _viewModel.FilterFrom = _dateFrom.Checked ? (DateTime?)_dateFrom.Value : null;
            _viewModel.Reload();
        }

        private void _dateTo_ValueChanged(object sender, EventArgs e)
        {
            _viewModel.FilterTo = _dateTo.Checked ? (DateTime?)_dateTo.Value : null;
            _viewModel.Reload();
        }

        public void LoadContacts(ICollection<SkypeContact> contacts)
        {
            _comboContact.Items.AddRange(contacts.Select(x => x.ToString()).Cast<object>().ToArray());
            _comboContact.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            _comboContact.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void ComboContactOnTextChanged(object sender, EventArgs e)
        {
            if (_comboContact.Items.Contains(_comboContact.Text))
            {
                _viewModel.FilterContact = _comboContact.Text;
                _viewModel.Reload();
            }
        }

        private void _btnContactClear_Click(object sender, EventArgs e)
        {
            _comboContact.Text = "All";
        }
    }
}
