using KasirKafe;
using KasirKafe_Kel6;
using System;
using System.Threading; // Wajib ditambahkan untuk ThreadExceptionEventHandler
using System.Windows.Forms;

namespace KasirKafe_Kel6
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();


            Application.ThreadException += new ThreadExceptionEventHandler(GlobalGuiExceptionHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            //Application.Run(new FormKasir());
            Application.Run(new FormUtama()); // test FormUtama
        }

        // Fungsi Peredam Crash Global UI
        private static void GlobalGuiExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            // Menampilkan pesan error yang ramah kepada user tanpa mematikan paksa aplikasi dashboard
            MessageBox.Show($"Terjadi kesalahan sistem pada operasi modul: {e.Exception.Message}\n\nSistem berhasil meredam error. Aplikasi tetap berjalan aman.",
                            "Sistem Keamanan Teraktivasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}