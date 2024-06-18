using System;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class ComboBoxes
    {
        public static String GetTextThreadSafe(this ComboBox comboBox)
        {
            String text = "";

            if (Thread.CurrentThread.IsBackground)
            {
                comboBox.Invoke(new Action(() =>
                {
                    text = comboBox.Text;
                }));
            }
            else
            {
                text = comboBox.Text;
            }

            return text;
        }
    }
}