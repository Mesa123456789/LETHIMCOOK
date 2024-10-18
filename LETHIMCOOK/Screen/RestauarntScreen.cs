using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using MonoGame.Extended.Collisions;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using MonoGame.Extended.ViewportAdapters;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using LETHIMCOOK.Sprite;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;



namespace LETHIMCOOK.Screen
{
    public class RestauarntScreen : screen
    {
        ///***new <summary>
        Texture2D BBQ, oct, DragonFish, GreenShrimp, Dumpling, GrilledChicken, icecream_food, Mukrata, meatball_food, Pizza, Sasimi, Stone, Tempura, ThaiCrab;
        bool getBBQ = false, getDragonFish = false, getGreenShrimp = false, getDumpling = false, getGrilledChicken = false, geticecream_food = false, getMukrata = false
            , getmeatball_food = false, getPizza = false, getSasimi = false, getStone = false, getTempura = false, getThaiCrab = false;
        Texture2D coriander, grass, greendimon, hippowing, jeelyfishmeat, lemon, meatball;
        Texture2D Mendrek, noodle, pinkdimon, seafood, shumai, smileeggs;
        Texture2D stone, suki, tempura, purpledimon;
        Texture2D salmonmeat, redfishmeat, whalemeat, greenshimpmeat, pinkfishmeat, sharkmeat, shimpmeat, unimeat , octopus , shimai;
        Texture2D ayinomoto, chili, oil, milk, salt2, sauce2, rice, sugar, icecream;
        Texture2D foodTexture, crabmeat;
        Texture2D hippo, hippomeat;
        Texture2D chicken, chickenmeat;
        Texture2D rat, cheese;
        Texture2D slime, rainbowsmilemeat;
        Texture2D pinkslime, pinksmilemeat;
        Texture2D icebear, wipcream;
        public static bool IsCooking;
        Texture2D popup;
        Texture2D interact;
        Texture2D craft;
        Texture2D inventory;
        Texture2D FridgeUi;
        Texture2D QuestUI;
        Texture2D uni;
        AnimatedTexture SpriteTexture;
        public static Player player;
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;
        TiledMapObjectLayer _platformTiledObj;
        private readonly List<IEntity> _entities = new List<IEntity>();
        public static List<bool> QuestList = new List<bool>();
        public readonly CollisionComponent _collisionComponent;
        Game1 game;
        Vector2 playerPos;// = new Vector2((int)player.Bounds.Position.X,(int) player.Bounds.Position.Y);

        RectangleF Bounds = new RectangleF(new Vector2(161,350), new Vector2(40, 60));

