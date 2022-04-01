Feature: RechercheDvdNumero

@rechercheLivreNumero
Scenario: Chercher un DVD avec son numéro
	Given Je saisis la valeur 20001
	When Je clic sur le bouton Rechercher
	Then Les informations détaillées doivent afficher le titre Star Wars 5 L'empire contre attaque