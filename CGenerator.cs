using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGZ_POGI3_
{
    public class CGenerator
    {
        int[,] field;
        Random rnd = new Random();

        List<int> cards = new List<int>()
        {
            1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10,
            11, 11, 12, 12, 13, 13, 14, 14, 15, 15, 16, 16, 17, 17, 18, 18, 19, 19, 20, 20
        };

        public CGenerator(int weidth, int height)
        {
            field = new int[weidth, height];
        }

        public void generate()
        {

            int number;
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (cards.Count == 0) throw new Exception("Карточки исчерпаны");

                    number = rnd.Next(0, cards.Count);
                    field[i, j] = cards[number];

                    cards.RemoveAt(number);
                }
        }

        public int getcell(int i, int j, int weidth, int height)
        {
            if (i < weidth && j < height)
                return field[i, j];
            return 0;
        }
    }
}
