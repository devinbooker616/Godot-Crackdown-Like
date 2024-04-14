namespace Crackdownlike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public partial class PlayerLogic
{
    public abstract partial record State
    {
        public record Dead : State;
    }
}
