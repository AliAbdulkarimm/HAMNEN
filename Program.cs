using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;


namespace TuffUppgift
{
    class Program
    {
        //inkommande båtar
        static List<Båt> Båtar = new List<Båt>();
        
        //array för båtarna i hamnen
        static List<Båt>[] Hamnen = new List<Båt>[64];

        //Räknar dagarna parkerad/är i hamnen

        static int DagarIhamnen = 0;
        static int Av = 0;

    
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            //färgen till starter sektionen
            Console.ForegroundColor = ConsoleColor.Red;
           
            //Funktionen för alla symboler i consolen 
            Console.OutputEncoding = Encoding.Default;

            //Genererar listan i arrayen hamnen
            SkaparListanIArrayen(Hamnen);

            Console.WriteLine("Tryck Enter för att starta DAG 1 i hamnen ");
            Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            Console.ResetColor();

            bool loop = true;

            while (loop)
            {
                
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    //Funktionen för Enter knappen
                    case ConsoleKey.Enter:
                        //Lägger till dagar i hamnen 
                        DagarIhamnen++;
                        
                        //Skapar ny kommande båtar i hamnen 
                        GenererarBåtarna(Båtar);

                        //Räknar hur många dagar det är i hamnen
                        HamnDagar(Hamnen);

                        // Parkerade båtar i hamnen 
                        ParkeradeBåtarIhamnen(Hamnen, Båtar);

                        //Visar båtarna                        
                        VisarBåtarna(Hamnen);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"►►►►►►►►►►►►►►► Dag{DagarIhamnen} i hamnen ◄◄◄◄◄◄◄◄◄◄◄◄◄◄◄");
                        Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
                        Console.ResetColor();
                        break;
                       

                }
            }

           

        }

        public static void VisarBåtarna(List<Båt>[] hamnen)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Plats \t Båttyp \t ID \t Vikt \t MaxHastighet \t Unika egenskaper");
            Console.WriteLine("▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬");
            Console.ResetColor();
            for (int i = 0; i < hamnen.Length; i++)
            {
                //Funktionen ifall det är en tomt plats i hamnen**
                if (hamnen[i].Count == 0)
                {
                    Console.WriteLine($"{i}\t Tomt");
                }
                else
                {
                    //annars körs en foreach för varje båt**
                    foreach (Båt båtar in hamnen[i])
                    {
                        if (båtar is RoddBåtar)
                        {
                            Console.WriteLine($"{i}\t {båtar.BåtTyp} \t {båtar.IDnummer} \t {båtar.Vikt} \t {båtar.MaxHastighet} km/h \t {(((RoddBåtar)båtar).Passagerare)}\tPassagerare");
                        }
                        else if (båtar is MotorBåt)
                        {
                            Console.WriteLine($"{i}\t {båtar.BåtTyp} \t {båtar.IDnummer} \t {båtar.Vikt} \t {båtar.MaxHastighet} km/h \t {(((MotorBåt)båtar).HästKrafter)}\tHästkrafter");
                        }
                        else if (båtar is SegelBåtar)
                        {
                            Console.WriteLine($"{i}\t{båtar.BåtTyp} \t {båtar.IDnummer} \t {båtar.Vikt} \t {båtar.MaxHastighet} km/h \t {(((SegelBåtar)båtar).BåtLängd)} \t BåtLängd");
                        }
                        else if (båtar is Lagerfartyg)
                        {
                            Console.WriteLine($"{i}\t{båtar.BåtTyp} \t {båtar.IDnummer} \t {båtar.Vikt} \t {båtar.MaxHastighet} km/h \t {(((Lagerfartyg)båtar).containers)}\tContainer");
                        }

                    }
                }


            }
        }

        private static void ParkeradeBåtarIhamnen(List<Båt>[] hamnen, List<Båt> båtar)
        {
            //sorterar listan 
            List<Båt> SorteradeBåtar = båtar.OrderByDescending(b => b.BåtLängd).ToList();

            //för varje båt i listan, försök att placera den i hamnen, börjar med största båten först
            foreach (Båt b in SorteradeBåtar)
            {

                //metoden returnerar en bool om den fick lägga till eller inte
                if (PlacerarBåtariHamnen(hamnen, b))
                if (PlacerarBåtariHamnen(hamnen, b))
                {
                    //Båtar i hamnen
                }
                else
                {
                    //Avvisa båt
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Avvisad båt" + b.IDnummer + "\t" + b.BåtTyp);
                    Av++;
                    Console.ResetColor();
                }
            }
        }

        private static bool PlacerarBåtariHamnen(List<Båt>[] hamnen, Båt båtar)
        {
            for (int i = 0; i < hamnen.Length; i++)
            {
                if (båtar is RoddBåtar && hamnen[i].Count == 1 && hamnen[i].First() is RoddBåtar)
                {

                    hamnen[i].Add(båtar);
                    return true;

                }

                
                //vill tillägga att den här delen hade jag svårt för
                //Om platsen är tom och listan tar slut
                if (hamnen[i].Count == 0 && båtar.BåtLängd + i < hamnen.Length)
                {

                    int startIndex = i;
                    //Närliggande lediga platser
                    int numOfAdjacent = 0;
                    //Kollar om de nästkommande platserna också är tomma, t.o.m. båtens storlek
                    for (int j = startIndex; j < startIndex + båtar.BåtLängd; j++)
                    {
                        //om dom är det adderar vi
                        if (hamnen[j].Count == 0)
                        {
                            numOfAdjacent++;
                        }
                    }
                    //Om alla nästkommande platser tom båtens storlek är tomma kan får båten plats, så vi lägger till.
                    if (numOfAdjacent == båtar.BåtLängd)
                    {

                        //samma loop som förut, men nu vet vi att alla platser är tomma så då lägger vi till båten på dessa platser/index. 
                        for (int j = startIndex; j < startIndex + båtar.BåtLängd; j++)
                        {
                            hamnen[j].Add(båtar);

                        }
                        //Vi kunde lägga till båten --> true
                        return true;

                    }
                   
                    
                }

            }
            //fall vi kör igenom hela for-loopen och inget händer får vi avvisa båten
            return false;

        }


        //Checkar dagarna på båtarna 

        private static void HamnDagar(List<Båt>[] hamnen)
        {
            for (int i = 0; i < hamnen.Length; i++)
            {
                if (hamnen[i].Count == 0)
                {
                    continue;
                }
                else
                {
                    if (hamnen[i + 1].Count == 0 || i == hamnen.Length - 1)
                    {
                        hamnen[i].First().DockingsDagar--;
                    }
                    else if (hamnen[i].First().IDnummer != hamnen[i + 1].First().IDnummer)
                    {
                        hamnen[i].First().DockingsDagar--;
                    }
                }
            }
            for (int i = 0; i < hamnen.Length; i++)
            {
                if (hamnen[i].Count == 0)
                {
                    continue;
                }
                else
                {
                    if (hamnen[i].First().DockingsDagar == 0)
                    {
                        hamnen[i].Clear();
                    }
                }
            }
        }


        //Genererar listan för varje index i arrayen**
        private static void SkaparListanIArrayen(List<Båt>[] Hamnen)
        {
            for (int i = 0; i < Hamnen.Length; i++)
            {
                Hamnen[i] = new List<Båt>();
            }
        }


        // Genererar random båtar som är inkommande till hamnen//skaoar båtarna
        public static void GenererarBåtarna(List<Båt> boats)
        {
            //int för  mängd båtar per dag**
            int mängd = 5;
            for (int i = 0; i < mängd; i++)
            {
                int rndBÅTAR = rnd.Next(1, 5);

                switch (rndBÅTAR)
                {
                    case 1:
                        RoddBåtar Roddbåt = new RoddBåtar();
                        Roddbåt.Vikt = rnd.Next(100, 300);
                        Roddbåt.MaxHastighet = rnd.Next(1, 3);
                        Roddbåt.Passagerare = rnd.Next(1, 6);
                        Roddbåt.BåtTyp = "RoddBåt";
                        Roddbåt.DockingsDagar = 1;
                        Roddbåt.BåtLängd = 1;
                        string roddBåtID = GenereraID("R-");
                        Roddbåt.IDnummer = roddBåtID;
                        boats.Add(Roddbåt);
                        break;
                    case 2:
                        MotorBåt Motorbåt = new MotorBåt();
                        Motorbåt.Vikt = rnd.Next(200, 3000);
                        Motorbåt.MaxHastighet = rnd.Next(1, 60);
                        Motorbåt.HästKrafter = rnd.Next(10, 1000);
                        Motorbåt.BåtTyp = "Motorbåt";
                        Motorbåt.DockingsDagar = 3;
                        Motorbåt.BåtLängd = 1;
                        string MotorBåtID = GenereraID("M-");
                        Motorbåt.IDnummer = MotorBåtID;
                        boats.Add(Motorbåt);
                        break;
                    case 3:
                        SegelBåtar Segelbåt = new SegelBåtar();
                        Segelbåt.Vikt = rnd.Next(800, 6000);
                        Segelbåt.MaxHastighet = rnd.Next(1, 12);
                        Segelbåt.BåtLängd = rnd.Next(10, 60);
                        Segelbåt.BåtTyp = "Segelbåt";
                        Segelbåt.DockingsDagar = 4;
                        Segelbåt.BåtLängd = 2;
                        string SegelBåtID = GenereraID("S-");
                        Segelbåt.IDnummer = SegelBåtID;
                        boats.Add(Segelbåt);
                        break;
                    case 4:
                        Lagerfartyg Lastfartyg = new Lagerfartyg();
                        Lastfartyg.Vikt = rnd.Next(3000, 20000);
                        Lastfartyg.MaxHastighet = rnd.Next(1, 20);
                        Lastfartyg.containers = rnd.Next(0, 500);
                        Lastfartyg.BåtTyp = "Lastfartyg";
                        Lastfartyg.DockingsDagar = 6;
                        Lastfartyg.BåtLängd = 4;
                        string cboatID = GenereraID("L-");
                        Lastfartyg.IDnummer = cboatID;
                        boats.Add(Lastfartyg);
                        break;

                    default:
                        break;
                }
            }
        }


        // Genererar unika egenskaperna för båtarna ** 
        public static string GenereraID(string z)
        {
            for (int i = 0; i < 3; i++)
            {
                char randomChar = (char)rnd.Next('A', 'Z');
                z += randomChar;
            }
            return z;
        }


    }
}
