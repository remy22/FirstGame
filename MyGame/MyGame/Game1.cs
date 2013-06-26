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

		Player player;
		LevelBuilder builder;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			IsFixedTimeStep=false;
		}

		protected override void Initialize()
		{
			player=new Player("faceSheet",new Vector2(200,100),32,32,0,this);
			level=new Level();
			level.add(new Wall("wall",new Vector2(200,200),32,32,0,this));
			level.add(new Wall("wall",new Vector2(300,100),32,32,0,this));
			level.add(new Wall("wall", new Vector2(232,200), 32, 32, 0, this));
			level.add(new Wall("wall", new Vector2(264, 200), 32, 32, 0, this));
			level.add(new Wall("wall",new Vector2(296,168),32,32,0,this));
			//builder=new LevelBuilder(this);
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);
			level.loadContent(this.Content);
			player.loadContent(this.Content);
			//builder.loadContent(this.Content);
		}

		protected override void UnloadContent()
		{

		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();
			
			player.update(gameTime);
			//builder.update();
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			player.draw(spriteBatch);
			level.draw(spriteBatch);
			//builder.draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}