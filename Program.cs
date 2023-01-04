using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;
using System.Diagnostics;
namespace velomax

{
    class Program
    {
        public static MySqlConnection MaConnexion(string username,string password)
        {
            MySqlConnection maConnexion = null;
            try
            {
                //a. indiquer les infos pour la connexion 
                string connexionInfo = "SERVER=localhost;PORT=3306;" + "DATABASE=velomax ;UID="+username+";PASSWORD="+password;

                //b. objet de connexion
                maConnexion = new MySqlConnection(connexionInfo);

                //c. ouvrir le canal de communication/connexion
                maConnexion.Open();


            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur de connexion : " + e.ToString());

            }
            return maConnexion;
        }
        public static void Affichage(string requete, MySqlCommand maRequete)
        {
            maRequete.CommandText = requete;

            MySqlDataReader exeRequete = maRequete.ExecuteReader();

            string tableau = "";
            //Console.WriteLine("Il y a " + exeRequete.FieldCount + " colonnes");
            while (exeRequete.Read())
            {

                //pour chaque ligne
                string ligne = "";
                for (int i = 0; i < exeRequete.FieldCount; i++)
                {
                    //Construire la ligne dans une chaine de caractères (recup avec get value puis le transforme en str avec ToString())
                    ligne += "\t" + exeRequete.GetValue(i).ToString();
                }
                tableau += "\n" + ligne;
            }
            exeRequete.Close();  //fermeture de la requête
            Console.WriteLine(tableau);  //obtenir le tableau d'affichage
        }
        public static List<string> Reader2(string requete, MySqlCommand maRequete)
        {
            maRequete.CommandText = requete;
            MySqlDataReader exeRequete = maRequete.ExecuteReader();
            List<string> valueString = new List<string>();

            while (exeRequete.Read())
            {

                //pour chaque ligne

                //Construire la ligne dans une chaine de caractères (recup avec get value puis le transforme en str avec ToString())
                valueString.Add(exeRequete.GetValue(0).ToString());


            }
            exeRequete.Close();  //fermeture de la requête
            maRequete.Dispose();

            return valueString;
        }
        public static List<string> Reader3(string requete, MySqlCommand maRequete)
        {
            maRequete.CommandText = requete;
            MySqlDataReader exeRequete = maRequete.ExecuteReader();
            List<string> valueString = new List<string>();

            while (exeRequete.Read())
            {

                //pour chaque ligne

                for (int i = 0; i < exeRequete.FieldCount; i++)
                {
                    //Construire la ligne dans une chaine de caractères (recup avec get value puis le transforme en str avec ToString())
                    valueString.Add(exeRequete.GetValue(i).ToString());

                }

            }
            exeRequete.Close();  //fermeture de la requête
            maRequete.Dispose();
            return valueString;
        }
        public static string[] Reader(string requete, MySqlCommand maRequete)
        {
            maRequete.CommandText = requete;
            MySqlDataReader exeRequete = maRequete.ExecuteReader();
            string[] valueString = new string[exeRequete.FieldCount];

            while (exeRequete.Read())
            {

                //pour chaque ligne

                for (int i = 0; i < exeRequete.FieldCount; i++)
                {
                    //Construire la ligne dans une chaine de caractères (recup avec get value puis le transforme en str avec ToString())
                    valueString[i] = exeRequete.GetValue(i).ToString();

                }

            }
            exeRequete.Close();
            maRequete.Dispose();
            return valueString;
        }

        #region Gestion Bicyclette

