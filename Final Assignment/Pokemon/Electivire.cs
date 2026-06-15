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
        private Texture2D _Wtexture, _Otexture;
        private Rectangle _Olocation, _Wlocation;
        private bool _wild, _canAct;
        private string _move1, _move2, _move3, _move4, _name;
        private int _speed, _health, _defense, _attack, _sAttack, _sDefense, _move1_PP, _move2_PP, _move3_PP, _move4_PP;
        private float _battle_time, _frame_time, _textTime;
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
        public Electivire(Texture2D Wtexture, Texture2D Otexture, Rectangle Olocation, Rectangle Wlocation)
        {
            _wild = true;
            _canAct = true;
            _Wtexture = Wtexture;
            _Otexture = Otexture;
            _Wlocation = Wlocation;
            _Olocation = Olocation;
            _move1 = "Wild Charge";
            _move2 = "Thunder Punch";
            _move3 = "Ice Punch";
            _move4 = "Bulk Up";
            _name = "ELECTIVIRE";
            _move1_PP = 10;
            _move2_PP = 15;
            _move3_PP = 15;
            _move4_PP = 20;
            Random stats = new Random();
            _speed = stats.Next(80, 111);
            _health = stats.Next(65, 86);
            _healtCurrent = _health;
            _defense = stats.Next(54, 81);
            _attack = stats.Next(113, 134);
            _sAttack = stats.Next(80, 111);
            _sDefense = stats.Next(70, 91);
            _currentMove = Move.none;
        }

        public void Update(GameTime gametime)
        {
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
            }
            else
            {
                spriteBatch.Draw(_Otexture, _Olocation, Color.White);
            }
        }
    }
}
