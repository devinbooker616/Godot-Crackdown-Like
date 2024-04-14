using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    /// <summary>
    /// A killable entity. In this game, the only way to die is to fall to your
    /// death, so this will be called on entities that implement it.
    /// </summary>
    public interface IKillable
    {
        public void Kill();
    }
