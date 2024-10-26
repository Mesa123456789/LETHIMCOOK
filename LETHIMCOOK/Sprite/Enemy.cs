using LETHIMCOOK.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LETHIMCOOK.Sprite

{
    public class Enemy : IEntity, ICollisionActor
    {
        Game1 game;
        public string name;
        public int frame = 0;
        public int framePerSec = 7;
        public float totalElapsed;
        public float timePerFream = (float)1 / 7;
        bool isHit;
        Texture2D texture;
        Food[] droppedFood;
        public Vector2 enemyPosition;
        private double hitCooldown = 2.0; // Cooldown period in seconds
        private double lastHitTime = 0;
        int countDamage;
        int enemyHp = 3;
        public IShapeF Bounds { get; }
        
        public Enemy(string name, Texture2D enemytex, Food[] droppedFood)
        {
            this.name = name;
            texture = enemytex;
            this.droppedFood = droppedFood;
            
        }
        public Enemy(string name,Texture2D enemytex, Food[] droppedFood, Vector2 enemyPosition)
        {
            this.name = name;
            texture = enemytex;
            this.droppedFood = droppedFood;
            this.enemyPosition = enemyPosition;
            Bounds = new RectangleF(new Vector2(enemyPosition.X, enemyPosition.Y), new Vector2(32, 32));
        }

        RectangleF mouseRec;
        Vector2 mousepos;
        Vector2 posMouse;
        RectangleF mouseCheck; 
        public void Update(GameTime gameTime)
        {
            ///แยกเมธอดแต่ละscreen
            MouseState mouseSt = Mouse.GetState();
            if (Bounds.Intersects(GameplayScreen.player.Bounds) && !isHit)
            {
                Game1.currentHeart -= 10;
                isHit = true;
                if (mouseSt.LeftButton == ButtonState.Pressed && Bounds.Intersects(GameplayScreen.player.Bounds))
                {
                    // isCheck = true;
                    OnCollision();
                }
            }

  

            if (isHit == true)
            {
                countDamage += 1;
                {
                    if (countDamage > 100)
                    {
                        countDamage = 0;
                        isHit = false;
                    }
                }
            }
            
            UpdateFream((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        bool isCheck;
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, enemyPosition, new Rectangle(32 * frame, 0, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);
        }

        public void OnCollision()
        {
            for (int i = 0; i < droppedFood.Length; i++)
            {
                Game1.BagList.Add(droppedFood[i]);
            }
            Game1.IsPopUp = true;
            foreach (Enemy enemy in Game1.enemyList)
            {
                Game1.enemyList.Remove(this);
                break;
            }

        }
        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            if (collisionInfo.Other.ToString().Contains("PlatformEntity"))
            {
                Bounds.Position -= collisionInfo.PenetrationVector;
            }

        }
        void UpdateFream(float elapsed)
        {
            totalElapsed += elapsed;
            if (totalElapsed > timePerFream)
            {
                frame = (frame + 1) % 5;
                totalElapsed -= timePerFream;
            }
        }
        int counttime;
        public void CountTime(int timePopup)
        {
            counttime += 1;
            {
                if (counttime > timePopup)
                {
                    isCheck = false;
                }
            }
        }

    }
}
