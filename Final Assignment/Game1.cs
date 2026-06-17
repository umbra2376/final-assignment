using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Pokemon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;




namespace Final_Assignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState currentState, oldState;
        SpriteFont menuFont, healthFont;
        SoundEffect flamethrower, physical, defenseSound, fireBlastSound, pokeBallShaking, biteSound, hyperSound, howlSound, chargeSound, thunderSound, iceSound, bulkSound;
        SoundEffectInstance pokeBall;
        Song battleMusic, catchingMusic, titleMusic, pokeTheme, pokeLoseMusic;
        Rectangle window, menuLocation, moveInfoLocation, battleLocation, arrowSize, catchingArrow, charHealthBar, enemyHealthBar, charHealthImg, enemyHealthImg, charIconSize, enemyIconSize, menuTextbox, catchPokemon;
        Rectangle ballLocation, catchStarLocation;
        Snorlax snorlax;
        Arcanine arcanine;
        Electivire electivire;
        Texture2D snorlaxTexture, AOtexture, AWtexture, EWTexture, menu, healthbar, healthIcon, battle1Img, battle2Img, arrow, nameIcon, hyperBeam, hyperBeamImpact, defenseCurl, blastTexture, blastImpact, crunchTexture, flamethrowerTexture, howlTexture, ThunderP, IceP, bulkTexture;
        Texture2D losingScreen, winningScreen, catchingBackground, catchingMenu, catchingMenu2, ballOpen, catchStar;
        Vector2 moveType, moveName1, moveName2, moveName3, moveName4, typeText, PPText, movePP, charNameText, enemyNameText, totalHealthText, healthAmountText, yesText, noText, tutortialText;
        int charSpeed, enemySpeed, enemyChoice, crit, catchSuccess, introFrame, ballFrame, shakeCount, recoilDamage;
        int Snoremove1Damage, Snoremove2Damage, Snoremove3Damage, Arcmove1Damage, Arcmove2Damage, Arcmove3Damage, Elecmove1Damage, Elecmove2Damage, Elecmove3Damage;
        bool catchSelection, inBall, battle2Ready, catchFinished;
        float frameTime, shakeTimer;
        Random enemyMove = new Random();
        Random catchRate = new Random();
        Random critRate = new Random();
        enum BattleState
        {
            playerInput, playerAction, enemyAction, animation, turnEnd
        }
        enum Pokemon
        {
            Snorlax, Arcanine
        }
        enum Enemy
        {
            Arcanine, Electivire
        }
        enum Turn
        {
            charTurn, enemyTurn, none
        }
        enum Screen
        {
            Intro, Battle, Catching, Lose, End
        }
        enum Battle
        {
            First, Second
        }
        private Turn currentTurn;
        private Screen screen;
        private Pokemon currentPokemon;
        private Enemy currentEnemy;
        private BattleState battleState;
        private Battle battle;
        List<Texture2D> intro = new List<Texture2D>();
        List<Texture2D> catching = new List<Texture2D>();




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
            introFrame = 0;
            ballFrame = 0;
            shakeCount = 0;
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
            yesText = new Vector2(780, 600);
            noText = new Vector2(790, 670);
            healthAmountText = new Vector2(777, 460);
            tutortialText = new Vector2(130, 0);
            arrowSize = new Rectangle(20, 600, 50, 60);
            catchingArrow = new Rectangle(720, 600, 50, 60);
            charHealthImg = new Rectangle(570, 390, 370, 100);
            charIconSize = new Rectangle(530, 340, 460, 200);
            charHealthBar = new Rectangle(670, 428, 235, 30);
            enemyIconSize = new Rectangle(20, 20, 460, 150);
            enemyHealthImg = new Rectangle(70, 50, 370, 100);
            enemyHealthBar = new Rectangle(170, 88, 235, 30);
            catchPokemon = new Rectangle(350, 250, 300, 300);
            ballLocation = new Rectangle(320, 220, 80, 80);
            catchStarLocation = new Rectangle(300, 220, 60, 60);
            enemyNameText = new Vector2(100, 40);
            catchSelection = false;
            inBall = false;
            battle2Ready = false;
            catchFinished = false;
            screen = Screen.Intro;
            battle = Battle.First;
            currentPokemon = Pokemon.Snorlax;
            currentEnemy = Enemy.Arcanine;
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            intro.Add(Content.Load<Texture2D>("pokeIntroFinal(1)"));
            intro.Add(Content.Load<Texture2D>("pokeIntroFinal(2)"));
            losingScreen = Content.Load<Texture2D>("losingScreen");
            winningScreen = Content.Load<Texture2D>("winningScreen");
            catchingBackground = Content.Load<Texture2D>("catchBackground");
            catchingMenu = Content.Load<Texture2D>("catchingMenu");
            catchingMenu2 = Content.Load<Texture2D>("catchingMenu2");
            snorlaxTexture = Content.Load<Texture2D>("snorlax");
            AWtexture = Content.Load<Texture2D>("arcanine");
            AOtexture = Content.Load<Texture2D>("ownedArcanine");
            EWTexture = Content.Load<Texture2D>("WElectivire");
            healthbar = Content.Load<Texture2D>("tile");
            healthIcon = Content.Load<Texture2D>("healthBar");
            menu = Content.Load<Texture2D>("starterMoveset");
            menuFont = Content.Load<SpriteFont>("pokeFont");
            healthFont = Content.Load<SpriteFont>("healthFont");
            battle1Img = Content.Load<Texture2D>("grassBattlefield");
            battle2Img = Content.Load<Texture2D>("sandBattlefield");
            arrow = Content.Load<Texture2D>("select");
            nameIcon = Content.Load<Texture2D>("nameIcon");
            hyperBeam = Content.Load<Texture2D>("hyperBeam");
            hyperBeamImpact = Content.Load<Texture2D>("hyperBeamImpact");
            defenseCurl = Content.Load<Texture2D>("defenseCurl");
            blastTexture = Content.Load<Texture2D>("blastBeam");
            blastImpact = Content.Load<Texture2D>("blastImpact");
            crunchTexture = Content.Load<Texture2D>("crunch");
            flamethrowerTexture = Content.Load<Texture2D>("flamethrower");
            howlTexture = Content.Load<Texture2D>("Howl");
            ThunderP = Content.Load<Texture2D>("thunderPunch");
            IceP = Content.Load<Texture2D>("icePunch");
            bulkTexture = Content.Load<Texture2D>("bulkUp");
            ballOpen = Content.Load<Texture2D>("pokeBallFail");
            catching.Add(Content.Load<Texture2D>("pokeBall1"));
            catching.Add(Content.Load<Texture2D>("pokeBall2"));
            catching.Add(Content.Load<Texture2D>("pokeBall3"));
            catchStar = Content.Load<Texture2D>("successfulCatch");


            flamethrower = Content.Load<SoundEffect>("flamethrowerSound");
            physical = Content.Load<SoundEffect>("tackle");
            defenseSound = Content.Load<SoundEffect>("defenseSound");
            fireBlastSound = Content.Load<SoundEffect>("FireBlast");
            biteSound = Content.Load<SoundEffect>("Bite");
            hyperSound = Content.Load<SoundEffect>("hyperSound");
            howlSound = Content.Load<SoundEffect>("howlSound");
            chargeSound = Content.Load<SoundEffect>("WChargeSound");
            iceSound = Content.Load<SoundEffect>("PiceSound");
            thunderSound = Content.Load<SoundEffect>("PthunderSound");
            bulkSound = Content.Load<SoundEffect>("BulkUpSound");
            pokeBallShaking = Content.Load<SoundEffect>("pokeballShaking");
            pokeBall = pokeBallShaking.CreateInstance();
            pokeBall.IsLooped = false;


            battleMusic = Content.Load<Song>("battleMusic");
            catchingMusic = Content.Load<Song>("catchingMusic");
            titleMusic = Content.Load<Song>("titleTheme");
            pokeTheme = Content.Load<Song>("pokeTheme");
            pokeLoseMusic = Content.Load<Song>("pokeLoseMusic");
            MediaPlayer.Volume = 0.4f;

            arcanine = new Arcanine(AWtexture, AOtexture, blastTexture, blastImpact, crunchTexture, flamethrowerTexture, howlTexture, new Rectangle(190, 340, 300, 300), new Rectangle(610, 90, 300, 300));
            snorlax = new Snorlax(snorlaxTexture, hyperBeam, hyperBeamImpact, defenseCurl, new Rectangle(120, 283, 400, 400));
            electivire = new Electivire(EWTexture, ThunderP, IceP, bulkTexture, new Rectangle(610, 90, 300, 300));
            // TODO: use this.Content to load your game content here
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            currentState = Keyboard.GetState();
            if (screen == Screen.Intro)
            {
                if (MediaPlayer.State != MediaState.Playing)
                {
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Play(titleMusic);
                }
            }
            if (screen == Screen.Intro)
            {
                battle = Battle.First;
                frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (frameTime >= 0.5)
                {
                    introFrame = 1;
                }
                if (frameTime >= 1)
                {
                    introFrame = 0;
                    frameTime = 0;
                }
                if (currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                    StartBattle(Battle.First);
            }
            if (screen == Screen.Battle)
            {
                MoveArrow();
                switch (battleState)
                {
                    case BattleState.playerInput:
                        PlayerInput();
                        break;
                    case BattleState.playerAction:
                        PlayerAction();
                        break;
                    case BattleState.enemyAction:
                        EnemyAction();
                        break;
                    case BattleState.animation:
                        Animation(gameTime);
                        break;
                    case BattleState.turnEnd:
                        TurnEnd();
                        break;
                }
                if (battle == Battle.First)
                {
                    if (charHealthBar.Width == 0)
                    {
                        frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (frameTime >= 3.5)
                        {
                            screen = Screen.Lose;
                            MediaPlayer.Stop();
                            MediaPlayer.IsRepeating = false;
                            MediaPlayer.Play(pokeLoseMusic);
                            frameTime = 0;
                        }
                    }
                    if (enemyHealthBar.Width == 0)
                    {
                        frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (frameTime >= 3.5)
                        {
                            screen = Screen.Catching;
                            MediaPlayer.Stop();
                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Play(catchingMusic);
                            frameTime = 0;
                        }
                    }
                }
                if (battle == Battle.Second)
                {
                    if (charHealthBar.Width == 0)
                    {
                        frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (frameTime >= 3.5)
                        {
                            screen = Screen.Lose;
                            MediaPlayer.Stop();
                            MediaPlayer.IsRepeating = false;
                            MediaPlayer.Play(pokeLoseMusic);
                            frameTime = 0;
                        }
                    }
                    if (enemyHealthBar.Width == 0)
                    {
                        frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (frameTime >= 3.5)
                        {
                            screen = Screen.End;
                            MediaPlayer.Stop();
                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Play(pokeTheme);
                            frameTime = 0;
                        }
                    }
                }
            }
            if (screen == Screen.End)
            {
                frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (battle == Battle.Second)
                {
                    if (frameTime >= 10)
                    {
                        screen = Screen.Intro;
                        MediaPlayer.Stop();
                        MediaPlayer.IsRepeating = true;
                        MediaPlayer.Play(pokeTheme);
                    }
                }
            }
            if (screen == Screen.Catching)
            {
                frameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (catchingArrow.Y == 600 && currentState.IsKeyDown(Keys.Down))
                    catchingArrow.Y = 670;
                if (catchingArrow.Y == 670 && currentState.IsKeyDown(Keys.Up))
                    catchingArrow.Y = 600;
                if (catchingArrow.Y == 600 && !catchSelection)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        catchSelection = true;
                        catchSuccess = catchRate.Next(0, 10);
                    }
                }
                else if (catchingArrow.Y == 670 && !catchSelection)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        inBall = false;
                        catchPokemon.Width = 300;
                        catchPokemon.Height = 300;
                        shakeCount = 0;
                        battle2Ready = true;
                    }
                }
                if (catchSelection && frameTime <= 3 && catchPokemon.Width > 0)
                {
                    catchPokemon.Width -= 5;
                    catchPokemon.Height -= 5;
                    if (catchPokemon.Width < 0)
                        catchPokemon.Width = 0;
                    if (catchPokemon.Height < 0)
                        catchPokemon.Height = 0;
                }
                if (frameTime >= 3 && catchSelection)
                {
                    catchSelection = false;
                    inBall = true;
                    frameTime = 0;
                    ballFrame = 0;
                    shakeCount = 0;
                    shakeTimer = 0f;
                }
                if (inBall)
                {
                    pokeBall.Play();
                    shakeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (shakeCount < 3)
                    {
                        if (shakeTimer >= 0.5f)
                        {
                            shakeTimer = 0;
                            if (ballFrame == 0)
                                ballFrame = 1;
                            else if (ballFrame == 1)
                                ballFrame = 2;
                            else
                            {
                                ballFrame = 0;
                                shakeCount++;
                            }
                        }
                    }
                    else
                    {
                        catchFinished = true;
                        ballFrame = 0;
                        if (catchSuccess <= 7)
                        {
                            inBall = false;
                            currentPokemon = Pokemon.Arcanine;
                            arcanine.Wild = false;
                            arcanine.OwnedLocations();
                            shakeCount = 0;
                            battle2Ready = true;
                        }
                        else
                        {
                            inBall = false;
                            catchPokemon.Width = 300;
                            catchPokemon.Height = 300;
                            shakeCount = 0;
                            battle2Ready = true;
                        }
                    }
                }
                if (battle2Ready)
                {
                    if (currentState.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
                    {
                        catchFinished = false;
                        StartBattle(Battle.Second);
                        battle2Ready = false;
                    }
                }
            }
            oldState = currentState;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(intro[introFrame], window, Color.White);
                _spriteBatch.DrawString(menuFont, "Press A to select and use arrow keys to move.", tutortialText, Color.Black);
            }
            if (screen == Screen.Battle)
            {
                if (battle == Battle.First)
                    _spriteBatch.Draw(battle1Img, battleLocation, Color.White);
                else if (battle == Battle.Second)
                    _spriteBatch.Draw(battle2Img, battleLocation, Color.White);
                if (currentEnemy == Enemy.Arcanine)
                    arcanine.Draw(_spriteBatch);
                else if (currentEnemy == Enemy.Electivire)
                    electivire.Draw(_spriteBatch);
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
                    _spriteBatch.Draw(nameIcon, charIconSize, Color.White);
                    if (charHealthBar.Width >= 117)
                        _spriteBatch.Draw(healthbar, charHealthBar, Color.LimeGreen);
                    else if (charHealthBar.Width >= 47 && charHealthBar.Width <= 117)
                        _spriteBatch.Draw(healthbar, charHealthBar, Color.YellowGreen);
                    else
                        _spriteBatch.Draw(healthbar, charHealthBar, Color.Red);
                    _spriteBatch.Draw(healthIcon, charHealthImg, Color.White);
                    _spriteBatch.DrawString(healthFont, Pokemon.Snorlax.ToString(), charNameText, Color.Black);
                    _spriteBatch.DrawString(healthFont, "/" + Convert.ToString(snorlax.Health), totalHealthText, Color.Black);
                    _spriteBatch.DrawString(healthFont, Convert.ToString(snorlax.HealthCurrent), healthAmountText, Color.Black);

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
                }
                if (currentPokemon == Pokemon.Arcanine)
                {
                    arcanine.DrawPokemon(_spriteBatch);
                    _spriteBatch.Draw(menu, menuLocation, Color.White);
                    _spriteBatch.Draw(menu, moveInfoLocation, Color.White);
                    _spriteBatch.Draw(arrow, arrowSize, Color.Black);
                    _spriteBatch.DrawString(menuFont, arcanine.Move1, moveName1, Color.Black);
                    _spriteBatch.DrawString(menuFont, arcanine.Move2, moveName2, Color.Black);
                    _spriteBatch.DrawString(menuFont, arcanine.Move3, moveName3, Color.Black);
                    _spriteBatch.DrawString(menuFont, arcanine.Move4, moveName4, Color.Black);
                    _spriteBatch.DrawString(menuFont, "Type/", typeText, Color.Black);
                    if (arrowSize.X == 20 && arrowSize.Y == 600)
                    {
                        _spriteBatch.DrawString(menuFont, "PP         /15", PPText, Color.Black);
                        _spriteBatch.DrawString(menuFont, Convert.ToString(arcanine.Move1PP), movePP, Color.Black);
                        _spriteBatch.DrawString(menuFont, "Fire", moveType, Color.Black);
                    }
                    if (arrowSize.X == 20 && arrowSize.Y == 680)
                    {
                        _spriteBatch.DrawString(menuFont, "PP         /15", PPText, Color.Black);
                        _spriteBatch.DrawString(menuFont, Convert.ToString(arcanine.Move2PP), movePP, Color.Black);
                        _spriteBatch.DrawString(menuFont, "Dark", moveType, Color.Black);
                    }
                    if (arrowSize.X == 290 && arrowSize.Y == 600)
                    {
                        _spriteBatch.DrawString(menuFont, "PP         /5", PPText, Color.Black);
                        _spriteBatch.DrawString(menuFont, Convert.ToString(arcanine.Move3PP), movePP, Color.Black);
                        _spriteBatch.DrawString(menuFont, "Fire", moveType, Color.Black);
                    }
                    if (arrowSize.X == 290 && arrowSize.Y == 680)
                    {
                        _spriteBatch.DrawString(menuFont, "PP         /40", PPText, Color.Black);
                        _spriteBatch.DrawString(menuFont, Convert.ToString(arcanine.Move4PP), movePP, Color.Black);
                        _spriteBatch.DrawString(menuFont, "Normal", moveType, Color.Black);
                    }
                    _spriteBatch.Draw(arrow, arrowSize, Color.Black);
                    _spriteBatch.Draw(nameIcon, charIconSize, Color.White);
                    if (charHealthBar.Width >= 117)
                        _spriteBatch.Draw(healthbar, charHealthBar, Color.LimeGreen);
                    else if (charHealthBar.Width >= 47 && charHealthBar.Width <= 117)
                        _spriteBatch.Draw(healthbar, charHealthBar, Color.YellowGreen);
                    else
                        _spriteBatch.Draw(healthbar, charHealthBar, Color.Red);
                    _spriteBatch.Draw(healthIcon, charHealthImg, Color.White);
                    _spriteBatch.DrawString(healthFont, Pokemon.Arcanine.ToString(), charNameText, Color.Black);
                    _spriteBatch.DrawString(healthFont, "/" + Convert.ToString(arcanine.Health), totalHealthText, Color.Black);
                    _spriteBatch.DrawString(healthFont, Convert.ToString(arcanine.HealthCurrent), healthAmountText, Color.Black);

                    if (arcanine.CurrentText == Arcanine.Text.flamethrower && arcanine.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Arcanine used Flamethrower!", moveName1, Color.Black);
                    }
                    if (arcanine.CurrentText == Arcanine.Text.crunch && arcanine.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Arcanine used Crunch", moveName1, Color.Black);
                    }
                    if (arcanine.CurrentText == Arcanine.Text.fireblast && arcanine.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Arcanine used Fire Blast!", moveName1, Color.Black);
                    }
                    if (arcanine.CurrentText == Arcanine.Text.howl && arcanine.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Arcanine used Howl!", moveName1, Color.Black);
                        _spriteBatch.DrawString(menuFont, "Arcanine Atttack Rose!", moveName2, Color.Black);
                    }
                }
                if (currentEnemy == Enemy.Electivire)
                {
                    if (electivire.CurrentText == Electivire.Text.wildCharge && electivire.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Electivire used Wild Charge!", moveName1, Color.Black);
                    }
                    if (electivire.CurrentText == Electivire.Text.thunderPunch && electivire.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Electivire used Thunder Punch!", moveName1, Color.Black);
                    }
                    if (electivire.CurrentText == Electivire.Text.icePunch && electivire.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Electivire used Ice Punch!", moveName1, Color.Black);
                    }
                    if (electivire.CurrentText == Electivire.Text.bulkUp && electivire.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Electivire used Bulk Up!", moveName1, Color.Black);
                        _spriteBatch.DrawString(menuFont, "Electivire's Attack and Defense Rose!", moveName2, Color.Black);
                    }
                }
                if (currentPokemon == Pokemon.Arcanine)
                    arcanine.DrawAttack(_spriteBatch);
                if (currentEnemy == Enemy.Arcanine)
                {
                    if (arcanine.CurrentText == Arcanine.Text.flamethrower && arcanine.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Arcanine used Flamethrower!", moveName1, Color.Black);
                    }
                    if (arcanine.CurrentText == Arcanine.Text.crunch && arcanine.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Arcanine used Crunch!", moveName1, Color.Black);
                    }
                    if (arcanine.CurrentText == Arcanine.Text.fireblast && arcanine.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Arcanine used Fire Blast!", moveName1, Color.Black);
                    }
                    if (arcanine.CurrentText == Arcanine.Text.howl && arcanine.TextTime <= 3)
                    {
                        _spriteBatch.Draw(menu, menuTextbox, Color.White);
                        _spriteBatch.DrawString(menuFont, "Arcanine used Howl!", moveName1, Color.Black);
                        _spriteBatch.DrawString(menuFont, "Arcanine's Attack Rose!", moveName2, Color.Black);
                    }
                }
                _spriteBatch.Draw(nameIcon, enemyIconSize, Color.White);
                if (enemyHealthBar.Width >= 117)
                    _spriteBatch.Draw(healthbar, enemyHealthBar, Color.LimeGreen);
                else if (enemyHealthBar.Width >= 47 && enemyHealthBar.Width <= 117)
                    _spriteBatch.Draw(healthbar, enemyHealthBar, Color.YellowGreen);
                else
                    _spriteBatch.Draw(healthbar, enemyHealthBar, Color.Red);
                _spriteBatch.Draw(healthIcon, enemyHealthImg, Color.White);
                _spriteBatch.DrawString(healthFont, currentEnemy.ToString(), enemyNameText, Color.Black);
            }
            if (screen == Screen.Lose)
            {
                _spriteBatch.Draw(losingScreen, window, Color.White);
            }
            if (screen == Screen.End)
            {
                _spriteBatch.Draw(winningScreen, window, Color.White);
            }
            if (screen == Screen.Catching)
            {
                _spriteBatch.Draw(catchingBackground, window, Color.White);
                _spriteBatch.Draw(catchingMenu, menuLocation, Color.White);
                _spriteBatch.Draw(catchingMenu2, moveInfoLocation, Color.White);
                _spriteBatch.DrawString(menuFont, "Would you like to attempt", moveName1, Color.White);
                _spriteBatch.DrawString(menuFont, "to catch this pokemon?", moveName2, Color.White);
                _spriteBatch.Draw(arrow, catchingArrow, Color.LightBlue);
                _spriteBatch.DrawString(menuFont, "Yes", yesText, Color.White);
                _spriteBatch.DrawString(menuFont, "No", noText, Color.White);
                if (catchSelection)
                {
                    _spriteBatch.Draw(ballOpen, ballLocation, Color.White);
                }
                if (currentEnemy == Enemy.Arcanine && !inBall)
                    _spriteBatch.Draw(AWtexture, catchPokemon, Color.White);
                if (inBall)
                {
                    _spriteBatch.Draw(catching[ballFrame], ballLocation, Color.White);
                }
                if (!catchSelection && !inBall && catchFinished
                    )
                {
                    if (catchSuccess <= 7)
                    {
                        _spriteBatch.Draw(catching[0], ballLocation, Color.White);
                        _spriteBatch.Draw(catchingMenu, menuLocation, Color.White);
                        _spriteBatch.Draw(catchingMenu2, moveInfoLocation, Color.White);
                        _spriteBatch.Draw(catchStar, catchStarLocation, Color.White);
                        _spriteBatch.DrawString(menuFont, "The pokemon has been", moveName1, Color.White);
                        _spriteBatch.DrawString(menuFont, "successfully caught!", moveName2, Color.White);
                        currentPokemon = Pokemon.Arcanine;
                    }
                    else
                    {
                        _spriteBatch.Draw(AWtexture, catchPokemon, Color.White);
                        _spriteBatch.Draw(catchingMenu, menuLocation, Color.White);
                        _spriteBatch.Draw(catchingMenu2, moveInfoLocation, Color.White);
                        _spriteBatch.Draw(ballOpen, ballLocation, Color.White);
                        _spriteBatch.DrawString(menuFont, "The pokemon escaped", moveName1, Color.White);
                        _spriteBatch.DrawString(menuFont, "better luck next time.", moveName2, Color.White);
                    }
                }
                if (battle2Ready)
                {
                    _spriteBatch.DrawString(menuFont, "Press enter to continue", tutortialText, Color.White);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        void PlayerInput()
        {
            if (currentPokemon == Pokemon.Snorlax)
            {
                if (!snorlax.CanAct) return;
                if (arrowSize.X == 20 && arrowSize.Y == 600 && snorlax.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        snorlax.Move1PP -= 1;
                        snorlax.CurrentMove = Snorlax.Move.headbutt;
                        battleState = BattleState.playerAction;
                    }
                }
                else if (arrowSize.X == 20 && arrowSize.Y == 680 && snorlax.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        snorlax.Move2PP -= 1;
                        snorlax.CurrentMove = Snorlax.Move.bodyPress;
                        battleState = BattleState.playerAction;
                    }
                }
                else if (arrowSize.X == 290 && arrowSize.Y == 600 && snorlax.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        snorlax.Move3PP -= 1;
                        snorlax.CurrentMove = Snorlax.Move.hyperBeam;
                        battleState = BattleState.playerAction;
                    }
                }
                else if (arrowSize.X == 290 && arrowSize.Y == 680 && snorlax.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        snorlax.Move4PP -= 1;
                        snorlax.CurrentMove = Snorlax.Move.defenseCurl;
                        battleState = BattleState.playerAction;
                    }
                }
            }
            else if (currentPokemon == Pokemon.Arcanine)
            {
                if (!arcanine.CanAct) return;
                if (arrowSize.X == 20 && arrowSize.Y == 600 && arcanine.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        arcanine.Move1PP -= 1;
                        arcanine.CurrentMove = Arcanine.Move.flamethrower;
                        battleState = BattleState.playerAction;
                    }
                }
                else if (arrowSize.X == 20 && arrowSize.Y == 680 && arcanine.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        arcanine.Move2PP -= 1;
                        arcanine.CurrentMove = Arcanine.Move.crunch;
                        battleState = BattleState.playerAction;
                    }
                }
                else if (arrowSize.X == 290 && arrowSize.Y == 600 && arcanine.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        arcanine.Move3PP -= 1;
                        arcanine.CurrentMove = Arcanine.Move.fireblast;
                        battleState = BattleState.playerAction;
                    }
                }
                else if (arrowSize.X == 290 && arrowSize.Y == 680 && arcanine.CanAct)
                {
                    if (currentState.IsKeyDown(Keys.A) && oldState.IsKeyUp(Keys.A))
                    {
                        arcanine.Move4PP -= 1;
                        arcanine.CurrentMove = Arcanine.Move.howl;
                        battleState = BattleState.playerAction;
                    }
                }
            }
        }
        void PlayerAction()
        {
            currentTurn = Turn.enemyTurn;
            if (currentPokemon == Pokemon.Snorlax)
                snorlax.CanAct = false;
            else if (currentPokemon == Pokemon.Arcanine)
                arcanine.CanAct = false;
            if (currentEnemy == Enemy.Arcanine && currentPokemon == Pokemon.Snorlax)
            {
                switch (snorlax.CurrentMove)
                {
                    case Snorlax.Move.headbutt:
                        arcanine.HealthCurrent -= Snoremove1Damage;
                        if (arcanine.HealthCurrent < 0)
                            arcanine.HealthCurrent = 0;
                        enemyHealthBar.Width = (int)(235f * arcanine.HealthCurrent / arcanine.Health);
                        physical.Play();
                        break;
                    case Snorlax.Move.bodyPress:
                        arcanine.HealthCurrent -= Snoremove2Damage;
                        if (arcanine.HealthCurrent < 0)
                            arcanine.HealthCurrent = 0;
                        enemyHealthBar.Width = (int)(235f * arcanine.HealthCurrent / arcanine.Health);
                        physical.Play();
                        break;
                    case Snorlax.Move.hyperBeam:
                        arcanine.HealthCurrent -= Snoremove3Damage;
                        if (arcanine.HealthCurrent < 0)
                            arcanine.HealthCurrent = 0;
                        enemyHealthBar.Width = (int)(235f * arcanine.HealthCurrent / arcanine.Health);
                        hyperSound.Play();
                        break;
                    case Snorlax.Move.defenseCurl:
                        defenseSound.Play();
                        break;
                }
            }
            else if (currentEnemy == Enemy.Electivire && currentPokemon == Pokemon.Snorlax)
            {
                switch (snorlax.CurrentMove)
                {
                    case Snorlax.Move.headbutt:
                        electivire.HealthCurrent -= Snoremove1Damage;
                        if (electivire.HealthCurrent < 0)
                            electivire.HealthCurrent = 0;
                        enemyHealthBar.Width = (int)(235f * electivire.HealthCurrent / electivire.Health);
                        physical.Play();
                        break;
                    case Snorlax.Move.bodyPress:
                        electivire.HealthCurrent -= Snoremove2Damage;
                        if (electivire.HealthCurrent < 0)
                            electivire.HealthCurrent = 0;
                        enemyHealthBar.Width = (int)(235f * electivire.HealthCurrent / electivire.Health);
                        physical.Play();
                        break;
                    case Snorlax.Move.hyperBeam:
                        electivire.HealthCurrent -= Snoremove3Damage;
                        if (electivire.HealthCurrent < 0)
                            electivire.HealthCurrent = 0;
                        enemyHealthBar.Width = (int)(235f * electivire.HealthCurrent / electivire.Health);
                        hyperSound.Play();
                        break;
                    case Snorlax.Move.defenseCurl:
                        defenseSound.Play();
                        break;
                }
            }
            else if (currentEnemy == Enemy.Electivire && currentPokemon == Pokemon.Arcanine)
            {
                switch (arcanine.CurrentMove)
                {
                    case Arcanine.Move.flamethrower:
                        electivire.HealthCurrent -= Arcmove1Damage;
                        if (electivire.HealthCurrent < 0)
                            electivire.HealthCurrent = 0;
                        enemyHealthBar.Width = (int)(235f * electivire.HealthCurrent / electivire.Health);
                        flamethrower.Play();
                        break;
                    case Arcanine.Move.crunch:
                        electivire.HealthCurrent -= Arcmove2Damage;
                        if (electivire.HealthCurrent < 0)
                            electivire.HealthCurrent = 0;
                        enemyHealthBar.Width = (int)(235f * electivire.HealthCurrent / electivire.Health);
                        biteSound.Play();
                        break;
                    case Arcanine.Move.fireblast:
                        electivire.HealthCurrent -= Arcmove3Damage;
                        if (electivire.HealthCurrent < 0)
                            electivire.HealthCurrent = 0;
                        enemyHealthBar.Width = (int)(235f * electivire.HealthCurrent / electivire.Health);
                        fireBlastSound.Play();
                        break;
                    case Arcanine.Move.howl:
                        howlSound.Play();
                        break;
                }
            }
            if (enemyHealthBar.Width <= 0)
            {
                battleState = BattleState.animation;
                currentTurn = Turn.none;
                return;
            }
            battleState = BattleState.animation;
        }
        void EnemyAction()
        {
            if (currentEnemy == Enemy.Arcanine)
            {
                currentTurn = Turn.charTurn;
                enemyChoice = enemyMove.Next(1, 5);
                arcanine.CanAct = false;
                switch (enemyChoice)
                {
                    case 1:
                        arcanine.CurrentMove = Arcanine.Move.flamethrower;
                        snorlax.HealthCurrent -= Arcmove1Damage;
                        if (snorlax.HealthCurrent < 0)
                            snorlax.HealthCurrent = 0;
                        charHealthBar.Width = (int)(235f * snorlax.HealthCurrent / snorlax.Health);
                        flamethrower.Play();
                        break;
                    case 2:
                        arcanine.CurrentMove = Arcanine.Move.crunch;
                        snorlax.HealthCurrent -= Arcmove2Damage;
                        if (snorlax.HealthCurrent < 0)
                            snorlax.HealthCurrent = 0;
                        charHealthBar.Width = (int)(235f * snorlax.HealthCurrent / snorlax.Health);
                        biteSound.Play();
                        break;
                    case 3:
                        arcanine.CurrentMove = Arcanine.Move.fireblast;
                        snorlax.HealthCurrent -= Arcmove3Damage;
                        if (snorlax.HealthCurrent < 0)
                            snorlax.HealthCurrent = 0;
                        charHealthBar.Width = (int)(235f * snorlax.HealthCurrent / snorlax.Health);
                        fireBlastSound.Play();
                        break;
                    case 4:
                        arcanine.CurrentMove = Arcanine.Move.howl;
                        howlSound.Play();
                        break;
                }
                battleState = BattleState.animation;
            }
            else if (currentEnemy == Enemy.Electivire)
            {
                currentTurn = Turn.charTurn;
                enemyChoice = enemyMove.Next(1, 5);
                electivire.CanAct = false;
                switch (enemyChoice)
                {
                    case 1:
                        electivire.CurrentMove = Electivire.Move.wildCharge;
                        if (currentPokemon == Pokemon.Snorlax)
                        {
                            snorlax.HealthCurrent -= Elecmove1Damage;
                            recoilDamage = Elecmove1Damage / 4;
                            electivire.HealthCurrent -= recoilDamage;
                            enemyHealthBar.Width = (int)(235f * electivire.HealthCurrent / electivire.Health);
                            charHealthBar.Width = (int)(235f * snorlax.HealthCurrent / snorlax.Health);
                            if (snorlax.HealthCurrent < 0)
                                snorlax.HealthCurrent = 0;
                        }
                        else if (currentPokemon == Pokemon.Arcanine)
                        {
                            arcanine.HealthCurrent -= Elecmove1Damage;
                            recoilDamage = Elecmove1Damage / 4;
                            electivire.HealthCurrent -= recoilDamage;
                            enemyHealthBar.Width = (int)(235f * electivire.HealthCurrent / electivire.Health);
                            charHealthBar.Width = (int)(235f * arcanine.HealthCurrent / arcanine.Health);
                            if (arcanine.HealthCurrent < 0)
                                arcanine.HealthCurrent = 0;
                        }
                        chargeSound.Play();
                        break;
                    case 2:
                        electivire.CurrentMove = Electivire.Move.thunderPunch;
                        if (currentPokemon == Pokemon.Snorlax)
                        {
                            snorlax.HealthCurrent -= Elecmove2Damage;
                            charHealthBar.Width = (int)(235f * snorlax.HealthCurrent / snorlax.Health);
                            if (snorlax.HealthCurrent < 0)
                                snorlax.HealthCurrent = 0;
                        }
                        if (currentPokemon == Pokemon.Arcanine)
                        {
                            arcanine.HealthCurrent -= Elecmove2Damage;
                            charHealthBar.Width = (int)(235f * arcanine.HealthCurrent / arcanine.Health);
                            if (arcanine.HealthCurrent < 0)
                                arcanine.HealthCurrent = 0;
                        }
                        thunderSound.Play();
                        break;
                    case 3:
                        electivire.CurrentMove = Electivire.Move.icePunch;
                        if (currentPokemon == Pokemon.Snorlax)
                        {
                            snorlax.HealthCurrent -= Elecmove3Damage;
                            charHealthBar.Width = (int)(235f * snorlax.HealthCurrent / snorlax.Health);
                            if (snorlax.HealthCurrent < 0)
                                snorlax.HealthCurrent = 0;
                        }
                        if (currentPokemon == Pokemon.Arcanine)
                        {
                            arcanine.HealthCurrent -= Elecmove3Damage;
                            charHealthBar.Width = (int)(235f * arcanine.HealthCurrent / arcanine.Health);
                            if (arcanine.HealthCurrent < 0)
                                arcanine.HealthCurrent = 0;
                        }
                        iceSound.Play();
                        break;
                    case 4:
                        electivire.CurrentMove = Electivire.Move.bulkUp;
                        bulkSound.Play();
                        break;
                }
                battleState = BattleState.animation;
            }
        }
        void Animation(GameTime gameTime)
        {
            System.Diagnostics.Debug.WriteLine(
            $"State:{battleState}  " +
            $"Snorlax:{snorlax.CanAct}  " +
            $"Arcanine:{arcanine.CanAct}  " +
            $"Electivire:{electivire.CanAct}");
            snorlax.Update(gameTime);
            arcanine.Update(gameTime);
            electivire.Update(gameTime);
            if (battle == Battle.First)
            {
                if (snorlax.CanAct && arcanine.CanAct)
                    battleState = BattleState.turnEnd;
            }
            else if (battle == Battle.Second)
            {
                if (currentPokemon == Pokemon.Snorlax)
                {
                    if (snorlax.CanAct && electivire.CanAct)
                        battleState = BattleState.turnEnd;
                }
                else if (currentPokemon == Pokemon.Arcanine)
                {
                    if (arcanine.CanAct && electivire.CanAct)
                        battleState = BattleState.turnEnd;
                }
            }
        }
        void TurnEnd()
        {
            if (enemyHealthBar.Width <= 0)
            {
                battleState = BattleState.playerInput;
                return;
            }
            snorlax.CanAct = true;
            arcanine.CanAct = true;
            electivire.CanAct = true;

            snorlax.CurrentMove = Snorlax.Move.none;
            arcanine.CurrentMove = Arcanine.Move.none;
            electivire.CurrentMove = Electivire.Move.none;

            if (currentTurn == Turn.enemyTurn)
                battleState = BattleState.enemyAction;
            else
                battleState = BattleState.playerInput;
        }
        void StartBattle(Battle nextBattle)
        {
            battle = nextBattle;
            MediaPlayer.Stop();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(battleMusic);
            arcanine.ResetAnimation();
            catchPokemon.Width = 300;
            catchPokemon.Height = 300;
            enemyHealthBar.Width = 235;
            charHealthBar.Width = 235;
            snorlax.HealthCurrent = snorlax.Health;
            arcanine.HealthCurrent = arcanine.Health;
            electivire.HealthCurrent = electivire.Health;
            snorlax.CanAct = true;
            arcanine.CanAct = true;
            electivire.CanAct = true;
            snorlax.CurrentMove = Snorlax.Move.none;
            arcanine.CurrentMove = Arcanine.Move.none;
            electivire.CurrentMove = Electivire.Move.none;

            arrowSize.X = 20;
            arrowSize.Y = 600;

            frameTime = 0;
            shakeCount = 0;
            catchSelection = false;
            inBall = false;
            switch (battle)
            {
                case Battle.First:
                    currentPokemon = Pokemon.Snorlax;
                    currentEnemy = Enemy.Arcanine;
                    arcanine.Wild = true;
                    arcanine.WildLocations();
                    break;
                case Battle.Second:
                    currentEnemy = Enemy.Electivire;
                    electivire.ResetAnimation();
                    if (currentPokemon == Pokemon.Arcanine)
                    {
                        arcanine.Wild = false;
                        arcanine.OwnedLocations();
                    }
                    break;
            }
            screen = Screen.Battle;

            SetMoveDamage();

            if (charSpeed >= enemySpeed)
            {
                battleState = BattleState.playerInput;
                currentTurn = Turn.charTurn;
            }
            else
            {
                battleState = BattleState.enemyAction;
                currentTurn = Turn.enemyTurn;
            }
        }
        void SetMoveDamage()
        {
            if (battle == Battle.First)
            {
                charSpeed = snorlax.Speed;
                enemySpeed = arcanine.Speed;

                Snoremove1Damage = (int)((((2 * 50 + 10) / 10) * ((float)snorlax.Attack / arcanine.Defense) * 70 + 2) / 50);
                Snoremove2Damage = (int)((((2 * 50 + 10) / 10) * ((float)snorlax.Attack / arcanine.Defense) * 80 + 2) / 50);
                Snoremove3Damage = (int)((((2 * 50 + 10) / 10) * ((float)snorlax.Attack / arcanine.Defense) * 150 + 2) / 50);

                Arcmove1Damage = (int)((((2 * 50 + 10) / 10) * ((float)arcanine.SAttack / snorlax.SDefense) * 70 + 2) / 50);
                Arcmove2Damage = (int)((((2 * 50 + 10) / 10) * ((float)arcanine.Attack / snorlax.Defense) * 80 + 2) / 50);
                Arcmove3Damage = (int)((((2 * 50 + 10) / 10) * ((float)arcanine.SAttack / snorlax.SDefense) * 150 + 2) / 50);
            }
            if (battle == Battle.Second)
            {
                if (currentPokemon == Pokemon.Snorlax)
                    charSpeed = snorlax.Speed;
                else if (currentPokemon == Pokemon.Arcanine)
                    charSpeed = arcanine.Speed;
                enemySpeed = electivire.Speed;
                Snoremove1Damage = (int)((((2 * 50 + 10) / 10) * ((float)snorlax.Attack / electivire.Defense) * 70 + 2) / 50);
                Snoremove2Damage = (int)((((2 * 50 + 10) / 10) * ((float)snorlax.Attack / electivire.Defense) * 80 + 2) / 50);
                Snoremove3Damage = (int)((((2 * 50 + 10) / 10) * ((float)snorlax.Attack / electivire.Defense) * 150 + 2) / 50);


                Arcmove1Damage = (int)((((2 * 50 + 10) / 10) * ((float)arcanine.SAttack / electivire.SDefense) * 70 + 2) / 50);
                Arcmove2Damage = (int)((((2 * 50 + 10) / 10) * ((float)arcanine.Attack / electivire.Defense) * 80 + 2) / 50);
                Arcmove3Damage = (int)((((2 * 50 + 10) / 10) * ((float)arcanine.SAttack / electivire.SDefense) * 150 + 2) / 50);
                if (currentPokemon == Pokemon.Snorlax)
                {
                    Elecmove1Damage = (int)((((2 * 50 + 10) / 10) * ((float)electivire.Attack / snorlax.Defense) * 90 + 2) / 50);
                    Elecmove2Damage = (int)((((2 * 50 + 10) / 10) * ((float)electivire.Attack / snorlax.Defense) * 75 + 2) / 50);
                    Elecmove3Damage = (int)((((2 * 50 + 10) / 10) * ((float)electivire.Attack / snorlax.Defense) * 75 + 2) / 50);
                }
                else if (currentPokemon == Pokemon.Arcanine)
                {
                    Elecmove1Damage = (int)((((2 * 50 + 10) / 10) * ((float)electivire.Attack / arcanine.Defense) * 90 + 2) / 50);
                    Elecmove2Damage = (int)((((2 * 50 + 10) / 10) * ((float)electivire.Attack / arcanine.Defense) * 75 + 2) / 50);
                    Elecmove3Damage = (int)(((((2 * 50 + 10) / 10) * ((float)electivire.Attack / arcanine.Defense) * 75 + 2) / 50) * 0.5);
                }
            }
        }
        void MoveArrow()
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
        }
    }
}