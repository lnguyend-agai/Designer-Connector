
using System.Windows;


namespace WPFWindow
{
    /// <summary>
    /// Interaction logic for BoxDialog.xaml
    /// </summary>
    public partial class BoxDialog : Window
    {
        public BoxDialog()
        {
            InitializeComponent();
        }
        public void SetValue(long value, ulong? Max)
        {
            pbStatus.Value = value;
            pbStatus.Maximum = (double)Max;
        }
    }
}
