<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	echo "Connected successfully";
	
	//$sql = "SELECT name, score FROM scores";
	$sql = "SELECT COUNT(*) as total FROM `uczen`";
	
	$result = $conn->query($sql);
	//$data = mysql_query("SELECT COUNT(*) as total FROM `uczen`");
	//$info = mysql_fetch_assoc($result);
	//echo "Rows found :" . $info["total"];
	$row = $result->fetch_assoc();
	echo $row["total"];
	
	if ($results->num_rows > 0){
		//output data of each row
		while($row = $result->fetch_assoc()) {
			//echo "id: " . $row["id"]. " - Name: "$row["name"]. " " .$row["score"]. "<br>";
		}
	} else {
		echo ",0 results";
	}
	$conn->close();
	
?>