using System;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class TextBoxes

    {
        public static void SetTextThreadSafe(this TextBox textBox, String argText)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                textBox.Invoke(new Action(() =>
                {
                    textBox.Text = argText;
                }));
            }
            else
            {
                textBox.Text = argText;
            }
        }

        public static void AppendTextThreadSafe(this TextBox textBox, String argText, Boolean useTimeStamp = false)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                textBox.Invoke(new Action(() =>
                {
                    if (useTimeStamp)
                    {
                        textBox.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + argText + Environment.NewLine);
                    }
                    else
                    {
                        textBox.AppendText(argText);
                    }
                }));
            }
            else
            {
                textBox.AppendText(argText);
            }
        }

        public static String GetTextThreadSafe(this TextBox textBox)
        {
            String text = null;

            if (Thread.CurrentThread.IsBackground)
            {
                textBox.Invoke(new Action(() =>
                {
                    text = textBox.Text;
                }));
            }
            else
            {
                text = textBox.Text;
            }

            return text;
        }
    }
}