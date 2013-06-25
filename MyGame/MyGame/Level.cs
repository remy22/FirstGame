using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MyGame
{
	public class Level
	{
		private Entity[,] level;
		private int width,height,hOffset,vOffset;

		public Level(int width,int height,int hOffset,int vOffset)
		{
			this.width=width;
			this.height=height;
			this.hOffset=hOffset;
			this.vOffset=vOffset;

			level=new Entity[(width/hOffset)+hOffset,(height/vOffset)+vOffset];
		}

		public bool add(Entity obj,int x,int y)
		{
			return(true);
		}

		public bool add(Entity obj)
		{
			int temp;
			
			if ((temp=(((int)obj.Pos.X)%hOffset))!=0)
			{
				if (temp<(hOffset/2))
					obj.Pos-=new Vector2(temp,0);
				else
					obj.Pos+=new Vector2(hOffset-temp,0);
			}

			if ((temp=(((int)obj.Pos.Y)%vOffset))!=0)
			{
				if (temp<(vOffset/2))
					obj.Pos-=new Vector2(0,temp);
				else
					obj.Pos+=new Vector2(0,vOffset-temp);
			}

			if (level[(int)(obj.Pos.X/hOffset),(int)(obj.Pos.Y/vOffset)]==null)
			{
				level[(int)(obj.Pos.X/hOffset),(int)(obj.Pos.Y/vOffset)]=obj;
				return(true);
			}
			return(false);
		}

		public bool add(Entity[] obj)
		{
			int temp;
			for(int i=0;i<obj.Length;i++)
			{
				if ((temp=(((int)obj[i].Pos.X)%hOffset))!=0)
				{
					if (temp<(hOffset/2))
						obj[i].Pos-=new Vector2(temp,0);
					else
						obj[i].Pos+=new Vector2(hOffset-temp,0);
				}

				if ((temp=(((int)obj[i].Pos.Y)%vOffset))!=0)
				{
					if (temp<(vOffset/2))
						obj[i].Pos-=new Vector2(0,temp);
					else
						obj[i].Pos+=new Vector2(0,vOffset-temp);
				}

				level[((int)obj[i].Pos.X)/32,((int)obj[i].Pos.Y)/32]=obj[i];
			}
			return(true);
		}

		public Entity[] getArea(int x1,int y1,int x2,int y2)
		{
			Entity[] group;
			int count=0;

			if (x2>width)
				x2=(width/hOffset)+hOffset;
			else
				x2/=hOffset;

			if (y2>height)
				y2=(height/vOffset)+vOffset;
			else
				y2/=vOffset;


			if (x1<0 || y1<0)
			{
				x1=0;
				y1=0;
			}
			else
			{
				x1/=hOffset;
				y1/=vOffset;
			}

			group=new Entity[(x2-x1)*(y2-y1)];
			for(int i=x1;i<x2;i++)
			{
				for(int j=y1;j<y2;j++)
				{
					if (level[i,j]!=null)
					{
						group[count++]=level[i,j];
					}
				}
			}
			return(group);
		}

		public void draw(SpriteBatch sBatch)
		{
			for(int i=0;i<(width/hOffset)+hOffset;i++)
			{
				for(int j=0;j<(height/vOffset)+vOffset;j++)
				{
					if (level[i,j]!=null)
					{
						level[i,j].draw(sBatch);
					}
				}
			}
		}

		public void loadContent(ContentManager cManager)
		{
			for (int i = 0; i < (width / hOffset) + hOffset; i++)
			{
				for (int j = 0; j < (height / vOffset) + vOffset; j++)
				{
					if (level[i, j] != null)
					{
						level[i, j].loadContent(cManager);
					}
				}
			}
		}
	}
}
