using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Game
{
    public class Agent : IAgent
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Direction { get; set; }

        public Agent(int x, int y, int dir)
        {
            X = x;
            Y = y;
            Direction = dir;
        }

        public void Build()
        {
            throw new NotImplementedException(); 
        }
        public void Interact(AgentEnvironment environment)
        {
            throw new NotImplementedException();
        }

        public int GetX()
        {
            throw new NotImplementedException();
        }

        public int GetY()
        {
            throw new NotImplementedException();
        }

        public void SetX(int x)
        {
            throw new NotImplementedException();
        }

        public void SetY(int y)
        {
            throw new NotImplementedException();
        }
    }
}
