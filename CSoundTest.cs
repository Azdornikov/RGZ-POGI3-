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
            CSound s = new CSound();
            var exception = Assert.Throws<Exception>(() => s.play(@"C:\RGZ(POGI3)\Cards\0.png")); //задаём вариант ошибки с неправильным расширением
            // сравнение полученного сообщения с ожидаемым
            Assert.That(exception.Message, Is.EqualTo("музыку в студию"));
        }
    }
}
