using System;
using System.Collections.Generic;
using System.IO;

namespace CA_problem
{
    class Program
    {
        static List<Node> RandomUpdate(List<Node> nodes, Random random, int[] TimeRange, int ticks)
        {
            List<Node> NodeToRemove = new List<Node>();
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].NoTicksRemaining == 0)
                {
                    NodeToRemove.Add(nodes[i]);
                }
                else
                {
                    nodes[i].NoTicksRemaining--;
                }
            }
            if (NodeToRemove.Count == 1)
            {
                nodes.Remove(NodeToRemove[0]);
            }
            else
            {
                for (int i = 0; i < NodeToRemove.Count; i++)
                {
                    nodes[i].NoOfCollisions++;
                    int rangeTop = TimeRange[1] * (2 ^ nodes[i].NoOfCollisions);
                    NodeToRemove[i].NoTicksRemaining = random.Next(TimeRange[0], rangeTop);
                }
            }
            return nodes;
        }
        static List<Node> NewRandomList(int NoOfNodes, int[] TimeRandomRange, int[] DataRandomRange, Random random)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < NoOfNodes; i++)
            {
                nodes.Add(new Node(random.Next(TimeRandomRange[0], TimeRandomRange[1]), random.Next(DataRandomRange[0], DataRandomRange[1]), false));
            }
            return nodes;
        }
        static List<int> PrimesList(int startNumber, int NumberNeeded)
        {
            List<int> primes = new List<int>();
            int PrimesFound = 0;
            for (int i = startNumber; PrimesFound < NumberNeeded; i++)
            {
                bool Prime = true;
                for (int j = 2; j < i/2; j++)
                {
                    if (i % j == 0)
                    {
                        Prime = false;
                    }
                }
                if (Prime)
                {
                    primes.Add(i);
                    PrimesFound++;
                }
            }
            return primes;
        }
        static List<Node> NewList(int NoOfNodes, List<int> primes, int[] DataRandomRange, Random random)
        {
            List<Node> nodes = new List<Node>();
            for (int i = 0; i < NoOfNodes; i++)
            {
                nodes.Add(new Node(primes[i], random.Next(DataRandomRange[0], DataRandomRange[1]), true));
            }
            return nodes;
        }
        static List<Node> Update(List<Node> nodes)
        {
            List<Node> NodeToRemove = new List<Node>();
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].NoTicksRemaining == 0)
                {
                    NodeToRemove.Add(nodes[i]);
                }
                else
                {
                    nodes[i].NoTicksRemaining--;
                }
            }
            if (NodeToRemove.Count == 1)
            {
                nodes.Remove(NodeToRemove[0]);
            }
            else
            {
                for (int i = 0; i < NodeToRemove.Count; i++)
                {
                    NodeToRemove[i].NoTicksRemaining = NodeToRemove[i].AssignedPrime;
                }
            }
            return nodes;
        }



        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            Random random = new Random();
            int[] NoOfNodesRange = { 0, 100 };
            int TotalTicks = 0;
            int Ticks = 0;
            int Counter = 0;
            for (int i = NoOfNodesRange[0]; i < NoOfNodesRange[1]; i++)
            {
                int NoOfNodes = i;
                List<Node> nodes = new List<Node>();
                int[] TimeRandomRange = { 0, 3 };
                int[] DataRandomRange = { 1, 4 };
                int StartingPrime = 2;
                List<int> primes = PrimesList(StartingPrime, NoOfNodes);

                for (int x = 1; x < 6; x++)
                {
                    for (int y = 0; y < 1000; y++)
                    {
                        Ticks = 0;
                        nodes = NewRandomList(NoOfNodes, TimeRandomRange, DataRandomRange, random);
                        while (nodes.Count > 0)
                        {
                            nodes = RandomUpdate(nodes, random, TimeRandomRange, Ticks);
                            Ticks++;
                        }
                        TotalTicks += Ticks;
                    }
                }
                int RandomTicks = (TotalTicks / 5000);

                TotalTicks = 0;
                Ticks = 0;
                nodes = NewList(NoOfNodes, primes, DataRandomRange, random);
                while (nodes.Count > 0)
                {
                    nodes = Update(nodes);
                    Ticks++;
                }
                int PrimeTicks = Ticks;

                if (RandomTicks > PrimeTicks)
                {
                    Console.WriteLine("Primes wins by " + (RandomTicks - PrimeTicks) + " when there are " + NoOfNodes + " nodes");
                }
                if (PrimeTicks > RandomTicks)
                {
                    Console.WriteLine("Random wins by " + (PrimeTicks - RandomTicks) + " when there are " + NoOfNodes + " nodes");
                    Counter++;
                }
                if (PrimeTicks == RandomTicks)
                {
                    Console.WriteLine("Draw at " + NoOfNodes + " nodes");
                }

                lines.Add(NoOfNodes + "," + PrimeTicks + "," + RandomTicks);

                TotalTicks = 0;
                Ticks = 0;
            }


            File.WriteAllLines(@"F:\CA\CSMA.csv", lines);
            Console.WriteLine("Done " + Counter);
            Console.ReadKey();
        }
    }
}
