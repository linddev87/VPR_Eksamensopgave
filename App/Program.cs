

using App.UI;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterface.Message("Welcome to the application. Type 'help' for options.\n");
            UserInterface.Init();    
        }
    }
}