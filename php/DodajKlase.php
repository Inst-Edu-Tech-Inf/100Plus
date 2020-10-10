<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nauczycielPass = $_POST["nauczycielPass"];
	$skrotSzkoly = $_POST["skrotSzkoly"];
	$skrotKlasyPass = $_POST["skrotKlasyPass"];
	$wiekUczniow = $_POST["wiekUczniow"];
	$iluUczniowDodac = $_POST["iluUczniowDodac"];
	$skrotU1 = $_POST["skrotU1"];
	$skrotU2 = $_POST["skrotU2"];
	$skrotU3 = $_POST["skrotU3"];
	$skrotU4 = $_POST["skrotU4"];
	$skrotU5 = $_POST["skrotU5"];
	$skrotU6 = $_POST["skrotU6"];
	$skrotU7 = $_POST["skrotU7"];
	$skrotU8 = $_POST["skrotU8"];
	$skrotU9 = $_POST["skrotU9"];
	$skrotU10 = $_POST["skrotU10"];
	$skrotU11 = $_POST["skrotU11"];
	$skrotU12 = $_POST["skrotU12"];
	$skrotU13 = $_POST["skrotU13"];
	$skrotU14 = $_POST["skrotU14"];
	$skrotU15 = $_POST["skrotU15"];
	$skrotU16 = $_POST["skrotU16"];
	$skrotU17 = $_POST["skrotU17"];
	$skrotU18 = $_POST["skrotU18"];
	$skrotU19 = $_POST["skrotU19"];
	$skrotU20 = $_POST["skrotU20"];
	$skrotU21 = $_POST["skrotU21"];
	$skrotU22 = $_POST["skrotU22"];
	$skrotU23 = $_POST["skrotU23"];
	$skrotU24 = $_POST["skrotU24"];
	$skrotU25 = $_POST["skrotU25"];
	$skrotU26 = $_POST["skrotU26"];
	$skrotU27 = $_POST["skrotU27"];
	$skrotU28 = $_POST["skrotU28"];
	$skrotU29 = $_POST["skrotU29"];
	$skrotU30 = $_POST["skrotU30"];
	$skrotU31 = $_POST["skrotU31"];
	$skrotU32 = $_POST["skrotU32"];
	$skrotU33 = $_POST["skrotU33"];
	$skrotU34 = $_POST["skrotU34"];
	$skrotU35 = $_POST["skrotU35"];
	$skrotU36 = $_POST["skrotU36"];
	$skrotU37 = $_POST["skrotU37"];
	$skrotU38 = $_POST["skrotU38"];
	$skrotU39 = $_POST["skrotU39"];
	$skrotU40 = $_POST["skrotU40"];
	$ileSzkol = "";
	//$ileNauczycieli = "";
	$ileKlas = "";
	$ileUczniow = "";
	$nrUczniaTeams = "";
	$i = 0;
	$nrSzkoly = "";
	$tab = array($skrotU1, $skrotU2, $skrotU3, $skrotU4, $skrotU5, $skrotU6,  $skrotU7, $skrotU8, $skrotU9, $skrotU10, $skrotU11, $skrotU12, $skrotU13, $skrotU14, $skrotU15, $skrotU16, $skrotU17, $skrotU18, $skrotU19, $skrotU20, $skrotU21, $skrotU22, $skrotU23, $skrotU24, $skrotU25, $skrotU26, $skrotU27, $skrotU28, $skrotU29, $skrotU30, $skrotU31, $skrotU32, $skrotU33, $skrotU34, $skrotU35, $skrotU36, $skrotU37, $skrotU38, $skrotU39, $skrotU40);
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";

    $sql = "SELECT szkola FROM `nauczyciel` WHERE `identyfikator` LIKE \"$nauczycielPass\" ";	
	
	$result = $conn->query($sql);
	//echo $nauczycielPass;
	if ($result->num_rows > 0){
		while($row = $result->fetch_assoc()) {	
			$nrSzkoly = $row["szkola"];
			//echo "nrSzkoly:";
			//echo $row["szkola"];
			//echo "<br>";
		}
	
		$sql = "SELECT COUNT(*) as total FROM `klasa`";
		
		$result = $conn->query($sql);

		$row = $result->fetch_assoc();
		$ileKlas = $row["total"];
		//echo "ileKlas:";
		//echo $ileKlas;
		//echo "<br>";
		
		$sql = "INSERT INTO `klasa` (`id`, `szkola`, `ucz1`, `ucz2`, `ucz3`, `ucz4`, `ucz5`, `ucz6`, `ucz7`, `ucz8`, `ucz9`, `ucz10`, `ucz11`, `ucz12`, `ucz13`, `ucz14`, `ucz15`, `ucz16`, `ucz17`, `ucz18`, `ucz19`, `ucz20`, `ucz21`, `ucz22`, `ucz23`, `ucz24`, `ucz25`, `ucz26`, `ucz27`, `ucz28`, `ucz29`, `ucz30`, `ucz31`, `ucz32`, `ucz33`, `ucz34`, `ucz35`, `ucz36`, `ucz37`, `ucz38`, `ucz39`, `ucz40`, `nazwa`, `u1kod`, `u2kod`, `u3kod`, `u4kod`, `u5kod`, `u6kod`, `u7kod`, `u8kod`, `u9kod`, `u10kod`, `u11kod`, `u12kod`, `u13kod`, `u14kod`, `u15kod`, `u16kod`, `u17kod`, `u18kod`, `u19kod`, `u20kod`, `u21kod`, `u22kod`, `u23kod`, `u24kod`, `u25kod`, `u26kod`, `u27kod`, `u28kod`, `u29kod`, `u30kod`, `u31kod`, `u32kod`, `u33kod`, `u34kod`, `u35kod`, `u36kod`, `u37kod`, `u38kod`, `u39kod`, `u40kod`, `wiek`) VALUES (\"$ileKlas\", \"$nrSzkoly\", '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', \"$skrotKlasyPass\", \"$skrotU1\", \"$skrotU2\", \"$skrotU3\", \"$skrotU4\", \"$skrotU5\", \"$skrotU6\", \"$skrotU7\", \"$skrotU8\", \"$skrotU9\", \"$skrotU10\", \"$skrotU11\", \"$skrotU12\", \"$skrotU13\", \"$skrotU14\", \"$skrotU15\", \"$skrotU16\", \"$skrotU17\", \"$skrotU18\", \"$skrotU19\", \"$skrotU20\", \"$skrotU21\", \"$skrotU22\", \"$skrotU23\", \"$skrotU24\", \"$skrotU25\", \"$skrotU26\", \"$skrotU27\", \"$skrotU28\", \"$skrotU29\", \"$skrotU30\", \"$skrotU31\", \"$skrotU32\", \"$skrotU33\", \"$skrotU34\", \"$skrotU35\", \"$skrotU36\", \"$skrotU37\", \"$skrotU38\", \"$skrotU39\", \"$skrotU40\", \"$wiekUczniow\");";
	   
		//echo $sql;
		//echo "<br>";
		$result = $conn->query($sql);
		
		//$sql = "SELECT COUNT(*) as totalU FROM `uczen`";
		//$result = $conn->query($sql);

		//$row = $result->fetch_assoc();
		//$ileUczniow = $row["totalU"];
		$sql = "SELECT COUNT(*) as totalUCZEN FROM `uczen`";
		$result = $conn->query($sql);

		$row = $result->fetch_assoc();
		$nrWTabeli = $row["totalUCZEN"] + 1;
		
		
		$sql = "SELECT COUNT(*) as totalTEAM FROM `jos_djl_teams`";
		$result = $conn->query($sql);

		$row = $result->fetch_assoc();
		$nrUczniaTeams = $row["totalTEAM"];
		
		//teraz FOR
		$tmpNrUcznia = $nrWTabeli;
		$tmpNrUczniaTeams = $nrUczniaTeams;
		$pozycja = "";
		$licznik = 0;
		//echo "ileUczniowDodac:";
		//echo $iluUczniowDodac;
		for($i=0; $i < intval($iluUczniowDodac); $i++)
		{
			$licznik = $licznik + 1;
			$tmpNrUcznia = $tmpNrUcznia + 1;
			$tmpNrUczniaTeams = $tmpNrUczniaTeams + 1;
			$pozycja = $tab[$i];
			//$sql = "INSERT INTO `nauczyciel` (`id`, `identyfikator`, `szkola`) VALUES(\"$ileNauczycieli\", \"$userID\", \"$ileSzkol\");";
			$sql = "INSERT INTO `uczen` (`id`, `klasa`, `identyfikator`, `team_nr`, `parametry`, `skrot`) VALUES(\"$tmpNrUcznia\", \"$ileKlas\", '', \"$tmpNrUczniaTeams\", '', \"$pozycja\");";
			//echo $sql;
			//echo "<br>";
			$result = $conn->query($sql);
			
			$sql = "UPDATE `klasa` SET `ucz$licznik` = \"$tmpNrUcznia\" WHERE `klasa`.`id` = \"$ileKlas\";";
			//echo $sql;
			//echo "<br>";
			$result = $conn->query($sql);
			//nazwaUcznia = skrotSzkoly + "/" + nazwaKlasyInput.text + "/" + Licznik.ToString();
			$nazwaUcznia = "\"$skrotSzkoly/$skrotKlasyPass/$licznik\"";
			$sql = "INSERT INTO `jos_djl_teams` (`id`, `name`, `alias`, `logo`, `city`, `venue`, `checked_out`, `checked_out_time`, `created`, `created_by`, `params`) VALUES(\"$tmpNrUczniaTeams\", $nazwaUcznia, '', '', '', '', '0', '', '', '', '');";
			//echo $sql;
			//echo "<br>";
			$result = $conn->query($sql);
		}


	}
	else{
		echo "false";
	}
	
	//echo $ileSzkol;
	//if ($row["total"] == 1)
	//	echo "true";
	//else
	//	echo "false";
	
	//$result = $conn->query($sql);
	
	//if ($result->num_rows > 0){
		//output data of each row
	//	while($row = $result->fetch_assoc()) {
	//		echo $row["nazwa"];
	//		echo "<br>";
			//" - Name: "$row["klasa"]. " " .$row["identyfikator"]. "<br>";
	//	}
	//} 
	$conn->close();
	
?>