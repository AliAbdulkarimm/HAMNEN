using System;
using System.Collections.Generic;
using System.Text;

namespace TuffUppgift
{
    class Båt
    {

        public string IDnummer { get; set; }
        public int Vikt { get; set; }
        public int MaxHastighet { get; set; }
        public int DockingsDagar { get; set; }
        public string BåtTyp { get; set; }
        public double BåtLängd { get; set; }
    }

    class RoddBåtar : Båt
    {

        public int Passagerare { get; set; }

    }

    class MotorBåt : Båt
    {

        public int HästKrafter { get; set; }
    }

    class SegelBåtar : Båt
    {

        public int Båtlängd { get; set; }
    }

    class Lagerfartyg : Båt
    {

        public int containers { get; set; }
    }
}
