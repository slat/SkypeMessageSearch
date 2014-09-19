using System;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;

namespace SkypeMessageSearch
{
    /// <summary>
    /// Inspired from here:
    /// http://stackoverflow.com/questions/15868817/button-inside-a-winforms-textbox/18050445
    /// </summary>
    public class SearchTextBox : TextBox
    {
        private readonly PictureBox _picSearch;
        private readonly Button _btnClear;
        private string _placeholder;
        private readonly System.Timers.Timer _delayTimer; // used for the delay
        private bool _keysPressed; // makes event fire immediately if it wasn't a keypress
        private bool _timerElapsed; // if true OnTextChanged is fired.
        private string _oldValue;
        private const int DelayTime = 200;
        private delegate void DelayOverHandler();

        public SearchTextBox()
        {
            // Initialize Timer
            _delayTimer = new System.Timers.Timer(DelayTime);
            _delayTimer.Elapsed += DelayTimer_Elapsed;
            _picSearch = new PictureBox {Image = Properties.Resources.search};
            _picSearch.SizeChanged += (o, e) => OnResize(e);
            _btnClear = new Button {Image = Properties.Resources.clear, Visible = false};
            _btnClear.SizeChanged += (o, e) => OnResize(e);
            _btnClear.Click += BtnClearOnClick;
            Controls.Add(_picSearch);
            Controls.Add(_btnClear);
            ApplyCue();
        }

        public event EventHandler DelayTextChanged;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Appearance")]
        [DefaultValue("Search")]
        [Description("Set placeholder text to be diplayed when no text is entered.")]
        public string Placeholder
        {
            get { return _placeholder; }
            set
            {
                _placeholder = value;
                ApplyCue();
            }
        }

        private void BtnClearOnClick(object sender, EventArgs e)
        {
            Text = "";
            SearchTriggered();
            Select();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            var empty = Text.Length == 0;
            _btnClear.Visible = !empty;
            _picSearch.Visible = empty;
            ApplyCue();
        }

        private void ApplyCue()
        {
            if (Text.Length == 0 && !string.IsNullOrEmpty(Placeholder))
                CueProvider.SetCue(this, " " + Placeholder);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _picSearch.Size = new Size(16, 16);
            _picSearch.Location = new Point(ClientSize.Width - _picSearch.Width, 2);
            _picSearch.Cursor = Cursors.Default;
            
            _btnClear.Size = new Size(25, ClientSize.Height + 2);
            _btnClear.Location = new Point(ClientSize.Width - _btnClear.Width, -1);
            _btnClear.Cursor = Cursors.Default;

            // Send EM_SETMARGINS to prevent text from disappearing underneath the button or pic
            SendMessage(Handle, 0xd3, (IntPtr)2, (IntPtr)(_btnClear.Width << 16));
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private void DelayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // stop timer.
            _delayTimer.Enabled = false;

            // set timer elapsed to true, so the OnTextChange knows to fire
            _timerElapsed = true;

            try
            {
                // use invoke to get back on the UI thread.
                Invoke(new DelayOverHandler(DelayOver), null);
            }
            catch
            {

            }
        }

        private void DelayOver()
        {
            OnDelayTextChanged(new EventArgs());
        }

        private void OnDelayTextChanged(EventArgs e)
        {
            // if the timer elapsed or text was changed by something besides a keystroke
            // fire base.OnTextChanged
            if (!_timerElapsed && _keysPressed) return;

            _timerElapsed = false;
            _keysPressed = false;

            // same as old value, has not changed
            if (Text.Equals(_oldValue))
                return;

            _oldValue = Text;

            if (DelayTextChanged != null)
                DelayTextChanged(this, e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Text = "";
                e.SuppressKeyPress = true;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            SearchTriggered();
        }

        private void SearchTriggered()
        {
            // Reset timer
            _delayTimer.Enabled = false;
            _delayTimer.Enabled = true;
            _keysPressed = true;
        }
    }
}