        bool octfood = false, BBQfood = false, DragonFishfood = false, GreenShrimpfood = false, Dumplingfood = false,
            GrilledChickenfood = false, icecream_foodfood = false, Mukratafood = false,
                meatball_foodfood = false, Pizzafood = false, Sasimifood = false, Stonefood = false, Tempurafood = false, ThaiCrabfood = false;
        public RestauarntScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            //Game1.sendingMenu = true;
            IsCooking = true;
            game._bgPosition = new Vector2(400, 225);//******/
            SpriteTexture = new AnimatedTexture(new Vector2(16,16), 0, 2f, 1f);
            SpriteTexture.Load(game.Content, "Player-Sheet", 5, 4,10);
            player = new Player(SpriteTexture, playerPos, game, Bounds);
            popup = game.Content.Load<Texture2D>("popup");
            interact = game.Content.Load<Texture2D>("interact");
            craft = game.Content.Load<Texture2D>("craft");
            inventory = game.Content.Load<Texture2D>("inventory");
            FridgeUi = game.Content.Load<Texture2D>("FridgeUI");
            QuestUI = game.Content.Load<Texture2D>("QuestUI");
            player.Load(game.Content, "Sword");
            player.Load(game.Content, "Effect");
            ////****food
            uni = game.Content.Load<Texture2D>("Uni");
            oct = game.Content.Load<Texture2D>("food/oct");
            BBQ = game.Content.Load<Texture2D>("food/BBQ");
            DragonFish = game.Content.Load<Texture2D>("food/DragonFish");
            GreenShrimp = game.Content.Load<Texture2D>("food/GreenShrimp");
            Dumpling = game.Content.Load<Texture2D>("food/Dumpling_PNG");
            GrilledChicken = game.Content.Load<Texture2D>("food/GrilledChicken");
            icecream_food = game.Content.Load<Texture2D>("food/ice-cream");
            Mukrata = game.Content.Load<Texture2D>("food/Mukrata");
            meatball_food = game.Content.Load<Texture2D>("food/meatball");
            Pizza = game.Content.Load<Texture2D>("food/Pizza");
            Sasimi = game.Content.Load<Texture2D>("food/Sasimi");
            Stone = game.Content.Load<Texture2D>("food/Stone");
            Tempura = game.Content.Load<Texture2D>("food/Tempura_PNG");
            ThaiCrab = game.Content.Load<Texture2D>("food/ThaiCrab");
            QuestList.Add(octfood);
            QuestList.Add(BBQfood);
            QuestList.Add(DragonFishfood);
            QuestList.Add(GreenShrimpfood);
            QuestList.Add(Dumplingfood);
            QuestList.Add(GrilledChickenfood);
            QuestList.Add(icecream_foodfood);
            QuestList.Add(Mukratafood);
            QuestList.Add(meatball_foodfood);
            QuestList.Add(Pizzafood);
            QuestList.Add(Sasimifood);
            QuestList.Add(Stonefood);
            QuestList.Add(Tempurafood);
            QuestList.Add(ThaiCrabfood);
            ///*****
            Game1.ingredentList.Add(new Food(0, "ayinomoto", false));
            Game1.ingredentList.Add(new Food(1, "chili", false));
            Game1.ingredentList.Add(new Food(2, "oil",  false));
            Game1.ingredentList.Add(new Food(3, "milk" ,  false));
            Game1.ingredentList.Add(new Food(4, "salt2", false));
            Game1.ingredentList.Add(new Food(5, "sauce2", false));
            Game1.ingredentList.Add(new Food(6, "rice", false));
            Game1.ingredentList.Add(new Food(7, "sugar", false));
            //***
            Game1.ingredentList.Add(new Food(8, "crab", false));
            Game1.ingredentList.Add(new Food(9, "pinksmaile",false));
            Game1.ingredentList.Add(new Food(10, "hippo", false));
            Game1.ingredentList.Add(new Food(11, "chicken",  false));
            Game1.ingredentList.Add(new Food(12, "rat",false));
            Game1.ingredentList.Add(new Food(13, "slime",false));
            Game1.ingredentList.Add(new Food(14,"icebear", false));
            //***
            Game1.ingredentList.Add(new Food(15, "redfish",false));
            Game1.ingredentList.Add(new Food(16, "salmon", false));
            Game1.ingredentList.Add(new Food(17, "whalemeat", false));
            Game1.ingredentList.Add(new Food(18, "greenshimpmeat",false));
            Game1.ingredentList.Add(new Food(19, "pinkfishmeat", false));
            Game1.ingredentList.Add(new Food(20, "sharkmeat",false));
            Game1.ingredentList.Add(new Food(21, "shimpmeat", false));
            Game1.ingredentList.Add(new Food(22, "unimeat", false));
            ///***new
            Game1.ingredentList.Add(new Food(23, "jellyfish",false));
            Game1.ingredentList.Add(new Food(24,"coriander", false));
            Game1.ingredentList.Add(new Food(25,"grass", false));
            Game1.ingredentList.Add(new Food(26,"greendimon", false));
            Game1.ingredentList.Add(new Food(27,"lemon", false));
            Game1.ingredentList.Add(new Food(28,"Mendrek", false));
            Game1.ingredentList.Add(new Food(29,"noodle", false));
            Game1.ingredentList.Add(new Food(30,"pinkdimon", false));
            Game1.ingredentList.Add(new Food(31,"shumai", false));
            Game1.ingredentList.Add(new Food(32,"stone" ,false));
            Game1.ingredentList.Add(new Food(33,"tempura",  false));
            Game1.ingredentList.Add(new Food(34,"suki",  false));
            Game1.ingredentList.Add(new Food(35,"seafood",false));
            Game1.ingredentList.Add(new Food(36, "icecream", false));
            Game1.ingredentList.Add(new Food(37, "shimai", false));
            Game1.ingredentList.Add(new Food(38, "octopus",  false));
            Game1.ingredentList.Add(new Food(39, "pig", false));//39
            Game1.ingredentList.Add(new Food(40, "purpledimon", false));//39


