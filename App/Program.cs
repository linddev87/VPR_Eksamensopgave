

using App.UI;

namespace App
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            UserInterface.Message("Welcome to the application. Type 'help' for options.\n");
            UserInterface.Init();
        }
    }
}