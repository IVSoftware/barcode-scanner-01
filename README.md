

To make a scan detector that works for both barcodes and QR codes, install a MessageFilter for the main form in order to intercept WM_KEYDOWN and append the key code to a `Buffer` class.

    public partial class BarcodeScannerForm : Form, IMessageFilter
    {        
        public BarcodeScannerForm()
        {
            InitializeComponent();
            // Add message filter to hook WM_KEYDOWN events.
            Application.AddMessageFilter(this);
            Disposed += (sender, e) => Application.RemoveMessageFilter(this);
            _buffer = new Buffer(this);
        }
        .
        .
        .
    }
    class Buffer
    {
        StringBuilder _sbScan = new StringBuilder();
        public void Append(char @char)
        {
            _sbScan.Append(@char);
        }
        public string Text => _sbScan.ToString(); 
        public int Length => _sbScan.Length;
        public void Clear() => _sbScan.Clear();
        // The owner arg is not used but now it can be constucted the same.
        public Buffer(Form owner){ } // 
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