            _collisionComponent = new CollisionComponent(new RectangleF(0, 0, 800, 450));
            _tiledMap = game.Content.Load<TiledMap>("Tile_Inrestaurant");

            _tiledMapRenderer = new TiledMapRenderer(game.GraphicsDevice, _tiledMap);
            //Get object layers
            foreach (TiledMapObjectLayer layer in _tiledMap.ObjectLayers)
            {
                if (layer.Name == "Tile_Wall_Inretaurant")
                {
                    _platformTiledObj = layer;
                }
            }
            foreach (TiledMapObject obj in _platformTiledObj.Objects)
            {
                Vector2 position = new Vector2(obj.Position.X, obj.Position.Y);
                _entities.Add(new PlatformEntity(game, new RectangleF(position, obj.Size)));
            }

            _entities.Add(player);

            foreach (IEntity entity in _entities)
            {
                _collisionComponent.Insert(entity);
            }
            this.game = game;
        }

      RectangleF mouseRec, craftBox , inventoryBox;
       public static RectangleF doorRec = new RectangleF(161,397, 200, 20);
        bool IssendMenuInterect = false;
        bool IsInterect = false;   
        bool finsihcraft = false;
        MouseState msPre, ms;
        static Random randomQuest = new Random();
        
        ///**bool food
        bool getPongneng =false;
        bool onRandom = true;
        public override void Update(GameTime theTime)
        {
            bool onQuest = randomQuest.Next(0, 2) == 0;
            if(onQuest && onRandom)
            {
                for(int i = 0; i < 1; i++)
                {
                    int getQuest = randomQuest.Next(0, QuestList.Count);
                    QuestList[getQuest] = true;
                    Console.WriteLine(QuestList[getQuest]);
                    Console.WriteLine(getQuest);
                    onRandom = false;
                } 
            }

            if (player.Bounds.Intersects(doorRec) && !GameplayScreen.EnterDoor)
            {
                IsCooking = false;
                ScreenEvent.Invoke(game.GameplayScreen, new EventArgs());
                GameplayScreen.player.Bounds.Position = new Vector2(770, 420);
                GameplayScreen.EnterDoor = true;
                return;
            }
            if (!player.Bounds.Intersects(doorRec))
            {
                GameplayScreen.EnterDoor = false;
            }
            game.UpdateUIRest(player,theTime);
            ms = Mouse.GetState();
  
            mouseRec = new RectangleF(ms.X, ms.Y,20,20);
            RectangleF FrigeRec = new RectangleF(348, 120, 40, 80);
            RectangleF tableBox = new RectangleF(450, 150, 130, 20);
            RectangleF sendMenu = new RectangleF(600,240,40,30);
            craftBox = new RectangleF(335, 140, 140, 50);
            if (player.Bounds.Intersects(FrigeRec))
            {
                IsFrigeInterect = true;
            }
            else { IsFrigeInterect = false;}
            if (player.Bounds.Intersects(tableBox))
            {
                IsInterect = true;
            }
            else
            {
                IsInterect = false;
            }
            if (player.Bounds.Intersects(sendMenu))
            {
                IssendMenuInterect = true;
            }
            else
            {
                IssendMenuInterect = false;
            }
            for (int i = 0; i < Game1.seasoningList.Count; i++)
            {
                if (mouseRec.Intersects(Game1.seasoningList[i].foodRec) && ms.LeftButton == ButtonState.Released && msPre.LeftButton == ButtonState.Pressed)
                {
                    if(Game1.openFridgeUI == true)
                    {
                        Game1.BagList.Add(Game1.seasoningList[i]);
                        Game1.IsPopUp = true;
                        break;
                    }   
                }
            }
 
            for (int i = Game1.BagList.Count - 1; i >= 0; i--)
            {
                foreach (Food food in Game1.BagList)
                {
                    Game1.BagList[i].foodPosition = Game1.inventBox[i];
                    break;
                }
                inventoryBox = new Rectangle((int)Game1.BagList[i].foodPosition.X, (int)Game1.BagList[i].foodPosition.Y, 32, 32);
                if (mouseRec.Intersects(inventoryBox) && ms.LeftButton == ButtonState.Released && msPre.LeftButton == ButtonState.Pressed && Game1.Ontable)
                {
                    Console.WriteLine("intersect!");
                    if(Game1.CraftList.Count < 4)
                    {
                        Game1.CraftList.Add(Game1.BagList[i]);
                        Game1.BagList.RemoveAt(i);
                    }
                    
                    for (int j = 0; j < Game1.BagList.Count; j++)
                    {
                        Game1.BagList[j].foodPosition = Game1.inventBox[j];
                    }
                    break;
                }
                //else if (mouseRec.Intersects(Game1.CraftList[i].foodBox) && ms.LeftButton == ButtonState.Released && msPre.LeftButton == ButtonState.Pressed && Game1.Ontable)
                //{
                //    Game1.BagList.Add(Game1.CraftList[i]);
                //    Game1.CraftList.RemoveAt(i);
                //    break;
                //}
                inventoryBox = new Rectangle((int)Game1.BagList[i].foodPosition.X, (int)Game1.BagList[i].foodPosition.Y, 32, 32);
            }
            if (msPre.LeftButton == ButtonState.Pressed && mouseRec.Intersects(craftBox))
            {
                craftBox.X += 10;
                for (int i = 0; i < Game1.ingredentList.Count; i++)
                {
                    Game1.ingredentList[i].istrue = false;
                    for (int j = 0; j < Game1.CraftList.Count; j++)
                    {

                        if (Game1.ingredentList[i].name == Game1.CraftList[j].name)
                        {
                            Game1.ingredentList[i].istrue = true;
                            Console.WriteLine("carft");
                        }
                        if (Game1.CraftList.Count < 3)
                        {
                            if (Game1.ingredentList[7].istrue == true && Game1.ingredentList[25].istrue == true)
                            {
                                Console.WriteLine("pongneng true");
                                getPongneng = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(3);
                            }
                            if (Game1.ingredentList[14].istrue == true && Game1.ingredentList[36].istrue == true)
                            {
                                Console.WriteLine("icecream true");
                                geticecream_food = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(3);
                            }
                            if (Game1.ingredentList[5].istrue == true && Game1.ingredentList[33].istrue == true)
                            {
                                Console.WriteLine("getTempura true");
                                getTempura = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(3);
                            }
                            if (Game1.ingredentList[18].istrue == true && Game1.ingredentList[0].istrue == true)
                            {
                                Console.WriteLine("getGreenShrimp true");
                                getGreenShrimp = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(3);
                            }
                            if (Game1.ingredentList[37].istrue == true && Game1.ingredentList[1].istrue == true)
                            {
                                Console.WriteLine("getDumpling true");
                                getDumpling = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(3);
                            }
                            if (Game1.ingredentList[5].istrue == true && Game1.ingredentList[10].istrue == true)
                            {
                                Console.WriteLine("getBBQ true");
                                getBBQ = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(3);
                            }
                            if (Game1.ingredentList[5].istrue == true && Game1.ingredentList[11].istrue == true)
                            {
                                Console.WriteLine("getGrilledChicken true");
                                getGrilledChicken = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(3);
                            }
                            //if (Game1.ingredentList[35].istrue == true && Game1.ingredentList[11].istrue == true)
                            //{
                            //    Console.WriteLine("getGrilledChicken true");
                            //    getGrilledChicken = true;
                            //    Game1.GotMenu = true;
                            //    finsihcraft = true;
                            //    RemoveCraft(3);
                            //}
                        }
                        if (Game1.CraftList.Count < 4)
                        {
                            if (Game1.ingredentList[0].istrue == true && Game1.ingredentList[6].istrue == true && Game1.ingredentList[22].istrue == true)
                            {
                                Console.WriteLine("getfood");
                                getUni = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(4);
                            }
                            if (Game1.ingredentList[15].istrue == true && Game1.ingredentList[5].istrue == true && Game1.ingredentList[1].istrue == true)
                            {
                                Console.WriteLine("getfood");
                                getDragonFish = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(4);
                            }
                            //ยำปู
                            //if (Game1.ingredentList[8].istrue == true && Game1.ingredentList[0].istrue == true && Game1.ingredentList[1].istrue == true)
                            //{
                            //    Console.WriteLine("getfood");
                            //    getUni = true;
                            //    Game1.GotMenu = true;
                            //    finsihcraft = true;
                            //    RemoveCraft(4);
                            //}
                            if (Game1.ingredentList[15].istrue == true && Game1.ingredentList[1].istrue == true && Game1.ingredentList[5].istrue == true)
                            {

                            }
                            if (Game1.ingredentList[20].istrue == true && Game1.ingredentList[0].istrue == true && Game1.ingredentList[1].istrue == true)
                            {

                            }
                        }
                        if (Game1.CraftList.Count < 5)
                        {
                            if (Game1.ingredentList[34].istrue == true && Game1.ingredentList[23].istrue == true && Game1.ingredentList[39].istrue == true && Game1.ingredentList[38].istrue == true)
                            {
                                Console.WriteLine("getfood");
                                getMukrata = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(5);
                            }
                            if (Game1.ingredentList[40].istrue == true && Game1.ingredentList[26].istrue == true && Game1.ingredentList[30].istrue == true && Game1.ingredentList[12].istrue == true)
                            {
                                Console.WriteLine("getfood");
                                getPizza = true;
                                Game1.GotMenu = true;
                                finsihcraft = true;
                                RemoveCraft(5);
                            }
                        }
                        }
                }
            }
            if (Game1.sendingMenu == true)
            {
                getUni = false;
                onRandom = true;
                Game1.MenuList.Add(new Food(uni, new Rectangle(0, 0, 128, 128)));
            }

            foreach (IEntity entity in _entities)
            {
                entity.Update(theTime);
            }
            _collisionComponent.Update(theTime);
            _tiledMapRenderer.Update(theTime);
            msPre = ms;
            player.Update(theTime);
            base.Update(theTime);
        }
        bool getUni = false;
        int MenuPopup;
        bool IsFrigeInterect = false;
        bool sendingMenu = false;
        public override void Draw(SpriteBatch _spriteBatch)
        {

            _tiledMapRenderer.Draw();//******//
            _spriteBatch.End();
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);//******//

