using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace LaRottaO.CSharp.WinFormsCrossThreads
{
    public static class Clipboards
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

        public static String getClipboardTextFromAnotherThread()
        {
            String clipboardText = "";

            Thread thread = new Thread((ThreadStart)delegate
            {
                clipboardText = Clipboard.GetText();
            });

            try
            {
                thread.TrySetApartmentState(ApartmentState.STA);
                thread.Start();

                while (!thread.IsAlive) { Thread.Sleep(1); }

                Thread.Sleep(1);
                thread.Join();

                return clipboardText;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to get text on clipboard: " + ex.ToString());
                return "";
            }
        }

        public static void ClearClipboardFromAnotherThread()
        {
            Thread thread = new Thread((ThreadStart)delegate
            {
                Clipboard.Clear();
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
                Console.WriteLine("Unable to clear clipboard: " + ex.ToString());
            }
        }
    }
}