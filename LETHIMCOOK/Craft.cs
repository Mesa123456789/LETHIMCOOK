using LETHIMCOOK.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


namespace LETHIMCOOK
{
    public class Craft
    {
        Game1 game;
        Food food;
        public static Dictionary<string,Food> Recipe = new Dictionary<string,Food>();
        public static Texture2D Uni;
        public static void Load(ContentManager content, string asset)
        {
           Uni = content.Load<Texture2D>("Uni");
           Recipe.Add("chicken" + "crab",new Food("Uni",Uni,Vector2.Zero));
        }
        public void DrawMenu(SpriteBatch spriteBatch)
        {
            foreach (var item in Game1.BagList)
            {
                if (item == "chicken" + "crab")
                {
                    Recipe["chicken" + "crab"].Draw(spriteBatch);
                }
            }
        }
    }
}
