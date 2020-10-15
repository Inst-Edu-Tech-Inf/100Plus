<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nauczycielPass = $_POST["nauczycielPass"];
	$aktywnaKlasa = $_POST["aktywnaKlasa"];
	$nrSzkoly = "";
	$licznik = 0;
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	$sql = "SELECT szkola FROM `nauczyciel` WHERE `identyfikator` LIKE \"$nauczycielPass\" ";	
	
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0){
		echo "true";
		
	}
	else
	{
		echo "false";
	}
	
	$conn->close();
	
?>