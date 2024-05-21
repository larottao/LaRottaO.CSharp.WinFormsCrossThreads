using System;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    /// <summary>
    ///
    /// Methods for safely updating Windows Forms Controls from a method running on another thread
    ///
    /// Original: 2021 06 15
    ///
    /// Converted to extension method: 2024 05 21
    ///
    /// by Felipe La Rotta
    ///
    /// </summary>
    ///

    public static class WinFormsCrossThreads
    {
        public static void SetClipboardTextFromAnotherThread(String argText)
        {
            if (argText == null)
            {
                return;
            }

            Thread thread = new Thread((ThreadStart)delegate
           {
               Clipboard.SetText(argText);
           });

            try
            {
                thread.TrySetApartmentState(ApartmentState.STA);
                thread.Start();

                while (!thread.IsAlive) { Thread.Sleep(1); }

                Thread.Sleep(1);
                thread.Join();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to set text on clipboard: " + ex.ToString());
            }
        }

        public static void AddItemFromAnotherThread(this ListBox listBox, String argText, Boolean useTimestamp, Boolean setIndexToLast)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                listBox.Invoke(new Action(() =>
                {
                    if (useTimestamp)
                    {
                        listBox.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + argText);
                    }
                    else
                    {
                        listBox.Items.Add(argText);
                    }

                    if (setIndexToLast)
                    {
                        listBox.SelectedIndex = listBox.Items.Count - 1;
                    }
                }));
            }
            else
            {
                listBox.Items.Add(argText);

                if (setIndexToLast)
                {
                    listBox.SelectedIndex = listBox.Items.Count - 1;
                }
            }
        }

        public static void SetTextFromAnotherThread(this TextBox textBox, String argText)
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

        public static void SetBindingSourceFromAnotherThread(this DataGridView dataGridView, BindingSource bindingSource)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                dataGridView.Invoke(new Action(() =>
                {
                    dataGridView.DataSource = bindingSource;
                }));
            }
            else
            {
                dataGridView.DataSource = bindingSource;
            }
        }

        public static void AppendTextFromAnotherThread(this TextBox textBox, String argText, Boolean useTimeStamp = false)
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

        public static int GetSelectedIndexFromAnotherThread(this ListBox listBox)
        {
            int selectedIndex = -1;

            if (Thread.CurrentThread.IsBackground)
            {
                listBox.Invoke(new Action(() =>
                {
                    selectedIndex = listBox.SelectedIndex;
                }));
            }
            else
            {
                selectedIndex = listBox.SelectedIndex;
            }

            return selectedIndex;
        }

        public static String GetTextFromAnotherThread(this ListBox listBox)
        {
            String texto = null;

            if (Thread.CurrentThread.IsBackground)
            {
                listBox.Invoke(new Action(() =>
                {
                    texto = listBox.Text;
                }));
            }
            else
            {
                texto = listBox.Text;
            }

            return texto;
        }

        public static void SetSelectedIndexFromAnotherThread(this ListBox listBox, int argIndex)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                listBox.Invoke(new Action(() =>
                {
                    listBox.SelectedIndex = argIndex;
                }));
            }
            else
            {
                listBox.SelectedIndex = argIndex;
            }
        }

        public static void ClearFromAnotherThread(this ListBox listBox)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                listBox.Invoke(new Action(() =>
                {
                    listBox.Items.Clear();
                }));
            }
            else
            {
                listBox.Items.Clear();
            }
        }

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