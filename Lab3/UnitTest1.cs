using System;
using Xunit;

namespace IIG.BinaryFlag
{
    public class BinaryFLagWhiteBoxTest: MultipleBinaryFlag
    {
        public BinaryFLagWhiteBoxTest(ulong length, bool initialValue = true) : base(length, initialValue)
        {
        }

        [Theory]
        [InlineData(500, true, 300, false)]
        [InlineData(700, false, 500, false)]
        [InlineData(900, true, 700, false)]
        [InlineData(1100, false, 900, false)]
        [InlineData(17179868704, false, 91255600, false)]
        public void NormalExecutionFlow(ulong l, bool v, ulong position, bool expected)
        { 
            var mbf = new MultipleBinaryFlag(l, v);
            Assert.Equal(mbf.GetFlag(), v);
            mbf.SetFlag(position);
            Assert.Equal(mbf.GetFlag(), v);
            mbf.ResetFlag(position);
            Assert.Equal(mbf.GetFlag(), expected);
            mbf.SetFlag(position);
            Assert.Equal(mbf.GetFlag(), v);
        }
        [Theory]
        [InlineData(10, false, 5, "FFFFFFFTTF")]
        [InlineData(15, true, 8, "TTTTTTTTFFTTTTT")]
        [InlineData(20, false, 12, "FFFFFFFFFFFFFFTTFFFF")]
        public void NormalStringExecutionFlow(ulong l, bool v, ulong position, string expectedStr)
        { 
            var mbf = new MultipleBinaryFlag(l, v);
            Assert.Equal(mbf.GetFlag(), v);
            mbf.SetFlag(position+1);
            mbf.SetFlag(position+2);
            mbf.SetFlag(position + 3);
            mbf.ResetFlag(position+1);
            mbf.ResetFlag(position);
            Assert.Equal(mbf.ToString(), expectedStr);
        }
        [Theory]
        [InlineData(171798687125215321)]
        [InlineData(1513215132513)]
        [InlineData(17179868705)]
        [InlineData(0)]
        [InlineData(1)]
        public void Error_InitExecutionFlow(ulong l)
        { 
            Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(l));
        }
        
        [Theory]
        [InlineData(215, 215)]
        [InlineData(1251, 2156)]
        [InlineData(2156362, 12512656)]
        public void Error_Set_Reset_ExecutionFlow(ulong l, ulong position)
        { 
            var mbf = new MultipleBinaryFlag(l);
            Assert.Throws<ArgumentOutOfRangeException>(() => mbf.SetFlag(position));
            Assert.Throws<ArgumentOutOfRangeException>(() => mbf.ResetFlag(position));
        }
    }
}