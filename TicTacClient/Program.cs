namespace TicTacClient
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
            var form=new Form1();
            form.Disposed += Form_Disposed;
            Application.Run(form);
            
        }

        private static void Form_Disposed(object? sender, EventArgs e)
        {
            MessageBox.Show("form1 is disposed");
        }
    }
}