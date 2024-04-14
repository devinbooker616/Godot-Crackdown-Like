using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

namespace Crackdownlike.Items.Interactables
{
    public partial class Interactable : Area3D
    {
        [Export] 
        string prompt = "interact";

        public void StartInteraction() 
        {
            
        }
    }
}