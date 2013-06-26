using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MyGame
{
	public class LevelBuilder
	{
		Entity[] level;
		Texture2D cursor;
		MouseState mouseState;
		Vector2 cursorPos;
		bool pressed;
		int current;
		Game1 currentGame;

		public LevelBuilder(Game1 currentGame)
		{
			level=new Entity[64];
			cursorPos=new Vector2((mouseState.X/32)*32,(mouseState.Y/32)*32);
			pressed=false;
			this.currentGame=currentGame;
		}

		public void update()
		{
			mouseState=Mouse.GetState();
			cursorPos=new Vector2((mouseState.X/32)*32,(mouseState.Y/32)*32);
			if (!pressed)
			{
				if (mouseState.LeftButton==ButtonState.Pressed)
				{
					pressed=true;
					placeObj();
				}
			}

			if (mouseState.LeftButton==ButtonState.Released)
				pressed=false;
		}

		public void placeObj()
		{
			if (!checkPos(cursorPos))
			{
				if (current>=level.Length)
				{
					Entity[] temp=new Entity[level.Length+32];
					level.CopyTo(temp,0);
					level=temp;
				}

				level[current++]=new Wall("wall",cursorPos,32,32,0,currentGame);
			}
		}

		public bool checkPos(Vector2 pos)
		{
			for(int i=0;i<level.Length;i++)
			{
				if (level[i]!=null)
				{
					if (level[i].Pos==pos)
					{
						return(true);
					}
				}
			}
			return(false);
		}

		public void loadContent(ContentManager cManager)
		{
			cursor = cManager.Load<Texture2D>("cursor");
			for(int i=0;i<level.Length;i++)
			{
				if (level[i]!=null)
				{
					level[i].loadContent(cManager);
				}
				else
				return;
			}
		}

		public void draw(SpriteBatch sBatch)
		{
			if (sBatch==null)
			{
				Console.WriteLine("sBatch is null");
			}
			else
			Console.WriteLine("sBatch is NOT null");

			sBatch.Draw(cursor, cursorPos, Color.White);
			if (sBatch==null)
			{
				Console.WriteLine("sBatch is null after first draw");
			}
			else
			Console.WriteLine("sBatch is STILL NOT null");
			/*(new Wall("wall",new Vector2(200,300),32,32,0,currentGame)).draw(sBatch);
			for(int i=0;i<level.Length;i++)
			{
				if (level[i]!=null)
				{
					level[i].draw(sBatch);
				}
				else
				return;
			}*/
		}
	}
}
