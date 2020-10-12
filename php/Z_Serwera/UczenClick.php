<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$uczenPass = $_POST["uczenPass"];
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
	$sql = "SELECT skrot, team_nr FROM `uczen` WHERE `identyfikator` LIKE \"$uczenPass\" ";
	//$sql = "SELECT skrot FROM `uczen` WHERE `identyfikator` LIKE \"e97c074fafde2707859a79d32dd0708929554e4d\" ";
	//$sql = "SELECT szkola FROM `nauczyciel` WHERE `identyfikator` LIKE \"e97c074fafde2707859a79d32dd0708929554e4d\" ";
	//sql = "SELECT * FROM `uczen` WHERE `identyfikator` LIKE '" + SkinManager.instance.UserID.ToString() + "';";
                        
	
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0){
		//output data of each row
		while($row = $result->fetch_assoc()) {
			//echo $row["szkola"];
			//
			//echo $row["skrot"];
			//echo "<br>";
			$teamNr = $row["team_nr"];
			$sql = "SELECT name FROM `jos_djl_teams` WHERE `id` LIKE \"$teamNr\" ";
			//echo $sql;
			$result = $conn->query($sql);
			while($row = $result->fetch_assoc()) {
				echo $row["name"];
			}
		}		
	}
	else
	{
		echo "false";
	}	
	$conn->close();
	
?>