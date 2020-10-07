<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	//variables submited by user_error
	$nauczycielPass = $_POST["nauczycielPass"];
	
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	//SkinManager.instance.UserID
	$sql = "SELECT * FROM `nauczyciel` WHERE `identyfikator` LIKE  \"$nauczycielPass\";";  
	$sql = "SELECT COUNT(*) as total FROM `uczen`";	
	//$sql = "SELECT *  FROM `nauczyciel` WHERE `identyfikator` LIKE bhjkcobmajfjimahiinombfclllpkkigcmpipcie ;";           
	
	$result = $conn->query($sql);
	
	//$row = $result->fetch_assoc();
	echo $row["total"];
	
	if ($results->num_rows > 0){
		//output data of each row
		while($row = $result->fetch_assoc()) {
			//if($row["password"] == $loginPass){
			//	echo "Username is already taken";
				//here get data
				echo "true";
			//}
			//else {
			//	echo "Creating new user";
			//	$sql2 = "INSERT INTO users (name....) VALUES ('" . $loginUser . "', '" . $loginPass . "', 1, 0)";
			//	if ($conn->query($sql2) === TRUE) {
			//		echo "New record created successfully";
			//	} else {
			//		echo "Error: " . $sql2 . "<br>" . $conn->error;
			//	}
			}
			//echo "id: " . $row["id"]. " - Name: "$row["name"]. " " .$row["score"]. "<br>";
		}
	 else {
		//echo "User name doesn't exist";
		echo "false"; 
		echo "<br>";
		echo $sql;
		echo "<br>";
		echo $results->num_rows;
	}
	
	$conn->close();
	
?>