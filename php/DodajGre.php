<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nauczycielPass = $_POST["nauczycielPass"];
	$clientID = $_POST["clientID"];
	$zwyciezca = $_POST["zwyciezca"];
    $pktHost = $_POST["pktHost"];
    $pktClient = $_POST["pktClient"];
	$kalendarz = $_POST["kalendarz"];
	$hostPoints = $_POST["hostPoints"];
	$clientPoints = $_POST["clientPoints"];

	$czyOK = false;
	$hostTeamNr = "";
	$clientTeamNr = "";
	$ileGier = "";
	
	

	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	$sql = "SELECT team_nr FROM `uczen` WHERE `identyfikator` LIKE \"$nauczycielPass\";";
	$result = $conn->query($sql);
	if ($result->num_rows > 0){
		while($row = $result->fetch_assoc()) {
			//echo $row["szkola"];
			//echo "<br>";
			$hostTeamNr = $row["team_nr"];
			$czyOK = true;
		}
	}
	
	$sql = "SELECT team_nr FROM `uczen` WHERE `identyfikator` LIKE \"$clientID\";";
	$result = $conn->query($sql);
	if ($result->num_rows > 0){
		while($row = $result->fetch_assoc()) {
			//echo $row["szkola"];
			//echo "<br>";
			$clientTeamNr = $row["team_nr"];
		}
	}
	
	if ($czyOK){
		$sql = "SELECT COUNT(*) as total FROM `jos_djl_games`";
		$row = $result->fetch_assoc();
		$ileGier = $row["total"];
		
		$sql = "INSERT INTO `jos_djl_games` (`id`, `league_id`, `round`, `team_home`, `team_away`, `date`, `city`, `venue`, `score_home`, `score_away`, `score_desc`, `winner`, `points_home`, `points_away`, `status`, `checked_out`, `checked_out_time`, `created`, `created_by`, `params`) VALUES (\"$ileGier\", '2', '1', \"$hostTeamNr\", \"$clientTeamNr\", \"$kalendarz\", '', '', \"$hostPoints\", \"$clientPoints\", '', \"$zwyciezca\", \"$pktHost\", \"$pktClient\", '1', '0', '', \"$kalendarz\", '272', '');";
        $result = $conn->query($sql);        
	}
	else{
		echo "false";
	}
	
		
	$conn->close();
	
?>