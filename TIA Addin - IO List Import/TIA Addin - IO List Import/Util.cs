﻿using System.Windows.Forms;

namespace TIA_Addin___IO_List_Import
{
    internal static class Util
    {
        internal static Form GetForegroundWindow()
        {
            // Workaround for Add-In Windows to be shown in foreground of TIA Portal
            Form form = new Form { Opacity = 0, ShowIcon = false };
            form.Show();
            form.TopMost = true;
            form.Activate();
            form.TopMost = false;
            return form;
        }
    }
}
