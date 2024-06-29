namespace KbdLayoutInstaller
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            
            if (args.Length >= 3)
            {
                ApplicationConfiguration.Initialize();
                var f = new FocusForm(args[0], args[1], args[2]);
                Application.Run(f);
                return f.resCode;

                //return KbdLayoutInstaller.InstallDll(args[0], args[1], args[2]);
            }
            return 1;
        }
    }
}