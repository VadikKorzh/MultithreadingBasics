using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingBasics
{
    class Program
    {
        static ConcurrentQueue<string> receivers = new ConcurrentQueue<string>();

        static void Main(string[] args)
        {
            string receiver;

            Thread sender = new Thread(SendMail);
            sender.Start();

            do
            {
                Console.Write("Enter a receiver: ");
                receiver = Console.ReadLine();

                if (receiver == "exit")
                {
                    sender.Abort();
                }
                else if(receiver == "resume")
                {
                    if (sender.ThreadState == ThreadState.Suspended)
                    {
                        sender.Resume();
                        continue;
                    }
                }
                else if(receiver == "pause")
                {
                    if (sender.ThreadState == ThreadState.Running)
                    {
                        sender.Suspend();
                        continue;
                    }
                }
                receivers.Enqueue(receiver);
            } while (true);

        }

        static void SendMail()
        {
            string receiver;
            do
            {
                Thread.Sleep(5000);
                if (receivers.Count != 0)
                {
                    Console.WriteLine("\n\nSENDING MAIL");
                    Console.WriteLine();
                }
                while (receivers.Count != 0)
                {
                    if (receivers.TryDequeue(out receiver))
                    {
                        Console.WriteLine($"Mail has been sent to {receiver}");
                    }
                }
            } while (true);
        }
    }
}
