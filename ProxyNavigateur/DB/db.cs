using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProxyNavigateur.Models;
using System.Data.SQLite;
using System.IO;
using System.Data;
using Dapper;

namespace ProxyNavigateur.DB
{
    public class db : BDDInterface
    {

        private string BdNom;
        private string sqliteInfo = string.Empty;

        public db() { }

        public db(string nomBase) {
            BdNom = nomBase; //"BDD.sqlite"
            sqliteInfo = "Data Source=" + BdNom + ";Version=3;";
        }

        public void creationTables()
        {

           // BdNom = nomBase; //"BDD.sqlite"

            if (!File.Exists(BdNom))
            {
                SQLiteConnection.CreateFile(BdNom);

                using (SQLiteConnection connexion = new SQLiteConnection(sqliteInfo))
                {

                    if (connexion.State == ConnectionState.Closed)
                    {
                        connexion.Open();
                    }

                    try
                    {
                        connexion.Execute(@"
                        CREATE TABLE IF NOT EXISTS ListeTheme
                        (theme VARCHAR(25) PRIMARY KEY)
                        ");

                        connexion.Execute(@"
                        CREATE TABLE IF NOT EXISTS Listes
                        (liste VARCHAR(25) PRIMARY KEY)
                        ");

                        connexion.Execute(@"
                        CREATE TABLE IF NOT EXISTS Synchronisation
                        (date DATETIME PRIMARY KEY)
                        ");

                        connexion.Execute(@"
                        CREATE TABLE IF NOT EXISTS Topologie (
                        idMachine VARCHAR(25) PRIMARY KEY,
                        fk_Date DATETIME,
                        FOREIGN KEY (fk_Date) REFERENCES Synchronisation(dateEntree)
                        )");

                        connexion.Execute(@"
                        CREATE TABLE IF NOT EXISTS Sites (
                        nomSite VARCHAR(25) PRIMARY KEY,
                        dateAjout DATETIME,
                        fk_Date DATETIME,
                        fk_Theme VARCHAR(25),
                        fk_liste VARCHAR(25),
                        FOREIGN KEY (fk_Date) REFERENCES Synchronisation(dateEntree),
                        FOREIGN KEY (fk_Theme) REFERENCES ListeTheme(theme),
                        FOREIGN KEY (fk_liste) REFERENCES Liste(liste)
                        )");

                        connexion.Execute(@"
                        CREATE TABLE IF NOT EXISTS ListeDynamique (
                        url VARCHAR(25) PRIMARY KEY,
                        dateAjout DATETIME,
                        fk_Date DATETIME,
                        fk_Theme VARCHAR(25),
                        FOREIGN KEY (fk_Date) REFERENCES Synchronisation(dateEntree),
                        FOREIGN KEY (fk_Theme) REFERENCES ListeTheme(theme)
                        )");

                        connexion.Execute(@"
                        CREATE TABLE IF NOT EXISTS MotCle (
                        mot VARCHAR(25) PRIMARY KEY,
                        valeur INTEGER,
                        dateAjout DATETIME,
                        fk_Date DATETIME,
                        fk_Theme VARCHAR(25),
                        FOREIGN KEY (fk_Date) REFERENCES Synchronisation(dateEntree),
                        FOREIGN KEY (fk_Theme) REFERENCES ListeTheme(theme)
                        )");

                        connexion.Execute(@"
                        CREATE TABLE IF NOT EXISTS Synonyme (
                        mot VARCHAR(25) PRIMARY KEY,
                        dateAjout DATETIME,
                        fk_Date DATETIME,
                        fk_trad VARCHAR(25),
                        FOREIGN KEY (fk_Date) REFERENCES Synchronisation(dateEntree),
                        FOREIGN KEY (fk_trad) REFERENCES MotCle(mot)
                        )");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    //tableSeed();
                }
            }
        }

        public void suppressionDB()
        {
            if (File.Exists(BdNom))
            {
                try
                {
                    File.Delete(BdNom);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void seeder()
        {
            TableSeed ts = new TableSeed(this);
            ts.seeder();
        }

        //méthodes sut la table Synchronisation

        public void SetSynchro(Synchronisation synch)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    try
                    {
                        connexion.Open();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    
                }
                try
                {
                    string msg = "INSERT INTO Synchronisation (date) VALUES ('" + synch.date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";
                    connexion.Execute(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public Synchronisation GetSynchro(DateTime date)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                string msg = "SELECT * FROM Synchronisation where date ='" + date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";
                Synchronisation sync;
                try
                {
                    sync = connexion.Query<Synchronisation>(msg).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return sync;
            }
        }

        public IEnumerable<Synchronisation> GetSynchros()
        {

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    return connexion.Query<Synchronisation>("SELECT * FROM Synchronisation");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        //méthodes sut la table Listes

        public void SetListe(Listes l)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string msg = "INSERT INTO Listes (liste) VALUES ('" + l.liste + "')";
                    connexion.Execute(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public Listes GetListes(string ms)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                string msg = "SELECT * FROM Listes where liste ='" + ms + "'";
                Listes l;
                try
                {
                    l = connexion.Query<Listes>(msg).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return l;
            }
        }

        public IEnumerable<Listes> GetListes()
        {

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    return connexion.Query<Listes>("SELECT * FROM Listes");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        //méthodes sut la table Theme

        public void SetListeTheme(ListeTheme theme)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    try
                    {
                        connexion.Open();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                }
                try
                {
                    string msg = "INSERT INTO ListeTheme (theme) VALUES ('" + theme.theme + "')";
                    connexion.Execute(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public ListeTheme GetTheme(string ms)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                string msg = "SELECT * FROM ListeTheme where theme ='" + ms + "'";
                ListeTheme t;
                try
                {
                    t = connexion.Query<ListeTheme>(msg).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return t;
            }
        }

        public IEnumerable<ListeTheme> GetListeThemes()
        {

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    return connexion.Query<ListeTheme>("SELECT * FROM ListeTheme");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        //méthodes sut la table Topologie

        public void SetTopologie(Topologie topo)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string msg = "INSERT INTO Topologie (idMachine,fk_Date) VALUES ('" + topo.idMachine + "','" + topo.fk_Date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";
                    connexion.Execute(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public Topologie GetTopo(string ms)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                string msg = "SELECT * FROM Topologie where idMachine ='" + ms + "'";
                Topologie topo;
                try
                {
                    topo = connexion.Query<Topologie>(msg).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return topo;
            }
        }

        public IEnumerable<Topologie> GetTopos()
        {

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    return connexion.Query<Topologie>("SELECT * FROM Topologie");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        //méthodes sut la table Listes Dynamique

        public void SetListeDynamique(ListeDynamique dyn)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string msg = "INSERT INTO ListeDynamique (url,DateAjout,fk_Date,fk_theme) VALUES ('" + dyn.url + "','" + dyn.DateAjout.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + dyn.fk_Date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + dyn.fk_theme + "')";
                    connexion.Execute(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public ListeDynamique GetListeDynamique(string ms)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                string msg = "SELECT * FROM ListeDynamique where url ='" + ms + "'";
                ListeDynamique dyn;
                try
                {
                    dyn = connexion.Query<ListeDynamique>(msg).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return dyn;
            }
        }

        public IEnumerable<ListeDynamique> GetListeDynamiques()
        {

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    return connexion.Query<ListeDynamique>("SELECT * FROM ListeDynamique");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public IEnumerable<ListeDynamique> GetListeDynamiques(string nomTheme)
        {

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string sqlQuery = "SELECT * FROM ListeDynamique join ListeTheme on ListeTheme.Theme = ListeDynamique.fk_theme where ListeTheme.Theme!='"+ nomTheme +"'";
                    var valMot = connexion.Query<ListeDynamique, ListeTheme, ListeDynamique>(sqlQuery, (dyn, theme) =>
                    {
                        dyn.theme = theme;
                        return dyn;
                    }, splitOn: "Theme").ToList();

                    return valMot;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        //méthodes sut la table Sites

        public void SetSites(Sites site)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string msg = "INSERT INTO Sites (nomSite,DateAjout,fk_Date,fk_theme,fk_liste) VALUES ('" + site.nomSite + "','" + site.DateAjout.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + site.fk_Date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + site.fk_theme + "','" + site.fk_liste + "')";
                    connexion.Execute(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public Sites GetSite(string ms)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                string msg = "SELECT * FROM Sites where nomSite ='" + ms + "'";
                Sites s;
                try
                {
                    s = connexion.Query<Sites>(msg).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return s;
            }
        }

        public IEnumerable<Sites> GetSites()
        {

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    IEnumerable<Sites> testListe = connexion.Query<Sites>("SELECT * FROM Sites");
                    return testListe;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        //méthodes sut la table Mot-cle

        public void SetMotCle(MotCle mc)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string msg = "INSERT INTO MotCle (mot,valeur,DateAjout,fk_Date,fk_theme) VALUES ('" + mc.mot + "','" + mc.valeur + "','" + mc.DateAjout.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + mc.fk_Date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + mc.fk_theme + "')";
                    connexion.Execute(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public MotCle GetMotCle(string ms)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                string msg = "SELECT * FROM MotCle where mot ='" + ms + "'";
                MotCle mc;
                try
                {
                    mc = connexion.Query<MotCle>(msg).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return mc;
            }
        }

        public IEnumerable<MotCle> GetMotCles()
        {

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    return connexion.Query<MotCle>("SELECT * FROM MotCle");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        //méthodes sut la table Synonyme

        public void SetSynonyme(Synonyme syn)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string msg = "INSERT INTO Synonyme (mot,DateAjout,fk_Date,fk_trad) VALUES ('" + syn.mot + "','" + syn.DateAjout.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + syn.fk_Date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + syn.fk_trad + "')";
                    connexion.Execute(msg);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }

        public Synonyme GetSynonyme(string ms)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                string msg = "SELECT * FROM Synonyme where mot ='" + ms + "'";
                Synonyme syn;
                try
                {
                    syn = connexion.Query<Synonyme>(msg).First();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return syn;
            }
        }

        public IEnumerable<Synonyme> GetSynonymes()
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    return connexion.Query<Synonyme>("SELECT * FROM Synonyme");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        //vérif valeur

        public bool verifSite(string msg) //à changer
        {
            bool test = false;
            if (!(String.IsNullOrEmpty(msg)))
            {
                using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
                {
                    if (connexion.State == ConnectionState.Closed)
                    {
                        connexion.Open();
                    }
                    try
                    {
                        dynamic resultat = connexion.Query("SELECT COUNT(nomSite) AS Count FROM Sites WHERE nomSite='" + msg + "'").Single();
                        if (resultat.Count > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return test;
        }

        public IEnumerable<Sites> getURL(string listeURL)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }

                try
                {
                    //string sqlQuery = @"SELECT * FROM Sites INNER JOIN Listes ON Sites.fk_theme=Listes.liste WHERE Listes.liste = '" + listeURL + "'";

                    string sqlQuery = @"SELECT * FROM Sites INNER JOIN Listes ON Sites.fk_liste=Listes.liste WHERE Listes.liste = '"+ listeURL + "'";

                    var valMot = connexion.Query<Sites, Listes, Sites>(sqlQuery, (sites, listes) =>
                    {
                        sites.Listes = listes;
                        return sites;
                    }, splitOn: "liste").ToList();

                    return valMot;
                }
                catch
                {
                    //Console.WriteLine(e.Message);
                    //throw new Exception("Invalide liste" + e.Message);
                    return null;
                }
            }
        }

        public bool checkPartWord(string phrase)
        {
            bool verif = false;

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string sqlQuery = "SELECT * FROM MotCle LEFT OUTER JOIN Synonyme ON MotCle.mot=Synonyme.fk_trad";
                    var valMot = connexion.Query<MotCle, Synonyme, MotCle>(sqlQuery, (motCle, syn) =>
                    {
                        motCle.Synonyme = syn;
                        return motCle;
                    }, splitOn: "mot").ToList();

                    foreach (MotCle m in valMot)
                    {
                        if (m.Synonyme == null)
                        {
                            if (phrase.Contains(m.mot))
                            {
                                verif = true;
                                break;
                            }
                        }
                        else
                        {
                            if (phrase.Contains(m.mot) || phrase.Contains(m.Synonyme.mot))
                            {
                                verif = true;
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return verif;
        }

        //retourne valeur

        public int retourVal(string mot)
        {
            int val = 0;

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string sqlQuery = "SELECT * FROM MotCle LEFT OUTER JOIN Synonyme ON MotCle.mot=Synonyme.fk_trad WHERE MotCle.mot='" + mot + "' OR Synonyme.mot='" + mot + "'";
                    var valMot = connexion.Query<MotCle, Synonyme, MotCle>(sqlQuery, (motCle, syn) =>
                    {
                        motCle.Synonyme = syn;
                        return motCle;
                    }, splitOn: "mot").ToList();

                    foreach (MotCle m in valMot)
                    {
                        val = m.valeur;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return val;
        }

        public string returnTheme(string word)
        {
            string returnWord = string.Empty;

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string sqlQuery = "SELECT * FROM MotCle LEFT OUTER JOIN Synonyme ON MotCle.mot=Synonyme.fk_trad WHERE MotCle.mot='" + word + "' OR Synonyme.mot='" + word + "'";
                    var valMot = connexion.Query<MotCle, Synonyme, MotCle>(sqlQuery, (motCle, syn) =>
                    {
                        motCle.Synonyme = syn;
                        return motCle;
                    }, splitOn: "mot").ToList();

                    foreach (MotCle m in valMot)
                    {
                        returnWord = m.fk_theme;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //returnWord = "";
                }
            }
            return returnWord;
        }

        public Dictionary<string,int> motsInterdit()
        {
            Dictionary<string, int> dictMots=new Dictionary<string, int>();

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string sqlQuery = "SELECT DISTINCT * FROM MotCle LEFT OUTER JOIN Synonyme ON MotCle.mot=Synonyme.fk_trad";
                    var valMot = connexion.Query<MotCle, Synonyme, MotCle>(sqlQuery, (motCle, syn) =>
                    {
                        motCle.Synonyme = syn;
                        return motCle;
                    }, splitOn: "mot").ToList();

                    foreach (MotCle m in valMot)
                    {
                        if (m.fk_theme!= "Approprie")
                        {
                            if (m.Synonyme!=null)
                            {
                                if (!dictMots.ContainsKey(m.Synonyme.mot)) {
                                    dictMots.Add(m.Synonyme.mot, 0);
                                }
                                if (!dictMots.ContainsKey(m.mot))
                                {
                                    dictMots.Add(m.mot, 0);
                                }
                            }
                            else
                            {
                                if (!dictMots.ContainsKey(m.mot))
                                {
                                    dictMots.Add(m.mot, 0);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return dictMots;
        }

        public Dictionary<string,int> motsInterditVal()
        {
            Dictionary<string, int> dictMotsVal = new Dictionary<string, int>();

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string sqlQuery = "SELECT * FROM MotCle LEFT OUTER JOIN Synonyme ON MotCle.mot=Synonyme.fk_trad";
                    var valMot = connexion.Query<MotCle, Synonyme, MotCle>(sqlQuery, (motCle, syn) =>
                    {
                        motCle.Synonyme = syn;
                        return motCle;
                    }, splitOn: "mot").ToList();

                    foreach (MotCle m in valMot)
                    {
                        if (m.fk_theme != "Approprie")
                        {
                            if (m.Synonyme != null)
                            {
                                if (!dictMotsVal.ContainsKey(m.Synonyme.mot))
                                {
                                    dictMotsVal.Add(m.Synonyme.mot, m.valeur);
                                }

                                if (!dictMotsVal.ContainsKey(m.mot))
                                {
                                    dictMotsVal.Add(m.mot, m.valeur);
                                }
                            }
                            else
                            {
                                if (!dictMotsVal.ContainsKey(m.mot))
                                {
                                    dictMotsVal.Add(m.mot, m.valeur);
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return dictMotsVal;
        }

        public Dictionary<string,string> retourTheme()
        {
            Dictionary<string, string> dictMotTheme = new Dictionary<string, string>();

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    string sqlQuery = "SELECT * FROM MotCle LEFT OUTER JOIN Synonyme ON MotCle.mot=Synonyme.fk_trad";
                    var valMot = connexion.Query<MotCle, Synonyme, MotCle>(sqlQuery, (motCle, syn) =>
                    {
                        motCle.Synonyme = syn;
                        return motCle;
                    }, splitOn: "mot").ToList();

                    foreach (MotCle m in valMot)
                    {
                        if (m.fk_theme != "Approprie")
                        {
                            if (m.Synonyme != null)
                            {
                                if (!dictMotTheme.ContainsKey(m.Synonyme.mot))
                                {
                                    dictMotTheme.Add(m.Synonyme.mot, m.fk_theme);
                                }

                                if (!dictMotTheme.ContainsKey(m.mot))
                                {
                                    dictMotTheme.Add(m.mot, m.fk_theme);
                                }
                            }
                            else
                            {
                                if (!dictMotTheme.ContainsKey(m.mot))
                                {
                                    dictMotTheme.Add(m.mot, m.fk_theme);
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return dictMotTheme;
        }

        public Dictionary<string,int> themeVal()
        {
            Dictionary<string, int> retourThemeVal = new Dictionary<string, int>();

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    List<ListeTheme> themes= connexion.Query<ListeTheme>("SELECT * FROM ListeTheme").ToList();

                    foreach (ListeTheme l in themes)
                    {
                        if (!retourThemeVal.ContainsKey(l.theme))
                        {
                            retourThemeVal.Add(l.theme,0);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return retourThemeVal;
        }

        public Dictionary<string, bool> retourListeSites()
        {
            Dictionary<string, bool> retourSites = new Dictionary<string, bool>();

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    List<Sites> siteListes = connexion.Query<Sites>("SELECT * FROM Sites").ToList();

                    foreach (Sites siteEnCours in siteListes)
                    {
                        if (!retourSites.ContainsKey(siteEnCours.nomSite) && siteEnCours.fk_liste.Equals("Liste Verte"))
                        {
                            retourSites.Add(siteEnCours.nomSite, true);
                        }
                        else if (!retourSites.ContainsKey(siteEnCours.nomSite) && siteEnCours.fk_liste.Equals("Liste Rouge"))
                        {
                            retourSites.Add(siteEnCours.nomSite, false);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return retourSites;
            }
        }

        public Dictionary<string, bool> retourListeDynamiqueSites()
        {
            Dictionary<string, bool> retourSites = new Dictionary<string, bool>();

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }
                try
                {
                    List<ListeDynamique> siteListes = connexion.Query<ListeDynamique>("SELECT * FROM ListeDynamique").ToList();

                    foreach (ListeDynamique siteEnCours in siteListes)
                    {
                        if (!retourSites.ContainsKey(siteEnCours.url) && siteEnCours.fk_theme.Equals("Approprie"))
                        {
                            retourSites.Add(siteEnCours.url, true);
                        }
                        else if(!retourSites.ContainsKey(siteEnCours.url))
                        {
                            retourSites.Add(siteEnCours.url, false);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }

                return retourSites;
            }
        }

    }
}
