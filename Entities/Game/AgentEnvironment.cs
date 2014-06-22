using System;
using System.Collections.Generic;

namespace Entities.Game
{
    public class AgentEnvironment
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public IAgent Agent { get; private set; }
        public AgentEnvironmentObserver Listener { get; set; }

        public AgentEnvironment(int width, int height)
        {
            Height = height;
            Width = width;
            Agent = new Agent();
        }

        private void AvoidBounds(IAgent agent)
        {
            int newX = agent.GetX();
            int newY = agent.GetY();
            
            if (newX < 0) newX = 0;
            if (newY < 0) newY = 0;
            if (newX > Width) newX = Width - 1;
            if (newY > Height) newY = Height - 1;

            agent.SetX(newX);
            agent.SetY(newY);
        }

        public void NextStep()
        {
            Agent.Interact(this);
            AvoidBounds(Agent);
            Listener.Notify(this);
        }

        public IEnumerable<T> Filter<T>() where T : IAgent
        {
            throw new NotImplementedException();
        }
    }
}