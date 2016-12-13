using PersistentDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistentDataStructures
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            List<PersistentQueue<int>> q = new List<PersistentQueue<int>>();
            q.Add(new PersistentQueue<int>());

            for (int i = 0; i < n; ++i)
            {
                string[] inp = Console.ReadLine().Split(' ');

                if (inp[0] == "1")
                {
                    q.Add(q[int.Parse(inp[1])].Push(int.Parse(inp[2])));
                }
                else
                {
                    Console.WriteLine(q[int.Parse(inp[1])].Peek());
                    q.Add(q[int.Parse(inp[1])].Pop());
                }
            }
        }
    }
}
