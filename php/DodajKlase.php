<?php 
    
	$servername = "s69.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
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
	$tab = array($skrotU1, $skrotU2, $skrotU3, $skrotU4, $skrotU5, $skrotU6,  $skrotU7, $skrotU8, $skrotU9, $skrotU10, $skrotU11, $skrotU12, $skrotU13, $skrotU14, $skrotU15, $skrotU16, $skrotU17, $skrotU18, $skrotU19, $skrotU20, $skrotU21, $skrotU22, $skrotU23, $skrotU24, $skrotU25, $skrotU26, $skrotU27, $skrotU28, $skrotU29, $skrotU30, $skrotU31, $skrotU32, $skrotU33, $skrotU34, $skrotU35, $skrotU36, $skrotU37, $skrotU38, $skrotU39, $skrotU40);
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	//sql2 = "SELECT COUNT(*) FROM `klasa`";
	//sql3 = "INSERT INTO `klasa` (`id`, `szkola`, `ucz1`, `ucz2`, `ucz3`, `ucz4`, `ucz5`, `ucz6`, `ucz7`, `ucz8`, `ucz9`, `ucz10`, `ucz11`, `ucz12`, `ucz13`, `ucz14`, `ucz15`, `ucz16`, `ucz17`, `ucz18`, `ucz19`, `ucz20`, `ucz21`, `ucz22`, `ucz23`, `ucz24`, `ucz25`, `ucz26`, `ucz27`, `ucz28`, `ucz29`, `ucz30`, `ucz31`, `ucz32`, `ucz33`, `ucz34`, `ucz35`, `ucz36`, `ucz37`, `ucz38`, `ucz39`, `ucz40`, `nazwa`, `u1kod`, `u2kod`, `u3kod`, `u4kod`, `u5kod`, `u6kod`, `u7kod`, `u8kod`, `u9kod`, `u10kod`, `u11kod`, `u12kod`, `u13kod`, `u14kod`, `u15kod`, `u16kod`, `u17kod`, `u18kod`, `u19kod`, `u20kod`, `u21kod`, `u22kod`, `u23kod`, `u24kod`, `u25kod`, `u26kod`, `u27kod`, `u28kod`, `u29kod`, `u30kod`, `u31kod`, `u32kod`, `u33kod`, `u34kod`, `u35kod`, `u36kod`, `u37kod`, `u38kod`, `u39kod`, `u40kod`, `wiek`)"+
    //            " VALUES ('" + nrKlasy.ToString() + "', '" + nrSzkoly.ToString() + "', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '" + nazwaKlasyInput.text + "', '" + tmpSkrot[0] + "', '" + tmpSkrot[1] + "', '" + tmpSkrot[2] + "', '" + tmpSkrot[3] + "', '" + tmpSkrot[4] + "', '" + tmpSkrot[5] + "', '" + tmpSkrot[6] + "', '" + tmpSkrot[7] + "', '" + tmpSkrot[8] + "', '" + tmpSkrot[9] + "', '" + tmpSkrot[10] + "', '" + tmpSkrot[11] + "', '" + tmpSkrot[12] + "', '" + tmpSkrot[13] + "', '" + tmpSkrot[14] + "', '" + tmpSkrot[15] + "', '" + tmpSkrot[16] + "', '" + tmpSkrot[17] + "', '" + tmpSkrot[18] + "', '" + tmpSkrot[19] + "', '" + tmpSkrot[20] + "', '" + tmpSkrot[21] + "', '" + tmpSkrot[22] + "', '" + tmpSkrot[23] + "', '" + tmpSkrot[24] + "', '" + tmpSkrot[25] + "', '" + tmpSkrot[26] + "', '" + tmpSkrot[27] + "', '" + tmpSkrot[28] + "', '" + tmpSkrot[29] + "', '" + tmpSkrot[30] + "', '" + tmpSkrot[31] + "', '" + tmpSkrot[32] + "', '" + tmpSkrot[33] + "', '" + tmpSkrot[34] + "', '" + tmpSkrot[35] + "', '" + tmpSkrot[36] + "', '" + tmpSkrot[37] + "', '" + tmpSkrot[38] + "', '" + tmpSkrot[39] + "', '" + wiekUczniowValue.text + "');";
    // string sql4 = "SELECT COUNT(*) FROM `uczen`";       
	//string sql4 = "SELECT COUNT(*) FROM `jos_djl_teams`";
	//for (int i=1; i<=ileUczniowSlider.value; ++i)
		//tmpNrUcznia++;
        //tmpNrUczniaTeams++;

	//string sql5 = "INSERT INTO `uczen` (`id`, `klasa`, `identyfikator`, `team_nr`, `parametry`, `skrot`) " +
	//	"VALUES('" + tmpNrUcznia.ToString() + "', '"+nrKlasy.ToString()+ "', '', '"+tmpNrUczniaTeams.ToString()+"', '', '"+tmpSkrot[Licznik-1]+"');";
    //  string sql5 = "UPDATE `klasa` SET `ucz" + Licznik.ToString() + "` = '" + tmpNrUcznia.ToString() + "' WHERE `klasa`.`id` = " + nrKlasy.ToString() + ";";
    // string sql5 = "INSERT INTO `jos_djl_teams` (`id`, `name`, `alias`, `logo`, `city`, `venue`, `checked_out`, `checked_out_time`, `created`, `created_by`, `params`) " +
    //               "VALUES('" + nrUczniaTeams.ToString() + "', '" + nazwaUcznia + "', '', '', '', '', '0', '', '', '', '');";
      //endfor slider                            
