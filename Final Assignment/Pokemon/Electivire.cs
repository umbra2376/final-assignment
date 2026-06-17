using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NVorbis.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;




namespace Pokemon
{
    class Electivire
    {
        private Texture2D _Wtexture, _Pthunder, _Pice, _bulkTexture;
        private Rectangle _Wlocation, _PthunderLocation;
        private bool _wild, _canAct, _bulkBoost;
        private string _move1, _move2, _move3, _move4, _name;
        private int _speed, _health, _defense, _attack, _sAttack, _sDefense, _move1_PP, _move2_PP, _move3_PP, _move4_PP, _BulkUpEffectA, _BulkUpEffectD;
        private float _battle_time, _frame_time, _textTime, _alpha;
        private int _healtCurrent;
        public enum Move
        {
            wildCharge, thunderPunch, icePunch, bulkUp, none
        }
        public enum Text
        {
            wildCharge, thunderPunch, icePunch, bulkUp, none
        }
        private Move _currentMove;
        private Text _currentText;
        public Electivire(Texture2D Wtexture, Texture2D ThunderP, Texture2D IceP, Texture2D bulkTexture, Rectangle Wlocation)
        {
            _wild = true;
            _canAct = true;
            _Wtexture = Wtexture;
            _Pthunder = ThunderP;
            _Pice = IceP;
            _bulkTexture = bulkTexture;
            _Wlocation = Wlocation;
            _move1 = "Wild Charge";
            _move2 = "Thunder Punch";
            _move3 = "Ice Punch";
            _move4 = "Bulk Up";
            _name = "ELECTIVIRE";
            _move1_PP = 10;
            _move2_PP = 15;
            _move3_PP = 15;
            _move4_PP = 20;
            _PthunderLocation = new Rectangle(120, 283, 400, 300);
            Random stats = new Random();
            _speed = stats.Next(80, 111);
            _health = stats.Next(65, 86);
            _healtCurrent = _health;
            _defense = stats.Next(54, 81);
            _attack = stats.Next(113, 134);
            _sAttack = stats.Next(80, 111);
            _sDefense = stats.Next(70, 91);
            _BulkUpEffectA = (int)(_attack * 0.67);
            _BulkUpEffectD = (int)(_defense * 0.67);
            _currentMove = Move.none;
        }




