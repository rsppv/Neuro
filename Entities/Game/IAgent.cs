using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Game
{
    public interface IAgent
    {
        void Interact(AgentEnvironment environment);
        int GetX();
        int GetY();
        void SetX(int x);
        void SetY(int y);
    }
}
