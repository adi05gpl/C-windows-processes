using System;
using System.Diagnostics;
using System.Threading;


namespace AutomationCLI
{
    class Program
    {
        public string text;
        public int frequency, lifetime;
        public Process p = new Process();

        public static void Main(string[] args)
        {
            Program obj = new Program();
            int i = 0;
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            Console.Clear();
            Console.CancelKeyPress += new ConsoleCancelEventHandler(myHandler);


            obj.text = readText(obj.text);
            obj.frequency = obj.readFrequency(obj.frequency);
            obj.lifetime = obj.readLifetime(obj.lifetime);

            obj.p.StartInfo.UseShellExecute = false;
            obj.p.StartInfo.FileName = obj.text;
            obj.p.StartInfo.Arguments = " " + obj.frequency + " " + obj.lifetime;
            obj.p.Start();

            Console.Write("In order to stop this program you need to press the 'q' key ! \n");
            do {
                cki = Console.ReadKey(true);

                if (cki.Key == ConsoleKey.Q)
                {
                    obj.p.Kill();
                    obj.p.WaitForExit();
                    obj.p.Close();
                    break;
                }
                else if (i < 6)
                {
                    Console.Write("The Process - {0} - lived {1} minutes. Press ENTER to continue \n", obj.text, i);
                    cki = Console.ReadKey(true);
                    Thread.Sleep(10000);
                    i++;
                    continue;
                }
                else
                {
                    Console.Write("The Process - {0} - outlived the treshhold. \n\nKilling the process...\n", obj.text);
                    obj.p.Kill();
                    Console.Write("The Process - {0} - has been killed. \n", obj.text);
                    obj.p.WaitForExit();
                    obj.p.Close();
                    i = 0;
                    Thread.Sleep(10000);
                    break;
                }
            //    cki = Console.ReadKey(true);
            }
            while (cki.Key != ConsoleKey.Q);

            if(obj.displayStatus(obj.p) == true)
                Console.Write("Test successfully completed. \n");
            else
                Console.Write("Test failed successfully: \n");


        }

        public static string readText(string str)
        {
            Console.Write("Enter the desired process name below: \n");
            str = Console.ReadLine();
 //           Console.WriteLine(str);
            return str;

        }
        
        public int readFrequency(int x)
        {
            Console.Write("Enter a maximum lifetime number of the above process: \n");
            x = Convert.ToInt32(Console.ReadLine());
            return x;
        }
        public int readLifetime(int y)
        {
            Console.Write("Enter the number of monitoring frequency: \n");
            y = Convert.ToInt32(Console.ReadLine());
            return y;
        }

        protected static void myHandler(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("\nThe read operation has been interrupted.");

            Console.WriteLine($"  Key pressed: {args.SpecialKey}");

            Console.WriteLine($"  Cancel property: {args.Cancel}");

            // Set the Cancel property to true to prevent the process from terminating.
            Console.WriteLine("Setting the Cancel property to true...");
            args.Cancel = true;

            // Announce the new value of the Cancel property.
            Console.WriteLine($"  Cancel property: {args.Cancel}");
            Console.WriteLine("The read operation will resume...\n");
        }

        public Boolean displayStatus(Process process)
        {
            process.Refresh();
                if (process.HasExited)
                {
                    Console.WriteLine("Processes has been closed");
                    return true;
                }
                else
                {
                    Console.WriteLine("Processes still running");
                    return false;
                }
        }   
    }
}
