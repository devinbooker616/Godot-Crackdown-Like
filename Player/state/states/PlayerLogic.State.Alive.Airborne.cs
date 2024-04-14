namespace Crackdownlike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public partial class PlayerLogic
{
    public partial record State
    {
        public record Airborne : Alive,
          IGet<Input.HitFloor>, IGet<Input.StartedFalling>
        {
            public IState On(Input.HitFloor input)
            {
                if (input.IsMovingHorizontally)
                {
                    return new Moving();
                }

                return new Idle();
            }

            public IState On(Input.StartedFalling input) => new Falling();
        }
    }
}
