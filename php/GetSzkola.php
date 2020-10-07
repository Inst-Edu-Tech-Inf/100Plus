<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nauczycielPass = $_POST["nauczycielPass"];
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	//$sql = "SELECT name, score FROM scores";
	//$sql = "SELECT nazwa, skrot FROM `szkola` WHERE `nazwa' LIKE \"$nauczycielPass\" ";// WHERE `identyfikator` LIKE \"$nauczycielPass\" ";
	$sql = "SELECT nazwa, skrot FROM `szkola`";// WHERE `nazwa' LIKE \"$nauczycielPass\" ";// WHERE `identyfikator` LIKE \"$nauczycielPass\" ";
	
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0){
		//output data of each row
		while($row = $result->fetch_assoc()) {
			echo $row["nazwa"];
			echo "<br>";
			//" - Name: "$row["klasa"]. " " .$row["identyfikator"]. "<br>";
		}
	} 
	$conn->close();
	
?>