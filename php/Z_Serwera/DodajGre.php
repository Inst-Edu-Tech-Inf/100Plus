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
	//$firstDate = $firstDateTimeObj->format('Y-m-d');
	//$secondDate = $secondDateTimeObj->format('Y-m-d');
	
	

	
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
	
	//sprawdza czy dzis juz grali
	$sql = "SELECT date FROM `jos_djl_games` WHERE (`team_home` LIKE \"$hostTeamNr\" AND team_away` LIKE \"$clientTeamNr\") ";
	$result = $conn->query($sql);
	if ($result->num_rows > 0){
		while($row = $result->fetch_assoc()) {
			$firstDateTimeObj = $row["date"];		
			// IF CAST(DateField1 AS DATE) = CAST(DateField2 AS DATE)//sql
			$firstDate = $firstDateTimeObj->format('Y-m-d');
			$secondDate = $kalendarz->format('Y-m-d');
			if (($firstDate == $secondDate)){
				$czyOK = false;
				}
		}
	}
	
	if ($czyOK){
		//$sql = "SELECT COUNT(*) as totalG FROM `jos_djl_games`;";
		//$sql = "SELECT COUNT(*) FROM `jos_djl_games`;";
		//echo $sql;
		//$row = $result->fetch_assoc();
		//$ileGier = $result->fetch_assoc();
		//$ileGier = $row["totalG"];
		//echo $row["totalG"];
		//echo "ileGier";
		//echo $ileGier;
		//echo "<br>";
		
		$sql = "SELECT COUNT(*) as total FROM `jos_djl_games`";
		$result = $conn->query($sql);

		$row = $result->fetch_assoc();
		$nrWTabeli = $row["total"] + 1;
		//echo $nrWTabeli;
		
		$sql = "INSERT INTO `jos_djl_games` (`id`, `league_id`, `round`, `team_home`, `team_away`, `date`, `city`, `venue`, `score_home`, `score_away`, `score_desc`, `winner`, `points_home`, `points_away`, `status`, `checked_out`, `checked_out_time`, `created`, `created_by`, `params`) VALUES (\"$nrWTabeli\", '2', '1', \"$hostTeamNr\", \"$clientTeamNr\", \"$kalendarz\", '', '', \"$hostPoints\", \"$clientPoints\", '', \"$zwyciezca\", \"$pktHost\", \"$pktClient\", '1', '0', '', \"$kalendarz\", '272', '');";
		//echo $sql;
        $result = $conn->query($sql);        
	}
	else{
		echo "false";
	}
	
		
	$conn->close();
	
?>