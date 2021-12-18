using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RGZ_POGI3_
{
    [TestFixture]
    class SGeneratorTest
    {
        [TestCase]
        public void TestGenerator()
        {
            CGenerator c = new CGenerator(8, 8); // задаём вариант ошибки

            // получение исключения
            var exception = Assert.Throws<Exception>(() => c.generate());
            // сравнение полученного сообщения с ожидаемым
            Assert.That(exception.Message, Is.EqualTo("Карточки закончились"));

        }
    }
}
