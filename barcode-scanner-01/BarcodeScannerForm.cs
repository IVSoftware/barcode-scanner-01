using QRCoder;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static QRCoder.QRCodeGenerator;

namespace barcode_scanner_00
{
    public partial class BarcodeScannerForm : Form, IMessageFilter
    {
        public BarcodeScannerForm()
        {
            InitializeComponent();
            // Add message filter to hook WM_KEYDOWN events.
            Application.AddMessageFilter(this);
            Disposed += (sender, e) => Application.RemoveMessageFilter(this);
            // Create the visible codes.
            initImages();
            _buffer = new Buffer(this);
        }
        private readonly Buffer _buffer;
        const int WM_CHAR = 0x0102;
        public bool PreFilterMessage(ref Message m)
        {
            // if(m.Msg.Equals(WM_KEYDOWN)) detectScan((char)m.WParam);
            if (m.Msg.Equals(WM_CHAR)) detectScan((char)m.WParam);
            return false;
        }
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

        private readonly QRCodeGenerator _generator = new QRCodeGenerator();
        private void displayBarCode(string text)
        {
            if (text.Length > 16)
            {
                pictureBoxBC.Image = null;
            }
            else
            {
                BarcodeLib.Barcode barcode = new BarcodeLib.Barcode();
                pictureBoxBC.Image =
                    barcode
                    .Encode(
                        BarcodeLib.TYPE.CODE128,
                        text,
                        pictureBoxBC.Width,
                        pictureBoxBC.Height
                    );
            }
        }
        private void initImages()
        {
            displayBarCode("0123456789ABCDE");
            displayQRCode("https://github.com/IVSoftware?tab=repositories");
        }

        private void displayQRCode(string text)
        {
            var qrCode = new QRCode(
                _generator
                .CreateQrCode(text, ECCLevel.Q)
            );
            var image = new Bitmap(qrCode.GetGraphic(20), pictureBoxQR.Size);
            pictureBoxQR.Image = image;
        }
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
}