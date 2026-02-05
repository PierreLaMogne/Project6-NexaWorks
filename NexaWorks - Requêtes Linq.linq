<Query Kind="Statements">
  <Connection>
    <ID>24a00a00-71ed-4081-b018-385c5f205b3b</ID>
    <NamingServiceVersion>3</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <UseMicrosoftDataSqlClient>true</UseMicrosoftDataSqlClient>
    <EncryptTraffic>true</EncryptTraffic>
    <Database>NexaWorks</Database>
    <MapXmlToString>false</MapXmlToString>
    <DriverData>
      <SkipCertificateCheck>true</SkipCertificateCheck>
    </DriverData>
  </Connection>
</Query>

// Référence du DataContext
var ctx = this;

// Variables pour préciser la recherche
bool resolu = true;
var nomProduit = string.Empty;
var nomVersion = string.Empty;
var dateDebut = DateOnly.FromDateTime(new DateTime ());
var dateFin = DateOnly.FromDateTime(new DateTime ());
string[] motsCles = [];

// Modèle complet de la requête
this.Problemes.ToList()
	.Where (x => x.Statut == resolu
		      && x.VersionOs.Version.Produit.Nom == nomProduit
			  && x.VersionOs.Version.Nom == nomVersion
			  && x.DateCreation >= dateDebut
			  && x.DateCreation <= dateFin
			  && motsCles.Any(mot => x.Description.Contains(mot)))
	.OrderByDescending(x => motsCles.Count(mot => x.Description.Contains(mot)))
	.ToList()
	.Dump();


// Afficher tous les problèmes résolu ou tous les problèmes en cours
this.Problemes.Where(x => x.Statut == resolu)
			  .Dump();

// Afficher tous les problèmes résolus ou non d'un produit
this.Problemes.Where(x => x.Statut == resolu
					   && x.VersionOs.Version.Produit.Nom == nomProduit)
			  .Dump();

// Afficher tous les problèmes résolu ou non d'une version d'un produit
this.Problemes.Where(x => x.Statut == resolu
					   && x.VersionOs.Version.Produit.Nom == nomProduit
					   && x.VersionOs.Version.Nom == nomVersion)
			  .Dump();

// Afficher tous les problèmes résolu ou non d'un produit survenus entre deux dates
this.Problemes.Where(x => x.Statut == resolu
					   && x.VersionOs.Version.Produit.Nom == nomProduit
					   && x.DateCreation >= dateDebut
					   && x.DateCreation <= dateFin)
			  .Dump();

// Afficher tous les problèmes résolu ou non d'une version d'un produit survenus entre deux dates
this.Problemes.Where(x => x.Statut == resolu
					   && x.VersionOs.Version.Produit.Nom == nomProduit
					   && x.VersionOs.Version.Nom == nomVersion
					   && x.DateCreation >= dateDebut
					   && x.DateCreation <= dateFin)
			  .Dump();

// Afficher tous les problèmes résolu ou non dont la description contient une liste de termes spécifiques
this.Problemes.ToList()
			  .Where(x => x.Statut == resolu
			           && motsCles.Any(mot => x.Description.Contains(mot)))
			  .ToList()
			  .Dump();

// Afficher tous les problèmes résolu ou non d'un produit dont la description contient une liste de termes spécifiques
this.Problemes.ToList()
			  .Where(x => x.Statut == resolu
			  		   && x.VersionOs.Version.Produit.Nom == nomProduit
			  		   && motsCles.Any(mot => x.Description.Contains(mot)))
			  .ToList()
			  .Dump();

// Afficher tous les problèmes résolu ou non d'une version d'un produit dont la description contient une liste de termes spécifiques
this.Problemes.ToList()
			  .Where(x => x.Statut == resolu
			  		   && x.VersionOs.Version.Produit.Nom == nomProduit
					   && x.VersionOs.Version.Nom == nomVersion
					   && motsCles.Any(mot => x.Description.Contains(mot)))
			  .ToList()
			  .Dump();

// Afficher tous les problèmes résolu ou non d'un produit survenus entre deux dates dont la description contient une liste de termes spécifiques
this.Problemes.ToList()
			  .Where(x => x.Statut == resolu
			  		   && x.VersionOs.Version.Produit.Nom == nomProduit
					   && x.DateCreation >= dateDebut
					   && x.DateCreation <= dateFin
					   && motsCles.Any(mot => x.Description.Contains(mot)))
			  .ToList()
			  .Dump();

// Afficher tous les problèmes résolu ou non de la version d'un produit survenus entre deux dates dont la description contient une liste de termes spécifiques
this.Problemes.ToList()
			  .Where(x => x.Statut == resolu
			           && x.VersionOs.Version.Produit.Nom == nomProduit
					   && x.VersionOs.Version.Nom == nomVersion
					   && x.DateCreation >= dateDebut
					   && x.DateCreation <= dateFin
					   && motsCles.Any(mot => x.Description.Contains(mot)))
			  .ToList()
			  .Dump();