        public static void AjoutBicyclette(MySqlCommand createinsert)
        {

            string rep;
            do
            {
                Console.WriteLine("Voulez-vous ajouter un modèle ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                string requete = "SELECT * FROM bicyclette;";
                Console.WriteLine("Voici la liste des modèles de bicyclette : ");
                Affichage(requete,createinsert);
                Console.Write("\nEntrez id modele : ");
                string id = Console.ReadLine();
                Console.Write("\nEntrez nom du modèle : ");
                string nom_modele = Console.ReadLine();
                Console.Write("\nEntrez grandeur (adultes/jeunes/hommes/dames/filles/garçons) : ");
                string grandeur = Console.ReadLine();
                grandeur = grandeur[0].ToString().ToUpper() + grandeur.Substring(1).ToLower();
                Console.Write("\nEntrez prix à l'unité : ");
                float prix = float.Parse(Console.ReadLine());
                Console.Write("\nEntrez la ligne : ");
                string ligne = Console.ReadLine();
                Console.Write("\nEntrez la date d'intro du modèle (JJ/MM/AAAA) : ");
                DateTime date_intro = DateTime.Parse(Console.ReadLine());
                string intro = date_intro.ToString("yyyy-MM-dd");
                //Console.Write("\nEntrez la date de discontinuité (JJ/MM/AAAA) : ");
                //DateTime date_disc = DateTime.Parse(Console.ReadLine());
                //string disc= date_disc.ToString("yyyy-MM-dd");

                //Insertion de la biclyclette avec tous les paramètres 
                string table1 = "INSERT INTO bicyclette VALUES ('" + id + "','" + nom_modele + "','" + grandeur + "'," + prix + ",'" + ligne + "','" + intro + "',null);";
                Console.WriteLine(table1);

                createinsert.CommandText = table1;
                createinsert.ExecuteNonQuery();
                createinsert.Dispose();

                //Affichage après ajout
                requete = "SELECT * FROM bicyclette;";
                Console.WriteLine();
                Affichage(requete, createinsert);

                do
                {
                    Console.WriteLine("\nVoulez-vous ajouter une bicyclette ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");

            }
        }
        public static void SuppressionBicyclette(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous supprimer un modèle ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                string requete = "SELECT * FROM bicyclette;";
                Console.WriteLine("Voici la liste des modèles de bicyclette :");
                Affichage(requete, createinsert);

                Console.Write("\nEntrez id du modele à supprimer: ");
                string id = Console.ReadLine();

                string table1 = "DELETE FROM bicyclette WHERE id_modele='" + id + "';";
                Console.WriteLine(table1);

                createinsert.CommandText = table1;
                createinsert.ExecuteNonQuery();

                requete = "SELECT * FROM bicyclette;";
                Affichage(requete, createinsert);





                do
                {
                    Console.WriteLine("\nVoulez-vous supprimer un modèle ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");
            }
            createinsert.Dispose();
        }
        public static void MajBicyclette(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous modifier un modèle ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                string requete = "SELECT * FROM bicyclette;";
                Console.WriteLine("Voici la liste des modèles de bicyclette :");
                Affichage(requete, createinsert);

                Console.Write("\nEntrez id modele à modifier : ");
                string id = Console.ReadLine();

                int choix;
                do
                {
                    Console.WriteLine("\nQue voulez-vous modifier ? (Tapez 9 pour sortir)\n"
                                     + "1 : id modèle\n"
                                     + "2 : nom modèle\n"
                                     + "3 : grandeur\n"
                                     + "4 : prix unit\n"
                                     + "5 : ligne\n"
                                     + "6 : date introduction\n"
                                     + "7 : date discontinuation\n"

                                     + "Quel est votre choix (1-7) ?");
                    choix = int.Parse(Console.ReadLine());

                    switch (choix)
                    {

                        case 1:
                            Console.WriteLine("Entrer nouveau id modèle :");
                            id = Console.ReadLine();
                            string maj1 = "UPDATE bicyclette SET id_modele='" + id + "'WHERE id_modele='" + id + "';";
                            Console.WriteLine(maj1);

                            createinsert.CommandText = maj1;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 2:
                            Console.WriteLine("Entrez nouveau nom modèle : ");
                            string rep1 = Console.ReadLine();
                            string maj2 = "UPDATE bicyclette SET nom_modele='" + rep1 + "'WHERE id_modele='" + id + "';";
                            Console.WriteLine(maj2);


                            createinsert.CommandText = maj2;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 3:
                            Console.WriteLine("Entrer nouvelle grandeur :");
                            rep1 = Console.ReadLine();
                            string maj3 = "UPDATE bicyclette SET grandeur='" + rep1 + "'WHERE id_modele='" + id + "';";
                            Console.WriteLine(maj3);

                            createinsert.CommandText = maj3;
                            createinsert.ExecuteNonQuery();


                            break;
                        case 4:
                            Console.Write("Entrer nouveau prix à l'unité : ");
                            float prix = float.Parse(Console.ReadLine());

                            string maj4 = "UPDATE bicyclette SET prix_unit='" + prix + "'WHERE id_modele='" + id + "';";
                            Console.WriteLine(maj4);

                            createinsert.CommandText = maj4;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 5:
                            Console.Write("Entrer nouvelle ligne : ");
                            rep1 = Console.ReadLine();
                            string maj5 = "UPDATE bicyclette SET ligne='" + rep1 + "'WHERE id_modele='" + id + "';";
                            Console.WriteLine(maj5);

                            createinsert.CommandText = maj5;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 6:
                            Console.Write("Entrer nouvelle date d'intro : ");
                            DateTime date = DateTime.Parse(Console.ReadLine());
                            rep1 = date.ToString("yyyy-MM-dd");
                            string maj6 = "UPDATE bicyclette SET date_intro='" + rep1 + "'WHERE id_modele='" + id + "';";
                            Console.WriteLine(maj6);

                            createinsert.CommandText = maj6;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 7:
                            Console.Write("Entrer nouvelle date de discontinuté : ");
                            date = DateTime.Parse(Console.ReadLine());
                            rep1 = date.ToString("yyyy-MM-dd");
                            string maj7 = "UPDATE bicyclette SET date_disc='" + rep1 + "'WHERE id_modele='" + id + "';";
                            Console.WriteLine(maj7);

                            createinsert.CommandText = maj7;
                            createinsert.ExecuteNonQuery();


                            break;

                        default:

                            break;

                    }
                    Console.WriteLine();
                    requete = "SELECT * FROM bicyclette;";
                    Console.WriteLine("Voici la liste des modèles de bicyclette :");
                    Affichage(requete, createinsert);

                } while (choix != 9);


                do
                {
                    Console.WriteLine("\nVoulez-vous modifier un modèle ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");
            }
            createinsert.Dispose();
        }

        #endregion

        #region Pieces
        public static void AjoutPiece(MySqlCommand createinsert)
        {
            string rep2;
            do
            {
                Console.WriteLine("Voulez-vous ajouter une pièce à l'un des modèles ?  oui/non");
                rep2 = Console.ReadLine();

            } while (rep2 != "oui" && rep2 != "non");
            while (rep2 == "oui")
            {
                string requete_piece = "SELECT * FROM piece;";
                string requete_assemble = "SELECT A.id_modele,A.id_piece,nom_modele,grandeur FROM assemblage A NATURAL JOIN piece NATURAL JOIN bicyclette order by id_modele,id_piece;";

                Console.WriteLine("Voici la liste des pièces de rechange :");
                Console.WriteLine("\tpiece  Descrpt nom_f  id_cata   date_intro  \t\tdate_dis  \t\tstock");
                Affichage(requete_piece, createinsert);
                Console.WriteLine("\nVoici la liste des assemblages :");
                Console.WriteLine("\tmodele\tpiece\tnom_modèle  \tgrandeur");
                Affichage(requete_assemble, createinsert);

                Console.Write("\nEntrez id de la nouvelle pièce : ");
                string id_piece = Console.ReadLine();
                Console.Write("\nEntrez description de la piece : ");
                string description = Console.ReadLine();
                Console.Write("\nEntrez le nom du fournisseur : ");
                string nom_fournisseur = Console.ReadLine();
                Console.Write("\nEntrez id catalogue : ");
                string id_catalogue = Console.ReadLine();
                Console.Write("\nEntrez la date d'intro de la pièce (JJ/MM/AAAA) : ");
                DateTime date_intro_piece = DateTime.Parse(Console.ReadLine());
                string intro_piece = date_intro_piece.ToString("yyyy-MM-dd");
                //Console.Write("\nEntrez la date de discontinuité de la pièce (JJ/MM/AAAA) : ");
                //DateTime date_disc_piece = DateTime.Parse(Console.ReadLine());
                //string disc_piece = date_disc_piece.ToString("yyyy-MM-dd");

                Console.WriteLine("A quel id modele voulez vous ajouter cette pièce? ");
                string id_modele = Console.ReadLine();

                string table2 = "INSERT INTO piece VALUES ('" + id_piece + "','" + description + "','" + nom_fournisseur + "','" + id_catalogue + "','" + intro_piece + "',null,1);";
                string table3 = "INSERT INTO assemblage VALUES ('" + id_modele + "','" + id_piece + "');";
                Console.WriteLine(table2);
                Console.WriteLine(table3);


                createinsert.CommandText = table2;
                createinsert.ExecuteNonQuery();

                createinsert.CommandText = table3;
                createinsert.ExecuteNonQuery();

                requete_piece = "SELECT * FROM piece;";
                requete_assemble = "SELECT A.id_modele,A.id_piece,nom_modele,grandeur FROM assemblage A NATURAL JOIN piece NATURAL JOIN bicyclette order by id_modele,id_piece;";

                Console.WriteLine("Voici la liste des pièces de rechange :");
                Affichage(requete_piece, createinsert);
                Console.WriteLine("\nVoici la liste des assemblages :");
                Affichage(requete_assemble, createinsert);



                do
                {
                    Console.WriteLine("\nVoulez-vous ajouter une autre pièce ?  oui/non");
                    rep2 = Console.ReadLine();

                } while (rep2 != "oui" && rep2 != "non");

            }
            createinsert.Dispose();

        }
        public static void SuppressionPiece(MySqlCommand createinsert)
        {
            string rep2;
            do
            {
                Console.WriteLine("Voulez-vous supprimer une pièce ?  oui/non");
                rep2 = Console.ReadLine();

            } while (rep2 != "oui" && rep2 != "non");

            while (rep2 == "oui")
            {
                string requete_piece = "SELECT * FROM piece;";
                Console.WriteLine("Voici la liste des pièces de rechange :");
                Console.WriteLine("\tpiece  Descrpt nom_f  id_cata   date_intro  \t\tdate_dis  \t\tstock");
                Affichage(requete_piece, createinsert);

                Console.Write("\nEntrez id de la pièce à supprimer: ");
                string id = Console.ReadLine();


                //string table = "DELETE FROM assemblage WHERE id_piece='" + id + "';";
                string table1 = "DELETE FROM piece WHERE id_piece='" + id + "';";
                Console.WriteLine(table1);



                //createinsert.CommandText = table;
                //createinsert.ExecuteNonQuery();

                createinsert.CommandText = table1;
                createinsert.ExecuteNonQuery();

                requete_piece = "SELECT * FROM piece;";
                Console.WriteLine("Voici la liste des pièces de rechange :");
                Affichage(requete_piece, createinsert);

                createinsert.Dispose();

                do
                {
                    Console.WriteLine("\nVoulez-vous supprimer une autre pièce ?  oui/non");
                    rep2 = Console.ReadLine();

                } while (rep2 != "oui" && rep2 != "non");
            }
        }
        public static void MajPiece(MySqlCommand createinsert)
        {
            string rep;
            string rep2;
            do
            {
                Console.WriteLine("Voulez-vous modifier un pièce ?  oui/non");
                rep2 = Console.ReadLine();

            } while (rep2 != "oui" && rep2 != "non");

            while (rep2 == "oui")
            {
                string requete_piece = "SELECT * FROM piece;";
                Console.WriteLine("Voici la liste des pièces de rechange :");
                Console.WriteLine("\tpiece  Descrpt nom_f  id_cata   date_intro  \t\tdate_dis  \t\tstock");
                Affichage(requete_piece, createinsert);

                Console.Write("\nEntrez id pièce à modifier : ");
                string id_piece = Console.ReadLine();

                int choix;
                do
                {
                    Console.WriteLine("\nQue voulez-vous modifier ? (Tapez 9 pour sortir)\n"
                                     + "1 : id piece\n"
                                     + "2 : description\n"
                                     + "3 : nom fournisseur\n"
                                     + "4 : id catalogue\n"
                                     + "5 : date introduction\n"
                                     + "6 : date discontinuation\n"

                                     + "Quel est votre choix (1-6) ?");
                    choix = int.Parse(Console.ReadLine());

                    switch (choix)
                    {
                        case 1:
                            Console.Write("\nEntrer nouveau id pièce :");
                            rep = Console.ReadLine();
                            string maj1 = "UPDATE piece SET id_piece='" + rep + "'WHERE id_piece='" + id_piece + "';";
                            Console.WriteLine(maj1);

                            createinsert.CommandText = maj1;
                            createinsert.ExecuteNonQuery();


                            break;
                        case 2:
                            Console.Write("\nEntrez nouvelle description : ");
                            rep = Console.ReadLine();
                            string maj2 = "UPDATE piece SET description='" + rep + "'WHERE id_piece='" + id_piece + "';";


                            createinsert.CommandText = maj2;
                            createinsert.ExecuteNonQuery();


                            break;
                        case 3:
                            Console.Write("\nEntrez nouveau nom fournisseur : ");
                            rep = Console.ReadLine();
                            string maj3 = "UPDATE piece SET nom_fournisseur='" + rep + "'WHERE id_piece='" + id_piece + "';";


                            createinsert.CommandText = maj3;
                            createinsert.ExecuteNonQuery();


                            break;
                        case 4:
                            Console.Write("\nEntrez nouveau id catalogue : ");
                            rep = Console.ReadLine();
                            string maj4 = "UPDATE piece SET id_catalogue='" + rep + "'WHERE id_piece='" + id_piece + "';";


                            createinsert.CommandText = maj4;
                            createinsert.ExecuteNonQuery();


                            break;

                        case 5:
                            Console.Write("\nEntrez nouvelle date d'intro (JJ/MM/AAAA) : ");
                            DateTime date_intro_piece = DateTime.Parse(Console.ReadLine());
                            string intro_piece = date_intro_piece.ToString("yyyy-MM-dd");
                            string maj5 = "UPDATE piece SET date_intro_piece='" + intro_piece + "'WHERE id_piece='" + id_piece + "';";


                            createinsert.CommandText = maj5;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 6:
                            Console.Write("\nEntrez nouvelle date de discontinuité : ");
                            DateTime date_disc = DateTime.Parse(Console.ReadLine());
                            string disc = date_disc.ToString("yyyy-MM-dd");
                            string maj6 = "UPDATE piece SET date_disc_piece='" + disc + "'WHERE id_piece='" + id_piece + "';";


                            createinsert.CommandText = maj6;
                            createinsert.ExecuteNonQuery();

                            break;

                        default:

                            break;
                    }

                    Console.WriteLine();
                    requete_piece = "SELECT * FROM piece;";
                    Console.WriteLine("Voici la liste des pièces de rechange :");
                    Console.WriteLine("\tpiece  Descrpt nom_f  id_cata   date_intro  \t\tdate_dis  \t\tstock");
                    Affichage(requete_piece, createinsert);


                } while (choix != 9);

                do
                {
                    Console.WriteLine("\nVoulez-vous modifier une autre pièce ?  oui/non");
                    rep2 = Console.ReadLine();

                } while (rep2 != "oui" && rep2 != "non");
                createinsert.Dispose();
            }
        }

        #endregion

        #region Gestion clients Particuliers

        public static void AjoutClientParticulier(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous ajouter un client particulier ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                Console.WriteLine("Voici la liste des clients particuliers :");
                Console.WriteLine("  id_client     nom    prenom \t  tel \t\t  courriel \t      type\tadresse");
                string requete = "SELECT * FROM contact WHERE id_typecontact=1 ;";
                Affichage(requete, createinsert);

                //typecontact directement =1 car on crée un particulier
                Console.Write("\nEntrez id contact : ");
                string id_contact = Console.ReadLine();
                Console.Write("\nEntrez nom du client : ");
                string nom = Console.ReadLine();
                Console.Write("\nEntrez prénom du client : ");
                string prenom = Console.ReadLine();
                Console.Write("\nEntrez tel : ");
                string tel = Console.ReadLine();
                Console.Write("\nEntrez courriel : ");
                string courriel = Console.ReadLine();

                //création d'une adresse reliée au contact

                requete = "SELECT * FROM adresse ;";
                Affichage(requete, createinsert);

                Console.Write("\nEntrez id adresse : ");
                string id_adresse = Console.ReadLine();

                //Si adresse n'existe pas on la crée
                string req = "SELECT id_adresse from adresse ;";
                List<string> adr = Reader2(req, createinsert);
                if (!adr.Contains(id_adresse))
                {
                    Console.Write("\nEntrez la rue : ");
                    string rue = Console.ReadLine();
                    Console.Write("\nEntrez la ville : ");
                    string ville = Console.ReadLine();
                    Console.Write("\nEntrez code postal : ");
                    string postal = Console.ReadLine();
                    Console.Write("\nEntrez la province : ");
                    string province = Console.ReadLine();
                    string create_adresse = "INSERT INTO adresse VALUES ('" + id_adresse + "','" + rue + "','" + ville + "'," + postal + ",'" + province + "');";
                    Console.WriteLine(create_adresse);
                    createinsert.CommandText = create_adresse;
                    createinsert.ExecuteNonQuery();
                }

                string create_contact = "INSERT INTO contact VALUES ('" + id_contact + "','" + nom + "','" + prenom + "'," + tel + ",'" + courriel + "',1,'" + id_adresse + "');";

                Console.WriteLine(create_contact);

                createinsert.CommandText = create_contact;
                createinsert.ExecuteNonQuery();

                //Affichage après ajout

                requete = "SELECT * FROM contact WHERE id_typecontact=1 ;";
                Console.WriteLine();
                Affichage(requete, createinsert);

                string programme;
                do
                {
                    Console.WriteLine("\nVoulez-vous adhérer au programme Fidelio ? oui/non ");
                    programme = Console.ReadLine();

                } while (programme != "oui" && programme != "non");

                if (programme == "oui")
                {
                    //affiche les programmes Fidélio
                    requete = "SELECT * FROM programme;";
                    Console.WriteLine("N° Programme\t" + "Description\t" + "Coût\t" + "Durée\t" + "Rabais\t");
                    Affichage(requete, createinsert);


                    //enregistre le choix du programme que le client choisit
                    Console.WriteLine("\nQuelle programme voulez-vous choisir (1-4) ? (Tapez 9 pour sortir)");
                    int choix = int.Parse(Console.ReadLine());

                    //enregistre la durée du programme choisi
                    string temps = "SELECT duree from programme WHERE id_programme=" + choix + ";";
                    createinsert.CommandText = temps;
                    MySqlDataReader exeRequete = createinsert.ExecuteReader();
                    int duree = 0;
                    while (exeRequete.Read())
                    {
                        duree = int.Parse(exeRequete.GetValue(0).ToString());

                    }

                    exeRequete.Close();
                    Console.WriteLine(duree);

                    string create_programme = "INSERT INTO adhesion VALUES ('" + id_contact + "','" + choix + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.AddYears(duree).ToString("yyyy-MM-dd") + "');";
                    Console.WriteLine(create_programme);
                    createinsert.CommandText = create_programme;
                    createinsert.ExecuteNonQuery();
                    requete = "SELECT * FROM adhesion ;";
                    Console.WriteLine();
                    Affichage(requete, createinsert);
                }

                do
                {
                    Console.WriteLine("\nVoulez-vous ajouter un autre client particulier ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");
            }
            createinsert.Dispose();
        }
        public static void SuppresionClientParticulier(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous supprimer un client particulier ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                Console.WriteLine();
                Console.WriteLine("  id_client     nom    prenom \t  tel \t\t  courriel \t      type\tadresse");
                string requete = "SELECT * FROM contact WHERE id_typecontact=1 ;";
                Affichage(requete, createinsert);


                //récupère nom et prénom du client à suppr
                Console.Write("\nEntrez id du client à supprimer: ");
                string id = Console.ReadLine();


                string table1 = "DELETE FROM contact WHERE nom_contact='" + id + "';";
                Console.WriteLine(table1);

                createinsert.CommandText = table1;
                createinsert.ExecuteNonQuery();

                Console.WriteLine();
                Console.WriteLine("  id_client     nom    prenom \t  tel \t\t  courriel \t      type\tadresse");
                requete = "SELECT * FROM contact WHERE id_typecontact=1 ;";
                Affichage(requete, createinsert);

                do
                {
                    Console.WriteLine("\nVoulez-vous supprimer un autre client particulier ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");
            }
            createinsert.Dispose();
        }
        public static void MajClientParticulier(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous modifier un client particulier ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");



            while (rep == "oui")
            {
                Console.WriteLine();
                Console.WriteLine("  id_client     nom    prenom \t  tel \t\t  courriel \t      type\tadresse");
                string requete = "SELECT * FROM contact WHERE id_typecontact=1 ;";
                Affichage(requete, createinsert);

                Console.Write("\nEntrez id du client à modifier : ");
                string id = Console.ReadLine();

                int choix;
                do
                {
                    Console.WriteLine("\nQue voulez-vous modifier ? (Tapez 9 pour sortir)\n"
                                     + "1 : nom\n"
                                     + "2 : prenom\n"
                                     + "3 : tel\n"
                                     + "4 : courriel\n"
                                     + "5 : adresse\n"

                                     + "Quel est votre choix ?");
                    choix = int.Parse(Console.ReadLine());

                    switch (choix)
                    {
                        case 1:
                            Console.Write("\nEntrez nouveau nom : ");
                            string rep1 = Console.ReadLine();
                            string maj2 = "UPDATE contact SET nom_contact='" + rep1 + "'WHERE id_contact='" + id + "';";
                            Console.WriteLine(maj2);

                            createinsert.CommandText = maj2;
                            createinsert.ExecuteNonQuery();


                            break;
                        case 2:
                            Console.Write("\nEntrer nouveau prénom : ");
                            rep1 = Console.ReadLine();
                            string maj3 = "UPDATE contact SET prenom_contact='" + rep1 + "'WHERE id_contact='" + id + "';";
                            Console.WriteLine(maj3);

                            createinsert.CommandText = maj3;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 3:
                            Console.Write("\nEntrer nouveau tel : ");
                            rep1 = Console.ReadLine();

                            string maj4 = "UPDATE contact SET tel='" + rep1 + "'WHERE id_contact='" + id + "';";
                            Console.WriteLine(maj4);


                            createinsert.CommandText = maj4;
                            createinsert.ExecuteNonQuery();



                            break;
                        case 4:
                            Console.Write("\nEntrer nouveau courriel : ");
                            rep1 = Console.ReadLine();
                            string maj5 = "UPDATE contact SET courriel='" + rep1 + "'WHERE id_contact='" + id + "';";
                            Console.WriteLine(maj5);

                            createinsert.CommandText = maj5;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 5:

                            requete = "SELECT * FROM adresse ;";
                            Affichage(requete, createinsert);
                            Console.Write("\nEntrez id adresse parmi les adresses déjà enregistrées ou nouveau id adresse: ");
                            string id_adresse = Console.ReadLine();


                            createinsert.CommandText = id_adresse;
                            string req = "SELECT id_adresse from adresse ;";
                            List<string> adr = Reader2(req, createinsert);

                            //Si id adresse n'est pas dans la liste en créer une
                            if (!adr.Contains(id_adresse))
                            {
                                Console.Write("\nEntrez la rue : ");
                                string rue = Console.ReadLine();
                                Console.Write("\nEntrez la ville : ");
                                string ville = Console.ReadLine();
                                Console.Write("\nEntrez code postal : ");
                                string postal = Console.ReadLine();
                                Console.Write("\nEntrez la province : ");
                                string province = Console.ReadLine();
                                string create_adresse = "INSERT INTO adresse VALUES ('" + id_adresse + "','" + rue + "','" + ville + "','" + postal + "','" + province + "');";
                                Console.WriteLine(create_adresse);
                                createinsert.CommandText = create_adresse;
                                createinsert.ExecuteNonQuery();
                            }

                            //on update l'adresse par la nouvelle 
                            string maj6 = "UPDATE contact SET id_adresse='" + id_adresse + "'WHERE id_contact='" + id + "';";
                            Console.WriteLine(maj6);

                            createinsert.CommandText = maj6;
                            createinsert.ExecuteNonQuery();


                            break;

                        default:

                            break;

                    }
                    Console.WriteLine();

                    Console.WriteLine("  id_client     nom    prenom \t  tel \t\t  courriel \t      type\tadresse");
                    requete = "SELECT * FROM contact WHERE id_typecontact=1 ;";
                    Affichage(requete, createinsert);
                } while (choix != 9);


                do
                {
                    Console.WriteLine("\nVoulez-vous modifier un autre client particulier ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");
            }
            createinsert.Dispose();
        }

        #endregion

        #region Gestion client Boutique
        public static void AjoutClientBoutique(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous ajouter un client boutique ?  oui/non");
                rep = Console.ReadLine();


            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                Console.WriteLine("Voici la liste des boutiques : ");
                Console.WriteLine("\tnom    \t\ttel \t\t  courriel\t      id_c  id_adresse  volume");
                string requete = "SELECT * FROM boutique,contact ;";
                Affichage(requete, createinsert);

                //renseignement boutique
                Console.Write("\nEntrez nom_compagnie : ");
                string nom = Console.ReadLine();
                Console.Write("\nEntrez numero tel : ");
                string tel_b = Console.ReadLine();
                Console.Write("\nEntrez courriel : ");
                string courriel_b = Console.ReadLine();
                Console.Write("\nEntrez id_contact : ");
                string id_contact = Console.ReadLine();

                Console.WriteLine("Voici la liste des adresses :");
                requete = "SELECT * FROM adresse ;";
                Affichage(requete, createinsert);
                Console.Write("\nEntrer id_adresse : ");
                string id_adresse = Console.ReadLine();

                //renseignement adresse
                string req = "SELECT id_adresse from adresse ;";
                List<string> adr = Reader2(req, createinsert);
                if (!adr.Contains(id_adresse))
                {
                    Console.Write("\nEntrez la rue : ");
                    string rue = Console.ReadLine();
                    Console.Write("\nEntrez la ville : ");
                    string ville = Console.ReadLine();
                    Console.Write("\nEntrez code postal : ");
                    string postal = Console.ReadLine();
                    Console.Write("\nEntrez la province : ");
                    string province = Console.ReadLine();
                    string create_adresse = "INSERT INTO adresse VALUES ('" + id_adresse + "','" + rue + "','" + ville + "','" + postal + "','" + province + "');";
                    Console.WriteLine(create_adresse);
                    createinsert.CommandText = create_adresse;
                    createinsert.ExecuteNonQuery();
                }
                //renseignement du contact
                Console.Write("\nEntrez nom contact : ");
                string nom_contact = Console.ReadLine();
                Console.Write("\nEntrez prénom contact : ");
                string prenom_contact = Console.ReadLine();



                string table2 = "INSERT INTO contact VALUES ('" + id_contact + "','" + nom_contact + "','" + prenom_contact + "','" + tel_b + "','" + courriel_b + "',2,'" + id_adresse + "');";

                string table1 = "INSERT INTO boutique VALUES ('" + nom + "','" + tel_b + "','" + courriel_b + "','" + id_contact + "','" + id_adresse + "','0');";
                Console.WriteLine(table1);


                createinsert.CommandText = table2;
                createinsert.ExecuteNonQuery();
                createinsert.CommandText = table1;
                createinsert.ExecuteNonQuery();


                Console.WriteLine();
                Console.WriteLine("  id_client     nom    prenom \t  tel \t\t  courriel \t      type\tadresse");
                requete = "SELECT * FROM contact WHERE id_typecontact=2 ;"; //le faire aussi pour boutique ?
                Affichage(requete, createinsert);

                do
                {
                    Console.WriteLine("\nVoulez-vous ajouter un autre client boutique ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");

            }
            createinsert.Dispose();
        }
        public static void SuppressionClientBoutique(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous supprimer une boutique ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {

                Console.WriteLine("Voici la liste des id_contact correspondant à leur boutique :");
                string id_contact_boutique = "SELECT id_contact, nom_compagnie FROM boutique;";
                Affichage(id_contact_boutique, createinsert);
                Console.Write("\nEntrez id contact pour boutique à supprimer: ");
                string id = Console.ReadLine();


                string table = "DELETE FROM contact WHERE id_contact='" + id + "';";

                Console.WriteLine(table);
                createinsert.CommandText = table;
                createinsert.ExecuteNonQuery();

                Console.WriteLine("Voici la liste des id_contact correspondant à leur boutique :");
                id_contact_boutique = "SELECT id_contact, nom_compagnie FROM boutique;";
                Affichage(id_contact_boutique, createinsert);
                Console.WriteLine();

                Console.WriteLine("Voici la liste des boutiques : ");
                Console.WriteLine("\tnom    \t\ttel \t\t  courriel\t      id_c  id_adresse  volume");
                string requete = "SELECT * FROM boutique ;";
                Affichage(requete, createinsert);

                do
                {
                    Console.WriteLine("\nVoulez-vous supprimer un autre client ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");
            }
            createinsert.Dispose();
        }
        public static void MajClientBoutique(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous modifier un client boutique ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {

                string requete = "SELECT id_contact, nom_compagnie FROM boutique;";
                Console.WriteLine("Voici la liste des id_contact correspondant à leur boutique :");
                Affichage(requete, createinsert);

                Console.WriteLine();
                Console.WriteLine("Voici la liste des boutiques : ");
                Console.WriteLine("\tnom    \t\ttel \t\t  courriel\t      id_c  id_adresse  volume");

                requete = "SELECT * FROM boutique ;";
                Affichage(requete, createinsert);

                Console.Write("\nEntrez id_contact de la boutique à modifier : ");
                string id_contact = Console.ReadLine();


                int choix;
                do
                {
                    Console.WriteLine("\nQue voulez-vous modifier ? (Tapez 9 pour sortir)\n"
                                     + "1 : nom_compagnie\n"
                                     + "2 : tel_b\n"
                                     + "3 : courriel_b\n"
                                     + "4 : contact\n"
                                     + "5 : adresse\n"
                                     + "6 : volume\n"


                                     + "Quel est votre choix ?");

                    choix = int.Parse(Console.ReadLine());
                    switch (choix)
                    {
                        case 1:
                            Console.WriteLine("Entrer nouveau nom de compagnie :");
                            rep = Console.ReadLine();
                            string maj1 = "UPDATE boutique SET nom_compagnie='" + rep + "'WHERE id_contact='" + id_contact + "';";
                            Console.WriteLine(maj1);

                            createinsert.CommandText = maj1;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 2:
                            Console.WriteLine("Entrer nouveau tel :");
                            rep = Console.ReadLine();
                            string maj2 = "UPDATE contact SET tel='" + rep + "'WHERE id_contact='" + id_contact + "';";

                            string maj2b = "UPDATE boutique SET tel_b='" + rep + "'WHERE id_contact='" + id_contact + "';";
                            Console.WriteLine(maj2);



                            createinsert.CommandText = maj2;
                            createinsert.ExecuteNonQuery();

                            createinsert.CommandText = maj2b;
                            createinsert.ExecuteNonQuery();


                            break;
                        case 3:
                            Console.WriteLine("Entrer nouveau courriel :");
                            rep = Console.ReadLine();
                            string maj3 = "UPDATE contact SET courriel='" + rep + "'WHERE id_contact='" + id_contact + "';";

                            string maj3b = "UPDATE boutique SET courriel_b='" + rep + "'WHERE id_contact='" + id_contact + "';";
                            Console.WriteLine(maj3);



                            createinsert.CommandText = maj3;
                            createinsert.ExecuteNonQuery();
                            createinsert.CommandText = maj3b;
                            createinsert.ExecuteNonQuery();

                            break;


                        case 4:
                            Console.WriteLine("entrez nouveau nom contact");
                            rep = Console.ReadLine();
                            string maj4 = "UPDATE contact SET nom_contact='" + rep + "'WHERE id_contact='" + id_contact + "';";

                            Console.WriteLine("entrez nouveau prenom contact");
                            string rep1 = Console.ReadLine();
                            string maj4b = "UPDATE contact SET prenom_contact='" + rep1 + "'WHERE id_contact='" + id_contact + "';";

                            createinsert.CommandText = maj4;
                            createinsert.ExecuteNonQuery();
                            createinsert.CommandText = maj4b;
                            createinsert.ExecuteNonQuery();


                            break;

                        case 5:
                            requete = "SELECT * FROM adresse ;";
                            Affichage(requete, createinsert);
                            Console.Write("\nEntrez id adresse parmi les adresses déjà enregistrées ou nouveau id adresse: ");
                            string id_adresse = Console.ReadLine();


                            createinsert.CommandText = id_adresse;
                            string req = "SELECT id_adresse from adresse ;";
                            List<string> adr = Reader2(req, createinsert);

                            //Si id adresse n'est pas dans la liste en créer une
                            if (!adr.Contains(id_adresse))
                            {
                                Console.Write("\nEntrez la rue : ");
                                string rue = Console.ReadLine();
                                Console.Write("\nEntrez la ville : ");
                                string ville = Console.ReadLine();
                                Console.Write("\nEntrez code postal : ");
                                string postal = Console.ReadLine();
                                Console.Write("\nEntrez la province : ");
                                string province = Console.ReadLine();
                                string create_adresse = "INSERT INTO adresse VALUES ('" + id_adresse + "','" + rue + "','" + ville + "','" + postal + "','" + province + "');";
                                Console.WriteLine(create_adresse);
                                createinsert.CommandText = create_adresse;
                                createinsert.ExecuteNonQuery();
                            }

                            //on update l'adresse par la nouvelle 
                            string maj6 = "UPDATE boutique SET id_adresse='" + id_adresse + "'WHERE id_contact='" + id_contact + "';";
                            Console.WriteLine(maj6);

                            createinsert.CommandText = maj6;
                            createinsert.ExecuteNonQuery();


                            break;
                        case 6:
                            Console.WriteLine("Entrer nouveau volume d'achat :");
                            rep = Console.ReadLine();
                            string maj7 = "UPDATE boutique SET volume='" + rep + "'WHERE id_contact='" + id_contact + "';";
                            Console.WriteLine(maj7);

                            createinsert.CommandText = maj7;
                            createinsert.ExecuteNonQuery();
                            break;


                        default:

                            break;
                    }
                    Console.WriteLine();
                    Console.WriteLine("Voici la liste des boutiques : ");
                    Console.WriteLine("\tnom    \t\ttel \t\t  courriel\t      id_c  id_adresse  volume");

                    requete = "SELECT * FROM boutique ;";
                    Affichage(requete, createinsert);


                } while (choix != 9);
                do
                {
                    Console.WriteLine("\nVoulez-vous modifier un client boutique ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");

            }
            createinsert.Dispose();
        }

        #endregion

        #region Gestion fournisseurs

        public static void AjoutFournisseur(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous ajouter un fournisseur ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                Console.WriteLine("Voici la liste des fournisseurs :");
                string requete = "SELECT * FROM fournisseur;";
                Affichage(requete, createinsert);
                Console.Write("\nEntrez siret fournisseur : ");
                string siret = Console.ReadLine();
                Console.Write("\nEntrez nom de l'entreprise : ");
                string ent = Console.ReadLine();
                Console.Write("\nEntrez libelle (1-4) : ");
                string libelle = Console.ReadLine();
                //création contact fournisseur
                Console.Write("\nEntrez id contact : ");
                string id_contact = Console.ReadLine();
                Console.Write("\nEntrez nom du contact : ");
                string nom = Console.ReadLine();
                Console.Write("\nEntrez prénom du contact : ");
                string prenom = Console.ReadLine();
                Console.Write("\nEntrez tel : ");
                string tel = Console.ReadLine();
                Console.Write("\nEntrez courriel : ");
                string courriel = Console.ReadLine();

                //création d'une adresse reliée au contact 
                Console.WriteLine("Voici la liste des adresses :");
                requete = "SELECT * FROM adresse ;";
                Affichage(requete, createinsert);
                Console.Write("\nEntrer id_adresse correspondant ou nouveau id adresse: ");
                string id_adresse = Console.ReadLine();
                string req = "SELECT id_adresse from adresse ;";
                List<string> adr = Reader2(req, createinsert);

                if (!adr.Contains(id_adresse))
                {
                    Console.Write("\nEntrez la rue : ");
                    string rue = Console.ReadLine();
                    Console.Write("\nEntrez la ville : ");
                    string ville = Console.ReadLine();
                    Console.Write("\nEntrez code postal : ");
                    string postal = Console.ReadLine();
                    Console.Write("\nEntrez la province : ");
                    string province = Console.ReadLine();
                    string create_adresse = "INSERT INTO adresse VALUES ('" + id_adresse + "','" + rue + "','" + ville + "','" + postal + "','" + province + "');";
                    Console.WriteLine(create_adresse);
                    createinsert.CommandText = create_adresse;
                    createinsert.ExecuteNonQuery();
                }


                string create_contact = "INSERT INTO contact VALUES ('" + id_contact + "','" + nom + "','" + prenom + "','" + tel + "','" + courriel + "',3,'" + id_adresse + "');";
                string create_fournisseur = "INSERT INTO fournisseur VALUES ('" + siret + "','" + ent + "','" + libelle + "','" + id_adresse + "','" + id_contact + "');";

                Console.WriteLine(create_contact);
                Console.WriteLine(create_fournisseur);

                createinsert.CommandText = create_contact;
                createinsert.ExecuteNonQuery();

                createinsert.CommandText = create_fournisseur;
                createinsert.ExecuteNonQuery();


                //Affichage après ajout
                Console.WriteLine("Voici la liste des fournisseurs :");
                requete = "SELECT * FROM fournisseur;";
                Affichage(requete, createinsert);


                do
                {
                    Console.WriteLine("\nVoulez-vous ajouter un autre fournisseur ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");
            }
            createinsert.Dispose();
        }
        public static void SuppressionFournisseur(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous supprimer un fournisseur ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                Console.WriteLine("Voici la liste des fournisseurs :");
                Console.WriteLine("\tsiret\t" + "\tnom_ent" + "  libelle" + "\tadresse" + "\tid_contact");

                string affiche = "SELECT * from fournisseur";
                Affichage(affiche, createinsert);

                //récupère siret du fournisseur à suppr
                Console.Write("\nEntrez id_contact du fournisseur à supprimer: ");
                string id_contact = Console.ReadLine();



                string table1 = "DELETE FROM contact WHERE id_contact='" + id_contact + "';";
                Console.WriteLine(table1);

                createinsert.CommandText = table1;
                createinsert.ExecuteNonQuery();

                Console.WriteLine("\tsiret" + "\tnom_ent" + "  libelle" + "\tadresse" + "\tid_contact");
                Console.WriteLine("Voici la liste des fournisseurs :");
                affiche = "SELECT * from fournisseur";
                Affichage(affiche, createinsert);

                do
                {
                    Console.WriteLine("\nVoulez-vous supprimer un autre fournisseur ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");
            }
            createinsert.Dispose();

        }
        public static void MajFournisseur(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("Voulez-vous modifier un fournisseur ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                Console.WriteLine("Voici la liste des fournisseurs :");
                Console.WriteLine("\tsiret\t" + "\tnom_ent" + " libelle" + "\tadresse" + "\tid_contact");
                string requete = "SELECT * FROM fournisseur;";
                Affichage(requete, createinsert);

                Console.WriteLine();


                Console.Write("\nEntrez id_contact du fournisseur à modifier : ");
                string id_contact = Console.ReadLine();


                int choix;
                do
                {
                    Console.WriteLine("\nQue voulez-vous modifier ? (Tapez 9 pour sortir)\n"
                                     + "1 : siret\n"
                                     + "2 : nom entreprise\n"
                                     + "3 : libelle\n"
                                     + "4 : contact\n"
                                     + "5 : adresse\n"


                                     + "Quel est votre choix ?");

                    choix = int.Parse(Console.ReadLine());
                    switch (choix)
                    {
                        case 1:
                            Console.WriteLine("Entrez nouveau siret:");
                            rep = Console.ReadLine();
                            string maj1 = "UPDATE fournisseur SET siret='" + rep + "'WHERE id_contact='" + id_contact + "';";
                            Console.WriteLine(maj1);

                            createinsert.CommandText = maj1;
                            createinsert.ExecuteNonQuery();

                            break;
                        case 2:
                            Console.WriteLine("Entrez nouveau nom entreprise :");
                            rep = Console.ReadLine();
                            string maj2 = "UPDATE fournisseur SET nom_ent='" + rep + "'WHERE id_contact='" + id_contact + "';";

                            createinsert.CommandText = maj2;
                            createinsert.ExecuteNonQuery();


                            break;
                        case 3:
                            Console.WriteLine("Entrez nouveau libelle :");
                            rep = Console.ReadLine();
                            string maj3 = "UPDATE fournisseur SET libelle='" + rep + "'WHERE id_contact='" + id_contact + "';";

                            createinsert.CommandText = maj3;
                            createinsert.ExecuteNonQuery();

                            break;


                        case 4:
                            Console.WriteLine("Entrez nouveau nom contact");
                            rep = Console.ReadLine();
                            string maj4 = "UPDATE contact SET nom_contact='" + rep + "'WHERE id_contact='" + id_contact + "';";

                            Console.WriteLine("Entrez nouveau prenom contact");
                            string rep1 = Console.ReadLine();
                            string maj4b = "UPDATE contact SET prenom_contact='" + rep1 + "'WHERE id_contact='" + id_contact + "';";

                            createinsert.CommandText = maj4;
                            createinsert.ExecuteNonQuery();
                            createinsert.CommandText = maj4b;
                            createinsert.ExecuteNonQuery();


                            break;

                        case 5:
                            Console.WriteLine("Voici la liste des adresses :");
                            requete = "SELECT * FROM adresse ;";
                            Affichage(requete, createinsert);
                            Console.Write("\nEntrez id adresse parmi les adresses déjà enregistrées ou nouveau id adresse: ");
                            string id_adresse = Console.ReadLine();


                            createinsert.CommandText = id_adresse;
                            string req = "SELECT id_adresse from adresse ;";
                            List<string> adr = Reader2(req, createinsert);

                            //Si id adresse n'est pas dans la liste en créer une
                            if (!adr.Contains(id_adresse))
                            {
                                Console.Write("\nEntrez la rue : ");
                                string rue = Console.ReadLine();
                                Console.Write("\nEntrez la ville : ");
                                string ville = Console.ReadLine();
                                Console.Write("\nEntrez code postal : ");
                                string postal = Console.ReadLine();
                                Console.Write("\nEntrez la province : ");
                                string province = Console.ReadLine();
                                string create_adresse = "INSERT INTO adresse VALUES ('" + id_adresse + "','" + rue + "','" + ville + "','" + postal + "','" + province + "');";
                                Console.WriteLine(create_adresse);
                                createinsert.CommandText = create_adresse;
                                createinsert.ExecuteNonQuery();
                            }

                            //on update l'adresse par la nouvelle 
                            string maj6 = "UPDATE fournisseur SET id_adresse='" + id_adresse + "'WHERE id_contact='" + id_contact + "';";

                            Console.WriteLine(maj6);

                            createinsert.CommandText = maj6;
                            createinsert.ExecuteNonQuery();



                            break;


                        default:

                            break;
                    }
                    Console.WriteLine("Voici la liste des fournisseurs :");
                    Console.WriteLine("\tsiret\t" + "\tnom_ent" + " libelle" + "\tadresse" + "\tid_contact");
                    requete = "SELECT * FROM fournisseur;";
                    Affichage(requete, createinsert);

                    Console.WriteLine("Voici la liste des contact :");
                    requete = "SELECT * FROM contact WHERE id_typecontact=3;";
                    Affichage(requete, createinsert);


                } while (choix != 9);
                do
                {
                    Console.WriteLine("\nVoulez-vous modifier un autre fournisseur ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");

            }
            createinsert.Dispose();
        }

        #endregion

        #region Gestion commandes

        public static void AjoutCommande(MySqlCommand createinsert)
        {
            string rep1 = " ";
            string id_contact = " ";
            string id_commande = " ";
            Random r = new Random();
            string date_commande = " ";
            string date_livraison = " ";
            string id_adresse = " ";
            string rep;
            do
            {
                Console.WriteLine("Voulez vous passer une commande? oui/non");
                rep1 = Console.ReadLine();
            } while (rep1 != "oui" && rep1 != "non");

            while (rep1 == "oui")
            {
                do
                {
                    Console.WriteLine("\nVoici la liste des clients :");
                    Console.WriteLine("  id_client     nom    prenom \t  tel \t\t  courriel \t      type\tadresse");
                    string requete = "SELECT * FROM contact WHERE id_typecontact=1 OR id_typecontact=2;";
                    Affichage(requete, createinsert);

                    Console.Write("\nEntrez votre id_contact : ");
                    id_contact = Console.ReadLine();

                    id_commande = "COM" + id_contact + Convert.ToString(r.Next(99));
                    DateTime dateCommande = DateTime.Now;
                    date_commande = dateCommande.ToString("yyyy-MM-dd");
                    //délai de livraison de 5 jours
                    DateTime dateLivraison = new DateTime(dateCommande.Year, dateCommande.Month, dateCommande.Day + 5);
                    date_livraison = dateLivraison.ToString("yyyy-MM-dd");
                    string requete_adresse = "SELECT id_adresse FROM contact WHERE id_contact='" + id_contact + "';";

                    string[] id_a = Reader(requete_adresse, createinsert);
                    id_adresse = id_a[0];

                    string table = "INSERT INTO commande VALUES ('" + id_commande + "','" + date_commande + "','" + date_livraison + "','" + id_adresse + "','" + id_contact + "');";
                    createinsert.CommandText = table;
                    createinsert.ExecuteNonQuery();

                    Console.WriteLine("\nVoici la liste des commandes :");
                    requete = "SELECT * FROM commande;";
                    Affichage(requete, createinsert);


                    Console.WriteLine("\nVoulez vous commander un modèle ?  oui/non");
                    rep = Console.ReadLine();


                } while (rep != "oui" && rep != "non");


                while (rep == "oui")
                {


                    Console.WriteLine("\nVoici la liste de tous les modèles : ");
                    string modele = "SELECT * FROM bicyclette;";
                    Affichage(modele, createinsert);
                    Console.WriteLine("\nVoici la liste des modèles disponible selon les stock de pièce :");
                    string requete = "SELECT id_modele,nom_modele FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece GROUP BY id_modele HAVING min(stock)>0;";
                    Affichage(requete, createinsert);
                    Console.Write("\nEntrez l'id du modèle que vous voulez : ");
                    string id_modele = Console.ReadLine();


                    //on verifie le stock des pieces pour ce modele
                    string requete1 = "SELECT MIN(stock) FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece WHERE id_modele='" + id_modele + "';";
                    Console.WriteLine("\nQuelle quantité voulez vous pour ce modèle ?");
                    int quantite = int.Parse(Console.ReadLine());
                    string[] minimum_stock = Reader(requete1, createinsert);
                    int ministock = int.Parse(minimum_stock[0]);
                    if ((ministock - quantite) <= 0)
                    {
                        Console.WriteLine("\nVotre modèle ne pourra pas être livré dans un délai de 5 jours car certaines pièces manquent");
                        string requete_pieces_manquantes = "SELECT id_piece FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece WHERE id_modele='" + id_modele + "' AND stock-" + quantite + " < 0 ;";

                        List<string> pieces_manquantes = Reader2(requete_pieces_manquantes, createinsert);
                        Console.WriteLine("\nCes pièces sont manquantes : ");
                        foreach (string i in pieces_manquantes)
                        {
                            Console.WriteLine(i);
                        }

                        Console.WriteLine("\nIl manque " + pieces_manquantes.Count + " piece(s)");
                       

                    }

                    // même si plus en stock on commande et on prévient que le délai va être plus long

                    string table1 = "INSERT INTO lmodele VALUES ('" + id_modele + "','" + id_commande + "'," + quantite + ");";
                    createinsert.CommandText = table1;
                    createinsert.ExecuteNonQuery();
                    //string query="SELECT prix_unit FROM bicyclette WHERE id_modele" List<string>prix=Reader3()
                    Console.WriteLine("\nLe modèle a été ajouté dans votre commande " + id_commande);

                    string requete_affichage_commande = "SELECT * FROM lmodele WHERE id_commande='" + id_commande + "' ;";

                    Console.WriteLine();
                    Affichage(requete_affichage_commande, createinsert);


                    do
                    {
                        Console.WriteLine("\nVoulez-vous ajouter un autre modèle ?  oui/non");
                        rep = Console.ReadLine();

                    } while (rep != "oui" && rep != "non");

                }
                do
                {

                    Console.WriteLine("\nVoulez vous commander une pièce de rechange ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");


                while (rep == "oui")
                {


                    Console.WriteLine("\nVoici la liste de toutes les pieces :");
                    string modele = "SELECT id_piece,description FROM piece;";
                    Affichage(modele, createinsert);
                    Console.WriteLine("\nVoici la liste des pieces disponibles : ");
                    string requete = "SELECT id_piece,description FROM assemblage NATURAL JOIN piece WHERE stock>0;";
                    Affichage(requete, createinsert);
                    Console.WriteLine("Entrez l'id de la piece que vous voulez : ");
                    string id_piece = Console.ReadLine();


                    //on verifie le stock des pieces 
                    string requete1 = "SELECT stock FROM piece  WHERE id_piece='" + id_piece + "';";
                    Console.WriteLine("\nQuelle quantité voulez vous pour cette pièce ? ");
                    int quantite = int.Parse(Console.ReadLine());
                    string[] minimum_stock = Reader(requete1, createinsert);
                    int ministock = int.Parse(minimum_stock[0]);
                    if ((ministock - quantite) <= 0)
                    {
                        Console.WriteLine("\nVotre piece ne pourra pas être livré dans un délai de 5 jours car la quantité demandée est supérieure au stock disponible");

                        string requete_delais_pieces = "SELECT delai_fournisseur FROM fournit WHERE id_piece='" + id_piece + "';";
                        string[] delais = Reader(requete_delais_pieces, createinsert);
                        string delais_piece = delais[0];
                        Console.WriteLine("\ndélai piece =" + delais_piece);
                        int delais_total = int.Parse(delais_piece);

                        DateTime dateCommande = DateTime.Now;
                        date_commande = dateCommande.ToString("yyyy-MM-dd");


                        DateTime dateLivraison = new DateTime(dateCommande.Year, dateCommande.Month, dateCommande.Day + delais_total);
                        date_livraison = dateLivraison.ToString("yyyy-MM-dd");
                        string maj1 = "UPDATE commande SET date_livraison='" + date_livraison + "'WHERE id_commande='" + id_commande + "';";
                        createinsert.CommandText = maj1;
                        createinsert.ExecuteNonQuery();


                        string commande = "SELECT * FROM commande WHERE id_commande='" + id_commande + "';";
                        Console.WriteLine("\nLa date de livraison a été modifiée.");
                        Affichage(commande, createinsert);

                    }

                    string table1 = "INSERT INTO lpiece VALUES ('" + id_piece + "','" + id_commande + "'," + quantite + ");";

                    createinsert.CommandText = table1;
                    createinsert.ExecuteNonQuery();
                    Console.WriteLine("\nLes pièces ont été ajoutées dans votre commande " + id_commande);

                    string requete_affichage_commande = "SELECT * FROM lpiece WHERE id_commande='" + id_commande + "';";
                    Console.WriteLine();
                    Affichage(requete_affichage_commande, createinsert);

                    do
                    {
                        Console.WriteLine("\nVoulez-vous ajouter une autre pièce ?  oui/non");
                        rep = Console.ReadLine();

                    } while (rep != "oui" && rep != "non");

                }
            }
            createinsert.Dispose();

        }
        public static void SuppressionCommande(MySqlCommand createinsert)
        {
            string rep;
            string id_contact;
            do
            {

                Console.WriteLine("\nVoulez-vous supprimer une commande ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                Console.WriteLine("\nVoici la liste des clients :");
                Console.WriteLine("  id_client     nom    prenom \t  tel \t\t  courriel \t      type\tadresse");
                string requete = "SELECT * FROM contact WHERE id_typecontact=1 OR id_typecontact=2;";

                Affichage(requete, createinsert);

                Console.WriteLine("\nEntrez votre id_contact : ");
                id_contact = Console.ReadLine();
                string count_commande_piece = "SELECT count(id_piece) FROM lpiece NATURAL JOIN commande WHERE id_contact='" + id_contact + "';";
                string count_commande_modele = "SELECT count(id_modele) FROM lmodele NATURAL JOIN commande WHERE id_contact='" + id_contact + "';";
                string count_p = Reader(count_commande_piece, createinsert)[0];
                string count_m = Reader(count_commande_modele, createinsert)[0];
                if (Convert.ToInt32(count_p) == 0 && Convert.ToInt32(count_m) == 0) Console.WriteLine("vous n'avez aucune commande");
                if (Convert.ToInt32(count_p) > 0)
                {
                    Console.WriteLine("\nVoici la liste des commandes de piece : ");
                    string requete_afficher_piece = "SELECT id_commande,id_piece, description FROM piece NATURAL JOIN lpiece NATURAL JOIN commande WHERE id_contact='" + id_contact + "';";
                    Affichage(requete_afficher_piece, createinsert);
                }
                if (Convert.ToInt32(count_m) > 0)
                {
                    Console.WriteLine("\nVoici la liste des commandes de modele : ");
                    string requete_afficher_modele = "SELECT id_commande, id_modele, nom_modele FROM commande NATURAL JOIN lmodele NATURAL JOIN bicyclette WHERE id_contact='" + id_contact + "';";
                    Affichage(requete_afficher_modele, createinsert);
                }

                Console.Write("\nEntrez id commande à supprimer: ");
                string id = Console.ReadLine();


                string table = "DELETE FROM commande WHERE id_commande='" + id + "';";

                Console.WriteLine(table);


                createinsert.CommandText = table;



                createinsert.ExecuteNonQuery();
                createinsert.Dispose();

                do
                {
                    Console.WriteLine("\nVoulez-vous supprimer une autre commande ?  oui/non");
                    rep = Console.ReadLine();

                } while (rep != "oui" && rep != "non");
            }
        }
        public static void MajCommande(MySqlCommand createinsert)
        {
            string rep;
            do
            {
                Console.WriteLine("\nVoulez-vous modifier une commande ?  oui/non");
                rep = Console.ReadLine();

            } while (rep != "oui" && rep != "non");


            while (rep == "oui")
            {
                Console.WriteLine("\nVoici la liste des clients :");
                Console.WriteLine("  id_client     nom    prenom \t  tel \t\t  courriel \t      type\tadresse");
                string requete = "SELECT * FROM contact WHERE id_typecontact=1 OR id_typecontact=2;";
                Affichage(requete, createinsert);
                Console.WriteLine("\nEntrez votre id_contact : ");
                string id_contact = Console.ReadLine();
                string requete_commande = "SELECT id_commande FROM commande WHERE id_contact='" + id_contact + "';";
                Console.WriteLine("\nVoici la liste de vos id_commande : ");
                Affichage(requete_commande, createinsert);
                Console.Write("\nEntrez id_commande à modifier : ");
                string id_commande = Console.ReadLine();

                int choix;
                do
                {
                    Console.WriteLine("\nQue voulez-vous modifier ? (Tapez 9 pour sortir)\n"
                                     + "1 : Repousser la date de livraison\n"
                                     + "2 : Adresse\n"

                                     + "Quel est votre choix ?");
                    choix = int.Parse(Console.ReadLine());
                    switch (choix)
                    {
                        case 1:
                            Console.WriteLine("\nDe combien de jour voulez vous repousser la livraison? :");
                            int reponse = Convert.ToInt32(Console.ReadLine());
                            string requete_livraison = "SELECT date_livraison FROM commande WHERE id_commande='" + id_commande + "';";
                            string[] dateliv = Reader(requete_livraison, createinsert);
                            string date = dateliv[0];
                            DateTime date2 = DateTime.Parse(date);
                            Console.WriteLine(date2);
                            date2 = date2.AddDays(reponse);
                            string date_commande = date2.ToString("yyyy-MM-dd");

                            //string[] tab = date.Split('/');
                            //int jour = Convert.ToInt32(tab[2][) + reponse;

                            //date = tab[0] + "-" + tab[1] + "-" + Convert.ToString(jour);
                            string maj1 = "UPDATE commande SET date_livraison='" + date_commande + "'WHERE id_commande='" + id_commande + "';";
                            Console.WriteLine(maj1);


                            createinsert.CommandText = maj1;
                            createinsert.ExecuteNonQuery();
                            string commande = "SELECT * FROM commande WHERE id_commande='" + id_commande + "';";
                            Console.WriteLine("\nLa date de livraison a été modifiée.");
                            Affichage(commande, createinsert);

                            break;
                        case 2:

                            Console.WriteLine("\nVoici votre adresse actuelle : ");
                            string requete_adresse = "SELECT id_adresse FROM adresse NATURAL JOIN contact WHERE id_contact='" + id_contact + "';";


                            Console.WriteLine("\nVoici la liste des adresses :");
                            requete = "SELECT * FROM adresse ;";
                            Affichage(requete, createinsert);
                            Console.Write("\nEntrer id_adresse : ");
                            string id_adresse = Console.ReadLine();

                            //renseignement adresse
                            string req = "SELECT id_adresse from adresse ;";
                            List<string> adr = Reader2(req, createinsert);
                            if (!adr.Contains(id_adresse))
                            {
                                Console.Write("\nEntrez la rue : ");
                                string rue = Console.ReadLine();
                                Console.Write("\nEntrez la ville : ");
                                string ville = Console.ReadLine();
                                Console.Write("\nEntrez code postal : ");
                                string postal = Console.ReadLine();
                                Console.Write("\nEntrez la province : ");
                                string province = Console.ReadLine();
                                string create_adresse = "INSERT INTO adresse VALUES ('" + id_adresse + "','" + rue + "','" + ville + "','" + postal + "','" + province + "');";
                                Console.WriteLine(create_adresse);
                                createinsert.CommandText = create_adresse;
                                createinsert.ExecuteNonQuery();
                            }
                            string maj2 = "UPDATE commande SET id_adresse='" + id_adresse + "'WHERE id_contact='" + id_contact + "';";

                            createinsert.CommandText = maj2;

                            createinsert.ExecuteNonQuery();
                            commande = "SELECT * FROM commande WHERE id_commande='" + id_commande + "';";
                            Affichage(commande, createinsert);


                            break;
                        default:
                            break;
                    }

                } while (choix != 9);
            }
            createinsert.Dispose();
        }


        #endregion

        #region 
        public static void stockPiece(MySqlCommand createinsert)
        {
            

            Console.WriteLine("Voici le stock pour chaque pièce produite : ");

            string stock = "SELECT id_piece, description, stock FROM piece;";
            Affichage(stock, createinsert);
            string ifPieceOutOfStock = "SELECT count(id_piece) FROM piece WHERE stock=0;";
            List<string> countPieceOut = Reader2(ifPieceOutOfStock, createinsert);
            if (Convert.ToInt32(countPieceOut[0]) > 0)
            {
                string rep3 = " ";

                Console.WriteLine("certaines pièces ont un stock saturé, en voici la liste : ");
                string requete_piece_sat = "SELECT id_piece, description FROM piece WHERE stock=0;";

                Affichage(requete_piece_sat,createinsert);
                do
                {
                    Console.WriteLine("Voulez vous passer commande auprès de vos fournisseurs?  ");
                    rep3 = Console.ReadLine();
                } while (rep3 != "oui" && rep3 != "non");

                string id_piece = " ";
                string requete_fournisseur = " ";
                
                double quantite = 0;
                
                while (rep3 == "oui")
                {
                    Console.WriteLine("Saisissez l'id pièce que vous voulez commander : ");
                    id_piece = Console.ReadLine();
                    Console.WriteLine("Quelle quantité de cette pièce voulez-vous?  ");
                    quantite = Convert.ToDouble(Console.ReadLine());

                    //Affichage fournisseurs triés selon libelle
                    Console.WriteLine("Voici la liste des fournisseurs triée selon votre critère de libellé : ");
                    requete_fournisseur = "SELECT nom_ent, siret, prix_piece, delai_fournisseur FROM fournit NATURAL JOIN piece  NATURAL JOIN fournisseur WHERE id_piece='" + id_piece + "' ORDER BY libelle;";
                    Affichage(requete_fournisseur, createinsert);

                    Console.Write("\nSélectionnez le nom de l'entreprise que vous voulez parmi cette liste : ");
                    string nom_ent = Console.ReadLine();
                    Console.WriteLine("Votre commande sera donc passée pour ce fournisseur avec le prix correspondant à la quantité demandée : ");
                    string fournisseur = "SELECT nom_ent, siret, prix_piece*" + quantite + " FROM fournit NATURAL JOIN fournisseur WHERE nom_ent='" + nom_ent + "' AND id_piece='" + id_piece + "';";
                    Affichage(fournisseur, createinsert);


                    string maj1 = "UPDATE piece SET stock=" + quantite + "WHERE id_piece='" + id_piece + "';";
                    createinsert.CommandText = maj1;
                    createinsert.ExecuteNonQuery();
                    createinsert.Dispose();

                    do
                    {
                        Console.WriteLine("Voulez vous revoir l'aperçu stock pièce ? : ");
                        rep3 = Console.ReadLine();

                    } while (rep3 != "oui" && rep3 != "non");
                }
                createinsert.Dispose();

            }
            Console.WriteLine("tapez entrer pour sortir");
            Console.ReadKey();


        }

        public static void stock(MySqlCommand createinsert)
        {
            int choix1;
            do
            {
                Console.Clear();
                Console.WriteLine("Accès au stock par :\n"
                                 + "1 : pièce \n"
                                 + "2 : fournisseur \n"
                                 + "3 : vélos\n"

                                 + "Quel est votre choix ?");
                choix1 = int.Parse(Console.ReadLine());
                switch (choix1)
                {
                    #region    Menu            

                    case 1:

                        stockPiece(createinsert);


                        break;
                    case 2:
                        stockFournisseur(createinsert);
                        break;
                    case 3:
                        stockVelo(createinsert);
                        break;
                    default:

                        break;
                }
                #endregion

                Console.WriteLine("Tapez 9 pour sortir");
                Console.ReadKey();
            } while (choix1 != 9);
        }

        public static void stockFournisseur(MySqlCommand createinsert)
        {

            Console.WriteLine("Voici la liste des differents fournisseurs et des pièces qu'ils fournissent : ");
            string piece_fournisseur = "SELECT nom_ent, id_piece,description FROM fournisseur NATURAL JOIN fournit NATURAL JOIN PIECE ORDER BY nom_ent;";
            Affichage(piece_fournisseur, createinsert);
            Console.WriteLine("tapez entrer pour sortir;");
            Console.ReadKey();
        }
        public static void stockVelo(MySqlCommand createinsert)
        {


            int choix;
            do
            {
                Console.Clear();
                Console.WriteLine("Stock de velo selon les critères suivant :\n"
                                 + "1 : tous les vélos \n"
                                 + "2 : par catégorie\n"

                                 + "Quel est votre choix ?");
                choix = int.Parse(Console.ReadLine());
                switch (choix)
                {
                    #region    Menu            

                    case 1:

                        Console.WriteLine("voici le stock de velos : ");
                        string requete = "SELECT nom_modele, min(stock) as stock FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece GROUP BY id_modele ORDER BY stock;";
                        Affichage(requete, createinsert);


                        break;
                    case 2:
                        Console.WriteLine("Voici les stocks pour chaque catégories : ");
                        string requete2 = "SELECT a.ligne, sum(a.stock) FROM (SELECT min(stock) as stock, ligne FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece GROUP BY id_modele ORDER BY stock) a GROUP BY ligne;";
                        Affichage(requete2, createinsert);
                        break;

                    default:

                        break;
                }
                #endregion

                Console.WriteLine("Tapez 9 pour sortir");
                Console.ReadKey();
            } while (choix != 9);



        }
        public static void statQuantite(MySqlCommand createinsert)
        {
            int choix1;
            do
            {
                Console.Clear();
                Console.WriteLine("Que voulez vous savoir sur les quantités vendues ? \n"
                                 + "1 : liste des quantités vendues pour chaque item \n"
                                 + "2 : item le plus vendu (piece et velo) \n"
                                 + "3 : item le moins vendu \n"

                                 + "Quel est votre choix ?");
                choix1 = int.Parse(Console.ReadLine());
                switch (choix1)
                {
                    #region    Menu            

                    case 1:
                        Console.WriteLine("\nQuantité vendue pour chaque id de piece : ");
                        string pieceVendu = "SELECT id_piece,description, sum(qte_piece)as quantiteVendu FROM piece NATURAL JOIN lpiece GROUP BY id_piece;";
                        Affichage(pieceVendu, createinsert);
                        Console.WriteLine("\nQuantité vendue pour chaque vélo : ");
                        string veloVendu = "SELECT id_modele,nom_modele, sum(qte_modele) as quantiteVendu FROM bicyclette NATURAL JOIN lmodele GROUP BY id_modele;";
                        Affichage(veloVendu, createinsert);

                        break;
                    case 2:
                        Console.WriteLine("\nPiece la plus vende suivi du prix : ");
                        string requete = "SELECT id_piece,description, sum(qte_piece) as quantiteVendu FROM piece NATURAL JOIN lpiece GROUP BY id_piece ORDER BY quantiteVendu DESC;";
                        List<string> piecePlus = Reader2(requete, createinsert);
                        Console.WriteLine(piecePlus[0] + "\t" + piecePlus[1]);
                        Console.WriteLine("\nModele le plus vendu suivi du prix : ");
                        string requete2 = "SELECT id_modele, nom_modele, sum(qte_modele) as quantiteVendu FROM bicyclette NATURAL JOIN lmodele GROUP BY id_modele ORDER BY quantiteVendu DESC;";
                        List<string> modelePlus = Reader2(requete2, createinsert);
                        Console.WriteLine(modelePlus[0] + "\t" + modelePlus[1]);
                        break;
                    case 3:
                        Console.WriteLine("\nPiece la plus vendue suivi du prix : ");
                        string requete3 = "SELECT id_piece,description, sum(qte_piece) as quantiteVendu FROM piece NATURAL JOIN lpiece GROUP BY id_piece ORDER BY quantiteVendu;";
                        List<string> pieceMoins = Reader2(requete3, createinsert);
                        Console.WriteLine(pieceMoins[0] + "\t" + pieceMoins[1]);
                        Console.WriteLine("\nModele le plus vendu suivi du prix : ");
                        string requete4 = "SELECT id_modele, nom_modele, sum(qte_modele) as quantiteVendu FROM bicyclette NATURAL JOIN lmodele GROUP BY id_modele ORDER BY quantiteVendu;";
                        List<string> modeleMoins = Reader2(requete4, createinsert);
                        Console.WriteLine(modeleMoins[0] + "\t" + modeleMoins[1]);
                        break;
                    default:

                        break;
                }
                #endregion

                Console.WriteLine("\nTapez 9 pour sortir");
                Console.ReadKey();
            } while (choix1 != 9);
        }
        public static void listMembre(MySqlCommand createinsert)
        {
            Console.WriteLine("\nListe des membres du progrmme Fidélio : ");
            string fidelio = "SELECT nom_contact, prenom_contact FROM contact NATURAL JOIN adhesion NATURAL JOIN programme WHERE id_programme=1; ";
            Affichage(fidelio, createinsert);
            Console.WriteLine("\nListe des membres du programme Fidelio Or : ");
            string fidelioOr = "SELECT nom_contact, prenom_contact FROM contact NATURAL JOIN adhesion NATURAL JOIN programme WHERE id_programme=2 ; ";
            Affichage(fidelioOr, createinsert);
            Console.WriteLine("\nListe des membres du programme Fidelio Platine : ");
            string fidelioP = "SELECT nom_contact, prenom_contact FROM contact NATURAL JOIN adhesion NATURAL JOIN programme WHERE id_programme=3 ; ";
            Affichage(fidelioP, createinsert);
            Console.WriteLine("\nListe des membres du programme Fidelio Max : ");
            string fidelioMax = "SELECT nom_contact, prenom_contact FROM contact NATURAL JOIN adhesion NATURAL JOIN programme WHERE id_programme=4 ; ";
            Affichage(fidelioMax, createinsert);
        }
        public static void listBestClient(MySqlCommand createinsert)
        {
            Console.WriteLine("\nVoici la liste des meilleures clients suivant les quantités achetées (piece+velo) : ");
            string requete = "SELECT nom_contact, prenom_contact, p1.pieceAchete+p2.modeleAchete as volumeItem FROM (SELECT nom_contact,prenom_contact, sum(qte_piece) as pieceAchete FROM contact NATURAL JOIN commande NATURAL JOIN lpiece GROUP BY id_contact ORDER BY nom_contact) p1 NATURAL JOIN (SELECT nom_contact,prenom_contact, sum(qte_modele) as modeleAchete FROM contact NATURAL JOIN commande NATURAL JOIN lmodele GROUP BY id_contact ORDER BY nom_contact) p2 GROUP BY nom_contact ORDER BY volumeItem DESC;";
            List<string> bestClient = Reader3(requete, createinsert);
            int i = 0;
            while (i <= 6)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(bestClient[i + j] + "\t");
                }
                Console.WriteLine();
                i += 3;
            }
        }
        public static void anaCommande(MySqlCommand createinsert)
        {
            string requete = "SELECT AVG(p1.quantitePiece) as qte_piece_moyenne FROM (SELECT id_commande, sum(qte_piece) as quantitePiece FROM commande NATURAL JOIN lpiece GROUP BY id_commande ORDER BY id_commande) p1;";


            string requete2 = "SELECT AVG(p2.quantiteVelo) as qte_velo_moyenne FROM (SELECT id_commande, sum(qte_modele) as quantiteVelo FROM commande NATURAL JOIN lmodele GROUP BY id_commande ORDER BY id_commande) p2;";


            List<string> qtePieceAvg = Reader2(requete, createinsert);
            List<string> qteVeloAvg = Reader2(requete2, createinsert);
            Console.WriteLine("\nLa moyenne du nombre de pieces par commande est de : " + qtePieceAvg[0]);

            Console.WriteLine("\nLa moyenne du nombre de velos par commande est de : " + qteVeloAvg[0]);
        

            string requete3 = "SELECT AVG(p.montant) FROM (SELECT id_commande,sum(qte_modele*prix_unit) as montant FROM commande NATURAL JOIN lmodele NATURAL JOIN bicyclette GROUP BY id_commande) p;";
            List<string> montantCommandeAvg = Reader2(requete3, createinsert);
            Console.WriteLine("\nLa moyenne des montants par commande de velo est de : " + montantCommandeAvg[0] + " euros");
        }
        public static void listeExpAdhesion(MySqlCommand createinsert)
        {
            string query = "SELECT nom_contact, prenom_contact, date_fin FROM contact NATURAL JOIN adhesion ORDER BY date_fin;";
            Console.WriteLine("\nVoici la liste des dates d'adhésion trié par expiration : ");
            Affichage(query, createinsert);
        }

        public static void moduleStatistique(MySqlCommand maRequete)
        {
            int choix1;
            do
            {
                Console.Clear();
                Console.WriteLine(" Quel type de stats voulez vous voir ? \n"
                                 + "1 : Quantités vendues de chaque item \n"
                                 + "2 : Liste des membres pour chaque programme d'adhésion \n"
                                 + "3 : Liste des dates d'expiration des adhésions\n"
                                 + "4 : Liste des meilleurs clients\n"
                                 + "5 : Analyse des commandes\n"
                                 + "Quel est votre choix ?");
                choix1 = int.Parse(Console.ReadLine());
                switch (choix1)
                {
                    #region    Menu            

                    case 1:
                        statQuantite(maRequete);
                        break;
                    case 2:

                        listMembre(maRequete);
                        break;
                    case 3:
                        listeExpAdhesion(maRequete);
                        break;
                    case 4:
                        listBestClient(maRequete);
                        break;
                    case 5:
                        anaCommande(maRequete);
                        break;
                    default:

                        break;
                }
                #endregion

                Console.WriteLine("\nTapez 9 pour sortir");
                Console.ReadKey();
            } while (choix1 != 9);
        }
        public static void exportXML(string username, string password)
        {
            string query = "SELECT id_piece, description, stock, nom_ent FROM piece NATURAL JOIN fournit NATURAL JOIN fournisseur p1 NATURAL JOIN fournisseur p2 WHERE stock<10 ORDER BY description, nom_ent;";
            MySqlCommand command = new MySqlCommand(query, MaConnexion(username, password));
            MaConnexion(username, password).CreateCommand();
            DataTable dt = new DataTable("fournisseurs");
            MySqlDataAdapter sda = new MySqlDataAdapter(command);
            sda.Fill(dt);
            dt.WriteXml("C:\\BDD_intero\\fournisseur.xml");
            
            MaConnexion(username, password).Close();
        }

        #endregion





        static void Main(string[] args)
        {
            Console.WriteLine("Veuillez rentrer votre identifiant : ");
            string username = Console.ReadLine();
            Console.WriteLine("Veuillez rentrer votre mot de passe : ");
            string password = Console.ReadLine();
            MySqlCommand createinsert = MaConnexion(username, password).CreateCommand();


            int choix;
            do
            {
                Console.Clear();
                Console.WriteLine("Menu :\n"
                                 + "1 : Gestion pièces de rechanges et vélos\n"
                                 + "2 : Gestion clients (particuliers et entreprise)\n"
                                 + "3 : Gestion fournisseur\n"
                                 + "4 : Gestion commande\n"
                                 + "5 : Gestion stock\n"
                                 + "6 : Module Statistiques\n"
                                 + "7 : Export des stocks faibles en XML\n"
                                 +"8 : Info supplémentaire (requêtes synchronisées, auto-jointure, union)\n"
                                 + "Quel est votre choix ?");
                choix = int.Parse(Console.ReadLine());
                switch (choix)
                {
                    #region    Menu            

                    case 1:
                        AjoutBicyclette(createinsert);
                        SuppressionBicyclette(createinsert);
                        MajBicyclette(createinsert);

                        AjoutPiece(createinsert);
                        SuppressionPiece(createinsert);
                        MajPiece(createinsert);


                        break;
                    case 2:
                        AjoutClientParticulier(createinsert);
                        SuppresionClientParticulier(createinsert);
                        MajClientParticulier(createinsert);

                        AjoutClientBoutique(createinsert);
                        SuppressionClientBoutique(createinsert);
                        MajClientBoutique(createinsert);

                        break;
                    case 3:
                        AjoutFournisseur(createinsert);
                        SuppressionFournisseur(createinsert);
                        MajFournisseur(createinsert);

                        break;
                    case 4:
                        AjoutCommande(createinsert);
                        SuppressionCommande(createinsert);
                        MajCommande(createinsert);

                        break;
                    case 5:
                        stock(createinsert);
                        break;
                    case 6:
                        moduleStatistique(createinsert);
                        break;
                    case 7:
                        exportXML(username, password);
                        Process.Start("C:\\BDD_intero\\fournisseur.xml");
                        break;
                  
                    case 8:
                        string query = "SELECT C1.nom_contact, C2.nom_contact FROM contact C1, contact C2 WHERE C1.id_adresse=C2.id_adresse AND C1.nom_contact<C2.nom_contact;";
                        Console.WriteLine("Voici les contacts qui ont inscrit la même adresse : ");
                        Affichage(query,createinsert);

                        string query2 = "SELECT V.id_modele,V.nom_modele FROM bicyclette V WHERE V.prix_unit< (SELECT AVG(V1.prix_unit) FROM bicyclette V1);";
                        Console.WriteLine("\nVoici la liste des vélos ayant un prix inférieur à la moyenne du prix des vélos : ");
                        Affichage(query2, createinsert);

                        string query3 = "SELECT id_modele item, id_commande, qte_modele qte_item FROM lmodele UNION SELECT * FROM lpiece ORDER BY id_commande ;";
                        Console.WriteLine("\nVoici la liste des commandes regroupant les pieces et les modeles tout confondu : ");
                        Affichage(query3, createinsert);
                        break;


                    default:

                        break;
                }
                #endregion

                Console.WriteLine("\nTapez 9 pour sortir");
                Console.ReadKey();
            } while (choix != 9);
            MaConnexion(username, password).Close();
            Console.ReadKey();
        }
    }
}
