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
        private const string sqliteInfo = "Data Source=BDD.sqlite;Version=3;";

        public void creationTables(string nomBase)
        {

            BdNom = nomBase; //"BDD.sqlite"

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
                    TableSeed ts = new TableSeed(this);
                    ts.seeder();
                    //tableSeed();
                }
            }

        }

        /*private void tableSeed()
        {
            Synchronisation synch = new Synchronisation(DateTime.Now);
            ListeTheme th = new ListeTheme("Approprie");
            Listes liste = new Listes("Liste Verte");
            Topologie topo = new Topologie("000001", synch.date);
            Sites site = new Sites("www.openclassroom.com", synch.date, th.theme, liste.liste);
            ListeDynamique dyn = new ListeDynamique("www.youtube.com", synch.date, th.theme);

            SetListeTheme(th);
            th.theme = "Pornographie";
            SetListeTheme(th);

            MotCle mc = new MotCle("sex", 4, synch.date, th.theme);
            Synonyme syn = new Synonyme("sexe", mc.mot, synch.date);


            SetSynchro(synch);
            //SetListeTheme(th);
            SetListe(liste);
            SetTopologie(topo);
            SetSites(site);
            SetListeDynamique(dyn);
            SetMotCle(mc);
            SetSynonyme(syn);

            liste.liste = "Liste Rouge";
            SetListe(liste);

            site.nomSite = "www.youporn.com";
            site.fk_theme = th.theme;
            site.fk_liste = liste.liste;
            SetSites(site);

            dyn.url = "www.facebook.com";
            dyn.fk_theme = th.theme;
            SetListeDynamique(dyn);

            mc.mot = "porn";
            mc.valeur = 15;
            mc.fk_theme = th.theme;
            SetMotCle(mc);

            syn.mot = "seks";
            SetSynonyme(syn);
        }*/

        //méthodes sut la table Synchronisation

        public void SetSynchro(Synchronisation synch)
        {
            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
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
                string msg = "SELECT * FROM Synchronisation where date ='" + date + "'";
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
                    connexion.Open();
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

        public bool verifMot(string msg)
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

    }
}
