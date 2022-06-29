using System;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.Csharp.UpdateGuiFromTask
{
    /// <summary>
    ///
    /// Methods for safely updating Windows Forms Controls from a method running on another thread
    ///
    /// 2021 06 15
    ///
    /// by Felipe La Rotta
    ///
    /// </summary>
    ///

    public class UpdateGui
    {
        private UpdateGui()
        {
            //not implemented
        }

        public static void setClipboardText(String argText)
        {
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

        public static void listboxAddItem(ListBox listBox, String argText, Boolean useTimestamp)
        {
            String timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (useTimestamp)
            {
                argText = timeStamp + ": " + argText;
            }

            if (Thread.CurrentThread.IsBackground)
            {
                listBox.Invoke(new Action(() =>
                {
                    listBox.Items.Add(argText);
                    listBox.SelectedIndex = listBox.Items.Count - 1;
                }));
            }
            else
            {
                listBox.Items.Add(argText);
                listBox.SelectedIndex = listBox.Items.Count - 1;
            }
        }

        public static void textBoxSetText(TextBox textBox, String argText)
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

        public static void dataGridViewSetBindingSource(DataGridView dataGridView, BindingSource bindingSource)
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

        public static void textBoxAppendText(TextBox textBox, String argText)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                textBox.Invoke(new Action(() =>
                {
                    textBox.AppendText(argText);
                }));
            }
            else
            {
                textBox.AppendText(argText);
            }
        }

        public static int listboxGetSelectedIndex(ListBox listBox)
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

        public static String listBoxGetText(ListBox listBox)
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

        public static void listboxSetSelectedIndex(ListBox listBox, int argIndex)
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

        public static void listboxClearAll(ListBox listBox)
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

        public static void progressBarSetMax(ProgressBar progressBar, int argMaxValue)
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

        public static void progressBarSetValue(ProgressBar progressBar, int argNewValue)
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