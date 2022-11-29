using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AlcoholValues{
    public struct Alcohol
    {
        public float quantity;
        public int degrees;
        public string name;
    }

   //------------------------Botellas------------------------
    public static Alcohol Cerveza = new Alcohol() { quantity = 0.33f, degrees = 5, name = "cervesa" };
    public static Alcohol Sidra = new Alcohol() { quantity = 0.33f, degrees = 5, name = "sidra" };

    //------------------------Copa---------------------------
    public static Alcohol Vino = new Alcohol() { quantity = 0.1f, degrees = 12, name = "vi" };
    public static Alcohol Champagne = new Alcohol() { quantity = 0.1f, degrees = 12, name = "xampany" };
    public static Alcohol Vermut = new Alcohol() { quantity = 0.1f, degrees = 15, name = "vermut" };
    public static Alcohol Cognac = new Alcohol() { quantity = 0.1f, degrees = 40, name = "cognac" };
    public static Alcohol Patxaran = new Alcohol() { quantity = 0.1f, degrees = 25, name = "patxaran" };
    public static Alcohol Anis = new Alcohol() { quantity = 0.1f, degrees = 35, name = "anís" };

    //------------------------Combinado---------------------------
    public static Alcohol Ron = new Alcohol() { quantity = 0.05f, degrees = 38, name = "rom" };
    public static Alcohol Jager = new Alcohol() { quantity = 0.05f, degrees = 35, name = "jäger" };
    public static Alcohol Vodka = new Alcohol() { quantity = 0.05f, degrees = 40, name = "vodka" };
    public static Alcohol Ginebra = new Alcohol() { quantity = 0.05f, degrees = 42, name = "ginebra" };
    public static Alcohol Whisky = new Alcohol() { quantity = 0.05f, degrees = 40, name = "whisky" };

    //------------------------Chupito---------------------------
    public static Alcohol Tequila = new Alcohol() { quantity = 0.03f, degrees = 43, name = "tequila" };
    public static Alcohol CremaLicor = new Alcohol() { quantity = 0.03f, degrees = 38, name = "crema de licor" };
    public static Alcohol OrujoHierbas = new Alcohol() { quantity = 0.03f, degrees = 30, name = "orujo d'herbes" };
    public static Alcohol OrujoBlanco = new Alcohol() { quantity = 0.03f, degrees = 45, name = "orujo blanc" };
    public static Alcohol Ratafia = new Alcohol() { quantity = 0.03f, degrees = 21, name = "ratafia" };
    public static Alcohol Aguardiente = new Alcohol() { quantity = 0.03f, degrees = 38, name = "aiguardent" };
    public static Alcohol Limoncello = new Alcohol() { quantity = 0.03f, degrees = 30, name = "limoncello" };
    public static Alcohol Absenta = new Alcohol() { quantity = 0.03f, degrees = 70, name = "absenta" };

    private static bool[] utilitzat = { true, false, true, false, false, false, false, false, true, false, false, false, false, true, false, false, false, false, false, false, false };
    private static Alcohol[] alcohols = { Cerveza, Sidra, Vino, Champagne, Vermut, Cognac, Patxaran, Anis, Ron, Jager, Vodka, Ginebra, Whisky, Tequila, CremaLicor, OrujoHierbas, OrujoBlanco, Ratafia, Aguardiente, Limoncello, Absenta };
    public static Alcohol GetRandomAlcohol()
    {
        Alcohol res;
        int i = 0;
        do
        {
            i = Random.Range(0, alcohols.Length);
        } while (utilitzat[i]);
        res = alcohols[i];
        utilitzat[i] = true;
        return res;
    }
}
