namespace Crackdownlike;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chickensoft.LogicBlocks;
using Chickensoft.LogicBlocks.Generator;

public partial class PlayerLogic
    {
        public interface IState : IStateLogic { }

        public abstract partial record State : StateLogic, IState { }
    }

