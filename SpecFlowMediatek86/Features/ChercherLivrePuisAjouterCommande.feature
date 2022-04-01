Feature: ChercherLivrePuisAjouterCommande
	Chercher un livre par son numéro et lui ajouter une commande 

@chercherLivrePuisAjouterCommande
Scenario: Chercher un livre puis lui ajouter une commande
	Given Je saisie la valeur 00017
	And Je clique sur le bouton Rechercher
	And Je clique sur le bouton Ajouter
	And Je saisie le numéro de commande 777
	And Je saisie le nombre d'exemplaire à 4
	And Je saisie le montant à 200
	When Je clique sur le bouton Valider
	Then Le détail de la commande doit afficher le numéro de commande 777