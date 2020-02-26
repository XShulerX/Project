using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.VisualObjects
{
    class GameObjectException : Exception
    {
        public GameObjectException(string Message)
            :base(Message)
        {

        }
    }
}
