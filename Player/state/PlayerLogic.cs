namespace Crackdownlike;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chickensoft.LogicBlocks;
using Chickensoft.LogicBlocks.Generator;



    public interface IPlayerLogic : ILogicBlock<PlayerLogic.IState> { }
    [StateMachine]
    public partial class PlayerLogic : LogicBlock<PlayerLogic.IState>, IPlayerLogic
    {
        public override IState GetInitialState() => new State.Disabled();

        public PlayerLogic(
          IPlayer player, Settings settings, IAppRepo appRepo, IGameRepo gameRepo
        )
        {
            Set(player);
            Set(settings);
            Set(appRepo);
            Set(gameRepo);
            Set(new Data());
        }
    }
