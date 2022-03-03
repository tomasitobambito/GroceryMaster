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

        public string Answer => TxtAnswer.Text;
    }
}