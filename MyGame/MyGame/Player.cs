using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
	class Player:Entity
	{
		private KeyboardState keyState;
		public enum State
		{
			Idle,
			Walking,
			Jumping
		}

		public State state;

		public Player(string asset,Vector2 pos,int frameWidth,int frameHeight,int offset,Game1 game):base(asset,pos,frameWidth,frameHeight,offset,game)
		{
			
		}

		public void update(GameTime gameTime)
		{
			base.update(gameTime);
			state=State.Idle;
			getInput();
			if (checkMove(new Vector2(0,5),getNearbyEnts())==null)
			{
				state=State.Jumping;
			}

			for(int i=(int)Math.Abs(Math.Ceiling(Hspeed));i>0;i--)
			{
				if (checkMove(new Vector2(i*Math.Sign(Hspeed),0),getNearbyEnts())==null)
				{
					Pos+=new Vector2(i*Math.Sign(Hspeed),0)*(float)gameTime.ElapsedGameTime.TotalSeconds;
					//Console.WriteLine(new Vector2(i*Math.Sign(Hspeed),0)*(float)gameTime.ElapsedGameTime.TotalSeconds);
				}
			}

			for(int i=(int)Math.Abs(Math.Ceiling(Vspeed));i>0;i--)
			{
				if (checkMove(new Vector2(0,i*Math.Sign(Vspeed)),getNearbyEnts())==null)
					Pos+=new Vector2(0,i*Math.Sign(Vspeed))*(float)gameTime.ElapsedGameTime.TotalSeconds;
			}
		}

		public void getInput()
		{
			keyState=Keyboard.GetState();

			if (keyState.IsKeyDown(Keys.W))
			{
				if (state!=State.Jumping)
				{
					Vspeed=-120;
					state=State.Jumping;
				}
			}

			if (keyState.IsKeyDown(Keys.A))
			{
				Hspeed=-20;
				state=State.Walking;
			}
			else
			if (keyState.IsKeyDown(Keys.D))
			{
				Hspeed=20;
				state=State.Walking;
			}
			else
			{
				Hspeed=0;
				state=State.Idle;
			}

			if (keyState.IsKeyDown(Keys.Space))
			{
				Console.WriteLine(Bbox.Origin);
				Console.WriteLine(Pos);
			}
		}

		public Entity checkMove(Vector2 offset,Entity[] obj)
		{	
			Entity temp;
			for(int i=0;i<obj.Length;i++)
			{
				if ((temp=base.checkMove(offset,obj[i]))!=null)
				{
					return(temp);
				}
			}
			return(null);
		}

		public Entity[] getNearbyEnts()
		{
			return(currentGame.level.getArea((int)Pos.X-(4*frameWidth),(int)Pos.Y-(4*frameHeight),frameWidth*9,frameHeight*9));
		}
	}
}
