using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LogicGates
{
    static class MouseExt
    {
        public static MouseState State { get; private set; }
        public static MouseState OldState { get; private set; }

        public static void Initialize()
        {
            State = Mouse.GetState();
        }

        public static void Update()
        {
            OldState = State;
            State = Mouse.GetState();
        }

        public static bool IsLeftButtonJustPressed()
        {
            return (State.LeftButton.Equals(ButtonState.Pressed) && OldState.LeftButton.Equals(ButtonState.Released));
        }

        public static bool IsLeftButtonJustReleased()
        {
            return (State.LeftButton.Equals(ButtonState.Released) && OldState.LeftButton.Equals(ButtonState.Pressed));
        }
    }
}
