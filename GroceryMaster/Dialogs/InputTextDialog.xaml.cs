using System;
using System.Windows;

namespace GroceryMaster.Dialogs
{
    public partial class InputTextDialog : Window
    {
        public InputTextDialog(string question, string defaultAnswer = "")
        {
            InitializeComponent();
            LblQuestion.Content = question;
            TxtAnswer.Text = defaultAnswer;
        }

        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            TxtAnswer.SelectAll();
            TxtAnswer.Focus();
        }

        public string Answer => TxtAnswer.Text;
    }
}