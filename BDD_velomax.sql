DROP DATABASE IF EXISTS velomax;
CREATE DATABASE IF NOT EXISTS velomax;
USE velomax;
#CREATE USER 'bozo'@'localhost' IDENTIFIED BY 'bozo';
#GRANT SELECT ON * . * TO 'bozo'@'localhost';



CREATE TABLE bicyclette(
   id_modele VARCHAR(25),
   nom_modele VARCHAR(50),
   grandeur VARCHAR(50),
   prix_unit float,
   ligne VARCHAR(50),
   date_intro DATE,
   date_disc DATE,
   PRIMARY KEY(id_modele)
);

CREATE TABLE piece(
   id_piece VARCHAR(50),
   description VARCHAR(50),
   nom_fournisseur VARCHAR(50),
   id_catalogue VARCHAR(50),
   date_intro_piece DATE,
   date_disc_piece DATE,
   stock INT,
   PRIMARY KEY(id_piece)
);

CREATE TABLE typeContact(
   id_typecontact INT,
   libelle VARCHAR(25),
   PRIMARY KEY(id_typecontact)
);


CREATE TABLE adresse(
   id_adresse VARCHAR(50),
   rue VARCHAR(50),
   ville VARCHAR(50),
   code_postal VARCHAR(5),
   province VARCHAR(50),
   PRIMARY KEY(id_adresse)
);


CREATE TABLE programme(
   id_programme INT,
   description VARCHAR(50),
   cout FLOAT,
   duree INT,
   rabais INT,
   PRIMARY KEY(id_programme)
);

   
CREATE TABLE contact(
   id_contact VARCHAR(50),
   nom_contact VARCHAR(50),
   prenom_contact VARCHAR(50),
   tel VARCHAR(10),
   courriel VARCHAR(50),
   id_typecontact INT,
   id_adresse VARCHAR(50),
   PRIMARY KEY(id_contact),
   FOREIGN KEY(id_adresse) REFERENCES adresse(id_adresse) ON UPDATE CASCADE ON DELETE CASCADE,
   FOREIGN KEY(id_typecontact) REFERENCES typeContact(id_typecontact) ON UPDATE CASCADE ON DELETE CASCADE
);


