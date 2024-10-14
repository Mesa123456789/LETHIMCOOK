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



namespace LETHIMCOOK.Screen
{
    public class RestauarntScreen : screen
    {
        ///***new
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
        public readonly CollisionComponent _collisionComponent;
        Game1 game;
        Vector2 playerPos;// = new Vector2((int)player.Bounds.Position.X,(int) player.Bounds.Position.Y);

        RectangleF Bounds = new RectangleF(new Vector2(180,330), new Vector2(40, 60));


        public RestauarntScreen(Game1 game, EventHandler theScreenEvent) : base(theScreenEvent)
        {
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
            ////****
            ayinomoto = game.Content.Load<Texture2D>("ingre/ayinomoto");
            chili = game.Content.Load<Texture2D>("ingre/chili");
            oil = game.Content.Load<Texture2D>("ingre/oil");
            milk = game.Content.Load<Texture2D>("ingre/milk");
            salt2 = game.Content.Load<Texture2D>("ingre/salt2");
            sauce2 = game.Content.Load<Texture2D>("ingre/sauce2");
            rice = game.Content.Load<Texture2D>("ingre/rice");
            sugar = game.Content.Load<Texture2D>("ingre/sugar");
            ///***
            uni = game.Content.Load<Texture2D>("Uni");
            crabmeat = game.Content.Load<Texture2D>("ingre/crabmeat");
            hippomeat = game.Content.Load<Texture2D>("ingre/hippomeat");
            chickenmeat = game.Content.Load<Texture2D>("ingre/chickenmeat");
            cheese = game.Content.Load<Texture2D>("ingre/cheese");
            rainbowsmilemeat = game.Content.Load<Texture2D>("ingre/rainbowsmilemeat");
            pinksmilemeat = game.Content.Load<Texture2D>("ingre/pinksmilemeat");
            wipcream = game.Content.Load<Texture2D>("ingre/wipcream");
            //Load the background texture for the screen
            salmonmeat = game.Content.Load<Texture2D>("ingre/salmonmeat");
            redfishmeat = game.Content.Load<Texture2D>("ingre/redfishmeat");
            whalemeat = game.Content.Load<Texture2D>("ingre/whalemeat");
            greenshimpmeat = game.Content.Load<Texture2D>("ingre/greenshimpmeat");
            pinkfishmeat = game.Content.Load<Texture2D>("ingre/pinkfishmeat");
            sharkmeat = game.Content.Load<Texture2D>("ingre/sharkmeat");
            shimpmeat = game.Content.Load<Texture2D>("ingre/shimpmeat");
            unimeat = game.Content.Load<Texture2D>("ingre/unimeat");
            ///***new
            coriander = game.Content.Load<Texture2D>("ingre/coriander");
            grass = game.Content.Load<Texture2D>("ingre/grass");
            greendimon = game.Content.Load<Texture2D>("ingre/greendimon");
            hippowing = game.Content.Load<Texture2D>("ingre/hippowing");
            lemon = game.Content.Load<Texture2D>("ingre/lemon");
            meatball = game.Content.Load<Texture2D>("ingre/meatball");
            Mendrek = game.Content.Load<Texture2D>("ingre/Mendrek");
            noodle = game.Content.Load<Texture2D>("ingre/noodle");
            pinkdimon = game.Content.Load<Texture2D>("ingre/pinkdimon");
            seafood = game.Content.Load<Texture2D>("ingre/seafood");
            shumai = game.Content.Load<Texture2D>("ingre/shumai");
            smileeggs = game.Content.Load<Texture2D>("ingre/smileeggs");
            stone = game.Content.Load<Texture2D>("ingre/stone");
            suki = game.Content.Load<Texture2D>("ingre/suki");
            tempura = game.Content.Load<Texture2D>("ingre/tempura");
            icecream = game.Content.Load<Texture2D>("ingre/icecream");
            shimai = game.Content.Load<Texture2D>("_fish/shimai");
            ///*****
            Game1.ingredentList.Add(new Food(0, "ayinomoto", ayinomoto, false));
            Game1.ingredentList.Add(new Food(1, "chili", chili, false));
            Game1.ingredentList.Add(new Food(2, "oil", oil, false));
            Game1.ingredentList.Add(new Food(3, "milk", milk, false));
            Game1.ingredentList.Add(new Food(4, "salt2", salt2, false));
            Game1.ingredentList.Add(new Food(5, "sauce2", sauce2, false));
            Game1.ingredentList.Add(new Food(6, "rice", rice, false));
            Game1.ingredentList.Add(new Food(7, "sugar", sugar, false));
            //***
            Game1.ingredentList.Add(new Enemy(8, "crab", foodTexture, false));
            Game1.ingredentList.Add(new Enemy(9, "pinksmaile", pinkslime, false));
            Game1.ingredentList.Add(new Enemy(10, "hippo", hippo, false));
            Game1.ingredentList.Add(new Enemy(11, "chicken", chicken, false));
            Game1.ingredentList.Add(new Enemy(12, "rat", rat, false));
            Game1.ingredentList.Add(new Enemy(13, "slime", slime, false));
            Game1.ingredentList.Add(new Enemy(14,"icebear", icebear, false));
            //***
            Game1.ingredentList.Add(new Fish(15, "redfish", redfishmeat, false));
            Game1.ingredentList.Add(new Fish(16, "salmon", salmonmeat, false));
            Game1.ingredentList.Add(new Fish(17, "whalemeat", whalemeat, false));
            Game1.ingredentList.Add(new Fish(18, "greenshimpmeat", greenshimpmeat, false));
            Game1.ingredentList.Add(new Fish(19, "pinkfishmeat", pinkfishmeat, false));
            Game1.ingredentList.Add(new Fish(20, "sharkmeat", sharkmeat, false));
            Game1.ingredentList.Add(new Fish(21, "shimpmeat", shimpmeat, false));
            Game1.ingredentList.Add(new Fish(22, "unimeat", unimeat, false));
            ///***new
            Game1.ingredentList.Add(new Enemy(23, "jellyfish", jeelyfishmeat,false));
            Game1.ingredentList.Add(new Food(24,"coriander", coriander, false));
            Game1.ingredentList.Add(new Food(25,"grass", grass, false));
            Game1.ingredentList.Add(new Food(26,"greendimon", greendimon, false));
            Game1.ingredentList.Add(new Food(27,"lemon", lemon, false));
            Game1.ingredentList.Add(new Food(28,"Mendrek", Mendrek, false));
            Game1.ingredentList.Add(new Food(29,"noodle", noodle, false));
            Game1.ingredentList.Add(new Food(30,"pinkdimon", pinkdimon, false));
            Game1.ingredentList.Add(new Food(31,"shumai", shumai, false));
            Game1.ingredentList.Add(new Food(32,"stone", stone, false));
            Game1.ingredentList.Add(new Food(33,"tempura", tempura, false));
            Game1.ingredentList.Add(new Food(34,"suki", suki, false));
            Game1.ingredentList.Add(new Food(35,"seafood", seafood, false));
            Game1.ingredentList.Add(new Food(36, "icecream", icecream, false));
            Game1.ingredentList.Add(new Food(37, "shimai", shimai, false));
            Game1.ingredentList.Add(new Food(38, "octopus", octopus, false));

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
       public static RectangleF doorRec = new RectangleF(120,40, 200, 20);
        bool IssendMenuInterect = false;
       // bool openFridgeUI = false;
        bool IsInterect = false;
        // public static bool Ontable = false;
        // bool GotMenu = false;    
        // bool Crafting = false;
        MouseState msPre, ms;
        public override void Update(GameTime theTime)
        {
            
            if (player.Bounds.Intersects(doorRec) && !GameplayScreen.EnterDoor)
            {
                ScreenEvent.Invoke(game.GameplayScreen, new EventArgs());
                GameplayScreen.player.Bounds.Position = new Vector2(600, 300);
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
                    Game1.CraftList.Add(Game1.BagList[i]);
                    Game1.BagList.RemoveAt(i);
                    for (int j = 0; j < Game1.BagList.Count; j++)
                    {
                        Game1.BagList[j].foodPosition = Game1.inventBox[j];
                    }
                    break;
                }
                //inventoryBox = new Rectangle((int)Game1.BagList[i].foodPosition.X, (int)Game1.BagList[i].foodPosition.Y, 32, 32);
            }
            msPre = ms;


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

                            }
                            if (Game1.ingredentList[14].istrue == true && Game1.ingredentList[36].istrue == true)
                            {

                            }
                            if (Game1.ingredentList[5].istrue == true && Game1.ingredentList[33].istrue == true)
                            {

                            }
                            if (Game1.ingredentList[18].istrue == true && Game1.ingredentList[0].istrue == true)
                            {

                            }
                            if (Game1.ingredentList[37].istrue == true && Game1.ingredentList[1].istrue == true)
                            {

                            }
                            if (Game1.ingredentList[5].istrue == true && Game1.ingredentList[10].istrue == true)
                            {

                            }
                            if (Game1.ingredentList[5].istrue == true && Game1.ingredentList[11].istrue == true)
                            {

                            }
                        }
                        if (Game1.CraftList.Count < 4)
                        {
                            if (Game1.ingredentList[0].istrue == true && Game1.ingredentList[6].istrue == true && Game1.ingredentList[22].istrue == true)
                            {
                                Console.WriteLine("getfood");
                                Game1.getUni = true;
                                Game1.GotMenu = true;
                                Game1.finsihcraft = true;
                            }
                            if (Game1.ingredentList[15].istrue == true && Game1.ingredentList[1].istrue == true && Game1.ingredentList[5].istrue == true)
                            {

                            }
                            if (Game1.ingredentList[20].istrue == true && Game1.ingredentList[0].istrue == true && Game1.ingredentList[1].istrue == true)
                            {

                            }
                        }
                    }
                    
                }
            }


            foreach (IEntity entity in _entities)
            {
                entity.Update(theTime);
            }
            _collisionComponent.Update(theTime);
            _tiledMapRenderer.Update(theTime);
            player.Update(theTime);
            base.Update(theTime);
        }

        int MenuPopup;
        bool IsFrigeInterect = false;
        bool sendingMenu = false;
        ///ย้ายไปเกม1 เมาสืจะไม่เพี้ยนมั้ง
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
            if (Game1.getUni == true && Game1.sendingMenu == false)
            {
                _spriteBatch.Draw(uni, new Rectangle((int)player.Bounds.Position.X, (int)player.Bounds.Position.Y + 20, 32,32), Color.White);
            }
            game.DrawUIRest(_spriteBatch);
            ////menu
            ////if (sendingMenu == true && GotMenu == true && food.getFood == 2)
            ////{
            ////    _spriteBatch.Draw(uni, new Rectangle((int)player.CharPosition.X, (int)player.CharPosition.Y + 13, 32, 32), Color.White);
            ////}
            //if (openFridgeUI == true)
            //{
            //    _spriteBatch.Draw(FridgeUi, new Vector2(0, 0), Color.White);
            //}
            //if (Ontable == true)
            //{
            //    _spriteBatch.Draw(craft, new Vector2(215, 60), Color.White);
            //    _spriteBatch.Draw(inventory, new Vector2(129, 220), Color.White);
            //    if (!GotMenu)
            //    {
            //        for (int i = 0; i < Game1.CraftList.Count; i++)
            //        {
            //            _spriteBatch.Draw(Game1.CraftList[i].foodTexture, new Vector2(285 + i * 68, 98), new Rectangle(0, 0, 32, 32), Color.White);
            //        }
            //    }
            //    for (int i = 0; i < Game1.BagList.Count; i++)
            //    {
            //        _spriteBatch.Draw(Game1.BagList[i].foodTexture, new Vector2(160 + i * 52, 250),new Rectangle(0,0,32,32), Color.White);
            //    }
            //}

            //if (Crafting == true && Ontable)// && food.getFood == 2)
            //{
            //    _spriteBatch.Draw(QuestUI, new Vector2(720, 320) , Color.White);
            //    //_spriteBatch.Draw(menuBG, new Rectangle(650, 230, 300, 300), Color.White);
            //    ///_spriteBatch.Draw(uni, new Rectangle(733, 330, 128, 128), Color.White);
            //    GotMenu = true;
            //}
            ////_spriteBatch.Draw(QuestUI, new Vector2(720, 320), new Rectangle(725, 133, 146, 190), Color.White,rotationMenuBG, Vector2.Zero, 1f, 0, 1);
            ////if (MenuPopup == 1 && !FinsihCooking)
            ////{
            ////    if (Crafting == true && food.getFood == 2)
            ////    {
            ////        _spriteBatch.Draw(QuestUI, new Vector2(720, 320), new Rectangle(725, 133, 146, 190), Color.White,
            ////            rotationMenuBG, Vector2.Zero, 1f, 0, 1);
            ////        _spriteBatch.Draw(menuBG, new Rectangle(650, 230, 300, 300), Color.White);
            ////        _spriteBatch.Draw(uni, new Rectangle(733, 330, 128, 128), Color.White);
            ////        GotMenu = true;
            ////    }

            ////    CountTime(200);
            ////}

        }

        public int countPopUp;
        public void CountTime(int timePopup)
        {
            countPopUp += 1;
            {
                if (countPopUp > timePopup)
                {
                    countPopUp = 0;
                    //GA.IsPopUp = false;
                    ///Ontable = false;
                    MenuPopup = 0;
                }
            }
        }

    }
}

