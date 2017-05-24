using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using LMCTerminal;
using LogicGates;

namespace demo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;//I added this
        MusicPlayer songify;
        IntroScreen intro;
        ControlConsole main;
        IPage activePage;
        List<IPage> allPages;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth =  800;
            graphics.PreferredBackBufferHeight =  600;
            graphics.ApplyChanges();
            KeyboardExt.Initialize(16);
            MouseExt.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load 
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            songify = new MusicPlayer(Content);
            intro = new IntroScreen(Content, this);
            main = new ControlConsole(Content);
            activePage = intro;
            allPages = new List<IPage>();
            allPages.Add(intro);
            allPages.Add(main);
            
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("whatEverIWant");//I added this
            songify.Play(0);
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseExt.Update();
            KeyboardExt.Update();
            activePage.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            spriteBatch.Begin();//I added this
            activePage.Draw(spriteBatch);
            spriteBatch.End();//I added this

            base.Draw(gameTime);
        }

        public void ChangeActivePageTo(string name)
        {
            activePage = allPages.Find(p => p.Name.Equals(name));
            songify.Play(1);
        }
    }
}
