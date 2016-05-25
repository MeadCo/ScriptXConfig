using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeadCo.ScriptX
{
    public interface IBitsFinder
    {
        List<IBitsProvider> Find(InstallScope scope);
        List<IBitsProvider> Find(MachineProcessor processor);

        IBitsProvider FindSingle(InstallScope scope, MachineProcessor processor);
    }
}
