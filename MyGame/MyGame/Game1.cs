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

namespace MyGame
{
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		//Data members available to all entities given a currentGame object.
		public Level level;

		//Private data members
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		//Player mySprite;
		Player player;
		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			player=new Player("faceSheet",new Vector2(200,100),32,32,0,this);
			level=new Level(this.GraphicsDevice.Viewport.Width,this.GraphicsDevice.Viewport.Height,32,32);
			level.add(new Wall("wall",new Vector2(300,200),32,32,0,this));

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			level.loadContent(this.Content);
			player.loadContent(this.Content);
		}

		protected override void UnloadContent()
		{

		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();
			
			//mySprite.update(gameTime);
			player.update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			//mySprite.draw(spriteBatch);
			player.draw(spriteBatch);
			level.draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
