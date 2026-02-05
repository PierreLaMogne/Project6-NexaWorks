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
bool resolu = false;
var nomProduit = string.Empty;
var nomVersion = string.Empty;
DateOnly dateDebut = new DateOnly();
var dateFin = DateOnly.FromDateTime(new DateTime());
string[] motsCles = [];

var request = this.Problemes.ToList();


// Ciblage de la requête
string askResolu;
do
{
	askResolu = Util.ReadLine("Souhaitez-vous consulter les tickets résolus (r), en cours (ec) ou tous les tickets (t) ?").ToLower().Trim();
} while (askResolu != "r" && askResolu != "ec" && askResolu != "t");

if (askResolu != "t")
{
	if (askResolu == "r")
		resolu = true;
	else resolu = false;
	
	request = request.Where (x => x.Statut == resolu).ToList();
}


string askProduit;
do
{
	askProduit = Util.ReadLine("Souhaitez-vous consulter les tickets d'un produit spécifique ? (o/n)").ToLower().Trim();
} while (askProduit != "o" && askProduit != "n");

if (askProduit != "n")
{
	nomProduit = Util.ReadLine("Quel est le nom du Produit à consulter ?").ToLower().Trim();
	request = request.Where (x => x.VersionOs.Version.Produit.Nom == nomProduit).ToList();
	
	string askVersion;
	do
	{
		askVersion = Util.ReadLine("Souhaitez-vous consulter les tickets d'une version spécifique ? (o/n)").ToLower().Trim();
	} while (askVersion != "o" && askVersion != "n");

	if (askVersion == "o")
	{
		nomVersion = Util.ReadLine("Quel est le nom de la version à consulter ?").ToLower().Trim();
		request = request.Where(x => x.VersionOs.Version.Nom == nomVersion).ToList();
	}
}


string askDate;
do
{
	askDate = Util.ReadLine("Souhaitez-vous focaliser votre recherche sur une période spécifique ? (o/n)").ToLower().Trim();
} while (askDate != "o" && askDate != "n");

if (askDate == "o")
{
	dateDebut = Util.ReadLine<DateOnly>("Saisissez la date de début de recherche au format AAAA/MM/JJ (ex: 2025/09/19)");
	dateFin = Util.ReadLine<DateOnly>("Saisissez la date de fin de recherche au format AAAA/MM/JJ (ex: 2025/12/31)");
	request = request.Where (x => x.DateCreation >= dateDebut && x.DateCreation <= dateFin).ToList();
}


string askMotCle;
do
{
	askMotCle = Util.ReadLine("Souhaitez-vous consulter uniquement les tickets contenant certains termes spécifiques ? (o/n)").ToLower().Trim();
} while (askMotCle != "o" && askMotCle != "n");

if (askMotCle == "o")
{
	motsCles = Util.ReadLine("Saisissez le ou les termes à rechercher (séparés par une virgule)").ToLower().Trim().Split(',', StringSplitOptions.TrimEntries);
	request = request.Where(x => motsCles.Any(mot => x.Description.Contains(mot)))
					 .OrderByDescending(x => motsCles.Count(mot => x.Description.Contains(mot)))
					 .ToList();
}

request.Dump();
Console.WriteLine("Appuyez sur F5 pour lancer une autre recherche");