CREATE TABLE commande(
   id_commande VARCHAR(50),
   date_commande DATE,
   date_livraison DATE,
   id_adresse VARCHAR(50) NOT NULL,
   id_contact VARCHAR(50) NOT NULL,
   PRIMARY KEY(id_commande),
   FOREIGN KEY(id_adresse) REFERENCES adresse(id_adresse) ON UPDATE CASCADE ON DELETE CASCADE,
   FOREIGN KEY(id_contact) REFERENCES contact(id_contact) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE remise(
   id_remise VARCHAR(50),
   volume INT,
   remise INT,
   PRIMARY KEY(id_remise)
   );

CREATE TABLE boutique(
   nom_compagnie VARCHAR(50),
   tel_b VARCHAR(10),
   courriel_b VARCHAR(50),
   id_contact VARCHAR(50),
   id_adresse VARCHAR(50),
   id_remise VARCHAR(50),
   PRIMARY KEY(nom_compagnie),
   FOREIGN KEY(id_remise) REFERENCES Remise(id_remise),
   FOREIGN KEY(id_contact) REFERENCES contact(id_contact) ON UPDATE CASCADE ON DELETE CASCADE,
   FOREIGN KEY(id_adresse) REFERENCES adresse(id_adresse) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE fournisseur(
   siret VARCHAR(14),
   nom_ent VARCHAR(50),
   libelle INT CHECK (libelle<=4),
   id_adresse VARCHAR(50) NOT NULL,
   id_contact VARCHAR(50) NOT NULL,
   PRIMARY KEY(siret),
   FOREIGN KEY(id_adresse) REFERENCES adresse(id_adresse) ON UPDATE CASCADE ON DELETE CASCADE,
   FOREIGN KEY(id_contact) REFERENCES contact(id_contact) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE lpiece(
   id_piece VARCHAR(50),
   id_commande VARCHAR(50),
   qte_piece DOUBLE,
   PRIMARY KEY(id_piece, id_commande),
   FOREIGN KEY(id_piece) REFERENCES piece(id_piece) ON UPDATE CASCADE ON DELETE CASCADE,
   FOREIGN KEY(id_commande) REFERENCES commande(id_commande) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE lmodele(
   id_modele VARCHAR(50),
   id_commande VARCHAR(50),
   qte_modele DOUBLE,
   PRIMARY KEY(id_modele, id_commande),
   FOREIGN KEY(id_modele) REFERENCES bicyclette(id_modele) ON UPDATE CASCADE ON DELETE CASCADE,
   FOREIGN KEY(id_commande) REFERENCES commande(id_commande) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE assemblage(
   id_modele VARCHAR(50),
   id_piece VARCHAR(50),
   PRIMARY KEY(id_modele, id_piece),
   FOREIGN KEY(id_modele) REFERENCES bicyclette(id_modele) ON UPDATE CASCADE ON DELETE CASCADE,
   FOREIGN KEY(id_piece) REFERENCES piece(id_piece) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE fournit(
   id_piece VARCHAR(50),
   siret VARCHAR(14),
   prix_piece DOUBLE,
   delai_fournisseur INT,
   PRIMARY KEY(id_piece, siret),
   FOREIGN KEY(id_piece) REFERENCES piece(id_piece) ON UPDATE CASCADE ON DELETE CASCADE,
   FOREIGN KEY(siret) REFERENCES fournisseur(siret) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE adhesion(
   id_contact VARCHAR(50),
   id_programme INT,
   date_adhésion DATE,
   date_fin DATE,
   PRIMARY KEY(id_contact, id_programme),
   FOREIGN KEY(id_contact) REFERENCES contact(id_contact) ON UPDATE CASCADE ON DELETE CASCADE,
   FOREIGN KEY(id_programme) REFERENCES programme(id_programme) ON UPDATE CASCADE ON DELETE CASCADE
);



-- Insertion dans table bicyclette :
INSERT INTO bicyclette VALUES ('101','Kilimandjaro','Adultes',569,'VTT','2020-12-25',null);
INSERT INTO bicyclette VALUES ('102','NorthPole','Adultes',329,'VTT','2020-12-25','2001-12-25');
INSERT INTO bicyclette VALUES ('103','MontBlanc','Jeunes',399,'VTT','2020-12-25',null);
INSERT INTO bicyclette VALUES ('104','Hooligan','Jeunes',199,'VTT','2020-12-25',null);
INSERT INTO bicyclette VALUES ('105','Orléans','Hommes',229,'Vélo de course','2020-12-25',null);
INSERT INTO bicyclette VALUES ('106','Orléans','Dames',229,'Vélo de course','2020-12-25',null);
INSERT INTO bicyclette VALUES ('107','BlueJay','Hommes',349,'Vélo de course','2020-12-25',null);
INSERT INTO bicyclette VALUES ('108','BlueJay','Dames',349,'Vélo de course','2020-12-25',null);
INSERT INTO bicyclette VALUES ('109','Trail Explorer','Filles',129,'Classique','2020-12-25',null);
INSERT INTO bicyclette VALUES ('110','Trail Explorer','Garçons',129,'Classique','2020-12-25',null);

-- Insertion dans table piece : 
INSERT INTO piece VALUES ('C32','Cadre','Alex','CF01','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('C34','Cadre','Alex','CF02','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('C76','Cadre','Alex','CF03','2000-10-25','2020-10-25',0);
INSERT INTO piece VALUES ('G7','Guidon','Seb','CF04','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('G9','Guidon','Seb','CF05','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('F3','Frein','Jean','CF06','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('S88','Selle','Seb','CF07','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('S37','Selle','Seb','CF08','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('DV133','Derailleur_Avant','Seb','CF09','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('DV17','Derailleur_Avant','Seb','CF10','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('DR56','Derailleur_Arriere','Seb','CF11','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('DR87','Derailleur_Arriere','Seb','CF12','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('R45','Roue_Avant','Seb','CF45','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('R48','Roue_Avant','Seb','CF48','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('R47','Roue_arriere','MisterV','CF47','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('R46','Roue_arriere','MisterV','CF46','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('P12','Pedalier','MisterV','CF12','2000-10-25','2020-10-25',1);
INSERT INTO piece VALUES ('O2','Ordinateur','MisterV','CF2','2000-10-25','2020-10-25',1);


-- Insertion dans table adresse :
INSERT INTO adresse VAlUES('A01','10 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A02','11 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A03','12 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A04','13 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A05','14 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A06','15 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A07','16 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A08','17 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A09','18 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A10','19 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');
INSERT INTO adresse VAlUES('A11','20 rue Leonard de Vinci','Courbevoie','92000','Ile-de-France');



-- Insertion dans table typecontact :
INSERT INTO typecontact VALUES(1,'Particulier');
INSERT INTO typecontact VALUES(2,'Boutique');
INSERT INTO typecontact VALUES(3,'Fournisseur');

-- Insertion dans table contact :
INSERT INTO contact VALUES ('C01','Stark','Tony','0612121212','IamIronMan@hotmail.fr',1,'A01');
INSERT INTO contact VALUES ('C02','Uzumaki','Naruto','0612121212','kyubi@hotmail.fr',1,'A01');
INSERT INTO contact VALUES ('C03','Hatake','Kakashi','0612121212','ninjacopieur@hotmail.fr',1,'A03');

INSERT INTO contact VALUES ('C04','Rogers','Steve','0610121314','captainAmerica@hotmail.fr',2,'A02');
INSERT INTO contact VALUES ('C05','Banner','Bruce','0612121217','Hulkvert@hotmail.fr',2,'A09');
INSERT INTO contact VALUES ('C06','Romanoff','Natasha','0612121218','BlackWidow@hotmail.fr',2,'A10');

INSERT INTO contact VALUES ('C07','Potter','Harry','0612141516','harrypotter@hotmail.fr',3,'A04');
INSERT INTO contact VALUES ('C08','Granger','Hermione','0612121216','hermioneG@hotmail.fr',3,'A05');
INSERT INTO contact VALUES ('C09','Weasley','Ron','0612121215','weasleyron@hotmail.fr',3,'A06');
INSERT INTO contact VALUES ('C10','Romanoff','Natasha','0612121218','BlackWidow@hotmail.fr',3,'A07');
INSERT INTO contact VALUES ('C11','Van damme','Jean-Claude','0612121219','Jean-ClaudeVD@hotmail.fr',3,'A08');
INSERT INTO contact VALUES ('C12','Uchiwa','Madara','0612121220','MadaraUchiwa@hotmail.fr',3,'A11');

-- Insertion remise :
INSERT INTO remise VALUES ("0",0, 0);
INSERT INTO remise VALUES("1",10000,15);
INSERT INTO remise VALUES("2",20000,35);

-- Insertion dans table boutique :
INSERT INTO boutique VALUES ('Decathlon','0111223344','decathlon@hotmail.fr','C04','A02','0');
INSERT INTO boutique VALUES ('Ride Bike','0111223345','ridebike@hotmail.fr','C05','A09','0');
INSERT INTO boutique VALUES ('Rouleboul','0111223346','decathlon@hotmail.fr','C06','A10','0');

-- Insertion dans table programme :
INSERT INTO programme VALUES (1,'Fidélio',15,1,5);
INSERT INTO programme VALUES (2,'Fidélio Or',25,2,8);
INSERT INTO programme VALUES (3,'Fidélio Platine',60,2,10);
INSERT INTO programme VALUES (4,'Fidélio Max',100,3,12);

-- Insertion dans table commande :
INSERT INTO commande VALUES ('COMC0110','2020-12-10','2020-12-23','A01','C01');
INSERT INTO commande VALUES ('COMC0211','2020-12-10','2020-12-23','A01','C02');
INSERT INTO commande VALUES ('COMC0112','2020-12-10','2020-12-23','A01','C01');
INSERT INTO commande VALUES ('COMC0113','2020-12-10','2020-12-23','A01','C01');
INSERT INTO commande VALUES ('COMC0214','2020-12-10','2020-12-23','A01','C02');
INSERT INTO commande VALUES ('COMC0315','2020-12-10','2020-12-23','A03','C03');

-- Insertion dans table fournisseur :
INSERT INTO fournisseur VALUES ('00011122200014','VttMax',1,'A04','C07');
INSERT INTO fournisseur VALUES ('00011122200015','SpeedMe',2,'A05','C08');
INSERT INTO fournisseur VALUES ('00011122200016','Bikeland',2,'A06','C09');
INSERT INTO fournisseur VALUES ('00011122200017','GoRide',3,'A07','C10');
INSERT INTO fournisseur VALUES ('00011122200018','Velopark',3,'A08','C11');
INSERT INTO fournisseur VALUES ('00011122200019','PieceBike',4,'A11','C12');

-- Insertion dans table assemblage :
INSERT INTO assemblage VALUES('101','C32');
INSERT INTO assemblage VALUES('101','G7');
INSERT INTO assemblage VALUES('101','F3');
INSERT INTO assemblage VALUES('101','S88');
INSERT INTO assemblage VALUES('101','DV133');
INSERT INTO assemblage VALUES('101','DR56');
INSERT INTO assemblage VALUES('101','R45');
INSERT INTO assemblage VALUES('101','R46');
INSERT INTO assemblage VALUES('101','P12');
INSERT INTO assemblage VALUES('101','O2');


INSERT INTO assemblage VALUES('102','C34');
INSERT INTO assemblage VALUES('102','G7');
INSERT INTO assemblage VALUES('102','F3');
INSERT INTO assemblage VALUES('102','S88');
INSERT INTO assemblage VALUES('102','DV17');
INSERT INTO assemblage VALUES('102','DR87');
INSERT INTO assemblage VALUES('102','R48');
INSERT INTO assemblage VALUES('102','R47');
INSERT INTO assemblage VALUES('102','P12');

INSERT INTO assemblage VALUES('103','C76');
INSERT INTO assemblage VALUES('103','G7');
INSERT INTO assemblage VALUES('103','F3');
INSERT INTO assemblage VALUES('103','S88');
INSERT INTO assemblage VALUES('103','DV17');
INSERT INTO assemblage VALUES('103','DR87');
INSERT INTO assemblage VALUES('103','R48');
INSERT INTO assemblage VALUES('103','R47');
INSERT INTO assemblage VALUES('103','P12');
INSERT INTO assemblage VALUES('103','O2');

-- Insertion dans table fournit
INSERT INTO fournit VALUES('C32','00011122200014',45,7);
INSERT INTO fournit VALUES('C34','00011122200014',40,4);
INSERT INTO fournit VALUES('C76','00011122200014',40,4);
INSERT INTO fournit VALUES('G7','00011122200014',40,4);
INSERT INTO fournit VALUES('F3','00011122200014',40,4);
INSERT INTO fournit VALUES('S88','00011122200014',40,4);


INSERT INTO fournit VALUES('C32','00011122200015',30,7);
INSERT INTO fournit VALUES('C34','00011122200015',65,2);
INSERT INTO fournit VALUES('C76','00011122200015',30,7);

-- Insertion dans table adhesion
INSERT INTO adhesion VALUES('C01',1,'2020-12-10','2021-12-10');
INSERT INTO adhesion VALUES('C02',1,'2020-12-10','2021-12-10');
INSERT INTO adhesion VALUES('C03',2,'2020-12-10','2022-12-10');
INSERT INTO adhesion VALUES('C04',3,'2020-12-10','2022-12-10');
INSERT INTO adhesion VALUES('C05',4,'2020-12-10','2023-12-10');
INSERT INTO adhesion VALUES('C06',3,'2020-12-10','2022-12-10');
INSERT INTO adhesion VALUES('C07',3,'2020-12-10','2022-12-10');
INSERT INTO adhesion VALUES('C08',1,'2020-12-10','2021-12-10');
INSERT INTO adhesion VALUES('C09',2,'2020-12-10','2022-12-10');

-- Insertion dans table lmodele
INSERT INTO lmodele VALUES('101','COMC0110',1);
INSERT INTO lmodele VALUES('102','COMC0110',1);
INSERT INTO lmodele VALUES('102','COMC0211',2);
INSERT INTO lmodele VALUES('102','COMC0214',3);
INSERT INTO lmodele VALUES('102','COMC0315',3);

-- insertion dans table lpiece
INSERT INTO lpiece VALUES('C34','COMC0110',1);
INSERT INTO lpiece VALUES ('C34','COMC0112',1);
INSERT INTO lpiece VALUES ('C34','COMC0211',3);
INSERT INTO lpiece VALUES ('C34','COMC0113',3);
INSERT INTO lpiece VALUES ('C34','COMC0214',3);
INSERT INTO lpiece VALUES ('C34','COMC0315',2);
INSERT INTO lpiece VALUES('C76','COMC0110',1);

select * from bicyclette;
select * from piece;
select * from typecontact;
select * from contact;
select * from assemblage;
select * from adresse;
select * from adhesion;
select * from commande;
select * from fournisseur;
select * from boutique;
select * from fournit;
select * from lmodele;

-- Test
SELECT * FROM contact WHERE id_typecontact=1 OR id_typecontact=2;
SELECT id_modele item, id_commande, qte_modele qte_item FROM lmodele UNION SELECT * FROM lpiece ORDER BY id_commande ;
SELECT avg(prix_unit) FROM bicyclette;
SELECT V.id_modele,V.nom_modele FROM bicyclette V WHERE V.prix_unit< (SELECT AVG(V1.prix_unit) FROM bicyclette V1);
SELECT C1.nom_contact, C2.nom_contact FROM contact C1, contact C2 WHERE C1.id_adresse=C2.id_adresse AND C1.nom_contact<C2.nom_contact;
SELECT nom_contact, prenom_contact, p1.pieceAchete+p2.modeleAchete as volumeItem FROM (SELECT nom_contact,prenom_contact, sum(qte_piece) as pieceAchete FROM contact NATURAL JOIN commande NATURAL JOIN lpiece GROUP BY id_contact ORDER BY nom_contact) p1 NATURAL JOIN (SELECT nom_contact,prenom_contact, sum(qte_modele) as modeleAchete FROM contact NATURAL JOIN commande NATURAL JOIN lmodele GROUP BY id_contact ORDER BY nom_contact) p2 GROUP BY nom_contact ORDER BY volumeItem DESC;
SELECT nom_contact, prenom_contact FROM contact NATURAL JOIN adhesion NATURAL JOIN programme WHERE id_programme='P1';
SELECT * FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece;
SELECT id_modele,nom_modele, min(stock) as stock FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece GROUP BY id_modele ORDER BY stock;
SELECT date_livraison FROM commande WHERE id_commande='COMC0201';
SELECT * FROM contact WHERE id_typecontact=1 OR id_typecontact=2;
delete bicyclette, assemblage from bicyclette natural join assemblage where id_modele='105'; #supprimer une donnée d'un tableau
delete from bicyclette where id_modele = '101';
SELECT duree from programme WHERE id_programme=1;
SELECT A.id_modele,A.id_piece,nom_modele,grandeur FROM assemblage A NATURAL JOIN piece NATURAL JOIN bicyclette order by id_modele,id_piece;
SELECT * FROM contact WHERE id_typecontact=1 ;

SELECT delai_fournisseur FROM fournit WHERE id_piece='C32';
SELECT * FROM fournisseur NATURAL JOIN contact ;
SELECT id_modele,nom_modele FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece GROUP BY id_modele HAVING min(stock)>0;
SELECT MIN(stock) FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece WHERE id_modele="101";
UPDATE bicyclette SET id_modele='110' WHERE id_modele='102';
SELECT id_piece FROM bicyclette NATURAL JOIN assemblage NATURAL JOIN piece WHERE id_modele='101' AND stock-5<0;