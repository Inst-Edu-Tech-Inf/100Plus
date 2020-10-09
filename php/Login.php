<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	//variables submited by user_error
	$loginUser = $_POST["loginUser"];
	$loginPass = $_POST["loginPass"];;
	
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	echo "Connected successfully";
	
	$sql = "SELECT password FROM users WHERE username == '" . $loginUser . "'";
	
	$result = $conn->query($sql);
	
	if ($results->num_rows > 0){
		//output data of each row
		while($row = $result->fetch_assoc()) {
			if($row["password"] == $loginPass){
				echo "Login Success.";
				//here get data
			}
			else {
				echo "Wrong Login/Password";
			}
			//echo "id: " . $row["id"]. " - Name: "$row["name"]. " " .$row["score"]. "<br>";
		}
	} else {
		echo "User name doesn't exist";
	}
	$conn->close();
	
?>