//	$sql = "SELECT nazwa, skrot FROM `szkola`";// WHERE `nazwa' LIKE \"$nauczycielPass\" ";// WHERE `identyfikator` LIKE \"$nauczycielPass\" ";
//	sql = "SELECT COUNT(*) as total FROM `szkola`";
//	 sql = "INSERT INTO `szkola` (`id`, `nazwa`, `kl1`, `kl2`, `kl3`, `kl4`, `kl5`, `kl6`, `kl7`, `kl8`, `kl9`, `kl10`," +                    " `kl11`, `kl12`, `kl13`, `kl14`, `kl15`, `kl16`, `kl17`, `kl18`, `kl19`, `kl20`, `skrot`," +                    " `kl1nazwa`, `kl2nazwa`, `kl3nazwa`, `kl4nazwa`, `kl5nazwa`, `kl6nazwa`, `kl7nazwa`, `kl8nazwa`, `kl9nazwa`, `kl10nazwa`, " +                    "`kl11nazwa`, `kl12nazwa`, `kl13nazwa`, `kl14nazwa`, `kl15nazwa`, `kl16nazwa`, `kl17nazwa`, `kl18nazwa`, `kl19nazwa`, `kl20nazwa`) " +                    "VALUES('" + nrSzkoly.ToString() + "', '" + nazwaSzkolyInput.text + "', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', " +                    "'" + skrotSzkolyText.text + "', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '');";
                
	
	$sql = "SELECT COUNT(*) as total FROM `klasa`";
	
	$result = $conn->query($sql);

	$row = $result->fetch_assoc();
	$ileKlas = $row["total"];
	
	$sql = "INSERT INTO `klasa` (`id`, `szkola`, `ucz1`, `ucz2`, `ucz3`, `ucz4`, `ucz5`, `ucz6`, `ucz7`, `ucz8`, `ucz9`, `ucz10`, `ucz11`, `ucz12`, `ucz13`, `ucz14`, `ucz15`, `ucz16`, `ucz17`, `ucz18`, `ucz19`, `ucz20`, `ucz21`, `ucz22`, `ucz23`, `ucz24`, `ucz25`, `ucz26`, `ucz27`, `ucz28`, `ucz29`, `ucz30`, `ucz31`, `ucz32`, `ucz33`, `ucz34`, `ucz35`, `ucz36`, `ucz37`, `ucz38`, `ucz39`, `ucz40`, `nazwa`, `u1kod`, `u2kod`, `u3kod`, `u4kod`, `u5kod`, `u6kod`, `u7kod`, `u8kod`, `u9kod`, `u10kod`, `u11kod`, `u12kod`, `u13kod`, `u14kod`, `u15kod`, `u16kod`, `u17kod`, `u18kod`, `u19kod`, `u20kod`, `u21kod`, `u22kod`, `u23kod`, `u24kod`, `u25kod`, `u26kod`, `u27kod`, `u28kod`, `u29kod`, `u30kod`, `u31kod`, `u32kod`, `u33kod`, `u34kod`, `u35kod`, `u36kod`, `u37kod`, `u38kod`, `u39kod`, `u40kod`, `wiek`) VALUES (\"$ileKlas\", \"$ileSzkol\", '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', \"$skrotKlasyPass\", \"$skrotU1\", \"$skrotU2\", \"$skrotU3\", \"$skrotU4\", \"$skrotU5\", \"$skrotU6\", \"$skrotU7\", \"$skrotU8\", \"$skrotU9\", \"$skrotU10\", \"$skrotU11\", \"$skrotU12\", \"$skrotU13\", \"$skrotU14\", \"$skrotU15\", \"$skrotU16\", \"$skrotU17\", \"$skrotU18\", \"$skrotU19\", \"$skrotU20\", \"$skrotU21\", \"$skrotU22\", \"$skrotU23\", \"$skrotU24\", \"$skrotU25\", \"$skrotU26\", \"$skrotU27\", \"$skrotU28\", \"$skrotU29\", \"$skrotU30\", \"$skrotU31\", \"$skrotU32\", \"$skrotU33\", \"$skrotU34\", \"$skrotU35\", \"$skrotU36\", \"$skrotU37\", \"$skrotU38\", \"$skrotU39\", \"$skrotU40\", \"$wiekUczniow\");";
   
	//echo $sql;
	//echo "<br>";
    $result = $conn->query($sql);
	
	$sql = "SELECT COUNT(*) as totalU FROM `uczen`";
	$result = $conn->query($sql);

	$row = $result->fetch_assoc();
	$ileUczniow = $row["totalU"];
	
	$sql = "SELECT COUNT(*) as totalTEAM FROM `jos_djl_teams`";
	$result = $conn->query($sql);

	$row = $result->fetch_assoc();
	$nrUczniaTeams = $row["totalTEAM"];
	
	//teraz FOR
	$tmpNrUcznia = $ileUczniow;
    $tmpNrUczniaTeams = $nrUczniaTeams;
	$pozycja = "";
	$licznik = 0;
	for($i=0; $i < $iluUczniowDodac; $i++)
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
		$nazwaUcznia = "$skrotSzkoly/$skrotKlasyPass/$licznik";
		$sql = "INSERT INTO `jos_djl_teams` (`id`, `name`, `alias`, `logo`, `city`, `venue`, `checked_out`, `checked_out_time`, `created`, `created_by`, `params`) VALUES(\"$tmpNrUczniaTeams\", $nazwaUcznia, '', '', '', '', '0', '', '', '', '');";
		//echo $sql;
		//echo "<br>";
		$result = $conn->query($sql);
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