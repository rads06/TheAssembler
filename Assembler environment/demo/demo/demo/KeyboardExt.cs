using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HarvestLegend
{
    //Keyboard emulator
    public class KeyboardExt
    {
        static KeyboardState state;
        public static KeyboardState State { get { return state; } }
        static KeyboardState oldState;
        public static KeyboardState OldState { get { return oldState; } }

        //Recursion handling
        static ulong recurseDelay;
        public static ulong RecurseDelay 
        { 
            get 
            {
                return recurseDelay;
            }

            set
            {
                double d = Math.Pow(2D, Convert.ToDouble(value));
                try
                {
                    recurseDelay = Convert.ToUInt64(d);
                }
                catch (OverflowException e)
                {
                    recurseDelay = UInt64.MaxValue;
                }
            }
        }

        static Dictionary<Keys, ulong> recurseLockouts;

        //Call this before using the emulator
        public static void Initialize(ushort delay)
        {
            RecurseDelay = delay;

            //Initialize recurse lockouts
            recurseLockouts = new Dictionary<Keys, ulong>();
            foreach (Keys k in Enum.GetValues(typeof(Keys)))
            {
                recurseLockouts[k] = 0;
            }
        }

        public static void Update()
        {
            oldState = state;
            state = Keyboard.GetState();

            //Update all recurse delays
            foreach (Keys k in Enum.GetValues(typeof(Keys)))
            {
                recurseLockouts[k] = recurseLockouts[k] >> 1;
            }
        }

        public static bool IsKeyJustPressed(Keys k)
        {
            if (state.IsKeyDown(k) && oldState.IsKeyUp(k))
            {
                recurseLockouts[k] = RecurseDelay;
                return true;
            }
            return false;
        }

        public static bool IsKeyJustReleased(Keys k)
        {
            return (state.IsKeyUp(k) && oldState.IsKeyDown(k));
        }

        public static bool IsKeyDown(Keys k)
        {
            if (state.IsKeyDown(k) && recurseLockouts[k] == 0)
            {
                recurseLockouts[k] = RecurseDelay;
                return true;
            }
            return false;
        }

        public static bool IsKeyUp(Keys k)
        {
            return state.IsKeyUp(k);
        }

        public static Keys[] GetPressedKeys()
        {
            Keys[] myKeys = State.GetPressedKeys();
            List<Keys> validKeys = new List<Keys>();
            foreach (Keys k in myKeys)
            {
                if (recurseLockouts[k] == 0 || IsKeyJustPressed(k))
                {
                    validKeys.Add(k);
                    recurseLockouts[k] = RecurseDelay;
                }
            }
            return validKeys.ToArray();
        }

        public static char[] GetPressedAlphas()
        {
            Keys[] myKeys = GetPressedKeys();
            List<char> myChars = new List<char>();

            foreach (Keys k in myKeys)
            {
                int i = (int)k;
                if (i >= 65 && i <= 90)
                    myChars.Add((char)i);
                if (i >= 48 && i <= 57)
                    myChars.Add((char)i);
            }

            return myChars.ToArray();
        }
    }
}
