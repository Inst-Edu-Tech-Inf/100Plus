<?php 
    
	$servername = "s140.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nazwaSzkolyPass = $_POST["nazwaSzkolyPass"];
	$skrotSzkolyPass = $_POST["skrotSzkolyPass"];
	$userID = $_POST["userID"];
	$ileSzkol = "";
	$ileNauczycieli = "";
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
//	$sql = "SELECT nazwa, skrot FROM `szkola`";// WHERE `nazwa' LIKE \"$nauczycielPass\" ";// WHERE `identyfikator` LIKE \"$nauczycielPass\" ";
//	sql = "SELECT COUNT(*) as total FROM `szkola`";
//	 sql = "INSERT INTO `szkola` (`id`, `nazwa`, `kl1`, `kl2`, `kl3`, `kl4`, `kl5`, `kl6`, `kl7`, `kl8`, `kl9`, `kl10`," +                    " `kl11`, `kl12`, `kl13`, `kl14`, `kl15`, `kl16`, `kl17`, `kl18`, `kl19`, `kl20`, `skrot`," +                    " `kl1nazwa`, `kl2nazwa`, `kl3nazwa`, `kl4nazwa`, `kl5nazwa`, `kl6nazwa`, `kl7nazwa`, `kl8nazwa`, `kl9nazwa`, `kl10nazwa`, " +                    "`kl11nazwa`, `kl12nazwa`, `kl13nazwa`, `kl14nazwa`, `kl15nazwa`, `kl16nazwa`, `kl17nazwa`, `kl18nazwa`, `kl19nazwa`, `kl20nazwa`) " +                    "VALUES('" + nrSzkoly.ToString() + "', '" + nazwaSzkolyInput.text + "', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', " +                    "'" + skrotSzkolyText.text + "', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '');";
                
	
	$sql = "SELECT COUNT(*) as total FROM `szkola`";
	
	$result = $conn->query($sql);

	$row = $result->fetch_assoc();
	$ileSzkol = $row["total"];
	
	$sql = "INSERT INTO `szkola` (`id`, `nazwa`, `kl1`, `kl2`, `kl3`, `kl4`, `kl5`, `kl6`, `kl7`, `kl8`, `kl9`, `kl10`, `kl11`, `kl12`, `kl13`, `kl14`, `kl15`, `kl16`, `kl17`, `kl18`, `kl19`, `kl20`, `skrot`, `kl1nazwa`, `kl2nazwa`, `kl3nazwa`, `kl4nazwa`, `kl5nazwa`, `kl6nazwa`, `kl7nazwa`, `kl8nazwa`, `kl9nazwa`, `kl10nazwa`, `kl11nazwa`, `kl12nazwa`, `kl13nazwa`, `kl14nazwa`, `kl15nazwa`, `kl16nazwa`, `kl17nazwa`, `kl18nazwa`, `kl19nazwa`, `kl20nazwa`) VALUES(\"$ileSzkol\", \"$nazwaSzkolyPass\", '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', \"$skrotSzkolyPass\" , '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '');";
	//echo $sql;
	//echo "<br>";
    $result = $conn->query($sql);
	
	$sql = "SELECT COUNT(*) as totalN FROM `nauczyciel`";
	$result = $conn->query($sql);

	$row = $result->fetch_assoc();
	$ileNauczycieli = $row["totalN"];
	
	$sql = "INSERT INTO `nauczyciel` (`id`, `identyfikator`, `szkola`) VALUES(\"$ileNauczycieli\", \"$userID\", \"$ileSzkol\");";
	//echo $sql;
	//echo "<br>";
	$result = $conn->query($sql);
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