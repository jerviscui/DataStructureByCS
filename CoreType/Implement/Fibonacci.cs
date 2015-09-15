using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreType.Implement
{
    public class Fibonacci
    {
        private int _number;
        private int _depth;
        private int _lNum = 0;
        private int _rNum = 1;

        public Fibonacci(int num)
        {
            //create fibonacci number not less than the num
            _number = num;
            GetFib();
        }

        private void GetFib()
        {
            if (_number <= 0)
            {
                _depth = 1;
                _number = 0;
                return;
            }

            if (_number == 1)
            {
                _depth = 2;
                _rNum = _number + _lNum;
                return;
            }

            _depth = 2;
            while (_rNum < _number)
            {
                _rNum += _lNum;
                _lNum = _rNum - _lNum;
                _depth ++;
            }
            _number = _rNum;
            _rNum = _number + _lNum;
        }

        public int Get()
        {
           return _number;
        }

        public void Prev()
        {
            if (_number == 0)
            {
                return;
            }

            _depth --;
            _rNum = _number;
            _number = _lNum;
            _lNum = _rNum - _number;
        }

        public void Next()
        {
            _depth++;
            _lNum = _number;
            _rNum += _number;
            _number = _rNum - _lNum;
        }
    }
}
