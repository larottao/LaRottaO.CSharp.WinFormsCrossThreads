using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class ProgressBars
    {
        public static void SetMaxFromAnotherThread(this ProgressBar progressBar, int argMaxValue)
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

        public static void SetValueFromAnotherThread(this ProgressBar progressBar, int argNewValue)
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