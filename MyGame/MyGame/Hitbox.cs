﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
	public class Hitbox
	{
		private Rectangle hitbox;
		public Vector2 Origin{get;private set;}
		private int width,height;
		private Entity follow;


		public Hitbox(Vector2 origin,int width,int height)
		{
			Origin=origin;
			this.width=width;
			this.height=height;
			hitbox=new Rectangle((int)Origin.X,(int)Origin.Y,width,height);

			follow=null;
		}

		public Hitbox(Vector2 origin,int width,int height,Entity attached)
		{
			Origin=origin;
			this.width=width;
			this.height=height;
			hitbox=new Rectangle((int)Origin.X,(int)Origin.Y,width,height);

			follow=attached;
		}

		public Rectangle getBox()
		{
			return(hitbox);
		}

		public Entity checkCol(Entity obj)
		{
			if (hitbox.Intersects(obj.Bbox.getBox()))
			{
				return(obj);
			}
			return(null);
		}

		public Entity checkCol(Vector2 loc,Entity obj)
		{
			if (obj==null)
				return null;

			int tempX=hitbox.X;
			int tempY=hitbox.Y;

			hitbox.X=(int)loc.X;
			hitbox.Y=(int)loc.Y;

			if (hitbox.Intersects(obj.Bbox.getBox()))
			{
				hitbox.X=tempX;
				hitbox.Y=tempY;
				return(obj);
			}
			hitbox.X=tempX;
			hitbox.Y=tempY;
			return(null);
		}

		public void update()
		{
			if (follow!=null)
			{
				Origin=follow.Pos;
			}
			hitbox.X=(int)Origin.X;
			hitbox.Y=(int)Origin.Y;
		}
	}
}