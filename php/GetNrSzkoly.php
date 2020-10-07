<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nauczycielPass = $_POST["nauczycielPass"];
	$nrSzkoly = "";
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	//$sql = "SELECT name, score FROM scores";
	$sql = "SELECT szkola FROM `nauczyciel` WHERE `identyfikator` LIKE \"$nauczycielPass\" ";
	$sql = "SELECT szkola FROM `nauczyciel` WHERE `identyfikator` LIKE \"bhjkcobmajfjimahiinombfclllpkkigcmpipcie\" ";
	//string sql = "SELECT * FROM `nauczyciel` WHERE `identyfikator` LIKE '" + SkinManager.instance.UserID + "';";
//string sql = "SELECT * FROM `szkola` WHERE `id` LIKE '" + nrSzkoly.ToString() + "';";
               //return nazwaszkoly,skrotszkoly
// string sql = "SELECT * FROM `klasa` WHERE `szkola` LIKE '" + nrSzkoly.ToString() + "';";
               //nazwy klas               
	
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0){
		//output data of each row
		while($row = $result->fetch_assoc()) {
			//echo $row["szkola"];
			//echo "<br>";
			$nrSzkoly = $row["szkola"];
		}
		$sql = "SELECT nazwa, skrot FROM `szkola` WHERE `id` LIKE \"$nrSzkoly\" ";
		$result = $conn->query($sql);
		if ($result->num_rows > 0){
		//output data of each row
			while($row = $result->fetch_assoc()) {
				//echo $row["szkola"];
				//
				echo $row["nazwa"];
				echo "<br>";
				echo $row["skrot"];
				echo "<br>";
			}
		}
		$sql = "SELECT nazwa FROM `klasa` WHERE `szkola` LIKE \"$nrSzkoly\" ";
		$result = $conn->query($sql);
		if ($result->num_rows > 0){
		//output data of each row
			while($row = $result->fetch_assoc()) {
				echo $row["nazwa"];
				echo "<br>";
			}
		}
	}
	else
	{
		echo "false";
	}	
	$conn->close();
	
?>