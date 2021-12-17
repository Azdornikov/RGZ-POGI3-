using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NUnit.Framework;


namespace RGZ_POGI3_
{
    [TestFixture]
    class CSoundTest
    {

        [TestCase]
        public void TestSound()
        {
            CSound zvuk = new CSound();
            var exception = Assert.Throws<Exception>(() => zvuk.play(@"C:\RGZ(POGI3)\Cards\0.png"));
            // сравнение полученного сообщения с ожидаемым
            Assert.That(exception.Message, Is.EqualTo("???"));
        }
    }
}
