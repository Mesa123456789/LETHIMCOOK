using LETHIMCOOK.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using MonoGame.Extended.Collisions;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using MonoGame.Extended.ViewportAdapters;
using System;


namespace LETHIMCOOK
{
    public class Craft
    {
        Game1 game;
        Food food;
        string key;
        public static Dictionary<string, Food> Recipe = new Dictionary<string, Food>();
        public static Texture2D Uni;
        public static List<Food> CraftList = new List<Food>();
        public static List<Food> ingredentList = new List<Food>();
        string[] craftBox = new string[4];

        public Craft()
        {
            getUni = false;
        }
        public static void Load(ContentManager content, string asset)
        {
            Uni = content.Load<Texture2D>("Uni");
            //Recipe.Add("chicken" + "crab",new Food("Uni",Uni,Uni,Vector2.Zero));
        }

        public static Vector2 mousepos;
        public static Vector2 posMouse;
        public static RectangleF mouseRec;
        bool getUni = false;

        public void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();
            mousepos = Mouse.GetState().Position.ToVector2();
            posMouse = new Vector2(mousepos.X + (game._cameraPosition.X), mousepos.Y + (game._cameraPosition.Y));
            mouseRec = new Rectangle((int)posMouse.X, (int)posMouse.Y, 24, 24);
            for (int i = Game1.BagList.Count - 1; i >= 0; i--)
            {
                if (mouseRec.Intersects(Game1.BagList[i].foodBox))
                {
                    CraftList.Add(Game1.BagList[i]);
                    Game1.BagList.RemoveAt(i);
                }
                if (mouseRec.Intersects(CraftList[i].foodBox))
                {
                    Game1.BagList.Add(Game1.BagList[i]);
                    CraftList.RemoveAt(i);
                }
            }
            for (int i = 0; i < ingredentList.Count; i++)
            {
                for (int j = 0; j < CraftList.Count; j++)
                {
                    if (CraftList[j].name == ingredentList[i].name)
                    {
                        ingredentList[i].istrue = true;
                        Console.WriteLine( CraftList[j].name + ingredentList[i].name);
                    }
                    else
                    {
                        ingredentList[i].istrue = false;
                    }
                }
            }
            if (ingredentList[0].istrue == true && ingredentList[1].istrue == true)
            {
                getUni = true;
            }
            //if (CraftList[0] != null)
            //{
            //    for(int i = 0; i < uni.Length; i++)
            //    {
            //        if ("chicken" == uni[i])
            //    }

            //    if (CraftList[0].name == "chicken")
            //    {
            //        menu1[0] = "chicken";
            //    }
            //    else if (CraftList[0].name == "crab")
            //    {
            //        crabseafood++;
            //    }
            //    else if (CraftList[0].name == "uni")
            //    {
            //        getUni++;
            //    }
            //}
            //if (CraftList[2] != null)
            //{
            //    if (CraftList[2].name == "chicken")
            //    {
            //        //chickenrice++;
            //    }
            //    else if (CraftList[2].name == "crab")
            //    {
            //        crabseafood++;
            //    }
            //    else if (CraftList[2].name == "uni")
            //    {
            //        getUni++;
            //    }
            //}
            //if(szdgfd)
            //{
            //    if (menu1[0] == "chicken" && menu1[1] = "crab"
            //}
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (getUni == true)
            {
                spriteBatch.Draw(Uni, Vector2.Zero, Color.White);
            }
        }
    }
}