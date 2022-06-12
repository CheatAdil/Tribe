using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Names_vil : MonoBehaviour
{
    private static string firstNames;
    private static string lastNames;
    private static string[] fn, ln;

    public static string NewName() 
    {
        int fc = fn.Length;
        int lc = ln.Length;
        string name = $"{fn[Random.Range(0, fc)]} {ln[Random.Range(0, lc)]}";
        return name;
    }
    private void Awake()
    {
        firstNames = "Jeremy," +
            "Jacob," +
            "Christopher," +
            "Mattew," +
            "Julia," +
             "Tommy," + 
"Kamora," +
"Marin," +
"Desirae," +
"Kiana," +
"Nevin," +
"Julian," +
"Vance," +
"Cory," +
"Jeremiah," +
"Yazmin," +
"Naima," +
"Javion," +
"Angelina," +
"Kamila," +
"Trey," +
"Saanvi," +
"Ava," +
"Dakari," +
"Elise," +
"Jaylen," +
"Triton," +
"Ari," +
"Chaim," +
"Priya," +
"Jailyn," +
"Noelani," +
"Rory," +
"Declan," +
"Sutton," +
"Ariyanna," +
"Alia," +
"Maycee," +
"Aliah," +
"Hafsa," +
"Ashwin," +
"Jacklyn," +
"Judson," +
"Barron," +
"Westin," +
"Jamere," +
"Maria," +
"Bruno," +
"Wilder," +
"Emil," +
"Gizelle," +
"Kaylin," +
"Lacy," +
"Lucy," +
"John," +
"George," +
"Ringo," +
"Paul," +
"Eleanor," +
"Malakhi,"; 



         lastNames = "Soveb," +
            "Beaver," +
            "Soul," +
            "Slow," +
            "Grast" +
            "Middleton" +
"Mcmanus," +
"Vargas," +
"Monroe," +
"Callahan," +
"Hudson," +
"Platt," +
"Lange," +
"Fowler," +
"Mayo," +
"Blackmon," +
"Wong," +
"Wills," +
"Valdez," +
"Prather," +
"Bledsoe," +
"Beltran," +
"Meeks," +
"Neal," +
"Hines," +
"Oconnor," +
"Fuller," +
"Blackburn," +
"Baca," +
"Clark," +
"Oconnell," +
"Porter," +
"Hill," +
"Gibson," +
"Dixon," +
"Stone," +
"Walter," +
"Sanderson," +
"Putnam," +
"Pham," +
"Corbin," +
"Odell," +
"Cordova," +
"Gross," +
"West," +
"Key," +
"Nava," +
"Mata," +
"Tidwell," +
"Duncan," +
"Spetim," +
"Jean," +
"Burks," +
"Vega," +
"In the sky with diamonds," +
"Lennon," +
"Harrison," +
"Mccartney," +
"Starr," +
"Rigby,";

        fn = firstNames.Split(',');
        ln = lastNames.Split(',');
    }
    

}
