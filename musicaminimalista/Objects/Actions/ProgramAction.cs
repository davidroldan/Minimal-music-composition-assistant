using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicaMinimalista.Objects.Actions
{
    public abstract class ProgramAction
    {
        public abstract void execute();
        public abstract void undo();
    }
}
