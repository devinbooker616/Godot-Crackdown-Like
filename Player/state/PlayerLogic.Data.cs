namespace Crackdownlike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;


    public partial class PlayerLogic
    {
        /// <summary>Data shared between states.</summary>
        public record Data
        {
            public Vector3 LastStrongDirection { get; set; } = Vector3.Forward;
            public Vector3 LastVelocity { get; set; } = Vector3.Zero;
            public bool WasOnFloor { get; set; } = true;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool HadNegativeYVelocity() => LastVelocity.Y < 0f;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool WasMovingHorizontally(Settings settings) =>
              (LastVelocity with { Y = 0f }).Length() >= settings.StoppingSpeed;
        }
    }
