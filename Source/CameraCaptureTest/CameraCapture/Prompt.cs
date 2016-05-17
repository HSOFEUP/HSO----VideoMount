using System;
using System.Drawing;
using System.Windows.Forms;


namespace CameraCapture
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form();
            prompt.Icon = new Icon("VideoMounting.ico");
            prompt.StartPosition = FormStartPosition.CenterScreen;
            
            prompt.Width = 500;
            prompt.Height = 150;
            prompt.Text = caption;
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            textBox.PasswordChar = '*';
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Focus();
            prompt.AcceptButton = confirmation;
            
            prompt.ShowDialog();
            
            return textBox.Text;
        }
    }
}