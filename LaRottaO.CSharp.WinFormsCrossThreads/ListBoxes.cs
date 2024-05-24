using System;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class ListBoxes
    {
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
    }
}