        public void Update(GameTime gametime)
        {
            if (_currentMove == Move.wildCharge)
            {
                _canAct = false;
                _currentText = Text.wildCharge;
                _textTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                _frame_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_frame_time <= 1f)
                {
                    _Wlocation.X -= 10;
                    _Wlocation.Y += 7;
                }
                else if (_frame_time <= 2f)
                {
                    _Wlocation.X += 10;
                    _Wlocation.Y -= 7;
                }
                else if (_battle_time >= 3f && _textTime >= 3)
                {
                    _currentMove = Move.none;
                    _currentText = Text.none;
                    _Wlocation.X = 610;
                    _Wlocation.Y = 90;
                    _battle_time = 0;
                    _frame_time = 0;
                    _textTime = 0;
                    _canAct = true;
                }
            }
            if (_currentMove == Move.thunderPunch)
            {
                _canAct = false;
                _currentText = Text.thunderPunch;
                _textTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_battle_time >= 3 && _textTime >= 3)
                {
                    _canAct = true;
                    _currentText = Text.none;
                    _currentMove = Move.none;
                    _battle_time = 0;
                    _textTime = 0;
                }
            }
            if (_currentMove == Move.icePunch)
            {
                _canAct = false;
                _currentText = Text.icePunch;
                _textTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_battle_time >= 3 && _textTime >= 3)
                {
                    _canAct = true;
                    _currentText = Text.none;
                    _currentMove = Move.none;
                    _battle_time = 0;
                    _textTime = 0;
                }
            }
            if (_currentMove == Move.bulkUp)
            {
                _canAct = false;
                _currentText = Text.bulkUp;
                _textTime += (float)gametime.ElapsedGameTime.TotalSeconds;
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (!_bulkBoost)
                {
                    _attack += (int)_BulkUpEffectA;
                    _defense += (int)_BulkUpEffectD;
                    _bulkBoost = true;
                }
                if (_battle_time >= 2 && _textTime >= 3)
                {
                    _currentMove = Move.none;
                    _currentText = Text.none;
                    _battle_time = 0;
                    _textTime = 0;
                    _canAct = true;
                    _bulkBoost = false;
                }
            }
            if (_healtCurrent == 0)
            {
                _battle_time += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (_battle_time >= 1)
                    _Wlocation.Y += 10;
                if (_battle_time >= 5)
                {
                    _Wlocation.Y = 90;
                    _battle_time = 0;
                }
            }
        }
        public Move CurrentMove
        {
            get { return _currentMove; }
            set { _currentMove = value; }
        }
        public Text CurrentText
        {
            get { return _currentText; }
            set { _currentText = value; }
        }
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        public int HealthCurrent
        {
            get { return _healtCurrent; }
            set { _healtCurrent = value; }
        }
        public int Speed
        {
            get { return _speed; }
        }
        public int Defense
        {
            get { return _defense; }
        }
        public int SDefense
        {
            get { return _sDefense; }
        }
        public int SAttack
        {
            get { return _sAttack; }
        }
        public int Attack
        {
            get { return _attack; }
        }
        public string Move1
        {
            get { return _move1; }
        }
        public string Move2
        {
            get { return _move2; }
        }
        public string Move3
        {
            get { return _move3; }
        }
        public string Move4
        {
            get { return _move4; }
        }
        public int Move1PP
        {
            get { return _move1_PP; }
            set { _move1_PP = value; }
        }
        public int Move2PP
        {
            get { return _move2_PP; }
            set { _move2_PP = value; }
        }
        public int Move3PP
        {
            get { return _move3_PP; }
            set { _move3_PP = value; }
        }
        public int Move4PP
        {
            get { return _move4_PP; }
            set { _move4_PP = value; }
        }
        public string Name
        {
            get { return _name; }
        }
        public bool CanAct
        {
            get { return _canAct; }
            set { _canAct = value; }
        }
        public float TextTime
        {
            get { return _textTime; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (_wild)
            {
                spriteBatch.Draw(_Wtexture, _Wlocation, Color.White);
                if (_currentMove == Move.wildCharge)
                    spriteBatch.Draw(_Wtexture, _Wlocation, Color.Yellow);
                if (_currentMove == Move.thunderPunch)
                {
                    _alpha = 0f;
                    if (_battle_time <= 0.5f)
                        _alpha = _battle_time / 0.5f;
                    else if (_battle_time <= 1.5f)
                        _alpha = 1;
                    else if (_battle_time <= 2f)
                        _alpha = 1f - ((_battle_time - 1.5f) / 0.5f);
                    else
                        _alpha = 0;
                    spriteBatch.Draw(_Pthunder, _PthunderLocation, Color.White * _alpha);
                }
                if (_currentMove == Move.icePunch)
                {
                    _alpha = 0f;
                    if (_battle_time <= 0.5f)
                        _alpha = _battle_time / 0.5f;
                    else if (_battle_time <= 1.5f)
                        _alpha = 1;
                    else if (_battle_time <= 2f)
                        _alpha = 1f - ((_battle_time - 1.5f) / 0.5f);
                    else
                        _alpha = 0;
                    spriteBatch.Draw(_Pice, _PthunderLocation, Color.White * _alpha);
                }
                if (_currentMove == Move.bulkUp)
                {
                    _alpha = 0f;
                    if (_battle_time <= 0.5f)
                        _alpha = _battle_time / 0.5f;
                    else if (_battle_time <= 1.5f)
                        _alpha = 1;
                    else if (_battle_time <= 2f)
                        _alpha = 1f - ((_battle_time - 1.5f) / 0.5f);
                    else
                        _alpha = 0;
                    spriteBatch.Draw(_bulkTexture, _Wlocation, Color.White * _alpha);
                }
            }
        }
        public void ResetAnimation()
        {
            _battle_time = 0;
            _frame_time = 0;
            _textTime = 0;
            _alpha = 0;
            _currentMove = Move.none;
            _currentText = Text.none;
            _Wlocation = new Rectangle(610, 90, 300, 300);
        }
    }
}