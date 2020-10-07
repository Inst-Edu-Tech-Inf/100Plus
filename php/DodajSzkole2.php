<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nazwaSzkolyPass = $_POST["nazwaSzkolyPass"];
	$skrotSzkolyPass = $_POST["skrotSzkolyPass"];
	$userID = $_POST["userID"];
	$ileSzkol = "";
	$ileNauczycieli = "";
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	echo "Connected successfully";

	$conn->close();
	
?>