using System.Windows;
using System.Windows.Controls;

namespace GuiVSIXProject
{
    public partial class MyToolWindowControl : UserControl
    {
        public MyToolWindowControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("GuiVSIXProject", "Button clicked");
        }
    }
}