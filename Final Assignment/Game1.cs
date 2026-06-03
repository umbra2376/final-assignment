using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pokemon;
using System;

namespace Final_Assignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState currentState, oldState;
        SpriteFont menuFont, healthFont;
        Rectangle window, menuLocation, moveInfoLocation, battleLocation, arrowSize, charHealthBar, enemyHealthBar, charHealthImg, enemyHealthImg, charIconSize, enemyIconSize, menuTextbox;
        Snorlax snorlax;
        Arcanine arcanine;
        Texture2D snorlaxTexture, AOtexture, AWtexture, menu, healthbar, healthIcon, battleImg, arrow, nameIcon, hyperBeam, hyperBeamImpact, defenseCurl, blastTexture, blastImpact;
        Vector2 moveType, moveName1, moveName2, moveName3, moveName4, typeText, PPText, movePP, charNameText, enemyNameText, totalHealthText, healthAmountText;
        int healthAmount, totalHealth, charSpeed, enemySpeed, enemyChoice, crit, Snoremove1Damage, Snoremove2Damage, Snoremove3Damage;
        float charHealth, enemyHealth;
        Random enemyMove = new Random();
        enum Pokemon
        {
            Snorlax, Ninetails, Arcanine, Flygon, Sceptile, Electivire
        }
        enum Enemy
        {
            Arcanine, Ninetails, Flygon, Sceptile, Electivire
        }
        enum Turn
        {
            charTurn, enemyTurn
        }
        private Pokemon currentPokemon;
        private Enemy currentEnemy;
        private Turn currentTurn;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            window = new Rectangle(0, 0, 1000, 800);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();
            menuLocation = new Rectangle(0, 550, 600, 250);
            moveInfoLocation = new Rectangle(600, 550, 400, 250);
            menuTextbox = new Rectangle(0, 550, 1000, 250);
            battleLocation = new Rectangle(0, 0, 1000, 550);
            typeText = new Vector2(660, 600);
            moveName1 = new Vector2(70, 600);
            moveName2 = new Vector2(70, 680);
            moveName3 = new Vector2(340, 600);
            moveName4 = new Vector2(340, 680);
            moveType = new Vector2(780, 600);
            PPText = new Vector2(660, 670);
            movePP = new Vector2(740, 670);
            charNameText = new Vector2(615, 370);
            totalHealthText = new Vector2(830, 460);
            healthAmountText = new Vector2(777, 460);
            arrowSize = new Rectangle(20, 600, 50, 60);
            charHealthImg = new Rectangle(570, 390, 370, 100);
            charIconSize = new Rectangle(530, 340, 460, 200);
            charHealthBar = new Rectangle(670, 428, 235, 30);
            enemyIconSize = new Rectangle(20, 20, 460, 150);
            enemyHealthImg = new Rectangle(70, 50, 370, 100);
            enemyHealthBar = new Rectangle(170, 88, 235, 30);
            enemyNameText = new Vector2(100, 40);
            arcanine = new Arcanine(AWtexture, AOtexture, blastTexture, blastImpact, new Rectangle(120, 283, 300, 300), new Rectangle(610, 90, 300, 300));
            snorlax = new Snorlax(snorlaxTexture, hyperBeam, hyperBeamImpact, defenseCurl, new Rectangle(120, 283, 400, 400));
            currentPokemon = Pokemon.Snorlax;
            currentEnemy = Enemy.Arcanine;
            if (currentPokemon == Pokemon.Snorlax)
            {
                healthAmount = snorlax.Health;
                totalHealth = snorlax.Health;
                charHealth = 235f / snorlax.Health;
                charSpeed = snorlax.Speed;
            }
            if (currentEnemy == Enemy.Arcanine)
            {
                enemyHealth = 235f / arcanine.Health;
                enemySpeed = arcanine.Speed;
                if (currentPokemon == Pokemon.Snorlax && currentEnemy == Enemy.Arcanine)
                {
                    Snoremove1Damage = (int)((((2 * 50 + 10) / 10) * ((float)snorlax.Attack / arcanine.Defense) * 70 + 2) / 50);
                    Snoremove2Damage = (int)((((2 * 50 + 10) / 10) * ((float)snorlax.Attack / arcanine.Defense) * 80 + 2) / 50);
                    Snoremove3Damage = (int)((((2 * 50 + 10) / 10) * ((float)snorlax.Attack / arcanine.Defense) * 150 + 2) / 50);
                }
            }
            if (charSpeed >= enemySpeed)
            {
                currentTurn = Turn.charTurn;
            }
            else
            {
                currentTurn = Turn.enemyTurn;
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            snorlaxTexture = Content.Load<Texture2D>("snorlax");
            AWtexture = Content.Load<Texture2D>("arcanine");
            AOtexture = Content.Load<Texture2D>("ownedArcanine");
            healthbar = Content.Load<Texture2D>("tile");
            healthIcon = Content.Load<Texture2D>("healthBar");
            menu = Content.Load<Texture2D>("starterMoveset");
            menuFont = Content.Load<SpriteFont>("pokeFont");
            healthFont = Content.Load<SpriteFont>("healthFont");
            battleImg = Content.Load<Texture2D>("grassBattlefield");
            arrow = Content.Load<Texture2D>("select");
            nameIcon = Content.Load<Texture2D>("nameIcon");
            hyperBeam = Content.Load<Texture2D>("hyperBeam");
            hyperBeamImpact = Content.Load<Texture2D>("hyperBeamImpact");
            defenseCurl = Content.Load<Texture2D>("defenseCurl");
            blastTexture = Content.Load<Texture2D>("blastBeam");
            blastImpact = Content.Load<Texture2D>("blastImpact");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            currentState = Keyboard.GetState();
            if (currentTurn == Turn.charTurn && currentPokemon == Pokemon.Snorlax && snorlax.CanAct)
            {
                if (arrowSize.X == 20 && arrowSize.Y == 600 && currentState.IsKeyDown(Keys.Down))
                    arrowSize.Y = 680;
                else if (arrowSize.X == 20 && arrowSize.Y == 600 && currentState.IsKeyDown(Keys.Right))
                    arrowSize.X = 290;
                else if (arrowSize.X == 20 && arrowSize.Y == 680 && currentState.IsKeyDown(Keys.Up))
                    arrowSize.Y = 600;
                else if (arrowSize.X == 20 && arrowSize.Y == 680 && currentState.IsKeyDown(Keys.Right))
                    arrowSize.X = 290;
                else if (arrowSize.X == 290 && arrowSize.Y == 600 && currentState.IsKeyDown(Keys.Down))
                    arrowSize.Y = 680;
                else if (arrowSize.X == 290 && arrowSize.Y == 600 && currentState.IsKeyDown(Keys.Left))
                    arrowSize.X = 20;
                else if (arrowSize.X == 290 && arrowSize.Y == 680 && currentState.IsKeyDown(Keys.Up))
                    arrowSize.Y = 600;
                else if (arrowSize.X == 290 && arrowSize.Y == 680 && currentState.IsKeyDown(Keys.Left))
                    arrowSize.X = 20;
                if (arrowSize.X == 20 && arrowSize.Y == 600 && snorlax.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        snorlax.Move1PP -= 1;
                        snorlax.CurrentMove = Snorlax.Move.headbutt;
                        if (currentEnemy == Enemy.Arcanine)
                            arcanine.HealthCurrent -= (int)(Snoremove1Damage);
                    }
                }
                else if (arrowSize.X == 20 && arrowSize.Y == 680 && snorlax.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        snorlax.Move2PP -= 1;
                        snorlax.CurrentMove = Snorlax.Move.bodyPress;
                        if (currentEnemy == Enemy.Arcanine)
                            arcanine.HealthCurrent -= (int)(Snoremove2Damage);
                    }
                }
                else if (arrowSize.X == 290 && arrowSize.Y == 600 && snorlax.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        snorlax.Move3PP -= 1;
                        snorlax.CurrentMove = Snorlax.Move.hyperBeam;
                        if (currentEnemy == Enemy.Arcanine)
                            arcanine.HealthCurrent -= (int)(Snoremove3Damage);
                    }
                }
                else if (arrowSize.X == 290 && arrowSize.Y == 680 && snorlax.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        snorlax.Move4PP -= 1;
                        snorlax.CurrentMove = Snorlax.Move.defenseCurl;
                    }
                }
        }
            /*if (currentTurn == Turn.enemyTurn && currentEnemy == Enemy.Arcanine && arcanine.CanAct)
            {
                enemyChoice = enemyMove.Next(1, 5);
                if (enemyChoice == 1)
                {
                    arcanine.CurrentMove = Arcanine.Move.fireblast;
                }
                else if (enemyChoice == 2)
                {
                    arcanine.CurrentMove = Arcanine.Move.crunch;
                }
                else if (enemyChoice == 3)
                {
                    arcanine.CurrentMove = Arcanine.Move.flamethrower;
                }
                else if (enemyChoice == 4)
                {
                    arcanine.CurrentMove = Arcanine.Move.howl;
                }
            }*/
            snorlax.Update(gameTime);
            if (snorlax.CanAct && currentEnemy == Enemy.Arcanine)
            {
                enemyHealthBar.Width = (int)(235 * (float)arcanine.HealthCurrent / arcanine.Health);
            }
            arcanine.Update(gameTime);
            oldState = currentState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(battleImg, battleLocation, Color.White);
            if (currentEnemy == Enemy.Arcanine)
            {
                arcanine.Draw(_spriteBatch);
            }
            if (currentPokemon == Pokemon.Snorlax)
            {
                snorlax.Draw(_spriteBatch);
                _spriteBatch.Draw(menu, menuLocation, Color.White);
                _spriteBatch.Draw(menu, moveInfoLocation, Color.White);
                _spriteBatch.Draw(arrow, arrowSize, Color.Black);
                _spriteBatch.DrawString(menuFont, snorlax.Move1, moveName1, Color.Black);
                _spriteBatch.DrawString(menuFont, snorlax.Move2, moveName2, Color.Black);
                _spriteBatch.DrawString(menuFont, snorlax.Move3, moveName3, Color.Black);
                _spriteBatch.DrawString(menuFont, snorlax.Move4, moveName4, Color.Black);
                _spriteBatch.DrawString(menuFont, "Type/", typeText, Color.Black);
                _spriteBatch.DrawString(menuFont, snorlax.MoveType, moveType, Color.Black);
                if (arrowSize.X == 20 && arrowSize.Y == 600)
                {
                    _spriteBatch.DrawString(menuFont, "PP         /15", PPText, Color.Black);
                    _spriteBatch.DrawString(menuFont, Convert.ToString(snorlax.Move1PP), movePP, Color.Black);
                }
                if (arrowSize.X == 20 && arrowSize.Y == 680)
                {
                    _spriteBatch.DrawString(menuFont, "PP         /10", PPText, Color.Black);
                    _spriteBatch.DrawString(menuFont, Convert.ToString(snorlax.Move2PP), movePP, Color.Black);
                }
                if (arrowSize.X == 290 && arrowSize.Y == 600)
                {
                    _spriteBatch.DrawString(menuFont, "PP         /5", PPText, Color.Black);
                    _spriteBatch.DrawString(menuFont, Convert.ToString(snorlax.Move3PP), movePP, Color.Black);
                }
                if (arrowSize.X == 290 && arrowSize.Y == 680)
                {
                    _spriteBatch.DrawString(menuFont, "PP         /30", PPText, Color.Black);
                    _spriteBatch.DrawString(menuFont, Convert.ToString(snorlax.Move4PP), movePP, Color.Black);
                }
                _spriteBatch.Draw(arrow, arrowSize, Color.Black);
                if (snorlax.CurrentText == Snorlax.Text.bodyPress && snorlax.TextTime <= 3)
                {
                    _spriteBatch.Draw(menu, menuTextbox, Color.White);
                    _spriteBatch.DrawString(menuFont, "Snorlax used Body Press!", moveName1, Color.Black);
                }
                if (snorlax.CurrentText == Snorlax.Text.headbutt && snorlax.TextTime <= 3)
                {
                    _spriteBatch.Draw(menu, menuTextbox, Color.White);
                    _spriteBatch.DrawString(menuFont, "Snorlax used Headbutt!", moveName1, Color.Black);
                }
                if (snorlax.CurrentText == Snorlax.Text.hyperBeam && snorlax.TextTime <= 3)
                {
                    _spriteBatch.Draw(menu, menuTextbox, Color.White);
                    _spriteBatch.DrawString(menuFont, "Snorlax used Hyper Beam!", moveName1, Color.Black);
                }
                if (snorlax.CurrentText == Snorlax.Text.defenseCurl && snorlax.TextTime <= 3)
                {
                    _spriteBatch.Draw(menu, menuTextbox, Color.White);
                    _spriteBatch.DrawString(menuFont, "Snorlax used Defense Curl!", moveName1, Color.Black);
                    _spriteBatch.DrawString(menuFont, "Snorlax Defense Rose!", moveName2, Color.Black);
                }
                _spriteBatch.Draw(nameIcon, charIconSize, Color.White);
                _spriteBatch.Draw(healthbar, charHealthBar, Color.LimeGreen);
                _spriteBatch.Draw(healthIcon, charHealthImg, Color.White);
                _spriteBatch.DrawString(healthFont, Pokemon.Snorlax.ToString(), charNameText, Color.Black);
                _spriteBatch.DrawString(healthFont, "/" + Convert.ToString(totalHealth), totalHealthText, Color.Black);
                _spriteBatch.DrawString(healthFont, Convert.ToString(healthAmount), healthAmountText, Color.Black);
            }
            _spriteBatch.Draw(nameIcon, enemyIconSize, Color.White);
            _spriteBatch.Draw(healthbar, enemyHealthBar, Color.LimeGreen);
            _spriteBatch.Draw(healthIcon, enemyHealthImg, Color.White);
            _spriteBatch.DrawString(healthFont, currentEnemy.ToString(), enemyNameText, Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