            //foreach (IEntity entity in _entities)
            //{
            //    entity.Draw(_spriteBatch);
            //}
            //_spriteBatch.Draw(popup, new Rectangle((int)doorRec.X, (int)doorRec.Y, (int)doorRec.Width, (int)doorRec.Height), Color.White);
            if (IsInterect == true)
            {
                _spriteBatch.Draw(interact, new Rectangle(443, 148, 140, 44), Color.White);
            }
            if (IsFrigeInterect == true)
            {
                _spriteBatch.Draw(interact, new Rectangle(346, 116, 40, 80), Color.White);
            }
            if (IssendMenuInterect == true)
            {
                _spriteBatch.Draw(interact, new Rectangle(600, 240, 45, 33), Color.White);
            }
            

            player.Draw(_spriteBatch);
            if (getUni == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(uni, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32,32), Color.White);

            }
            if (getPongneng == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(oct, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);

            }
            if (geticecream_food == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(icecream_food, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);
            }
            if (getTempura == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(Tempura, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);
            }
            if (getDumpling == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(Dumpling, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);
            }
            if (getGreenShrimp == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(GreenShrimp, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);
            }
            if (getGrilledChicken == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(GrilledChicken, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);
            }
            if (getBBQ == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(BBQ, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);
            }
            if (getMukrata == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(Mukrata, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);
            }
            if (getPizza == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(Pizza, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);
            }
            if (getDragonFish == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(DragonFish, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32, 32), Color.White);
            }
            game.DrawUIRest(_spriteBatch);
            if (Game1.Ontable == true)
            {
                _spriteBatch.Draw(craft, new Vector2(215, 60), Color.White);
                _spriteBatch.Draw(inventory, new Vector2(129, 220), Color.White);
                // _spriteBatch.Draw(popup, craftBox, Color.White);
                for (int i = 0; i < Game1.CraftList.Count; i++)
                {
                    _spriteBatch.Draw(Game1.CraftList[i].foodTexBag, new Vector2(287 + i * 69, 95), new Rectangle(0, 0, 32, 32), Color.White);
                }
                for (int i = 0; i < Game1.BagList.Count; i++)
                {
                    _spriteBatch.Draw(Game1.BagList[i].foodTexBag, Game1.inventBox[i], new Rectangle(0, 0, 32, 32), Color.White);
                }
                if (getUni == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(uni, new Rectangle(343, 120, 128, 128), Color.White);
                    //_spriteBatch.Draw(QuestUI, new Vector2(720, 320), new Rectangle(725, 133, 146, 190), Color.White, rotationMenuBG , Vector2.Zero, 1f, 0, 1);
                }
                if (getPongneng == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(oct, new Rectangle(343 - 5, 120, 128, 128), Color.White);
                }
                if (geticecream_food == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(icecream_food, new Rectangle(343, 120, 128, 128), Color.White);
                }
                if (getTempura == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(Tempura, new Rectangle(343, 120, 128, 128), Color.White);
                }
                if (getDumpling == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(Dumpling, new Rectangle(343, 120, 128, 128), Color.White);
                }
                if (getGreenShrimp == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(GreenShrimp, new Rectangle(343, 120, 128, 128), Color.White);
                }
                if (getGrilledChicken == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(GrilledChicken, new Rectangle(343, 120, 128, 128), Color.White);
                }
                if (getBBQ == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(BBQ, new Rectangle(343, 120, 128, 128), Color.White);
                }
                if (getMukrata == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(Mukrata, new Rectangle(343, 120, 128, 128), Color.White);
                }
                if (getPizza == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(Pizza, new Rectangle(343, 120, 128, 128), Color.White);
                }
                if (getDragonFish == true && finsihcraft == true)
                {
                    _spriteBatch.Draw(QuestUI, new Vector2(333, 110), new Rectangle(725, 133, 200, 190), Color.White);
                    _spriteBatch.Draw(DragonFish, new Rectangle(343, 120, 128, 128), Color.White);
                }
                CountTime(200);
            }

        }
        int count;
        public void RemoveCraft(int amount)
        {
            count++;
            if (count > 100)
            {
                for (int i = 0; i < amount; i--)
                {
                    Game1.CraftList.RemoveAt(i);
                    break;
                }
            }

        }

        public int countPopUp;
        public void CountTime(int timePopup)
        {
            countPopUp += 1;
            {
                if (countPopUp > timePopup)
                {
                    countPopUp = 0;
                    MenuPopup = 0;
                    finsihcraft = false;
                    Game1.GotMenu = true;
                }
            }
        }

    }
}

