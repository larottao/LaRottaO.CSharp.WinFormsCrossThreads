using System;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class ProgressBars
    {
        public static void SetMaxThreadSafe(this ProgressBar progressBar, int argMaxValue)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                progressBar.Invoke(new Action(() =>
                {
                    progressBar.Maximum = argMaxValue;
                }));
            }
            else
            {
                progressBar.Maximum = argMaxValue;
            }
        }

        public static void SetValueThreadSafe(this ProgressBar progressBar, int argNewValue)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                progressBar.BeginInvoke(new Action(() =>
                {
                    progressBar.Value = argNewValue;
                }));
            }
            else
            {
                progressBar.Value = argNewValue;
            }
        }
    }
}