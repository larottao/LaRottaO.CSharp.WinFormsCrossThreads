using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class DataGridViews
    {
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

        public static int GetSelectedIndexFromAnotherThread(this DataGridView dataGridView)
        {
            try
            {
                int index = 0;

                if (Thread.CurrentThread.IsBackground)
                {
                    dataGridView.Invoke(new Action(() =>
                    {
                        index = dataGridView.SelectedRows[0].Index;
                    }));
                }
                else
                {
                    index = dataGridView.SelectedRows[0].Index;
                }

                return index;
            }
            catch
            {
                return -1;
            }
        }

        public static void RefreshFromAnotherThread(this DataGridView dataGridView)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                dataGridView.Invoke(new Action(() =>
                {
                    dataGridView.Refresh();
                }));
            }
            else
            {
                dataGridView.Refresh();
            }
        }

        public static void SetFirstDesplayedScrollingRowIndexFromAnotherThread(this DataGridView dataGridView, int index)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                dataGridView.Invoke(new Action(() =>
                {
                    dataGridView.FirstDisplayedScrollingRowIndex = index;
                }));
            }
            else
            {
                dataGridView.FirstDisplayedScrollingRowIndex = index;
            }
        }
    }
}