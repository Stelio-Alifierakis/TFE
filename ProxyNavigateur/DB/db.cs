using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BaseDonnees.Models;
using System.Data.SQLite;
using System.IO;
using System.Data;
using Dapper;

namespace BaseDonnees.DB
{
    /// <summary>
    /// Classe qui contient les fonctions d'accès à la base de données
    /// </summary>
    public class db : BDDInterface
    {
        /// <summary>
        /// Nom de la base de données
        /// </summary>
        private string BdNom;

        /// <summary>
        /// Chaîne de caractère qui sert à l'accès à la BDD sqlite
        /// </summary>
        private string sqliteInfo = string.Empty;

        /// <summary>
        /// constructeur
        /// </summary>
        public db() { }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nomBase">Nom de la base de données</param>
        public db(string nomBase) {
            BdNom = nomBase; //"BDD.sqlite"
            sqliteInfo = "Data Source=" + BdNom + ";Version=3;";
        }

        /// <summary>
        /// Fonction qui créé les différentes tables
        /// </summary>
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

        /// <summary>
        /// Fonction qui supprime le fichier de base de données
        /// </summary>
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

        /// <summary>
        /// Fonction qui appelle le seed des tables
        /// </summary>
        public void seeder()
        {
            TableSeed ts = new TableSeed(this);
            ts.seeder();
        }

        //méthodes sut la table Synchronisation

        /// <summary>
        /// Crée un champs synchronisation
        /// </summary>
        /// <param name="synch">Date de la synchro</param>
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

        /// <summary>
        /// Récupère une synchronisation
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>Synchronisation</returns>
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

        /// <summary>
        /// Récupère toutes les synchronisation
        /// </summary>
        /// <returns>Liste des synchronisation</returns>
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

        /// <summary>
        /// Crée une liste
        /// </summary>
        /// <param name="l">Liste</param>
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

        /// <summary>
        /// Récupère une liste
        /// </summary>
        /// <param name="ms">Nom de la liste</param>
        /// <returns>Liste</returns>
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

        /// <summary>
        /// Récupère toutes les listes
        /// </summary>
        /// <returns>L'ensemble des listes</returns>
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

        /// <summary>
        /// Créé un thème
        /// </summary>
        /// <param name="theme">Thème</param>
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

        /// <summary>
        /// Récupère un thème
        /// </summary>
        /// <param name="ms">Nom du thème</param>
        /// <returns>Thème</returns>
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

        /// <summary>
        /// Récupère tous les thèmes
        /// </summary>
        /// <returns>Liste des thèmes</returns>
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

        //méthodes sur la table Topologie

        /// <summary>
        /// Créé une topologie
        /// </summary>
        /// <param name="topo">Topologie</param>
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

        /// <summary>
        /// Récupère une topologie
        /// </summary>
        /// <param name="ms">ID machine de la topologie à récupérer</param>
        /// <returns>Topologie</returns>
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

        /// <summary>
        /// Récupère toutes les topologies
        /// </summary>
        /// <returns>Liste des topologies</returns>
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

        /// <summary>
        /// Crée un site dynamique
        /// </summary>
        /// <param name="dyn">URL du site</param>
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

        /// <summary>
        /// Récupère un site dynamique
        /// </summary>
        /// <param name="ms">URL du site</param>
        /// <returns></returns>
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

        /// <summary>
        /// Récupère tous les sites dynamiques
        /// </summary>
        /// <returns>Liste des sites dynamiques</returns>
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

        /// <summary>
        /// Récupère tous les sites dynamiques par thème
        /// </summary>
        /// <param name="nomTheme">Nom du thème</param>
        /// <returns>Liste des sites dynamiques</returns>
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

        /// <summary>
        /// Crée un site
        /// </summary>
        /// <param name="site">Nom du site</param>
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

        /// <summary>
        /// Récupère un site
        /// </summary>
        /// <param name="ms">Nom du site</param>
        /// <returns>Site</returns>
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

        /// <summary>
        /// Récupère tous les sites
        /// </summary>
        /// <returns>Liste des sites</returns>
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

        /// <summary>
        /// Crée un mot-clé
        /// </summary>
        /// <param name="mc">Mot</param>
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

