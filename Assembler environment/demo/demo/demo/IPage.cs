using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace demo
{
    //Identifies a page that can be loaded to the foreground
    public interface IPage
    {
        //The name of the page
        string Name { get; }
        
        //The update for the page
        void Update(GameTime gt);

        //The draw method
        void Draw(SpriteBatch sb);
    }
}
