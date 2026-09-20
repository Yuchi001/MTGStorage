using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MTGStorage.CustomUI
{
    public class NoAutoSelectComboBox : ComboBox
    {
        private NativeWindow _listWindow;

        private const int CB_GETCOMBOBOXINFO = 0x0164;
        private const int LB_FINDSTRING = 0x018F;
        private const int LB_FINDSTRINGEXACT = 0x01A2;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            SetupListWindow();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_listWindow != null)
            {
                _listWindow.ReleaseHandle();
                _listWindow = null;
            }

            base.OnHandleDestroyed(e);
        }

        private void SetupListWindow()
        {
            var info = new COMBOBOXINFO
            {
                cbSize = Marshal.SizeOf(typeof(COMBOBOXINFO))
            };

            if (!GetComboBoxInfo(Handle, ref info))
                return;

            if (info.hwndList != IntPtr.Zero)
            {
                _listWindow = new ComboBoxListWindow(this, info.hwndList);
            }
        }

        private class ComboBoxListWindow : NativeWindow
        {
            private readonly NoAutoSelectComboBox _comboBox;

            public ComboBoxListWindow(
                NoAutoSelectComboBox comboBox,
                IntPtr handle)
            {
                _comboBox = comboBox;
                AssignHandle(handle);
            }

            protected override void WndProc(ref Message m)
            {
                // ComboBox używa tego komunikatu do
                // automatycznego wyszukiwania elementu.
                //
                // Zamieniamy zwykłe "znajdź element zaczynający się od..."
                // na "znajdź dokładnie taki element".
                if (m.Msg == LB_FINDSTRING)
                {
                    m.Msg = LB_FINDSTRINGEXACT;
                }

                base.WndProc(ref m);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMBOBOXINFO
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public int buttonState;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }

        [DllImport(
            "user32.dll",
            EntryPoint = "GetComboBoxInfo",
            CharSet = CharSet.Unicode)]
        private static extern bool GetComboBoxInfo(
            IntPtr hWnd,
            ref COMBOBOXINFO info);
    }
}