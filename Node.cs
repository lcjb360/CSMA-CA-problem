using System;
using System.Collections.Generic;
using System.Text;

namespace CA_problem
{
    class Node
    {
        public int NoTicksRemaining;
        public int TimeToSend;
        public int AssignedPrime;
        public int NoOfCollisions;
        public Node(int StartTime, int DataSize, bool prime)
        {
            NoTicksRemaining = StartTime;
            TimeToSend = DataSize;
            if (prime)
            {
                AssignedPrime = StartTime;
            }
        }
    }
}
