using System;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class CheckBoxes
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

        public static Boolean GetCheckedThreadSafe(this CheckBox checkBox)
        {
            Boolean isChecked = false;

            if (Thread.CurrentThread.IsBackground)
            {
                checkBox.Invoke(new Action(() =>
                {
                    isChecked = checkBox.Checked;
                }));
            }
            else
            {
                isChecked = checkBox.Checked;
            }

            return isChecked;
        }

        public static void SetCheckedThreadSafe(this CheckBox checkBox, Boolean value)
        {
            if (Thread.CurrentThread.IsBackground)
            {
                checkBox.Invoke(new Action(() =>
                {
                    checkBox.Checked = value;
                }));
            }
            else
            {
                checkBox.Checked = value;
            }
        }
    }
}