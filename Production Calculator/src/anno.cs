﻿


namespace Production_Calculator
{
    enum Population
    {
        Beggars,
        Peasants,
        Citizens,
        Patricians,
        Noblemen,
        Nomads,
        Envoys,
        Length
    }
    enum Needs
    {
        Fish,
        Cider,
        LinenGarments,
        Spices,
        Bread,
        Beer,
        LeatherJerkins,
        Books,
        Candlesticks,
        Meat,
        Wine,
        Glasses,
        FurCoats,
        BrocadeRobes,
        Dates,
        Milk,
        Carpets,
        Coffee,
        PearlNecklaces,
        Parfum,
        Marzipan,
        Length
    }
    public class Constants
    {
        static public long[] BaseAddresses = new long[4] { -1, 0x12d5a90, -1, 0x2097040 };
        static public long[] AddressOffsets = new long[4] { -1, 0x7cd8, -1, 0xea44 };
        static public long[] PopulationOffsets = new long[7] { 0, 160, 192, 224, 256, 64, 96};
    }
}