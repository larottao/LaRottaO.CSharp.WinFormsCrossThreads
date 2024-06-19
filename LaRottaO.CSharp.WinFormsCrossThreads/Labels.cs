using System;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class Labels

    {
        public static void SetTextThreadSafe(this Label label, String argText)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                label.Invoke(new Action(() =>
                {
                    label.Text = argText;
                }));
            }
            else
            {
                label.Text = argText;
            }
        }

        public static String GetTextThreadSafe(this Label label)
        {
            String text = null;

            if (Thread.CurrentThread.IsBackground)
            {
                label.Invoke(new Action(() =>
                {
                    text = label.Text;
                }));
            }
            else
            {
                text = label.Text;
            }

            return text;
        }
    }
}