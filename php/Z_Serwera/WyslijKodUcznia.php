<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nauczycielPass = $_POST["nauczycielPass"];
	$skrotUcznia = $_POST["skrotUcznia"];
	$nrUczniaRejestracja = "";
	$nrUczniaDlaTeam = "";
	$uczenZajety = "";
	$leagueParameter = "";
	$nrWTabeli = "";

	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	$sql = "SELECT id, team_nr, skrot FROM `uczen` WHERE `skrot` LIKE \"$skrotUcznia\";";
    //$sql = "SELECT szkola FROM `nauczyciel` WHERE `identyfikator` LIKE \"$nauczycielPass\" ";	
	
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0){
		while($row = $result->fetch_assoc()) {
			
			$nrUczniaRejestracja = $row["id"];
			$nrUczniaDlaTeam = $row["team_nr"];
			$uczenZajety = $row["skrot"];
			//echo $row["szkola"];
			//echo "Uczen zajety.id";
			//echo $nrUczniaRejestracja;
			//echo "<br>";
		}
	}
	
	
	if ($uczenZajety != ""){
		$sql = "UPDATE `uczen` SET `identyfikator` = \"$nauczycielPass\" WHERE `uczen`.`id` = \"$nrUczniaRejestracja\";";
		$result = $conn->query($sql);
		
		$sql = "SELECT params FROM `jos_djl_leagues` WHERE `id` LIKE '2';";
		$result = $conn->query($sql);
		if ($result->num_rows > 0){
			while($row = $result->fetch_assoc()) {
				//echo $row["szkola"];
				//echo "<br>";
				$leagueParameter = $row["params"];
			}
		}
		
		//leagueParameter = leagueParameter.Replace("\",\"rounds", "," + nrUczniaDlaTeam.ToString() + "\",\"rounds");
		//$phrase  = "You should eat fruits, vegetables, and fiber every day.";
		$whatChange = array("\",\"rounds",'"');
		$changed   = array(",$nrUczniaDlaTeam\",\"rounds",'\\"');

		$correctLeagueParameter = str_replace($whatChange, $changed, $leagueParameter);
		
                //string sql = "SELECT * FROM `uczen` WHERE `skrot` LIKE '" + kodUczniaInput.text.ToString() + "';";
        $sql = "UPDATE `jos_djl_leagues` SET `params` = \"$correctLeagueParameter\" WHERE `jos_djl_leagues`.`id` = 2;";
		//echo $sql;
		//echo "</br>";
		$result = $conn->query($sql);
		
		$sql = "SELECT COUNT(*) as total FROM `jos_djl_tables`";
		$result = $conn->query($sql);

		$row = $result->fetch_assoc();
		$nrWTabeli = $row["total"] + 1;
		
		$sql = "INSERT INTO `jos_djl_tables` (`id`, `league_id`, `team_id`, `extra_points`, `ordering`) VALUES (\"$nrWTabeli\", '2', \"$nrUczniaDlaTeam\", '0', '0');";
		//echo $sql;
		//echo "</br>";
		$result = $conn->query($sql);
	}
	else {
		echo "false";
	}
		
	$conn->close();
	
?>