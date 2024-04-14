using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crackdownlike.Code.configs;
using Godot;

namespace Crackdownlike.Items.Interactables
{
    [GlobalClass]
    public partial class PickUppable : Interactable
    {
        [Export]
        ItemConfig.Keys itemKey = new ItemConfig.Keys();
        Node3D node = new Node3D();
        Node3D parent;
        public PickUppable() 
        {
            parent = node.GetParent<Node3D>();
        }

        public new void StartInteraction() 
        {
            parent.QueueFree();
        }
    }
}