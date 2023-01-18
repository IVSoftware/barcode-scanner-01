![screenshot](https://github.com/IVSoftware/barcode-scanner-01/blob/master/barcode-scanner-01/Screenshots/screenshot.png)

Scan detector requirements:

- Works for both barcodes and QR codes
- Works whether or not there is a focused `TextBox` to receive incoming key events.

***
**SOLUTION**

Install a MessageFilter for the main form in order to intercept WM_CHAR and append the char code to `StringBuilder` class.

    public partial class BarcodeScannerForm : Form, IMessageFilter
    {        
        public BarcodeScannerForm()
        {
            InitializeComponent();
            // Add message filter to hook WM_KEYDOWN events.
            Application.AddMessageFilter(this);
            Disposed += (sender, e) => Application.RemoveMessageFilter(this);
        }
        private readonly StringBuilder _buffer = new StringBuilder();
        const int WM_CHAR = 0x0102;
        public bool PreFilterMessage(ref Message m)
        {
            // SOLUTION DO THIS (Thanks Jimi!)
            if (m.Msg.Equals(WM_CHAR)) detectScan((char)m.WParam);
            // NOT THIS
            // if(m.Msg.Equals(WM_KEYDOWN)) detectScan((char)m.WParam);
            return false;
        }

The detector algorithm appends each new character to the buffer and restarts a 100 ms watchdog timer. If the WDT expires without a new keystroke, it checks to see how many characters have been received. If the count is > 8 it's considered a scan.

    private void detectScan(char @char)
    {
        Debug.WriteLine(@char);
        if(_keyCount == 0) _buffer.Clear();
        int charCountCapture = ++_keyCount;
        _buffer.Append(@char);
        Task
            .Delay(TimeSpan.FromSeconds(SECONDS_PER_CHARACTER_MIN_PERIOD))
            .GetAwaiter()
            .OnCompleted(() => 
            {
                if (charCountCapture.Equals(_keyCount))
                {
                    _keyCount = 0;
                    if(_buffer.Length > SCAN_MIN_LENGTH)
                    {
                        BeginInvoke(()=>MessageBox.Show(_buffer.Text));
                    }
                }
            });
    }
    int _keyCount = 0;
    const int SCAN_MIN_LENGTH = 8;
    const double SECONDS_PER_CHARACTER_MIN_PERIOD = 0.1;
}