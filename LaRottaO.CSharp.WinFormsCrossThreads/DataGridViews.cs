using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class DataGridViews
    {
        public static void SetBindingSourceThreadSafe(this DataGridView dataGridView, BindingSource bindingSource)
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

        public static int GetSelectedIndexThreadSafe(this DataGridView dataGridView)
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

        public static Tuple<Boolean, dynamic> getValueByColHeaderThreadSafe(this DataGridView dataGridView, string HeaderText)
        {
            try
            {
                int selectedRow = GetSelectedIndexThreadSafe(dataGridView);
                DataGridViewCellCollection dataGridViewCellCollection;
                dynamic value = "";

                if (Thread.CurrentThread.IsBackground)
                {
                    dataGridView.Invoke(new Action(() =>
                    {
                        dataGridViewCellCollection = dataGridView.Rows[selectedRow].Cells;
                        value = dataGridViewCellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == HeaderText).Value;
                    }));
                }
                else
                {
                    dataGridViewCellCollection = dataGridView.Rows[selectedRow].Cells;
                    value = dataGridViewCellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == HeaderText).Value;
                }

                return new Tuple<Boolean, dynamic>(true, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get DataGridView value at column with header: " + HeaderText + ". " + ex);
                return new Tuple<Boolean, dynamic>(false, "");
            }
        }

        public static Boolean SetSelectedIndexThreadSafe(this DataGridView dataGridView, int index)
        {
            try
            {
                if (Thread.CurrentThread.IsBackground)
                {
                    dataGridView.Invoke(new Action(() =>
                    {
                        dataGridView.ClearSelection();
                        dataGridView.Rows[index].Selected = true;
                    }));
                }
                else
                {
                    dataGridView.ClearSelection();
                    dataGridView.Rows[index].Selected = true;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to select DataGridView index: " + ex);
                return false;
            }
        }

        public static int GetRowCountThreadSafe(this DataGridView dataGridView)
        {
            try
            {
                int rowCount = 0;

                if (Thread.CurrentThread.IsBackground)
                {
                    dataGridView.Invoke(new Action(() =>
                    {
                        rowCount = dataGridView.RowCount;
                    }));
                }
                else
                {
                    rowCount = dataGridView.RowCount;
                }

                return rowCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get DataGridView row count: " + ex);
                return 0;
            }
        }

        public static void ClearSelectionThreadSafe(this DataGridView dataGridView)
        {
            try
            {
                if (Thread.CurrentThread.IsBackground)
                {
                    dataGridView.Invoke(new Action(() =>
                    {
                        dataGridView.ClearSelection();
                    }));
                }
                else
                {
                    dataGridView.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to clear DataGridView selection: " + ex);
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