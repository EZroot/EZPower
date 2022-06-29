using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower.Core
{
    [Serializable]
    public class BufferMeDaddy
    {
        int _width;
        int _height;

        public void SetConsoleBuffer(int x, int y)
        {
            _width = x;
            _height = y;
        }

        void UpdateBuffer()
        {
            
        }
    }
}
