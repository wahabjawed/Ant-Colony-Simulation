using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AntColony
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static int width = 600;
        public static int height = 400;
        Texture2D whiteRectangle;
        bool oneTime=false;
        public List<trackpoint> trackpoints= new List<trackpoint>();
        Texture2D home;


        bool isSimulation = false;
        Texture2D startScreen;

        public static Random rand = new Random();
        public Color ANT_COLOR = new Color(68, 0, 8);
        public int DIRT_R = 217;
        public int DIRT_G = 165;
        public int DIRT_B = 78;
        public Color DIRT_COLOR;
        public Color FOOD_COLOR = new Color(158, 55, 17);
        public int HOME_R = 96;
        public int HOME_G = 85;
        public int HOME_B = 33;
        Color PHER_HOME_COLOR;
        public int FOOD_R = 255;
        public int FOOD_G = 255;
        public int FOOD_B = 255;
        Color PHER_FOOD_COLOR;

        Color FillColor;

        Color TrackColor;

        public Colony col;
        public Food food;
        public Map pherHome;
        public Map pherFood;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
                    }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
           PHER_HOME_COLOR = new Color(HOME_R, HOME_G, HOME_B,100);
            DIRT_COLOR = new Color(DIRT_R, DIRT_G, DIRT_B);
            PHER_FOOD_COLOR = new Color(FOOD_R, FOOD_G, FOOD_B);
            height=graphics.GraphicsDevice.Viewport.Height;

            width = graphics.GraphicsDevice.Viewport.Width;
            pherHome = new Map(width, height);
            pherFood = new Map(width, height);
            col = new Colony(100, 100, 100, pherHome, pherFood);
            food = new Food(width, height);

            // Sprinkle some crumbs around
            for (int i = 0; i < 10; i++)
            {
               food.addFood(400 + rand.Next(-50, 50), 300 + rand.Next(-50, 50));
               // food.addFood(250, 250);
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            whiteRectangle = new Texture2D(GraphicsDevice, 1, 1);
            whiteRectangle.SetData(new[] { Color.White });
            home = Content.Load<Texture2D>(@"home");
            startScreen= Content.Load<Texture2D>(@"start");
        }

           

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (isSimulation)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    food.addFood(Mouse.GetState().X, Mouse.GetState().Y);
                }

                foreach (trackpoint p in trackpoints)
                {

                    p.alpha -= 0.0009f;
                }

                for (int i = trackpoints.Count - 1; i >= 0; i--)
                {
                    if (trackpoints[i].alpha < -0.1f)
                    {
                        trackpoints.RemoveAt(i);
                    }
                }

                // TODO: Add your update logic here
                pherHome.step();
                pherFood.step();

            }
            else {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    isSimulation = true;
                }

            }



           

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           // if (!oneTime)
           // {
                GraphicsDevice.Clear(DIRT_COLOR);
               // oneTime = true;
            //}
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
            if (!isSimulation)
            {
                spriteBatch.Draw(startScreen, new Rectangle(0, 0, startScreen.Width, startScreen.Height), Color.White);
            }
            else
            {

                spriteBatch.Draw(home, new Rectangle(50, 50, home.Width, home.Height), Color.SandyBrown);

                for (int i = 0; i < 800; i++)
                {
                    for (int j = 0; j < 600; j++)
                    {
                        // Color pixelColor;
                        if (food.dmapVals[i, j] == true)
                        {
                            //  Console.WriteLine(i + " " + j);
                            //    // Draw food
                            //    // pixelColor = FOOD_COLOR;
                            spriteBatch.Draw(whiteRectangle, new Vector2((float)i, (float)j), null,
                                   FOOD_COLOR, 0f, Vector2.Zero, new Vector2(.56f, .56f),
                                    SpriteEffects.None, 0f);
                        }
                    }
                }

                for (int i = 0; i < col.ants.Length; i++)
                {
                    // Console.WriteLine(col.ants.Length);
                    Ant thisAnt = col.ants[i];
                    TrackColor = DIRT_COLOR;
                    oneTime = false;
                    thisAnt.step();

                    int thisXi = thisAnt.intX;
                    int thisYi = thisAnt.intY;
                    float thisXf = (float)thisAnt.x;
                    float thisYf = (float)thisAnt.y;

                    FillColor = (ANT_COLOR);

                    if (thisAnt.hasFood)
                    {
                        oneTime = true;
                        TrackColor = PHER_FOOD_COLOR;
                        FillColor = (FOOD_COLOR);
                        if (thisXi > col.x - 10 && thisXi < col.x + 10 && thisYi > col.y - 10 && thisYi < col.y + 10)
                        {
                            // Close enough to home
                            thisAnt.hasFood = false;
                            thisAnt.homePher = 100;
                            TrackColor = PHER_FOOD_COLOR;
                        }

                    }
                    else if (food.getValue(thisXi, thisYi))
                    {
                        oneTime = true;
                        thisAnt.hasFood = true;
                        thisAnt.foodPher = 100;
                        food.bite(thisXi, thisYi);
                        TrackColor = PHER_FOOD_COLOR;
                    }

                    if (Math.Abs(thisAnt.dx) > Math.Abs(thisAnt.dy))
                    {
                        // Horizontal ant
                        spriteBatch.Draw(whiteRectangle, new Vector2(thisXf, thisYf), null,
                            FillColor, 0f, Vector2.Zero, new Vector2(3f, 2f),
                            SpriteEffects.None, 0f);
                        if (oneTime)
                        {
                            trackpoints.Add(new trackpoint(new Vector2(thisXf, thisYf), TrackColor));
                        }
                    }
                    else
                    {
                        // Vertical ant
                        spriteBatch.Draw(whiteRectangle, new Vector2(thisXf, thisYf), null,
                          FillColor, 0f, Vector2.Zero, new Vector2(2f, 3f),
                           SpriteEffects.None, 0f);
                        if (oneTime)
                        {
                            trackpoints.Add(new trackpoint(new Vector2(thisXf, thisYf), TrackColor));
                        }
                    }
                }

                foreach (trackpoint p in trackpoints)
                {

                    spriteBatch.Draw(whiteRectangle, new Vector2(p.getPoint().X, p.getPoint().Y), null,
                          p.getColor() * p.alpha, 0f, Vector2.Zero, new Vector2(0.8f, 0.8f),
                           SpriteEffects.None, 0f);
                }
            }
             spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
