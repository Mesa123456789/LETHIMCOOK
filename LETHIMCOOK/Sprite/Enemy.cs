﻿using LETHIMCOOK.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LETHIMCOOK.Sprite

{
    public class Enemy : Food
    {
        Game1 game;
        public int frame;
        public int framePerSec;
        public float totalElapsed;
        public float timePerFream;
        bool isHit;
        Texture2D texture;
        Texture2D enemyTexbag;
        public Vector2 enemyPosition;
        private double hitCooldown = 2.0; // Cooldown period in seconds
        private double lastHitTime = 0;
        int countDamage;
        int enemyHp = 3;

        public Enemy(string name,Texture2D enemytex, Texture2D enemyTexbag, Vector2 enemyPosition) : base(name,enemytex, enemyTexbag, enemyPosition)
        {
            texture = enemytex;
            this.enemyTexbag = enemyTexbag;
            this.enemyPosition = enemyPosition;
            framePerSec = 7;
            timePerFream = (float)1 / framePerSec;
            frame = 0;
            
        }
        RectangleF mouseRec;
        Vector2 mousepos;
        Vector2 posMouse;
        RectangleF mouseCheck; 
        public override void Update(GameTime gameTime)
        {
            ///แยกเมธอดแต่ละscreen
            MouseState mouseSt = Mouse.GetState();
            if (foodBox.Intersects(GameplayScreen.player.Bounds) && !isHit)
            {
                Game1.currentHeart -= 10;
                isHit = true;
                if (mouseSt.LeftButton == ButtonState.Pressed && foodBox.Intersects(GameplayScreen.player.Bounds))
                {
                    // isCheck = true;
                    OnCollision();
                }
            }
            if (foodBox.Intersects(SeaScreen.player.Bounds) && !isHit)
            {
                Game1.currentHeart -= 10;
                isHit = true;
                if (mouseSt.LeftButton == ButtonState.Pressed && foodBox.Intersects(SeaScreen.player.Bounds))
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
            foodBox = new RectangleF((int)enemyPosition.X, (int)enemyPosition.Y, 50, 50);
            UpdateFream((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        bool isCheck;
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture,enemyPosition, new Rectangle(32 * frame, 0, 32, 32), Color.White, 0.0f, new Vector2(16, 16), 2.0f, SpriteEffects.None, 0.0f);
        }
        public override void DrawBag(SpriteBatch batch)
        {
            batch.Draw(enemyTexbag, enemyPosition, new Rectangle(0, 0, 32, 32), Color.White);
        }

        public override void OnCollision()
        {
            OntableAble = true;
            Game1.BagList.Add(this);
            Game1.IsPopUp = true;
            foreach (Enemy enemy in Game1.enemyList)
            {
                Game1.enemyList.Remove(this);
                break;
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