        /// <summary>
        /// Récupère un mot clé
        /// </summary>
        /// <param name="ms">Mot</param>
        /// <returns>Mot-clé</returns>
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

        /// <summary>
        /// Récupère tous les mots-clé
        /// </summary>
        /// <returns>Liste des mots-clé</returns>
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

        /// <summary>
        /// Crée un synonyme
        /// </summary>
        /// <param name="syn">Mot</param>
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

        /// <summary>
        /// Récupère un synonyme
        /// </summary>
        /// <param name="ms">Mot</param>
        /// <returns>Synonyme</returns>
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

        /// <summary>
        /// Récupère tous les synonymes
        /// </summary>
        /// <returns>Liste des synonymes</returns>
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

        /// <summary>
        /// Fonction qui vérifie un site.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        /// <remarks>Ne marche plus</remarks>
        [System.Obsolete]
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

        /// <summary>
        /// Récupère tous les sites qui sont dans la liste dont le nom est stocké dans la variable <paramref name="listeURL"/>
        /// </summary>
        /// <param name="listeURL">Nom de la liste</param>
        /// <returns>Liste de sites</returns>
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

        /// <summary>
        /// Vérifie que la phrase contenue dans la variable <paramref name="phrase"/> comporte l'un des mots-clé ou des synonymes
        /// </summary>
        /// <param name="phrase">Phrase dans laquelle chercher les mots</param>
        /// <returns>Booléen vrai ou faux</returns>
        /// <value>vrai ou faux</value>
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

        /// <summary>
        /// Retourne la valeur d'un mot
        /// </summary>
        /// <param name="mot">Mot</param>
        /// <returns>Valeur</returns>
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

        /// <summary>
        /// Retourne le nom du thème d'un mot
        /// </summary>
        /// <param name="word">Mot</param>
        /// <returns>Thème</returns>
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

        /// <summary>
        /// Retourne la liste des mots-clé (ou synonyme) et une valeur qui est égale à 0 dans un dictionnaire
        /// </summary>
        /// <returns>Dictionnaire mot-clé et valeur 0</returns>
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

        /// <summary>
        /// Retourne la liste des mots-clé (ou synonyme) et leurs valeurs
        /// </summary>
        /// <returns>Dictionnaire mots-clé et valeurs</returns>
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

        /// <summary>
        /// Retourne les thèmes liés aux synonymes et mots-clés
        /// </summary>
        /// <returns>Dictionnaire de thèmes et mots-clé ou synonymes</returns>
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

        /// <summary>
        /// Retourne un dictionnaire de thèmes
        /// </summary>
        /// <returns>Dictionnaires de thèmes et valeurs 0</returns>
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

        /// <summary>
        /// Retourne un dictionnaire d'URL et de valeur booléenne
        /// </summary>
        /// <returns>Dictionnaire de thèmes et valeurs booléennes</returns>
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

        /// <summary>
        /// Retourne un dictionnaire de sites dynamique et de valeurs booléennes
        /// </summary>
        /// <returns>Dictionnaire de sites dynamique et valeurs booléennes</returns>
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

        /// <summary>
        /// Retourne un dictionnaire des sites liés avec leurs thèmes
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> retourSiteTheme()
        {
            Dictionary<string, string> retourSites = new Dictionary<string, string>();

            using (IDbConnection connexion = new SQLiteConnection(sqliteInfo))
            {
                if (connexion.State == ConnectionState.Closed)
                {
                    connexion.Open();
                }

                try
                {
                    List<Sites> siteListes = connexion.Query<Sites>("SELECT * FROM Sites").ToList();

                    foreach (Sites s in siteListes)
                    {
                        if (!retourSites.ContainsKey(s.nomSite))
                        {
                            retourSites.Add(s.nomSite, s.fk_theme);
                        }
                    }

                    List<ListeDynamique> siteListeDyn = connexion.Query<ListeDynamique>("SELECT * FROM ListeDynamique").ToList();

                    foreach (ListeDynamique s in siteListeDyn)
                    {
                        if (!retourSites.ContainsKey(s.url))
                        {
                            retourSites.Add(s.url, s.fk_theme);
                        }
                    }
                    return retourSites;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }

            //return retourSites;
        }

    }
}
