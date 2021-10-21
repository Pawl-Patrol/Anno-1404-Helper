namespace Production_Calculator
{
    public class Calculator
    {
        public static float[] CalculateNeeds(int[] population)
        {
            float[] needs = new float[(int)Needs.Length];

            needs[(int)Needs.Fish] =
                (float)population[(int)Population.Beggars] / 285 +
                (float)population[(int)Population.Peasants] / 200 +
                (float)population[(int)Population.Citizens] / 500 +
                (float)population[(int)Population.Patricians] / 909 +
                (float)population[(int)Population.Noblemen] / 1250;

            needs[(int)Needs.Spices] =
                (float)population[(int)Population.Citizens] / 500 +
                (float)population[(int)Population.Patricians] / 909 +
                (float)population[(int)Population.Noblemen] / 1250;

            needs[(int)Needs.Bread] =
                (float)population[(int)Population.Patricians] / 727 +
                (float)population[(int)Population.Noblemen] / 1025;

            needs[(int)Needs.Meat] =
                (float)population[(int)Population.Noblemen] / 1136;

            needs[(int)Needs.Cider] =
                (float)population[(int)Population.Beggars] / 500 +
                (float)population[(int)Population.Peasants] / 340 +
                (float)population[(int)Population.Citizens] / 340 +
                (float)population[(int)Population.Patricians] / 652 +
                (float)population[(int)Population.Noblemen] / 1153;

            needs[(int)Needs.Beer] =
                (population[(int)Population.Patricians] > 510 ?
                    ((float)population[(int)Population.Patricians] / 625) : 0) +
                (float)population[(int)Population.Noblemen] / 1071;

            needs[(int)Needs.Wine] =
                (population[(int)Population.Noblemen] > 1500 ?
                    ((float)population[(int)Population.Noblemen] / 1000) : 0);

            needs[(int)Needs.LinenGarments] =
                (float)population[(int)Population.Citizens] / 476 +
                (float)population[(int)Population.Patricians] / 1052 +
                (float)population[(int)Population.Noblemen] / 2500;

            needs[(int)Needs.LeatherJerkins] =
                (population[(int)Population.Patricians] > 690 ?
                    ((float)population[(int)Population.Patricians] / 1428) : 0) +
                (float)population[(int)Population.Noblemen] / 2500;

            needs[(int)Needs.FurCoats] =
                (population[(int)Population.Noblemen] > 950 ?
                    ((float)population[(int)Population.Noblemen] / 1562) : 0);

            needs[(int)Needs.BrocadeRobes] =
                (population[(int)Population.Noblemen] > 4000 ?
                    ((float)population[(int)Population.Noblemen] / 2112) : 0);

            needs[(int)Needs.Books] =
                (population[(int)Population.Patricians] > 940 ?
                    ((float)population[(int)Population.Patricians] / 1875) : 0) +
                (float)population[(int)Population.Noblemen] / 3333;

            needs[(int)Needs.Candlesticks] =
                population[(int)Population.Noblemen] > 3000 ?
                    (float)population[(int)Population.Patricians] / 2500 +
                    (float)population[(int)Population.Noblemen] / 3333
                    : 0;

            needs[(int)Needs.Glasses] =
                (population[(int)Population.Noblemen] > 2200 ?
                    ((float)population[(int)Population.Noblemen] / 1709) : 0);

            needs[(int)Needs.Dates] =
                (float)population[(int)Population.Nomads] / 450 +
                (float)population[(int)Population.Envoys] / 600;

            needs[(int)Needs.Milk] =
                (population[(int)Population.Nomads] > 145 ?
                    ((float)population[(int)Population.Nomads] / 436) : 0) +
                (float)population[(int)Population.Envoys] / 666;

            needs[(int)Needs.Carpets] =
                (population[(int)Population.Nomads] > 295 ?
                    ((float)population[(int)Population.Nomads] / 909) : 0) +
                (float)population[(int)Population.Envoys] / 1500;

            needs[(int)Needs.Coffee] =
                (float)population[(int)Population.Envoys] / 1000;

            needs[(int)Needs.PearlNecklaces] =
                (population[(int)Population.Envoys] > 1040 ?
                    ((float)population[(int)Population.Envoys] / 751) : 0);

            needs[(int)Needs.Parfum] =
                (population[(int)Population.Envoys] > 2600 ?
                    ((float)population[(int)Population.Envoys] / 1250) : 0);

            needs[(int)Needs.Marzipan] =
                (population[(int)Population.Envoys] > 4360 ?
                    ((float)population[(int)Population.Envoys] / 2453) : 0);

            return needs;
        }
    }
}