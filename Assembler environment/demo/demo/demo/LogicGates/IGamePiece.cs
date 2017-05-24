using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/* Gates being used
 * AND
 * OR
 * XOR
 * INV
 * MUX 1bit
 * MUX 2bit
 * ALU
 */

namespace LogicGates
{
    /// <summary>
    /// Interface to be used for objects that can be grabbed by the Hand.
    /// </summary>
    public interface IGamePiece
    {
        //The grabable area of the object
        Rectangle Area { get; }

        //This update function must be called instead of all game update logic.
        void Update(Hand h);

        //Draw logic
        void Draw(SpriteBatch sb);
    }
}
